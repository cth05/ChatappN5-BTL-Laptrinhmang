using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_app
{
    public partial class fOpenPort : AntdUI.Window
    {
        public fOpenPort()
        {
            InitializeComponent();
        }
        public int port { get; private set; }
        public string ip { get; private set; }

        private void fOpenPort_Load(object sender, EventArgs e)
        {
            lbIP.Text = GetLocalIPv4();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPort.Text, out int port))
            {
                MessageBox.Show("Port không đúng định dạng"); return;
            }
            if (port < 0 || port > 65535)
            {
                MessageBox.Show("Port không khả dụng"); return;
            }
            if (!IsPortAvailable(port))
            {
                MessageBox.Show("Port đã được sử dụng"); return;
            }
            if (!IPAddress.TryParse(lbIP.Text, out IPAddress ipAddress))
            {
                MessageBox.Show("IP không đúng định dạng"); return;
            }
            Server s = new Server();
            string ip = lbIP.Text;
            try
            {
                s.Start(port);
                s.ipAddress = ip;
                s.port = port;
                fMain f = new fMain(s);
                f.Show();
                this.Hide();
            }
            catch (Exception ex) {
                AntdUI.Modal.open(new AntdUI.Modal.Config(this, $"Lỗi: {ex.Message}", AntdUI.TType.Error));
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int defaultPort = 9000;
            int freePort = IsPortAvailable(defaultPort) ? defaultPort : GetFreePort();
            if (freePort > 0)
                txtPort.Text = freePort.ToString();
        }
        public static bool IsPortAvailable(int port)
        {
            var ipProps = IPGlobalProperties.GetIPGlobalProperties();

            bool inUse = ipProps.GetActiveTcpListeners().Any(p => p.Port == port) ||
                         ipProps.GetActiveUdpListeners().Any(p => p.Port == port);

            return !inUse;
        }
        public static int GetFreePort()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
        public static string GetLocalIPv4()
        {
            string localIP = "";
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                try
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    localIP = endPoint.Address.ToString();
                }
                catch
                {
                    // Nếu không có mạng, lấy IP đầu tiên tìm thấy
                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (var ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            return ip.ToString();
                        }
                    }
                }
            }
            return localIP;
        }
    }
}
