namespace Client_app
{
    partial class fMain
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
            this.lbTarget = new AntdUI.Label();
            this.txtInput = new AntdUI.Input();
            this.btnUpload = new AntdUI.Button();
            this.btnSend = new AntdUI.Button();
            this.pnlToolChat = new AntdUI.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSendImage = new AntdUI.Button();
            this.pnlChatInfo = new AntdUI.Panel();
            this.btnCall = new AntdUI.Button();
            this.pbAvatarTarget = new System.Windows.Forms.PictureBox();
            this.pnlUser = new AntdUI.Panel();
            this.pnInfoUser = new System.Windows.Forms.Panel();
            this.btnSignup = new AntdUI.Button();
            this.btnLogin = new AntdUI.Button();
            this.msgList = new AntdUI.Chat.MsgList();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.pnlChat = new AntdUI.Panel();
            this.pnlToolChat.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlChatInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatarTarget)).BeginInit();
            this.pnlUser.SuspendLayout();
            this.pnInfoUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTarget
            // 
            this.lbTarget.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbTarget.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTarget.IconRatio = 1.5F;
            this.lbTarget.Location = new System.Drawing.Point(65, 12);
            this.lbTarget.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Prefix = "User";
            this.lbTarget.PrefixColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(95)))), ((int)(((byte)(167)))));
            this.lbTarget.PrefixSvg = "User";
            this.lbTarget.Size = new System.Drawing.Size(455, 39);
            this.lbTarget.Suffix = "";
            this.lbTarget.SuffixSvg = "";
            this.lbTarget.TabIndex = 0;
            this.lbTarget.Text = "";
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInput.Location = new System.Drawing.Point(0, 0);
            this.txtInput.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.PlaceholderText = "Nhập tin nhắn";
            this.txtInput.Size = new System.Drawing.Size(427, 51);
            this.txtInput.TabIndex = 6;
            // 
            // btnUpload
            // 
            this.btnUpload.BackExtend = "";
            this.btnUpload.ColorScheme = AntdUI.TAMode.Light;
            this.btnUpload.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUpload.IconRatio = 1.5F;
            this.btnUpload.IconSvg = "UploadOutlined";
            this.btnUpload.Location = new System.Drawing.Point(478, 0);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(51, 51);
            this.btnUpload.TabIndex = 4;
            this.btnUpload.Type = AntdUI.TTypeMini.Primary;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackExtend = "";
            this.btnSend.ColorScheme = AntdUI.TAMode.Light;
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.IconRatio = 1.5F;
            this.btnSend.IconSvg = "SendOutlined";
            this.btnSend.Location = new System.Drawing.Point(529, 0);
            this.btnSend.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnSend.Name = "btnSend";
            this.btnSend.Shape = AntdUI.TShape.Round;
            this.btnSend.Size = new System.Drawing.Size(51, 51);
            this.btnSend.TabIndex = 3;
            this.btnSend.Type = AntdUI.TTypeMini.Primary;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // pnlToolChat
            // 
            this.pnlToolChat.Controls.Add(this.panel1);
            this.pnlToolChat.Controls.Add(this.btnSendImage);
            this.pnlToolChat.Controls.Add(this.btnUpload);
            this.pnlToolChat.Controls.Add(this.btnSend);
            this.pnlToolChat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlToolChat.Enabled = false;
            this.pnlToolChat.Location = new System.Drawing.Point(227, 410);
            this.pnlToolChat.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pnlToolChat.Name = "pnlToolChat";
            this.pnlToolChat.Size = new System.Drawing.Size(580, 51);
            this.pnlToolChat.TabIndex = 14;
            this.pnlToolChat.Text = "panel2";
            this.pnlToolChat.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtInput);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(427, 51);
            this.panel1.TabIndex = 8;
            // 
            // btnSendImage
            // 
            this.btnSendImage.BackExtend = "";
            this.btnSendImage.ColorScheme = AntdUI.TAMode.Light;
            this.btnSendImage.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSendImage.IconRatio = 1.5F;
            this.btnSendImage.IconSvg = "FileImageOutlined";
            this.btnSendImage.Location = new System.Drawing.Point(427, 0);
            this.btnSendImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSendImage.Name = "btnSendImage";
            this.btnSendImage.Size = new System.Drawing.Size(51, 51);
            this.btnSendImage.TabIndex = 7;
            this.btnSendImage.Type = AntdUI.TTypeMini.Primary;
            this.btnSendImage.Click += new System.EventHandler(this.btnSendImage_Click);
            // 
            // pnlChatInfo
            // 
            this.pnlChatInfo.Controls.Add(this.btnCall);
            this.pnlChatInfo.Controls.Add(this.lbTarget);
            this.pnlChatInfo.Controls.Add(this.pbAvatarTarget);
            this.pnlChatInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChatInfo.Location = new System.Drawing.Point(227, 43);
            this.pnlChatInfo.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pnlChatInfo.Name = "pnlChatInfo";
            this.pnlChatInfo.Size = new System.Drawing.Size(580, 57);
            this.pnlChatInfo.TabIndex = 15;
            this.pnlChatInfo.Text = "panel2";
            this.pnlChatInfo.Visible = false;
            // 
            // btnCall
            // 
            this.btnCall.BackExtend = "";
            this.btnCall.ColorScheme = AntdUI.TAMode.Light;
            this.btnCall.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCall.IconRatio = 1.5F;
            this.btnCall.IconSvg = "PhoneOutlined";
            this.btnCall.Location = new System.Drawing.Point(529, 0);
            this.btnCall.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(51, 57);
            this.btnCall.TabIndex = 8;
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // pbAvatarTarget
            // 
            this.pbAvatarTarget.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pbAvatarTarget.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbAvatarTarget.Location = new System.Drawing.Point(0, 0);
            this.pbAvatarTarget.Name = "pbAvatarTarget";
            this.pbAvatarTarget.Size = new System.Drawing.Size(57, 57);
            this.pbAvatarTarget.TabIndex = 0;
            this.pbAvatarTarget.TabStop = false;
            // 
            // pnlUser
            // 
            this.pnlUser.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.pnlUser.Controls.Add(this.pnInfoUser);
            this.pnlUser.Controls.Add(this.msgList);
            this.pnlUser.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlUser.Location = new System.Drawing.Point(0, 43);
            this.pnlUser.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(227, 418);
            this.pnlUser.TabIndex = 13;
            this.pnlUser.Text = "panel1";
            // 
            // pnInfoUser
            // 
            this.pnInfoUser.Controls.Add(this.btnSignup);
            this.pnInfoUser.Controls.Add(this.btnLogin);
            this.pnInfoUser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnInfoUser.Location = new System.Drawing.Point(0, 362);
            this.pnInfoUser.Name = "pnInfoUser";
            this.pnInfoUser.Size = new System.Drawing.Size(227, 56);
            this.pnInfoUser.TabIndex = 1;
            // 
            // btnSignup
            // 
            this.btnSignup.BackExtend = "";
            this.btnSignup.ColorScheme = AntdUI.TAMode.Light;
            this.btnSignup.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSignup.IconRatio = 1.5F;
            this.btnSignup.IconSvg = "UserAddOutlined";
            this.btnSignup.Location = new System.Drawing.Point(0, 0);
            this.btnSignup.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnSignup.Name = "btnSignup";
            this.btnSignup.Shape = AntdUI.TShape.Round;
            this.btnSignup.Size = new System.Drawing.Size(114, 56);
            this.btnSignup.TabIndex = 5;
            this.btnSignup.Text = "Đăng ký";
            this.btnSignup.Click += new System.EventHandler(this.btnSignup_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackExtend = "";
            this.btnLogin.ColorScheme = AntdUI.TAMode.Light;
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLogin.IconRatio = 1.5F;
            this.btnLogin.IconSvg = "LoginOutlined";
            this.btnLogin.Location = new System.Drawing.Point(114, 0);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Shape = AntdUI.TShape.Round;
            this.btnLogin.Size = new System.Drawing.Size(113, 56);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Type = AntdUI.TTypeMini.Success;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // msgList
            // 
            this.msgList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msgList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msgList.Location = new System.Drawing.Point(0, 0);
            this.msgList.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.msgList.Name = "msgList";
            this.msgList.Size = new System.Drawing.Size(227, 418);
            this.msgList.TabIndex = 0;
            this.msgList.Text = "msgList1";
            this.msgList.ItemClick += new AntdUI.ItemClickEventHandler(this.msgList_ItemClick);
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(807, 43);
            this.pageHeader1.TabIndex = 12;
            this.pageHeader1.Text = "Chat app - Rule: Client";
            // 
            // pnlChat
            // 
            this.pnlChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlChat.Location = new System.Drawing.Point(227, 100);
            this.pnlChat.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pnlChat.Name = "pnlChat";
            this.pnlChat.Size = new System.Drawing.Size(580, 310);
            this.pnlChat.TabIndex = 16;
            this.pnlChat.Text = "panel2";
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 461);
            this.Controls.Add(this.pnlChat);
            this.Controls.Add(this.pnlToolChat);
            this.Controls.Add(this.pnlChatInfo);
            this.Controls.Add(this.pnlUser);
            this.Controls.Add(this.pageHeader1);
            this.Name = "fMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMain_FormClosing);
            this.Load += new System.EventHandler(this.fMain_Load);
            this.pnlToolChat.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlChatInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatarTarget)).EndInit();
            this.pnlUser.ResumeLayout(false);
            this.pnInfoUser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private AntdUI.Label lbTarget;
        private AntdUI.Input txtInput;
        private AntdUI.Button btnUpload;
        private AntdUI.Button btnSend;
        private AntdUI.Panel pnlToolChat;
        private AntdUI.Panel pnlChatInfo;
        private AntdUI.Panel pnlUser;
        private AntdUI.Chat.MsgList msgList;
        private AntdUI.PageHeader pageHeader1;
        private System.Windows.Forms.Panel pnInfoUser;
        private AntdUI.Button btnLogin;
        private AntdUI.Button btnSignup;
        private System.Windows.Forms.PictureBox pbAvatarTarget;
        private AntdUI.Panel pnlChat;
        private AntdUI.Button btnSendImage;
        private System.Windows.Forms.Panel panel1;
        private AntdUI.Button btnCall;
    }
}

