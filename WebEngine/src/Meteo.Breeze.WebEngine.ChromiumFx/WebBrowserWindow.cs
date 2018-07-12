//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：WebBrowserWindow.cs
// 项目名称：Meteo.Breeze.WebEngine.ChromiumFx
// 创建时间：2018-07-06 16:25:51
// 创建人员：宋杰军
// 负 责 人：北京绘云天科技有限公司
// 参与人员：Breeze 开发组
// ===================================================================
// 修改日期：
// 修改人员：
// 修改内容：
// ===================================================================
using Chromium;
using Chromium.Event;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Meteo.Breeze.WebEngine.ChromiumFx
{
	/// <summary>
	/// 浏览器窗口常用操作类
	/// </summary>
	public class WebBrowserWindow : IWebBrowserWindow, IDisposable
	{
		/// <summary>
		/// 浏览器窗口常用操作类
		/// </summary>
		/// <param name="defWidth">默认宽度</param>
		/// <param name="defHeight">默认高度</param>
		/// <param name="initialUrl">初始的超链接，默认为空白页</param>
		public WebBrowserWindow(int defWidth, int defHeight, string initialUrl = "about:blank")
		{
			_currentWidth = defWidth;
			_currentHeight = defHeight;
			_currentURL = initialUrl;

			_browserClient = new FxCore.BrowserClient(this);			
		}

		#region 成员变量

		/// <summary>
		/// 当前浏览器控件宽度
		/// </summary>
		private int _currentWidth = 0;
		/// <summary>
		/// 当前浏览器控件高度
		/// </summary>
		private int _currentHeight = 0;

		private string _currentURL = "about:blank";


		/// <summary>
		/// 当前的浏览器对象
		/// </summary>
		private CfxBrowser _currentBrowser;
		/// <summary>
		/// 当前的浏览器主机对象
		/// </summary>
		private CfxBrowserHost _currentHost;
		/// <summary>
		/// 当前的浏览器  Win32 窗口句柄号
		/// </summary>
		private IntPtr _currentHandler;

		
		/// <summary>
		/// 浏览器客户端事件处理对象
		/// </summary>
		internal FxCore.BrowserClient _browserClient;

		#endregion

		#region 成员属性



		#endregion

		#region 成员方法



		#endregion




		#region 成员处理事件

		/// <summary>
		/// 
		/// </summary>
		//private TaskCompletionSource<bool> BrowserCreatedTCS = new TaskCompletionSource<bool>();
		/// <summary>
		/// 
		/// </summary>
		//internal Task OnBrowserCreatedAsync()
		//{
		//	return BrowserCreatedTCS.Task;
		//}
		/// <summary>
		/// 浏览器创建结果
		/// </summary>
		/// <param name="e">传递事件</param>
		internal void OnBrowserCreated(CfxOnAfterCreatedEventArgs e)
		{
			_currentBrowser = e.Browser;
			_currentHost = _currentBrowser.Host;
			_currentHandler = _currentHost.WindowHandle;
			OnResize(_currentWidth, _currentHeight);
			Browser = new WebBrowser(_currentBrowser, _currentHost);
			Browser.LoadURL(_currentURL);
			IsBrowserCreated = true;

			//BrowserCreatedTCS.SetResult(true);
		}
		/// <summary>
		/// 浏览器框架内容查找结果
		/// </summary>
		/// <param name="e">传递事件</param>
		internal void OnFound(CfxFindHandlerOnFindResultEventArgs e)
		{
			FindHandler?.Invoke(null, new FindEventArgs(e.FinalUpdate, e.Identifier, e.Count));
		}

		#endregion

		#region 成员窗口位置飘移事件

		/// <summary>
		/// 更改指定窗口的属性
		/// <para>该函数还将指定偏移处的32位（长）值设置到额外窗口存储器中</para>
		/// </summary>
		/// <param name="hWnd">窗口句柄号</param>
		/// <param name="nIndex">要设置的值的从零开始的偏移量</param>
		/// <param name="dwNewLong">指定的替换值</param>
		/// <returns>如果函数成功，返回值是指定的32位整数的原来的值。如果函数失败，返回值为0。</returns>
		[DllImport("user32", SetLastError = false)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
		/// <summary>
		/// 设定窗口的位置、大小和Z序(Z序)
		/// </summary>
		/// <param name="hWnd">窗口句柄号</param>
		/// <param name="hWndInsertAfter">用于标识在z-顺序的此 CWnd 对象之前的 CWnd 对象</param>
		/// <param name="X">以客户坐标指定窗口新位置的左边界</param>
		/// <param name="Y">以客户坐标指定窗口新位置的顶边界</param>
		/// <param name="cx">以像素指定窗口的新的宽度</param>
		/// <param name="cy">以像素指定窗口的新的高度</param>
		/// <param name="uFlags">窗口尺寸和定位的标志</param>
		/// <returns>如果函数成功，返回值为非零；如果函数失败，返回值为零。</returns>
        [DllImport("user32", SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_SHOWWINDOW = 0x0040;
        private const uint SWP_HIDEWINDOW = 0x0080;
        private const uint SWP_NOCOPYBITS = 0x0100;
        private const uint SWP_ASYNCWINDOWPOS = 0x4000;

		#endregion

		#region 接口实现[IWebBrowserWindow]
		
		/// <summary>
		/// 浏览器框架内容查找事件
		/// </summary>
		public event EventHandler<FindEventArgs> FindHandler;

		/// <summary>
		/// 浏览器是否创建
		/// </summary>
		public bool IsBrowserCreated { get; private set; }
		/// <summary>
		/// 浏览器 Win32 窗口句柄号
		/// </summary>
		public IntPtr WindowHandle => _currentHandler;
		/// <summary>
		/// 浏览器常用操作对象
		/// </summary>
		public IWebBrowser Browser { get; private set; }
		
		#region Bworser methods

		/// <summary>
		/// 关闭浏览器
		/// </summary>
		/// <param name="isPromptUser">是否提示用户即将关闭浏览器</param>
		public void CloseBrowser(bool isPromptUser)
		{
			_currentHost.CloseBrowser(isPromptUser);
		}
		/// <summary>
		/// 关闭浏览器
		/// </summary>
		public void TryCloseBrowser()
		{
			_currentHost?.TryCloseBrowser();
		}

		#endregion

		#region DevTools methods

		/// <summary>
		/// 显示开发者调试工具
		/// </summary>
		public void ShowDevTools()
		{
			var windowInfo = new CfxWindowInfo();
			windowInfo.Style = WindowStyle.WS_OVERLAPPEDWINDOW | WindowStyle.WS_CLIPCHILDREN | WindowStyle.WS_CLIPSIBLINGS | WindowStyle.WS_VISIBLE;
			windowInfo.ParentWindow = IntPtr.Zero;
			windowInfo.WindowName = "开发者工具栏";
			windowInfo.X = 200;
			windowInfo.Y = 200;
			windowInfo.Width = 800;
			windowInfo.Height = 600;
			_currentHost?.ShowDevTools(windowInfo, new CfxClient(), new CfxBrowserSettings(), null);
		}
		/// <summary>
		/// 关闭开发者调试工具
		/// </summary>
		public void CloseDevTools()
		{
			_currentHost?.CloseDevTools();
		}
		
		#endregion

		#region Basic methods

		/// <summary>
		/// 窗口大小发生变化时
		/// </summary>
		/// <param name="width">宽度</param>
		/// <param name="height">高度</param>
		public void OnResize(int width, int height)
		{
			if (_currentHandler == IntPtr.Zero) return;
			if (height > 0 && width > 0)
			{				
				SetWindowLong(_currentHandler, -16, (int)(WindowStyle.WS_CHILD | WindowStyle.WS_CLIPCHILDREN | WindowStyle.WS_CLIPSIBLINGS | WindowStyle.WS_TABSTOP | WindowStyle.WS_VISIBLE));
				SetWindowPos(_currentHandler, IntPtr.Zero, 0, 0, width, height, SWP_NOMOVE | SWP_NOZORDER | SWP_SHOWWINDOW | SWP_NOCOPYBITS | SWP_ASYNCWINDOWPOS);
			}
			else
			{
				SetWindowLong(_currentHandler, -16, (int)(WindowStyle.WS_CHILD | WindowStyle.WS_CLIPCHILDREN | WindowStyle.WS_CLIPSIBLINGS | WindowStyle.WS_TABSTOP | WindowStyle.WS_DISABLED));
				SetWindowPos(_currentHandler, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_HIDEWINDOW | SWP_ASYNCWINDOWPOS);
			}
		}
		/// <summary>
		/// 取消当前正在进行的所有搜索。
		/// </summary>
		/// <param name="clearSelection">是否清空所有选择</param>
		public void StopFinding(bool clearSelection = true)
		{
			_currentHost?.StopFinding(clearSelection);
		}
		/// <summary>
		/// 浏览器框架内容查找
		/// </summary>
		/// <param name="identifier">查找ID</param>
		/// <param name="searchText">要查找的内容</param>
		/// <param name="forward">是否查找上一个</param>
		/// <param name="matchCase">是否区分大小写</param>
		/// <param name="findNext">是否查找下一个</param>
		public void Find(int identifier, string searchText, bool forward, bool matchCase, bool findNext)
		{
			_currentHost?.Find(identifier, searchText, forward, matchCase, findNext);
		}

		#endregion

		#endregion

		#region 接口实现[IDisposable]

		/// <summary>
		/// 资源释放
		/// </summary>
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion

	}
}
