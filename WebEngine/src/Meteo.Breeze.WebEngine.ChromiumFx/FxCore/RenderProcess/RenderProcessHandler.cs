//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：RenderProcessHandler.cs
// 项目名称：Meteo.Breeze.WebEngine.ChromiumFx
// 创建时间：2018-07-11 14:36:06
// 创建人员：宋杰军
// 负 责 人：北京绘云天科技有限公司
// 参与人员：Breeze 开发组
// ===================================================================
// 修改日期：
// 修改人员：
// 修改内容：
// ===================================================================
using Chromium;
using Chromium.Remote;
using Chromium.Remote.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.WebEngine.ChromiumFx.FxCore.RenderProcess
{
	/// <summary>
	/// 渲染进程 Handler
	/// </summary>
	//internal class RenderProcessHandler : CfrRenderProcessHandler
	//{
	//	/// <summary>
	//	/// 渲染进程
	//	/// </summary>
	//	private readonly RenderProcess remoteProcess;

	//	/// <summary>
	//	/// 
	//	/// </summary>
	//	/// <param name="remoteProcess"></param>
	//	internal RenderProcessHandler(RenderProcess remoteProcess)
	//	{
	//		this.remoteProcess = remoteProcess;

	//		OnContextCreated += RenderProcessHandler_OnContextCreated;
	//		OnBrowserCreated += RenderProcessHandler_OnBrowserCreated;
	//	}

	//	void RenderProcessHandler_OnBrowserCreated(object sender, CfrOnBrowserCreatedEventArgs e)
	//	{
	//		var id = e.Browser.Identifier;
	//		var wb = ChromiumWebBrowser.GetBrowser(id);
	//		if (wb != null)
	//		{

	//			var rp = wb.remoteProcess;
	//			if (rp != null && rp != this.remoteProcess)
	//			{
	//				// A new process has been created for the browser.
	//				// The old process is still alive, but probably it gets
	//				// killed soon after this callback returns.
	//				// So we suspend all callbacks from the old process.
	//				// If there are currently executing callbacks, 
	//				// this call will block until they are finished. 
	//				// When this call returns, it should be safe to 
	//				// continue execution and let the old process die.
	//				CfxRemoteCallbackManager.SuspendCallbacks(rp.RemoteProcessId);
	//			}

	//			wb.SetRemoteBrowser(e.Browser, remoteProcess);
	//		}
	//	}

	//	void RenderProcessHandler_OnContextCreated(object sender, CfrOnContextCreatedEventArgs e)
	//	{
	//		var wb = ChromiumWebBrowser.GetBrowser(e.Browser.Identifier);
	//		if (wb != null)
	//		{
	//			if (e.Frame.IsMain)
	//			{
	//				SetProperties(e.Context, wb.GlobalObject);
	//			}
	//			else
	//			{
	//				JSObject obj;
	//				if (wb.frameGlobalObjects.TryGetValue(e.Frame.Name, out obj))
	//				{
	//					SetProperties(e.Context, obj);
	//				}
	//			}
	//			wb.RaiseOnV8ContextCreated(e);
	//		}
	//	}

	//	private void SetProperties(CfrV8Context context, JSObject obj)
	//	{
	//		foreach (var p in obj)
	//		{
	//			var v8Value = p.Value.GetV8Value(context);
	//			context.Global.SetValue(p.Key, v8Value, CfxV8PropertyAttribute.DontDelete | CfxV8PropertyAttribute.ReadOnly);
	//		}
	//	}

	//}
}
