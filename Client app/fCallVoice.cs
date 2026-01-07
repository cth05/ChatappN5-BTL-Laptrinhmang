using AntdUI;
using NAudio.Wave;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_app
{
    public partial class fCallVoice : AntdUI.Window
    {
        Client socket;
        User myInfo, target;
        IPEndPoint peerEP;
        UdpClient udp;
        int freePort;
        Thread udpReceiveThread;
        bool isCalling = false;
        // audio
        WaveInEvent waveIn;
        WaveOutEvent waveOut;
        BufferedWaveProvider buffer;
        AesSession aes;
        public bool IsSuccessed { get; private set; } = false;
        private void fCallVoice_Load(object sender, EventArgs e)
        {
            pageHeader1.Text = "Đang gửi yêu cầu";
            freePort = GetFreePort();
            udp = new UdpClient(freePort);
            socket.MessageReceived += OnMessageReceived;
            lbTarget.Text = "Đang gọi tới " + target.name;
            pbAvatarTarget.Image = fMain.Base64ToImage(target.avatarencoded);
            ChatMessage msg = new ChatMessage
            {
                type = "call-voice-require",
                from = myInfo,
                to = target,
                message = fMain.EncryptMessage(freePort.ToString(), aes)
            };
            socket.SendMessage(JsonConvert.SerializeObject(msg, formatting: Formatting.None));
        }
        public static int GetFreePort()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        public fCallVoice(Client socket, User myInfo, User target, AesSession aes)
        {
            InitializeComponent();
            this.socket = socket;
            this.myInfo = myInfo;
            this.target = target;
            this.aes = aes;
        }
        void EndCall(bool me)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => EndCall(me)));
                return;
            }
            isCalling = false;
            waveIn?.StopRecording();
            waveOut?.Stop();

            udp?.Close();
            udpReceiveThread?.Abort();

            if (me)
            {
                ChatMessage msg = new ChatMessage
                {
                    type = "call-voice-end",
                    from = myInfo,
                    to = target
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
            this.Close();
        }
        void StartVoice()
        {
            // ===== PLAYBACK =====
            buffer = new BufferedWaveProvider(
                new WaveFormat(8000, 16, 1)
            );

            waveOut = new WaveOutEvent();
            waveOut.Init(buffer);
            waveOut.Play();

            // ===== CAPTURE =====
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
        void ReceiveVoice()
        {
            IPEndPoint ep = null;

            while (isCalling)
            {
                try
                {
                    var data = udp.Receive(ref ep);
                    byte[] decryptedAudio = AesCrypto.Decrypt(
    data,
    aes.Key,
    aes.IV
);
                    buffer.AddSamples(decryptedAudio, 0, decryptedAudio.Length);
                }
                catch { break; }
            }
        }
        private void btnEndCall_Click(object sender, EventArgs e)
        {
            EndCall(true);
        }

        private void fCallVoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            socket.MessageReceived -= OnMessageReceived;
        }

        private void OnMessageReceived(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnMessageReceived(msg)));
                return;
            }
            JObject res = JObject.Parse(msg);
            if ((string)res["type"] == "call-voice-accept" && (int)res["from"]["id"] == target.id)
            {
                string peerIp = target.ipaddress;
                int peerPort = int.Parse((string)res["message"]);
                peerEP = new IPEndPoint(IPAddress.Parse(peerIp), peerPort);
                pageHeader1.Text = "Cuộc gọi đã được chấp nhận";
                isCalling = true;
                pageHeader1.Text = "Đang gọi...";
                IsSuccessed = true;
                StartVoice();

            }
            else if ((string)res["type"] == "call-voice-reject" && (int)res["from"]["id"] == target.id)
            {
                pageHeader1.Text = "Cuộc gọi đã bị từ chối";
                AntdUI.Modal.open(new Modal.Config(this, "Thông báo", "Cuộc gọi đã bị từ chối", AntdUI.TType.Info)
                {
                    CancelText = "Cancel",
                    OkText = "OK",
                    OnOk = config =>
                    {
                        return true;
                    },
                });
                this.Close();
            }
            else if ((string)res["type"] == "call-voice-end" && (int)res["from"]["id"] == target.id)
            {
                pageHeader1.Text = "Cuộc gọi đã kết thúc";
                EndCall(false);
            }
        }
    }
}
