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
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_app
{
    public partial class fRegister : AntdUI.Window
    {
        string fileSelected = "";
        public User created { get; private set; }
        public fRegister()
        {
            InitializeComponent();
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
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string avt = !string.IsNullOrEmpty(fileSelected) ? ImageToBase64(fileSelected) : "";
            string email = txtEmail.Text.ToLower();
            string password = txtPassword.Text;
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
            else if (string.IsNullOrEmpty(name))
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
            AuthMessage check = fMain.GetUserPublicByEmail(email);
            if (check != null)
            {
                AntdUI.Modal.open(new Modal.Config(this, $"Thông báo", "Email đã tồn tại", AntdUI.TType.Error)
                {
                    CancelText = "Cancel",
                    OkText = "OK",
                    OnOk = config =>
                    {
                        return true;
                    },
                });
            }
            else
            {
                password = Sha256Hash(password);
                if (fMain.InsertUser(name, email, password, avt))
                {
                    AntdUI.Modal.open(new Modal.Config(this, $"Thông báo", "Tạo tài khoản thành công", AntdUI.TType.Info)
                    {
                        CancelText = "Cancel",
                        OkText = "Exit",
                        OnOk = config =>
                        {
                            check = fMain.GetUserPublicByEmail(email);
                            created = new User
                            {
                                id = check.id,
                                email = check.email,
                                name = check.name,
                                avatarencoded = check.avatarencoded,
                                password = Sha256Hash(password)
                            };
                            this.DialogResult = DialogResult.OK;
                            return true;
                        },
                    });
                    this.Close();
                }
                else
                {
                    AntdUI.Modal.open(new Modal.Config(this, $"Thông báo", "Tạo tài khoản thất bại", AntdUI.TType.Error)
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
    }
}
