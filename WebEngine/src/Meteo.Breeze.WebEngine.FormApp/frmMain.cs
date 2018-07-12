using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meteo.Breeze.WebEngine.FormApp
{
	/// <summary>
	/// 程序主界面
	/// </summary>
	public partial class frmMain : Form
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public frmMain()
		{
			#region SplashScreen·Begin
			this.Hide();
			FSplash = SplashScreen.Begin();
			FSplash.MsgUpdate("主窗体加载中...");
			Thread.Sleep(1000);
			#endregion
			InitializeComponent();
			InitializeThis();
			#region SplashScreen·End
			SplashScreen.End();
			FSplash = null;
			this.Show();
			this.Activate();
			#endregion
		}

		#region 初始化

		/// <summary>
		/// 初始化
		/// </summary>
		private void InitializeThis()
		{
			FSplash.MsgUpdate("初始化基础数据...");
			Thread.Sleep(2000);
			this.Text = Library.ConfigHelper.AppName;
			this.tssLblVer.Text = string.Format("{1} {0}", Library.ConfigHelper.AppVersion, this.Text);
			//FSplash.MsgUpdate("初始化日志记录器...");
			//Thread.Sleep(100);
			//FLogger = new DawnLogHelper();
		}

		#endregion

		#region 窗体事件

		/// <summary>
		/// 窗体加载时
		/// </summary>
		/// <param name="sender">传送对象</param>
		/// <param name="e">传送事件</param>
		private void frmMain_Load(object sender, EventArgs e)
		{
			DawnXZ.FormUtility.FEffects.ShowEff(this.Handle, 500, DawnXZ.FormUtility.FEffects.AW_ACTIVATE | DawnXZ.FormUtility.FEffects.AW_CENTER);
		}
		/// <summary>
		/// 窗体显示时
		/// </summary>
		/// <param name="sender">传送对象</param>
		/// <param name="e">传送事件</param>
		private void frmMain_Shown(object sender, EventArgs e)
		{
			//FLogger.Write("程序启动。");

			var webWindow = new WinForm.WebWindow();
			webWindow.Dock = DockStyle.Fill;
			panel1.Controls.Add(webWindow);
			_currentBrowserWindow = webWindow;
		}
		/// <summary>
		/// 窗体关闭时
		/// </summary>
		/// <param name="sender">传送对象</param>
		/// <param name="e">传送事件</param>
		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (MessageBox.Show(string.Format("您确定要退出【{0}】吗？", Library.ConfigHelper.AppName), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				e.Cancel = false;
				DawnXZ.FormUtility.FEffects.ShowEff(this.Handle, 500, DawnXZ.FormUtility.FEffects.AW_HIDE | DawnXZ.FormUtility.FEffects.AW_BLEND);
			}
			else
			{
				e.Cancel = true;
			}
		}
		/// <summary>
		/// 窗体结束时
		/// </summary>
		/// <param name="sender">传送对象</param>
		/// <param name="e">传送事件</param>
		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			_currentBrowserWindow.BrowserWindow.TryCloseBrowser();

			//FLogger.Write("程序关闭。");
			this.Dispose();
			Application.ExitThread();
			Application.Exit();
		}

		#endregion

		#region 成员变量

		/// <summary>
		/// 闪屏窗体
		/// </summary>
		SplashScreen FSplash;
		/// <summary>
		/// 日志记录器
		/// </summary>
		//DawnLogHelper FLogger;

		private WinForm.WebWindow _currentBrowserWindow;

		#endregion
		
		
		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			_currentBrowserWindow.BrowserWindow.Browser.LoadURL(toolStripTextBox1.Text.Trim());
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			_currentBrowserWindow.FindToolbar.Visible = !_currentBrowserWindow.FindToolbar.Visible;
		}
		
		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			_currentBrowserWindow.BrowserWindow.ShowDevTools();
		}
	}
}
