using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server_app
{
    public class Server
    {
        public string ipAddress { get; set; }
        public int port { get; set; }
        private TcpListener listener;
        private Thread listenThread;
        private bool stopRequested = false;

        public readonly Dictionary<string, TcpClient> clientConnected = new Dictionary<string, TcpClient>();
        private readonly object lockObj = new object();

        public event Action<string, string> MessageReceived;  // (peerEndpoint, msg)
        public event Action<string> StatusChanged;
        public event Action<string, string> PeerChanged;
        public void Start(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            listenThread = new Thread(ListenLoop) { IsBackground = true };
            listenThread.Start();

            StatusChanged?.Invoke($"Đang lắng nghe trên port {port}");
        }
        private void ListenLoop()
        {
            while (!stopRequested)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    string endpoint = client.Client.RemoteEndPoint.ToString();
                    string newID = Guid.NewGuid().ToString();
                    lock (lockObj)
                    {
                        clientConnected[newID] = client;
                    }

                    StatusChanged?.Invoke($"🔗 Peer mới: {endpoint}");
                    PeerChanged?.Invoke("add", newID);
                    new Thread(() => ReceiveLoop(newID, client)) { IsBackground = true }.Start();
                }
                catch (Exception ex)
                {
                    if (!stopRequested) StatusChanged?.Invoke($"Lỗi: {ex.Message}");
                }
            }
        }
        private void ReceiveLoop(string id, TcpClient client)
        {
            try
            {
                NetworkStream ns = client.GetStream();

                while (!stopRequested)
                {
                    byte[] lenBuf = new byte[4];
                    int read = ns.Read(lenBuf, 0, 4);
                    if (read == 0) break;

                    int length = BitConverter.ToInt32(lenBuf, 0);
                    byte[] data = new byte[length];

                    int total = 0;
                    while (total < length)
                    {
                        int r = ns.Read(data, total, length - total);
                        if (r == 0) throw new IOException("Mất kết nối");
                        total += r;
                    }

                    string msg = Encoding.UTF8.GetString(data);
                    MessageReceived?.Invoke(id, msg);
                }
            }
            catch
            {
                StatusChanged?.Invoke($"❌ Mất kết nối với {id}");
                PeerChanged?.Invoke("delete", id);
                lock (lockObj)
                {
                    clientConnected.Remove(id);
                }
            }
            finally
            {
                try { client.Close(); } catch { }
            }
        }
        public void SendMessage(string id, string message)
        {
            lock (lockObj)
            {
                TcpClient client = null;

                // Tìm theo key endpoint
                if (clientConnected.ContainsKey(id))
                {
                    client = clientConnected[id];
                }
                else
                {
                    return;
                }

                if (client == null)
                    throw new Exception($"Không tìm thấy peer để gửi: {id}");

                NetworkStream ns = client.GetStream();

                byte[] data = Encoding.UTF8.GetBytes(message);
                byte[] len = BitConverter.GetBytes(data.Length);

                ns.Write(len, 0, 4);
                ns.Write(data, 0, data.Length);
            }
        }
        public void Stop()
        {
            stopRequested = true;

            try { listener.Stop(); } catch { }

            lock (lockObj)
            {
                foreach (var kv in clientConnected)
                {
                    try { kv.Value.Close(); } catch { }
                }
                clientConnected.Clear();
            }
        }

    }
}
