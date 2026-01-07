namespace Server_app
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
            this.pageHeader1 = new AntdUI.PageHeader();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbIP = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbConnected = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnTable = new AntdUI.Panel();
            this.panel2 = new AntdUI.Panel();
            this.table = new AntdUI.Table();
            this.panel1 = new AntdUI.Panel();
            this.lbTotalUser = new AntdUI.Label();
            this.btnDeleteSelected = new AntdUI.Button();
            this.btnAddUser = new AntdUI.Button();
            this.statusStrip1.SuspendLayout();
            this.pnTable.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(845, 23);
            this.pageHeader1.TabIndex = 0;
            this.pageHeader1.Text = "Server app";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lbIP,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.lbPort,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5,
            this.lbConnected});
            this.statusStrip1.Location = new System.Drawing.Point(0, 433);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(845, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(20, 17);
            this.toolStripStatusLabel1.Text = "IP:";
            // 
            // lbIP
            // 
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(22, 17);
            this.toolStripStatusLabel2.Text = "     ";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel3.Text = "Port:";
            // 
            // lbPort
            // 
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(22, 17);
            this.toolStripStatusLabel4.Text = "     ";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(63, 17);
            this.toolStripStatusLabel5.Text = "Đã kết nối:";
            // 
            // lbConnected
            // 
            this.lbConnected.Name = "lbConnected";
            this.lbConnected.Size = new System.Drawing.Size(0, 17);
            // 
            // pnTable
            // 
            this.pnTable.Controls.Add(this.panel2);
            this.pnTable.Controls.Add(this.panel1);
            this.pnTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnTable.Location = new System.Drawing.Point(0, 23);
            this.pnTable.Name = "pnTable";
            this.pnTable.Size = new System.Drawing.Size(845, 410);
            this.pnTable.TabIndex = 2;
            this.pnTable.Text = "panel1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.table);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(845, 365);
            this.panel2.TabIndex = 2;
            this.panel2.Text = "panel2";
            // 
            // table
            // 
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Font = new System.Drawing.Font("Segoe UI Semilight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.table.Gap = 12;
            this.table.Location = new System.Drawing.Point(0, 0);
            this.table.Name = "table";
            this.table.Size = new System.Drawing.Size(845, 365);
            this.table.TabIndex = 0;
            this.table.Text = "table1";
            this.table.CellButtonClick += new AntdUI.Table.ClickButtonEventHandler(this.table_CellButtonClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbTotalUser);
            this.panel1.Controls.Add(this.btnDeleteSelected);
            this.panel1.Controls.Add(this.btnAddUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(845, 45);
            this.panel1.TabIndex = 1;
            this.panel1.Text = "panel1";
            // 
            // lbTotalUser
            // 
            this.lbTotalUser.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbTotalUser.Location = new System.Drawing.Point(29, 6);
            this.lbTotalUser.Name = "lbTotalUser";
            this.lbTotalUser.Size = new System.Drawing.Size(469, 33);
            this.lbTotalUser.TabIndex = 6;
            this.lbTotalUser.Text = "";
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.BackExtend = "";
            this.btnDeleteSelected.ColorScheme = AntdUI.TAMode.Light;
            this.btnDeleteSelected.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeleteSelected.IconRatio = 1.5F;
            this.btnDeleteSelected.IconSvg = "DeleteOutlined";
            this.btnDeleteSelected.Location = new System.Drawing.Point(533, 0);
            this.btnDeleteSelected.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Shape = AntdUI.TShape.Round;
            this.btnDeleteSelected.Size = new System.Drawing.Size(159, 45);
            this.btnDeleteSelected.TabIndex = 5;
            this.btnDeleteSelected.Text = "Xóa danh sách đã chọn";
            this.btnDeleteSelected.Type = AntdUI.TTypeMini.Primary;
            this.btnDeleteSelected.Click += new System.EventHandler(this.btnDeleteSelected_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.BackExtend = "";
            this.btnAddUser.ColorScheme = AntdUI.TAMode.Light;
            this.btnAddUser.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddUser.IconRatio = 1.5F;
            this.btnAddUser.IconSvg = "UserAddOutlined";
            this.btnAddUser.Location = new System.Drawing.Point(692, 0);
            this.btnAddUser.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Shape = AntdUI.TShape.Round;
            this.btnAddUser.Size = new System.Drawing.Size(153, 45);
            this.btnAddUser.TabIndex = 4;
            this.btnAddUser.Text = "Thêm người dùng";
            this.btnAddUser.Type = AntdUI.TTypeMini.Primary;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 455);
            this.Controls.Add(this.pnTable);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pageHeader1);
            this.Name = "fMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMain_FormClosing);
            this.Load += new System.EventHandler(this.fMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnTable.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private AntdUI.PageHeader pageHeader1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbIP;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lbPort;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel lbConnected;
        private AntdUI.Panel pnTable;
        private AntdUI.Table table;
        private AntdUI.Panel panel1;
        private AntdUI.Button btnAddUser;
        private AntdUI.Panel panel2;
        private AntdUI.Button btnDeleteSelected;
        private AntdUI.Label lbTotalUser;
    }
}

