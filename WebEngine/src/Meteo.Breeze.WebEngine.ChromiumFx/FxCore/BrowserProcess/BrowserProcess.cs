//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：BrowserProcess.cs
// 项目名称：Meteo.Breeze.WebEngine.ChromiumFx
// 创建时间：2018-07-10 17:22:34
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

namespace Meteo.Breeze.WebEngine.ChromiumFx.FxCore
{
	/// <summary>
	/// 浏览器进程
	/// </summary>
	internal static class BrowserProcess
	{
		/// <summary>
		/// CfxApp
		/// </summary>
		internal static CfxApp app;
		/// <summary>
		/// 浏览器进程处理
		/// </summary>
		internal static CfxBrowserProcessHandler processHandler;

		/// <summary>
		/// 初始化是否完成
		/// </summary>
		internal static bool initialized;

		/// <summary>
		/// 初始化
		/// </summary>
		internal static void Initialize()
		{
			if (initialized)
			{
				throw new WebEngineException("对不起！初始化工作已经完成，请勿重复执行初始化工作。");
			}

			int retval = CfxRuntime.ExecuteProcess();
			if (retval >= 0) Environment.Exit(retval);

			app = new CfxApp();
			processHandler = new CfxBrowserProcessHandler();

			app.GetBrowserProcessHandler += (s, e) => e.SetReturnValue(processHandler);
			app.OnBeforeCommandLineProcessing += (s, e) => Bootstrapper.RaiseOnBeforeCommandLineProcessing(e);
			app.OnRegisterCustomSchemes += (s, e) => Bootstrapper.RaiseOnRegisterCustomSchemes(e);

			var settings = new CfxSettings();
			settings.MultiThreadedMessageLoop = true;
			settings.NoSandbox = true;

			Bootstrapper.RaiseOnBeforeCfxInitialize(settings, processHandler);

			//if (!CfxRuntime.Initialize(settings, app, RenderProcess.RenderProcessMain))
			if (!CfxRuntime.Initialize(settings, app))
			{
				throw new WebEngineException("对不起！无法初始化 CEF 运行库。");
			}

			initialized = true;
		}
	}
}
