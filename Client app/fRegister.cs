using AntdUI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_app
{
    public partial class fRegister : AntdUI.Window
    {
        private Client socket;
        string fileSelected = "";
        public string Message { get;private set; }
        public fRegister(Client socket)
        {
            InitializeComponent();
            this.socket = socket;
            socket.MessageReceived += OnMessageReceived;
        }
        void OnSignupResult()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    OnSignupResult();
                }));
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
        private void OnMessageReceived(string msg)
        {
            JObject authResp = JObject.Parse(msg);
            if ((string)authResp["status"] == "signed")
            {
                Message = (string)authResp["message"];
                OnSignupResult();
            }
            else if ((string)authResp["status"]== "error")
            {
                AntdUI.Modal.open(new Modal.Config(this, $"Thông báo", (string)authResp["message"], AntdUI.TType.Info)
                {
                    CancelText = "Cancel",
                    OkText = "OK",
                    OnOk = config =>
                    {
                        return true;
                    },
                });
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            AuthMessage authMsg = new AuthMessage
            {
                type = "signup",
                email = txtEmail.Text,
                password = txtPassword.Text,
                name = txtName.Text,
                avatarencoded = !string.IsNullOrEmpty(fileSelected) ? ImageToBase64(fileSelected) : ""
            };
            socket.SendMessage(JsonConvert.SerializeObject(authMsg, Formatting.None));
        }

        private void lbAvatar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            dialog.Title = "Chọn avatar";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileSelected = Path.GetFullPath(dialog.FileName);
                lbAvatar.Text = Path.GetFileName(dialog.FileName);

            }
        }
        private string ImageToBase64(string imagePath)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(imagePath);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }

        private void fRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            socket.MessageReceived -= OnMessageReceived;
        }
    }
}
