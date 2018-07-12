//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：RenderProcess.cs
// 项目名称：Meteo.Breeze.WebEngine.ChromiumFx
// 创建时间：2018-07-10 17:40:54
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
using System;
using System.Collections.Generic;

namespace Meteo.Breeze.WebEngine.ChromiumFx.FxCore
{
	/// <summary>
	/// 渲染进程
	/// </summary>
	//internal class RenderProcess
	//{
	//	/// <summary>
	//	/// 渲染进程主函数
	//	/// </summary>
	//	/// <returns>结果呢</returns>
	//	internal static int RenderProcessMain()
	//	{
	//		try
	//		{
	//			var rp = new RenderProcess();
	//			Bootstrapper.RaiseRemoteProcessCreated(rp.processHandler);
	//			return rp.RemoteMain();
	//		}
	//		catch (CfxRemotingException)
	//		{
	//			return -1;
	//		}
	//	}

 //       private readonly CfrApp app;
 //       private readonly RenderProcessHandler processHandler;

 //       private List<WeakReference> browserReferences = new List<WeakReference>();

 //       internal int RemoteProcessId { get; private set; }

 //       private RenderProcess() {
 //           RemoteProcessId = CfxRemoteCallContext.CurrentContext.ProcessId;
 //           app = new CfrApp();
 //           processHandler = new RenderProcessHandler(this);
 //           app.GetRenderProcessHandler += (s, e) => e.SetReturnValue(processHandler);
 //       }

 //       internal void AddBrowserReference(ChromiumWebBrowser browser) {
 //           for(int i = 0; i < browserReferences.Count; ++i) {
 //               if(browserReferences[i].Target == null) {
 //                   browserReferences[i] = new WeakReference(browser);
 //                   return;
 //               }
 //           }
 //           browserReferences.Add(new WeakReference(browser));
 //       }

 //       private int RemoteMain() {
 //           try {
 //               var retval = CfrRuntime.ExecuteProcess(app);
 //               return retval;
 //           } finally {
 //               foreach(var br in browserReferences) {
 //                   var b = (ChromiumWebBrowser)br.Target;
 //                   b?.RemoteProcessExited(this);
 //               }
 //           }
 //       }
	//}
}
