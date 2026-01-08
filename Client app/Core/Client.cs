using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client_app
{
    public class Client
    {
        private TcpClient controlClient;
        private NetworkStream controlNs;
        private Thread listenThread;
        private bool stopRequested = false;
        private readonly object lockObj = new object();

        public event Action<string> MessageReceived;
        public event Action<string> StatusChanged;
        public void Connect(string serverIp, int serverPort, string name = null)
        {
            controlClient = new TcpClient();
            controlClient.Connect(serverIp, serverPort);
            controlNs = controlClient.GetStream();
            stopRequested = false;

            listenThread = new Thread(ReceiveLoop) { IsBackground = true };
            listenThread.Start();

            StatusChanged?.Invoke($"Connected to {serverIp}:{serverPort}");
        }
        private void ReceiveLoop()
        {
            try
            {
                NetworkStream ns = controlClient.GetStream();

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
                    Console.WriteLine("Received message: " + msg);
                    Task.Run(() => MessageReceived?.Invoke(msg));
                }
            }
            catch
            {
                StatusChanged?.Invoke("Server Disconnected");
            }
            finally
            {
                try { controlClient.Close(); } catch { }
            }
        }
        public void SendMessage(string message)
        {
            lock (lockObj)
            {
                NetworkStream ns = controlClient.GetStream();

                byte[] data = Encoding.UTF8.GetBytes(message);
                byte[] len = BitConverter.GetBytes(data.Length);

                ns.Write(len, 0, 4);
                ns.Write(data, 0, data.Length);
            }
        }
        public void Stop()
        {
            stopRequested = true;
            try { controlClient.Close(); } catch { }
        }
    }
}
