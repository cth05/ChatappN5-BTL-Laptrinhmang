namespace Client_app
{
    partial class fLogin
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
            this.pageHeader1 = new AntdUI.PageHeader();
            this.label1 = new AntdUI.Label();
            this.label2 = new AntdUI.Label();
            this.txtEmail = new AntdUI.Input();
            this.txtPassword = new AntdUI.Input();
            this.btnLogin = new AntdUI.Button();
            this.SuspendLayout();
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(542, 23);
            this.pageHeader1.TabIndex = 0;
            this.pageHeader1.Text = "Login";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(62, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Email:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(40, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(96, 41);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(304, 39);
            this.txtEmail.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(96, 86);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(149, 40);
            this.txtPassword.TabIndex = 4;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(95)))), ((int)(((byte)(193)))));
            this.btnLogin.Location = new System.Drawing.Point(435, 58);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(94, 46);
            this.btnLogin.TabIndex = 20;
            this.btnLogin.Text = "Login";
            this.btnLogin.Type = AntdUI.TTypeMini.Primary;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // fLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 141);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pageHeader1);
            this.Name = "fLogin";
            this.Text = "fLogin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fLogin_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fLogin_FormClosed);
            this.Load += new System.EventHandler(this.fLogin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Label label1;
        private AntdUI.Label label2;
        private AntdUI.Input txtEmail;
        private AntdUI.Input txtPassword;
        private AntdUI.Button btnLogin;
    }
}