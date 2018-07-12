//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：LifeSpanHandler.cs
// 项目名称：Meteo.Breeze.WebEngine.ChromiumFx
// 创建时间：2018-07-09 17:07:42
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

namespace Meteo.Breeze.WebEngine.ChromiumFx.FxCore
{
	/// <summary>
	/// 浏览器生命周期事件处理类
	/// </summary>
	internal class LifeSpanHandler : CfxLifeSpanHandler
	{
		/// <summary>
		/// 浏览器生命周期事件处理类
		/// </summary>
		/// <param name="client">浏览器客户端事件处理对象</param>
		internal LifeSpanHandler(BrowserClient client)
		{
			_currentClient = client;
			OnAfterCreated += new CfxOnAfterCreatedEventHandler(LifeSpanHandler_OnAfterCreated);
			OnBeforePopup += new CfxOnBeforePopupEventHandler(LifeSpanHandler_OnBeforePopup);
		}

		/// <summary>
		/// 浏览器客户端事件处理对象
		/// </summary>
		internal BrowserClient _currentClient;

		/// <summary>
		/// 浏览器创建后执行事件
		/// </summary>
		/// <param name="sender">传递对象</param>
		/// <param name="e">传递事件</param>
		private void LifeSpanHandler_OnAfterCreated(object sender, CfxOnAfterCreatedEventArgs e)
		{
			_currentClient._browserWindow.OnBrowserCreated(e);
		}

		private void LifeSpanHandler_OnBeforePopup(object sender, CfxOnBeforePopupEventArgs e)
		{
			switch (e.TargetDisposition) {
				case CfxWindowOpenDisposition.NewBackgroundTab:
				case CfxWindowOpenDisposition.NewForegroundTab:
				case CfxWindowOpenDisposition.NewPopup:
				case CfxWindowOpenDisposition.NewWindow:
					_currentClient._browserWindow.Browser.LoadURL(e.TargetUrl);
					_currentClient._browserWindow.CloseBrowser(false);
					e.SetReturnValue(true);
					break;
				default:
					e.SetReturnValue(false);
					break;
			}
		}
	}
}
