//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：BrowserClient.cs
// 项目名称：Meteo.Breeze.WebEngine.ChromiumFx
// 创建时间：2018-07-09 17:03:45
// 创建人员：宋杰军
// 负 责 人：北京绘云天科技有限公司
// 参与人员：Breeze 开发组
// ===================================================================
// 修改日期：
// 修改人员：
// 修改内容：
// ===================================================================
using Chromium;

namespace Meteo.Breeze.WebEngine.ChromiumFx.FxCore
{
	/// <summary>
	/// 浏览器客户端事件处理类
	/// </summary>
	internal class BrowserClient : CfxClient
	{
		/// <summary>
		/// 浏览器客户端事件处理类
		/// </summary>
		public BrowserClient(WebBrowserWindow browserWindow)
		{
			_browserWindow = browserWindow;

			_lifeSpanHandler = new LifeSpanHandler(this);
			GetLifeSpanHandler += (s, e) => e.SetReturnValue(_lifeSpanHandler);

			_findHandler = new CfxFindHandler();
			_findHandler.OnFindResult += (s, e) => browserWindow.OnFound(e);
			GetFindHandler += (s, e) => e.SetReturnValue(_findHandler);
		}

		/// <summary>
		/// 浏览器窗口常用操作对象
		/// </summary>
		internal WebBrowserWindow _browserWindow;

		/// <summary>
		/// 浏览器生命周期事件处理对象
		/// </summary>
		internal LifeSpanHandler _lifeSpanHandler;
		/// <summary>
		/// 浏览器框架内容查找结果事件
		/// </summary>
		internal CfxFindHandler _findHandler;
	}
}
