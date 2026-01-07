namespace Client_app
{
    partial class fNotiCall
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
            this.btnAccept = new AntdUI.Button();
            this.lbTarget = new AntdUI.Label();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.pbAvatarTarget = new System.Windows.Forms.PictureBox();
            this.btnReject = new AntdUI.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatarTarget)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.BackExtend = "";
            this.btnAccept.ColorScheme = AntdUI.TAMode.Light;
            this.btnAccept.IconRatio = 1.5F;
            this.btnAccept.IconSvg = "PhoneOutlined";
            this.btnAccept.Location = new System.Drawing.Point(476, 53);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Shape = AntdUI.TShape.Round;
            this.btnAccept.Size = new System.Drawing.Size(65, 65);
            this.btnAccept.TabIndex = 9;
            this.btnAccept.Type = AntdUI.TTypeMini.Success;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lbTarget
            // 
            this.lbTarget.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbTarget.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTarget.IconRatio = 1.5F;
            this.lbTarget.Location = new System.Drawing.Point(114, 53);
            this.lbTarget.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Prefix = "User";
            this.lbTarget.PrefixColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(95)))), ((int)(((byte)(167)))));
            this.lbTarget.PrefixSvg = "User";
            this.lbTarget.Size = new System.Drawing.Size(286, 39);
            this.lbTarget.Suffix = "";
            this.lbTarget.SuffixSvg = "";
            this.lbTarget.TabIndex = 8;
            this.lbTarget.Text = "";
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(562, 23);
            this.pageHeader1.TabIndex = 7;
            this.pageHeader1.Text = "Call voice";
            // 
            // pbAvatarTarget
            // 
            this.pbAvatarTarget.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pbAvatarTarget.Location = new System.Drawing.Point(22, 53);
            this.pbAvatarTarget.Name = "pbAvatarTarget";
            this.pbAvatarTarget.Size = new System.Drawing.Size(73, 77);
            this.pbAvatarTarget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAvatarTarget.TabIndex = 6;
            this.pbAvatarTarget.TabStop = false;
            // 
            // btnReject
            // 
            this.btnReject.BackExtend = "";
            this.btnReject.ColorScheme = AntdUI.TAMode.Light;
            this.btnReject.IconRatio = 1.5F;
            this.btnReject.IconSvg = "CloseOutlined";
            this.btnReject.Location = new System.Drawing.Point(410, 53);
            this.btnReject.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnReject.Name = "btnReject";
            this.btnReject.Shape = AntdUI.TShape.Round;
            this.btnReject.Size = new System.Drawing.Size(65, 65);
            this.btnReject.TabIndex = 10;
            this.btnReject.Type = AntdUI.TTypeMini.Error;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // fNotiCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(562, 150);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lbTarget);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.pbAvatarTarget);
            this.Name = "fNotiCall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "fNotiCall";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fNotiCall_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fNotiCall_FormClosed);
            this.Load += new System.EventHandler(this.fNotiCall_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatarTarget)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private AntdUI.Button btnAccept;
        private AntdUI.Label lbTarget;
        private AntdUI.PageHeader pageHeader1;
        private System.Windows.Forms.PictureBox pbAvatarTarget;
        private AntdUI.Button btnReject;
    }
}