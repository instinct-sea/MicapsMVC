//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：WebBrowser.cs
// 项目名称：Meteo.Breeze.WebEngine.ChromiumFx
// 创建时间：2018-07-06 16:26:02
// 创建人员：宋杰军
// 负 责 人：北京绘云天科技有限公司
// 参与人员：Breeze 开发组
// ===================================================================
// 修改日期：
// 修改人员：
// 修改内容：
// ===================================================================
using Chromium;
using System;

namespace Meteo.Breeze.WebEngine.ChromiumFx
{
	/// <summary>
	/// 浏览器常用操作类
	/// </summary>
	public class WebBrowser : IWebBrowser
	{
		/// <summary>
		/// 浏览器常用操作类
		/// </summary>
		/// <param name="browser">浏览器</param>
		/// <param name="browserHost">主机</param>
		internal WebBrowser(CfxBrowser browser, CfxBrowserHost browserHost)
		{
			_currentBrowser = browser;
			_currentHost = browserHost;
			_currentMainFrame = browser.MainFrame;
		}

		/// <summary>
		/// 当前的浏览器对象
		/// </summary>
		private CfxBrowser _currentBrowser;
		/// <summary>
		/// 当前的浏览器主机对象
		/// </summary>
		private CfxBrowserHost _currentHost;
		/// <summary>
		/// 当前的浏览器主框架对象
		/// </summary>
		private CfxFrame _currentMainFrame;

		#region 接口实现[IWebBrowser]

		/// <summary>
		/// 向后导航
		/// </summary>
		public void GoBack()
		{
			if (_currentBrowser.CanGoBack)
			{
				_currentBrowser.GoBack();
			}
		}
		/// <summary>
		/// 向前导航
		/// </summary>
		public void GoForward()
		{
			if (_currentBrowser.CanGoForward)
			{
				_currentBrowser.GoForward();
			}
		}
		/// <summary>
		/// 重新加载当前页面
		/// </summary>
		public void Reload()
		{
			_currentBrowser.Reload();
		}
		/// <summary>
		/// 重新加载当前页面，并忽略任何缓存数据
		/// </summary>
		public void ReloadIgnoreCache()
		{
			_currentBrowser.ReloadIgnoreCache();
		}

		#endregion

		#region 接口实现[IWebFrame]

		/// <summary>
		/// 执行 JavaScript 脚本
		/// </summary>
		/// <param name="javascriptString">JavaScript 脚本字符串</param>
		public void ExecuteJavaScript(string javascriptString)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// 加载指定的字符串文本内容并展示
		/// </summary>
		/// <param name="stringValue">字符串文本内容</param>
		/// <param name="urlString">URL超链接</param>
		public void LoadString(string stringValue, string urlString)
		{
			_currentBrowser.StopLoad();
			_currentMainFrame.LoadString(stringValue, urlString);
		}
		/// <summary>
		/// 加载指定的URL超链接并展示
		/// </summary>
		/// <param name="urlString">URL超链接</param>
		public void LoadURL(string urlString)
		{
			_currentBrowser.StopLoad();
			_currentMainFrame.LoadUrl(urlString);
		}
		/// <summary>
		/// 显示当前框架的 HTML 源代码
		/// </summary>
		public void ViewSource()
		{
			_currentMainFrame.ViewSource();
		}

		#endregion

	}
}
