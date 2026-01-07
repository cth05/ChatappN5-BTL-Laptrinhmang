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
using System.Net.Sockets;
using System.Text;
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
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.ToLower();
            string name = txtName.Text;
            string password = txtPassword.Text;
            string avt = !string.IsNullOrEmpty(fileSelected) ? ImageToBase64(fileSelected) : "";
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
                                password = password
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
    }
}
