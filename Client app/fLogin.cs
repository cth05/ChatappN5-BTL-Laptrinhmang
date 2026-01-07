using AntdUI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_app
{
    public partial class fLogin : AntdUI.Window
    {
        private Client socket;
        public bool IsLoggedIn { get; private set; }
        public string CurrentEmail { get; private set; }
        public int Id { get; private set; }
        public string AvatarEncoded { get; private set; }
        public string Message { get;private set; }
        private string pubKeyRsa;
        public fLogin(Client socket, string pubKeyRSA)
        {
            InitializeComponent();
            this.socket = socket;
            this.pubKeyRsa= pubKeyRSA;
            socket.MessageReceived += OnMessageReceived;
        }
        void OnLoginResult()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    OnLoginResult();
                }));
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void OnMessageReceived(string msg)
        {
            JObject authResp = JObject.Parse(msg);
            if ((string)authResp["status"] == "logged")
            {
                IsLoggedIn = true;
                CurrentEmail = (string)authResp["data"]["email"];
                Name = (string)authResp["data"]["name"];
                Id = (int)authResp["data"]["id"];
                AvatarEncoded = (string)authResp["data"]["avatarencoded"];
                Message = (string)authResp["message"];
                OnLoginResult();
            }
            else if ((string)authResp["status"]== "error")
            {
                AntdUI.Modal.open(new Modal.Config(this, $"Thông báo", (string)authResp["message"], AntdUI.TType.Info)
                {
                    CancelText  = "Cancel",
                    OkText = "OK",
                    OnOk = config =>
                    {
                        return true;
                    },
                });
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.ToLower();
            string password = txtPassword.Text;
            if (!IsValidEmail(email))
            {
                AntdUI.Modal.open(new Modal.Config(this, $"Thông báo","Email nhập vào không hợp lệ", AntdUI.TType.Error)
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
            AuthMessage authMsg = new AuthMessage
            {
                type = "login",
                email = email,
                password = Sha256Hash(password),
                pubkeyrsa= pubKeyRsa
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
        private void fLogin_Load(object sender, EventArgs e)
        {

        }

        private void fLogin_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            socket.MessageReceived -= OnMessageReceived;
        }
    }
}
