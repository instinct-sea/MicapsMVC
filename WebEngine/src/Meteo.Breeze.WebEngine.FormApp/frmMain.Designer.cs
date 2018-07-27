namespace Meteo.Breeze.WebEngine.FormApp
{
	partial class frmMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.ssrMainbar = new System.Windows.Forms.StatusStrip();
			this.tssLblVer = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnGoBack = new System.Windows.Forms.ToolStripButton();
			this.btnGoForward = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.txtUrlAddress = new System.Windows.Forms.ToolStripTextBox();
			this.btnLoadUrl = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnOpenFindbar = new System.Windows.Forms.ToolStripButton();
			this.btnOpenDevtools = new System.Windows.Forms.ToolStripButton();
			this.palBrowserWindow = new System.Windows.Forms.Panel();
			this.ssrMainbar.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ssrMainbar
			// 
			this.ssrMainbar.BackColor = System.Drawing.Color.Transparent;
			this.ssrMainbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssLblVer});
			this.ssrMainbar.Location = new System.Drawing.Point(0, 707);
			this.ssrMainbar.Name = "ssrMainbar";
			this.ssrMainbar.Padding = new System.Windows.Forms.Padding(19, 0, 1, 0);
			this.ssrMainbar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.ssrMainbar.Size = new System.Drawing.Size(1008, 22);
			this.ssrMainbar.SizingGrip = false;
			this.ssrMainbar.TabIndex = 8;
			this.ssrMainbar.Text = "状态栏";
			// 
			// tssLblVer
			// 
			this.tssLblVer.ForeColor = System.Drawing.Color.White;
			this.tssLblVer.Name = "tssLblVer";
			this.tssLblVer.Size = new System.Drawing.Size(95, 17);
			this.tssLblVer.Text = "内部专用版 v1.0";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStrip1.AutoSize = false;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGoBack,
            this.btnGoForward,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.txtUrlAddress,
            this.btnLoadUrl,
            this.toolStripSeparator2,
            this.btnOpenFindbar,
            this.btnOpenDevtools});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
			this.toolStrip1.Size = new System.Drawing.Size(1008, 35);
			this.toolStrip1.TabIndex = 10;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnGoBack
			// 
			this.btnGoBack.Image = ((System.Drawing.Image)(resources.GetObject("btnGoBack.Image")));
			this.btnGoBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnGoBack.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnGoBack.Name = "btnGoBack";
			this.btnGoBack.Size = new System.Drawing.Size(52, 32);
			this.btnGoBack.Text = "后退";
			this.btnGoBack.Click += new System.EventHandler(this.btnGoBack_Click);
			// 
			// btnGoForward
			// 
			this.btnGoForward.Image = ((System.Drawing.Image)(resources.GetObject("btnGoForward.Image")));
			this.btnGoForward.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnGoForward.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnGoForward.Name = "btnGoForward";
			this.btnGoForward.Size = new System.Drawing.Size(52, 32);
			this.btnGoForward.Text = "前进";
			this.btnGoForward.Click += new System.EventHandler(this.btnGoForward_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(80, 32);
			this.toolStripLabel1.Text = "超链接地址：";
			// 
			// txtUrlAddress
			// 
			this.txtUrlAddress.Name = "txtUrlAddress";
			this.txtUrlAddress.Size = new System.Drawing.Size(400, 35);
			// 
			// btnLoadUrl
			// 
			this.btnLoadUrl.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadUrl.Image")));
			this.btnLoadUrl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnLoadUrl.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnLoadUrl.Name = "btnLoadUrl";
			this.btnLoadUrl.Size = new System.Drawing.Size(112, 32);
			this.btnLoadUrl.Text = "请开始你的表演";
			this.btnLoadUrl.Click += new System.EventHandler(this.btnLoadUrl_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
			// 
			// btnOpenFindbar
			// 
			this.btnOpenFindbar.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFindbar.Image")));
			this.btnOpenFindbar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnOpenFindbar.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnOpenFindbar.Name = "btnOpenFindbar";
			this.btnOpenFindbar.Size = new System.Drawing.Size(88, 32);
			this.btnOpenFindbar.Text = "查找工具栏";
			this.btnOpenFindbar.Click += new System.EventHandler(this.btnOpenFindbar_Click);
			// 
			// btnOpenDevtools
			// 
			this.btnOpenDevtools.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDevtools.Image")));
			this.btnOpenDevtools.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnOpenDevtools.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnOpenDevtools.Name = "btnOpenDevtools";
			this.btnOpenDevtools.Size = new System.Drawing.Size(88, 32);
			this.btnOpenDevtools.Text = "开发者工具";
			this.btnOpenDevtools.Click += new System.EventHandler(this.btnOpenDevtools_Click);
			// 
			// palBrowserWindow
			// 
			this.palBrowserWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.palBrowserWindow.BackColor = System.Drawing.Color.Gray;
			this.palBrowserWindow.Location = new System.Drawing.Point(0, 36);
			this.palBrowserWindow.Name = "palBrowserWindow";
			this.palBrowserWindow.Size = new System.Drawing.Size(1008, 671);
			this.palBrowserWindow.TabIndex = 9;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(85)))), ((int)(((byte)(150)))));
			this.ClientSize = new System.Drawing.Size(1008, 729);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.palBrowserWindow);
			this.Controls.Add(this.ssrMainbar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "通用应用程序模板";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.Shown += new System.EventHandler(this.frmMain_Shown);
			this.ssrMainbar.ResumeLayout(false);
			this.ssrMainbar.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip ssrMainbar;
		private System.Windows.Forms.ToolStripStatusLabel tssLblVer;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnGoBack;
		private System.Windows.Forms.ToolStripButton btnGoForward;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripTextBox txtUrlAddress;
		private System.Windows.Forms.ToolStripButton btnLoadUrl;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton btnOpenFindbar;
		private System.Windows.Forms.ToolStripButton btnOpenDevtools;
		private System.Windows.Forms.Panel palBrowserWindow;
	}
}