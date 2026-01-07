namespace Server_app
{
    partial class fOpenPort
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
            this.button1 = new AntdUI.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label3 = new AntdUI.Label();
            this.lbIP = new AntdUI.Label();
            this.label1 = new AntdUI.Label();
            this.btnSubmit = new AntdUI.Button();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(218, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Chọn port trống";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(122, 103);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(75, 20);
            this.txtPort.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(53, 99);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 37);
            this.label3.TabIndex = 9;
            this.label3.Text = "Port:";
            // 
            // lbIP
            // 
            this.lbIP.Location = new System.Drawing.Point(122, 58);
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(226, 23);
            this.lbIP.TabIndex = 8;
            this.lbIP.Text = "";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(67, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 37);
            this.label1.TabIndex = 7;
            this.label1.Text = "IP:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(95)))), ((int)(((byte)(193)))));
            this.btnSubmit.Location = new System.Drawing.Point(160, 154);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(98, 39);
            this.btnSubmit.TabIndex = 12;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.Type = AntdUI.TTypeMini.Primary;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // pageHeader1
            // 
            this.pageHeader1.BackColor = System.Drawing.SystemColors.Control;
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageHeader1.IconSvg = "";
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.Size = new System.Drawing.Size(418, 36);
            this.pageHeader1.TabIndex = 13;
            this.pageHeader1.Text = "Mở port server";
            // 
            // fOpenPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 205);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubmit);
            this.Name = "fOpenPort";
            this.Load += new System.EventHandler(this.fOpenPort_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AntdUI.Button button1;
        private System.Windows.Forms.TextBox txtPort;
        private AntdUI.Label label3;
        private AntdUI.Label lbIP;
        private AntdUI.Label label1;
        private AntdUI.Button btnSubmit;
        private AntdUI.PageHeader pageHeader1;
    }
}