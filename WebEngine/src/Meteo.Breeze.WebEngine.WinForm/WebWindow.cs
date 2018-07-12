//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：WebWindow.cs
// 项目名称：Meteo.Breeze.WebEngine.WinForm
// 创建时间：2018-07-06 15:34:48
// 创建人员：宋杰军
// 负 责 人：北京绘云天科技有限公司
// 参与人员：Breeze 开发组
// ===================================================================
// 修改日期：
// 修改人员：
// 修改内容：
// ===================================================================
using System;
using System.Windows.Forms;

namespace Meteo.Breeze.WebEngine.WinForm
{
	/// <summary>
	/// Web Window
	/// </summary>
	public class WebWindow : Control
	{
		/// <summary>
		/// Web Window
		/// </summary>
		public WebWindow()
		{
			_currentBrowserWindow = WebWindowFactory.GetBrowserWindow(Handle, Width, Height);
		}

		#region 窗体事件

		/// <summary>
		/// 窗体大小改变事件
		/// </summary>
		/// <param name="e">传递事件</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ResizeWebWindow();
		}
		/// <summary>
		/// 浏览器窗体大小改变事件
		/// </summary>
		internal void ResizeWebWindow()
		{
			int h;
			if (_findToolbar == null || !_findToolbar.Visible)
			{
				h = Height;
			}
			else
			{
				if (InvokeRequired)
				{
					Invoke((MethodInvoker)(() =>
					{
						_findToolbar.Width = Width;
						_findToolbar.Top = Height - _findToolbar.Height;
					}));
				}
				else
				{
					_findToolbar.Width = Width;
					_findToolbar.Top = Height - _findToolbar.Height;
				}
				h = _findToolbar.Top;
			}
			_currentBrowserWindow?.OnResize(Width, h);
		}

		#endregion

		/// <summary>
		/// 当前的 Web Browser Window
		/// </summary>
		private IWebBrowserWindow _currentBrowserWindow;
		/// <summary>
		/// 
		/// </summary>
		public IWebBrowserWindow BrowserWindow => _currentBrowserWindow;

		#region 浏览器框架内容查找处理事件

		/// <summary>
		/// 当前匹配的 Id
		/// </summary>
		private int _findId;
		/// <summary>
		/// 查找内容
		/// <para>当前的</para>
		/// </summary>
		private string _currentFindText;
		/// <summary>
		/// 是否区分大小写
		/// <para>当前的</para>
		/// </summary>
		private bool _currentMatchCase;

		/// <summary>
		/// 浏览器框架内容查找工具栏对象
		/// </summary>
		private FindToolbar _findToolbar;

		/// <summary>
		/// 浏览器框架内容查找工具栏对象
		/// </summary>
		public FindToolbar FindToolbar
		{
			get
			{
				if (_findToolbar == null)
				{
					_findToolbar = new FindToolbar(this);
				}
				return _findToolbar;
			}
		}

		/// <summary>
		/// 浏览器框架内容查找
		/// </summary>
		/// <param name="searchText">要查找的内容</param>
		/// <param name="forward">是否查找上一个</param>
		/// <param name="matchCase">是否区分大小写</param>
		/// <returns>匹配的 Id</returns>
		public int Find(string searchText, bool forward, bool matchCase)
		{
			if (!_currentBrowserWindow.IsBrowserCreated) return -1;

			var findNext = _currentFindText == searchText && _currentMatchCase == matchCase;
			if (!findNext)
			{
				_currentFindText = searchText;
				_currentMatchCase = matchCase;
				++_findId;
			}

			_currentBrowserWindow.Find(_findId, searchText, forward, matchCase, findNext);
			return _findId;
		}
		/// <summary>
		/// 浏览器框架内容查找
		/// </summary>
		/// <param name="searchText">要查找的内容</param>
		/// <param name="forward">是否查找上一个</param>
		/// <returns>匹配的 Id</returns>
		public int Find(string searchText, bool forward)
		{
			return Find(searchText, forward, false);
		}
		/// <summary>
		/// 浏览器框架内容查找
		/// </summary>
		/// <param name="searchText">要查找的内容</param>
		/// <returns>匹配的 Id</returns>
		public int Find(string searchText)
		{
			return Find(searchText, true, false);
		}

		#endregion

	}
}
