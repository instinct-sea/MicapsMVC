using Meteo.Breeze.WebEngine.WinForm;
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
			Hide();
			FSplash = SplashScreen.Begin();
			FSplash.MsgUpdate("主窗体加载中...");
			Thread.Sleep(1000);
			#endregion
			InitializeComponent();
			InitializeThis();
			#region SplashScreen·End
			SplashScreen.End();
			FSplash = null;
			Show();
			BringToFront();
			#endregion
		}

		#region 初始化

		/// <summary>
		/// 初始化
		/// </summary>
		private void InitializeThis()
		{
			FSplash.MsgUpdate("初始化基础数据...");
			Thread.Sleep(1000);
			Text = Library.ConfigHelper.AppName;
			tssLblVer.Text = string.Format("{1} {0}", Library.ConfigHelper.AppVersion, this.Text);

			btnLoadUrlPlus = new Button();
			btnLoadUrlPlus.Click += (s, e) =>
			{
				btnLoadUrl.PerformClick();
			};
			AcceptButton = btnLoadUrlPlus;
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
			var webWindow = new WebWindow();
			webWindow.Dock = DockStyle.Fill;
			palBrowserWindow.Controls.Add(webWindow);
			_currentBrowserWindow = webWindow;

			txtUrlAddress.Text = @"http://news.sina.com.cn";
			btnLoadUrl.PerformClick();
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
		}

		#endregion

		#region 成员变量

		/// <summary>
		/// 闪屏窗体
		/// </summary>
		private SplashScreen FSplash;

		private WebWindow _currentBrowserWindow;

		private Button btnLoadUrlPlus;

		#endregion


		private void btnGoBack_Click(object sender, EventArgs e)
		{
			_currentBrowserWindow?.BrowserWindow?.Browser?.GoBack();
		}

		private void btnGoForward_Click(object sender, EventArgs e)
		{
			_currentBrowserWindow?.BrowserWindow?.Browser?.GoForward();
		}

		private void btnLoadUrl_Click(object sender, EventArgs e)
		{
			_currentBrowserWindow?.BrowserWindow?.Browser?.LoadURL(txtUrlAddress.Text.Trim());
		}

		private void btnOpenFindbar_Click(object sender, EventArgs e)
		{
			_currentBrowserWindow.FindToolbar.Visible = !_currentBrowserWindow.FindToolbar.Visible;
		}

		private void btnOpenDevtools_Click(object sender, EventArgs e)
		{
			_currentBrowserWindow?.BrowserWindow.ShowDevTools();
		}
	}
}
