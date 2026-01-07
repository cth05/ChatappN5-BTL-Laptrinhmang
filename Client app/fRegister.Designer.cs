namespace Client_app
{
    partial class fRegister
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRegister = new AntdUI.Button();
            this.txtPassword = new AntdUI.Input();
            this.txtEmail = new AntdUI.Input();
            this.label2 = new AntdUI.Label();
            this.label1 = new AntdUI.Label();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.txtName = new AntdUI.Input();
            this.label3 = new AntdUI.Label();
            this.label4 = new AntdUI.Label();
            this.lbAvatar = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(95)))), ((int)(((byte)(193)))));
            this.btnRegister.Location = new System.Drawing.Point(211, 228);
            this.btnRegister.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(94, 32);
            this.btnRegister.TabIndex = 28;
            this.btnRegister.Text = "Đăng ký";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(143, 133);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(149, 40);
            this.txtPassword.TabIndex = 26;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(143, 88);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(319, 39);
            this.txtEmail.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(77, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 23);
            this.label2.TabIndex = 24;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(99, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 23);
            this.label1.TabIndex = 23;
            this.label1.Text = "Email:";
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(513, 23);
            this.pageHeader1.TabIndex = 22;
            this.pageHeader1.Text = "Tạo tài khoản";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(143, 44);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(319, 39);
            this.txtName.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(49, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 23);
            this.label3.TabIndex = 29;
            this.label3.Text = "Tên người dùng:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(96, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 23);
            this.label4.TabIndex = 31;
            this.label4.Text = "Avatar:";
            // 
            // lbAvatar
            // 
            this.lbAvatar.AutoSize = true;
            this.lbAvatar.Location = new System.Drawing.Point(158, 186);
            this.lbAvatar.Name = "lbAvatar";
            this.lbAvatar.Size = new System.Drawing.Size(23, 13);
            this.lbAvatar.TabIndex = 32;
            this.lbAvatar.TabStop = true;
            this.lbAvatar.Text = "File";
            this.lbAvatar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbAvatar_LinkClicked);
            // 
            // fRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 274);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.lbAvatar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pageHeader1);
            this.Name = "fRegister";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fRegister_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AntdUI.Button btnRegister;
        private AntdUI.Input txtPassword;
        private AntdUI.Input txtEmail;
        private AntdUI.Label label2;
        private AntdUI.Label label1;
        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Input txtName;
        private AntdUI.Label label3;
        private AntdUI.Label label4;
        private System.Windows.Forms.LinkLabel lbAvatar;
    }
}