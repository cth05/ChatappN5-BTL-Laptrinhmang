using AntdUI;
using NAudio.Wave;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_app
{
    public partial class fNotiCall : AntdUI.Window
    {
        private Client socket;
        User from, myInfo;
        IPEndPoint peerEP;
        UdpClient udp;
        Thread udpReceiveThread;
        AesSession aes;
        // audio
        bool isActive = true;
        WaveInEvent waveIn;
        WaveOutEvent waveOut;
        BufferedWaveProvider buffer;
        bool isCalling = false;
        public bool IsReply { get; private set; } = false;
        void ReceiveVoice()
        {
            IPEndPoint ep = null;

            while (true)
            {
                try
                {
                    byte[] data = udp.Receive(ref ep);
                    byte[] decryptedAudio = AesCrypto.Decrypt(
                        data,
                        aes.Key,
                        aes.IV
                    );
                    buffer.AddSamples(decryptedAudio, 0, decryptedAudio.Length);
                    if (decryptedAudio.Length < 1600)
                    {
                        Console.WriteLine("Không nhận được tín hiệu!");
                        this.Invoke(new Action(() => EndCall(false)));
                        break;
                    }
                }
                catch (SocketException ex)
                {
                    // Nếu mã lỗi là 10060 (Timed out)
                    if (ex.SocketErrorCode == SocketError.TimedOut)
                    {
                        Console.WriteLine("UDP Timeout - Đối phương mất kết nối");
                        EndCall(false); // Đóng cuộc gọi
                        break;
                    }
                }
                catch
                {
                    EndCall(false);
                    break;
                }
            }
        }
        private void btnReject_Click(object sender, EventArgs e)
        {
            ChatMessage msg = new ChatMessage
            {
                type = "call-voice-reject",
                from = myInfo,
                to = from
            };
            pageHeader1.Text = "Đã từ chối cuộc gọi";
            socket.SendMessage(Newtonsoft.Json.JsonConvert.SerializeObject(msg, formatting: Newtonsoft.Json.Formatting.None));
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if(isCalling)
            {
                EndCall(true);
                return;
            }
            IsReply = true;
            btnReject.Enabled = false;
            btnReject.Visible = false;
            btnAccept.Type = AntdUI.TTypeMini.Error;
            string peerIp = from.ipaddress;
            int peerPort = from.udpport;
            peerEP = new IPEndPoint(IPAddress.Parse(peerIp), peerPort);
            int freePort = GetFreePort();
            udp = new UdpClient(freePort);
            udp.Client.ReceiveTimeout = 3500;
            socket.MessageReceived += OnMessageReceived;
            isCalling = true;
            ChatMessage accept = new ChatMessage
            {
                type = "call-voice-accept",
                from = myInfo,
                to = from,
                message = freePort.ToString()
            };
            socket.SendMessage(Newtonsoft.Json.JsonConvert.SerializeObject(accept, formatting: Newtonsoft.Json.Formatting.None));
            pageHeader1.Text = "Đang gọi với " + from.name;
            StartVoice();
        }
        void EndCall(bool me)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => EndCall(me)));
                return;
            }
            waveIn?.StopRecording();
            waveOut?.Stop();
            udp?.Close();
            pageHeader1.Text = "Cuộc gọi đã kết thúc";
            if (me)
            {
                ChatMessage msg = new ChatMessage
                {
                    type = "call-voice-end",
                    from = myInfo,
                    to = from
                };

                socket.SendMessage(JsonConvert.SerializeObject(msg));
            }
            else
            {
                AntdUI.Modal.open(new Modal.Config(this, "Thông báo", "Cuộc gọi đã kết thúc", AntdUI.TType.Info)
                {
                    CancelText = "Cancel",
                    OkText = "OK",
                    OnOk = config =>
                    {
                        return true;
                    },
                });
            }
            isActive = false;
            this.Close();
        }
        void StartVoice()
        {
            buffer = new BufferedWaveProvider(
                new WaveFormat(8000, 16, 1)
            );

            waveOut = new WaveOutEvent();
            waveOut.Init(buffer);
            waveOut.Play();
            waveIn = new WaveInEvent
            {
                WaveFormat = new WaveFormat(8000, 16, 1)
            };

            waveIn.DataAvailable += (s, e) =>
            {
                byte[] rawAudio = new byte[e.BytesRecorded];
                Buffer.BlockCopy(e.Buffer, 0, rawAudio, 0, e.BytesRecorded);

                byte[] encryptedAudio = AesCrypto.Encrypt(
                    rawAudio,
                    aes.Key,
                    aes.IV
                );

                udp.Send(encryptedAudio, encryptedAudio.Length, peerEP);
            };

            waveIn.StartRecording();

            // ===== RECEIVE =====
            udpReceiveThread = new Thread(ReceiveVoice);
            udpReceiveThread.IsBackground = true;
            udpReceiveThread.Start();
        }
        public static int GetFreePort()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        private void fNotiCall_Load(object sender, EventArgs e)
        {
            pbAvatarTarget.Image=fMain.Base64ToImage(from.avatarencoded);
            lbTarget.Text = "Gọi từ "+ from.name;
        }
        private void OnMessageReceived(string msg)
        {
            JObject res = JObject.Parse(msg);
            if ((string)res["type"] == "call-voice-end" && (int)res["from"]["id"] == from.id)
            {
                EndCall(false);
            }
        }

        private void fNotiCall_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void fNotiCall_FormClosed(object sender, FormClosedEventArgs e)
        {
            socket.MessageReceived -= OnMessageReceived;
            udpReceiveThread?.Abort();
        }
        public fNotiCall(Client socket, User from, User myInfo, AesSession aes)
        {
            InitializeComponent();
            this.socket = socket;
            this.from = from;
            this.myInfo = myInfo;
            this.aes = aes;
            pageHeader1.Text = "Cuộc gọi đến từ " + from.name;
        }
    }
}
