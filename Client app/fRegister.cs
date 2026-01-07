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
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
            string email= txtEmail.Text.ToLower();
            string password= txtPassword.Text;
            string name = txtName.Text;
            if (!IsValidEmail(email))
            {
                AntdUI.Modal.open(new Modal.Config(this, $"Thông báo", "Email nhập vào không hợp lệ", AntdUI.TType.Error)
                {
                    CancelText = "Cancel",
                    OkText = "OK",
                    OnOk = config =>
                    {
                        return true;
                    },
                });
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                AntdUI.Modal.open(new Modal.Config(this, $"Thông báo", "Mật khẩu không được bỏ trống", AntdUI.TType.Error)
                {
                    CancelText = "Cancel",
                    OkText = "OK",
                    OnOk = config =>
                    {
                        return true;
                    },
                });
                return;
            }
            else if(string.IsNullOrEmpty(name))
            {
                AntdUI.Modal.open(new Modal.Config(this, $"Thông báo", "Tên không được bỏ trống", AntdUI.TType.Error)
                {
                    CancelText = "Cancel",
                    OkText = "OK",
                    OnOk = config =>
                    {
                        return true;
                    },
                });
                return;
            }
            AuthMessage authMsg = new AuthMessage
            {
                type = "signup",
                email = txtEmail.Text,
                password = Sha256Hash(txtPassword.Text),
                name = txtName.Text,
                avatarencoded = !string.IsNullOrEmpty(fileSelected) ? ImageToBase64(fileSelected) : ""
            };
            socket.SendMessage(JsonConvert.SerializeObject(authMsg, Formatting.None));
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }
        public static string Sha256Hash(string input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
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
