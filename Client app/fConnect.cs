using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Client_app
{
    public partial class fConnect : AntdUI.Window
    {
        public fConnect()
        {
            InitializeComponent();
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPort.Text, out int port))
            {
                MessageBox.Show("Port không đúng định dạng"); return;
            }
            if (port < 0 || port > 65535)
            {
                MessageBox.Show("Port không khả dụng"); return;
            }
            if (!IPAddress.TryParse(txtIP.Text, out IPAddress ipAddress))
            {
                MessageBox.Show("IP không đúng định dạng"); return;
            }
            Client c = new Client();
            try
            {
                c.Connect(txtIP.Text, port);
                fMain f = new fMain(c);
                this.Hide();
                f.Show();
            }
            catch(Exception ex) 
            {
                AntdUI.Modal.open(new AntdUI.Modal.Config(this,"Đã xảy ra lỗi", $"Không thể kết nối đến {txtIP.Text}:{port}\n{ex.Message}", AntdUI.TType.Error)
                {
                    CancelText = "Cancel",
                    OkText = "OK"
                });
            }
        }
    }
}
