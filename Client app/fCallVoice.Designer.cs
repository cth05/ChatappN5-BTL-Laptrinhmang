namespace Client_app
{
    partial class fCallVoice
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
            this.pbAvatarTarget = new System.Windows.Forms.PictureBox();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.lbTarget = new AntdUI.Label();
            this.btnEndCall = new AntdUI.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatarTarget)).BeginInit();
            this.SuspendLayout();
            // 
            // pbAvatarTarget
            // 
            this.pbAvatarTarget.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pbAvatarTarget.Location = new System.Drawing.Point(29, 39);
            this.pbAvatarTarget.Name = "pbAvatarTarget";
            this.pbAvatarTarget.Size = new System.Drawing.Size(73, 77);
            this.pbAvatarTarget.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAvatarTarget.TabIndex = 1;
            this.pbAvatarTarget.TabStop = false;
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(539, 23);
            this.pageHeader1.TabIndex = 2;
            this.pageHeader1.Text = "Call voice";
            // 
            // lbTarget
            // 
            this.lbTarget.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbTarget.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTarget.IconRatio = 1.5F;
            this.lbTarget.Location = new System.Drawing.Point(118, 39);
            this.lbTarget.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Prefix = "User";
            this.lbTarget.PrefixColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(95)))), ((int)(((byte)(167)))));
            this.lbTarget.PrefixSvg = "User";
            this.lbTarget.Size = new System.Drawing.Size(332, 39);
            this.lbTarget.Suffix = "";
            this.lbTarget.SuffixSvg = "";
            this.lbTarget.TabIndex = 3;
            this.lbTarget.Text = "";
            // 
            // btnEndCall
            // 
            this.btnEndCall.BackExtend = "";
            this.btnEndCall.ColorScheme = AntdUI.TAMode.Light;
            this.btnEndCall.IconRatio = 1.5F;
            this.btnEndCall.IconSvg = "PhoneOutlined";
            this.btnEndCall.Location = new System.Drawing.Point(460, 51);
            this.btnEndCall.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnEndCall.Name = "btnEndCall";
            this.btnEndCall.Shape = AntdUI.TShape.Round;
            this.btnEndCall.Size = new System.Drawing.Size(65, 65);
            this.btnEndCall.TabIndex = 4;
            this.btnEndCall.Type = AntdUI.TTypeMini.Error;
            this.btnEndCall.Click += new System.EventHandler(this.btnEndCall_Click);
            // 
            // fCallVoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(539, 139);
            this.Controls.Add(this.btnEndCall);
            this.Controls.Add(this.lbTarget);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.pbAvatarTarget);
            this.Name = "fCallVoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "fCallVoice";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fCallVoice_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.fCallVoice_FormClosed);
            this.Load += new System.EventHandler(this.fCallVoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatarTarget)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbAvatarTarget;
        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Label lbTarget;
        private AntdUI.Button btnEndCall;
    }
}