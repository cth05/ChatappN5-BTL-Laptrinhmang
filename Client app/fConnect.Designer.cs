namespace Client_app
{
    partial class fConnect
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
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label3 = new AntdUI.Label();
            this.label1 = new AntdUI.Label();
            this.btnConnect = new AntdUI.Button();
            this.SuspendLayout();
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.Size = new System.Drawing.Size(437, 23);
            this.pageHeader1.TabIndex = 0;
            this.pageHeader1.Text = "Connect Server";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(92, 45);
            this.txtIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(144, 20);
            this.txtIP.TabIndex = 18;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(92, 86);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(65, 20);
            this.txtPort.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(34, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 29);
            this.label3.TabIndex = 16;
            this.label3.Text = "Port:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(49, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 24);
            this.label1.TabIndex = 15;
            this.label1.Text = "IP:";
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(95)))), ((int)(((byte)(193)))));
            this.btnConnect.Location = new System.Drawing.Point(281, 56);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(105, 36);
            this.btnConnect.TabIndex = 19;
            this.btnConnect.Text = "Submit";
            this.btnConnect.Type = AntdUI.TTypeMini.Primary;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // fConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 144);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pageHeader1);
            this.Name = "fConnect";
            this.Text = "fConnect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private AntdUI.Label label3;
        private AntdUI.Label label1;
        private AntdUI.Button btnConnect;
    }
}