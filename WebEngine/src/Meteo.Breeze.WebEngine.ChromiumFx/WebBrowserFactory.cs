//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：WebBrowserFactory.cs
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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meteo.Breeze.WebEngine.ChromiumFx
{
	using WebBrowserReference = WeakReference<IWebBrowserWindow>;

	/// <summary>
	/// 浏览器工厂类
	/// <para>创建事务</para>
	/// <para>操作事务</para>
	/// <para>管理事务</para>
	/// </summary>
	public class WebBrowserFactory : IWebBrowserFactory, IDisposable
	{
		/// <summary>
		/// 创建一个浏览器窗口
		/// </summary>
		/// <param name="formHandle">当前承载浏览器窗口的 Form 窗体的句柄号(Handle)</param>
		/// <param name="defWidth">默认宽度</param>
		/// <param name="defHeight">默认高度</param>
		/// <param name="initialUrl">初始的超链接，默认为空白页</param>
		/// <returns></returns>
		public IWebBrowserWindow CreateBrowser(IntPtr formHandle, int defWidth, int defHeight, string initialUrl = "about:blank")
		{
			if (!Bootstrapper.Current.IsStarted)
			{
				Bootstrapper.Current.Start();
			}


			var browserWindow = new WebBrowserWindow(defWidth, defHeight);
			//await To doing;

			var windowInfo = new CfxWindowInfo();
			windowInfo.SetAsDisabledChild(formHandle);

			var browserSettings = new CfxBrowserSettings();

			if (!CfxBrowserHost.CreateBrowser(windowInfo, browserWindow._browserClient, initialUrl, browserSettings, null))
			{
				throw new WebEngineException("对不起！浏览器窗口创建失败，请确认所有配置正确后重试。");
			}

			//browserWindow.OnBrowserCreatedAsync();

			//browserWindow.Browser.LoadURL(initialUrl);


			_browsers.Add(new WebBrowserReference(browserWindow));

			return browserWindow;
		}


		/// <summary>
		/// 浏览器窗口管理容器
		/// </summary>
		private List<WebBrowserReference> _browsers = new List<WebBrowserReference>();


		/// <summary>
		/// 资源释放
		/// </summary>
		public void Dispose()
		{
			foreach (var browser in _browsers)
			{
				if (browser.TryGetTarget(out IWebBrowserWindow browserWindow))
				{
					browserWindow.CloseDevTools();
					browserWindow.TryCloseBrowser();
				}
			}
			Bootstrapper.Current.Stop();
		}
	}
}
