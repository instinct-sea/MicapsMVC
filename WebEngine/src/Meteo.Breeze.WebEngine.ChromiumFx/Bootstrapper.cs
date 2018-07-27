//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：Bootstrapper.cs
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
using Chromium.Remote;
using System;
using System.IO;

namespace Meteo.Breeze.WebEngine.ChromiumFx
{
	/// <summary>
	/// 启动加载器[引导程序]
	/// </summary>
	internal class Bootstrapper
	{
		/// <summary>
		/// 当前的启动加载器[引导程序]
		/// </summary>
		internal static Bootstrapper Current { get; } = new Bootstrapper();


		/// <summary>
		/// CEF 框架文件基路径
		/// </summary>
		private readonly string _basePath = @"cef";
		/// <summary>
		/// 启动是否完成
		/// </summary>
		internal bool IsStarted { get; private set; }


		/// <summary>
		/// 启动
		/// </summary>
		internal void Start()
		{
			CfxRuntime.LibCefDirPath = Path.GetFullPath($@"{_basePath}\{CfxRuntime.PlatformArch.ToString()}");
			CfxRuntime.LibCfxDirPath = Path.GetFullPath($"{_basePath}");

			if (!Directory.Exists(CfxRuntime.LibCefDirPath))
			{
				throw new DirectoryNotFoundException("Sorry! The CEF configuration directory was not found. Please confirm and try again.");
			}

			OnBeforeCfxInitialize += new EventHandler<OnBeforeCfxInitializeEventArgs>(ChromiumFx_OnBeforeCfxInitialize);
			OnBeforeCommandLineProcessing += new EventHandler<CfxOnBeforeCommandLineProcessingEventArgs>(ChromiumFx_OnBeforeCommandLineProcessing);
			Initialize();

			IsStarted = true;
		}
		/// <summary>
		/// 停止
		/// </summary>
		internal void Stop()
		{
			IsStarted = false;
			CfxRuntime.Shutdown();
		}


		#region 成员私有方法

		/// <summary>
		/// 在浏览器初始化之前执行事件
		/// </summary>
		/// <param name="sender">传递对象</param>
		/// <param name="e">浏览器初始化之前执行事件参数</param>
		private void ChromiumFx_OnBeforeCfxInitialize(object sender, OnBeforeCfxInitializeEventArgs e)
		{
			CfxRuntime.EnableHighDpiSupport();

			e.Settings.SingleProcess = true;
			//e.Settings.BrowserSubprocessPath = Path.GetFullPath("ChromiumFXRenderProcess.exe");

			//缓存数据存放位置
			e.Settings.CachePath = Path.GetFullPath($@"{_basePath}\LocalCache");
			//启用无窗口渲染
			e.Settings.WindowlessRenderingEnabled = true;
			//无沙箱
			e.Settings.NoSandbox = true;
			//集成消息循环(Message Loop Integration)
			//仅 Windows 操作系统有效
			e.Settings.MultiThreadedMessageLoop = true;
			//禁用日志
			e.Settings.LogSeverity = CfxLogSeverity.Disable;
			//指定中文为当前CEF环境的默认语言
			e.Settings.AcceptLanguageList = "zh-CN";
			e.Settings.Locale = "zh-CN";
			//资源文件及语言环境目录
			e.Settings.LocalesDirPath = Path.GetFullPath($@"{_basePath}\Resources\locales");
			e.Settings.ResourcesDirPath = Path.GetFullPath($@"{_basePath}\Resources");
		}
		/// <summary>
		/// 在浏览器命令行处理之前执行事件
		/// </summary>
		/// <param name="sender">传递对象</param>
		/// <param name="e">浏览器命令行处理事件参数</param>
		private void ChromiumFx_OnBeforeCommandLineProcessing(object sender, CfxOnBeforeCommandLineProcessingEventArgs e)
		{
			e.CommandLine.AppendSwitch("disable-gpu");

			//Uses WinHTTP to fetch and evaluate PAC scripts. Otherwise the default is to use Chromium's network stack to fetch, and V8 to evaluate.
			//https://peter.sh/experiments/chromium-command-line-switches/#winhttp-proxy-resolver
			e.CommandLine.AppendSwitch("--winhttp-proxy-resolver");

			//Don't use a proxy server, always make direct connections. Overrides any other proxy server flags that are passed. 
			//https://peter.sh/experiments/chromium-command-line-switches/#no-proxy-server
			e.CommandLine.AppendSwitch("--no-proxy-server");

			//Don't enforce the same-origin policy. (Used by people testing their sites.) 
			//禁用跨域安全检测 & 关闭同源策略
			e.CommandLine.AppendSwitch("--disable-web-security");

			//PepperFlash\manifest.json中的version
			e.CommandLine.AppendSwitchWithValue("ppapi-flash-version", "19.0.0.207");
			e.CommandLine.AppendSwitchWithValue("ppapi-flash-path", $@"{_basePath}\PepperFlash\pepflashplayer.dll");
			//使用系统 Flash
			//e.CommandLine.AppendSwitch("--enable-system-flash");

#if DEBUG
			Console.WriteLine("ChromiumWebBrowser_OnBeforeCommandLineProcessing");
			Console.WriteLine(e.CommandLine.CommandLineString);
#endif

		}

		#endregion

		#region 成员浏览器进程初始化事件

		/// <summary>
		/// 在浏览器初始化之前执行回调事件
		/// </summary>
		internal static event EventHandler<OnBeforeCfxInitializeEventArgs> OnBeforeCfxInitialize;
		/// <summary>
		/// 执行浏览器初始化之前回调事件
		/// </summary>
		/// <param name="settings">全局参数设置</param>
		/// <param name="processHandler">浏览器进程处理</param>
		internal static void RaiseOnBeforeCfxInitialize(CfxSettings settings, CfxBrowserProcessHandler processHandler)
		{
			OnBeforeCfxInitialize?.Invoke(null, new OnBeforeCfxInitializeEventArgs(settings, processHandler));
		}


		/// <summary>
		/// 在浏览器命令行处理之前执行回调事件
		/// </summary>
		internal static event EventHandler<CfxOnBeforeCommandLineProcessingEventArgs> OnBeforeCommandLineProcessing;
		/// <summary>
		/// 执行浏览器命令行处理之前执行回调事件
		/// </summary>
		/// <param name="e">浏览器命令行处理事件参数</param>
		internal static void RaiseOnBeforeCommandLineProcessing(CfxOnBeforeCommandLineProcessingEventArgs e)
		{
			OnBeforeCommandLineProcessing?.Invoke(null, e);
		}


		/// <summary>
		/// 浏览器自定义方案注册回调事件
		/// </summary>
		internal static event EventHandler<CfxOnRegisterCustomSchemesEventArgs> OnRegisterCustomSchemes;
		/// <summary>
		/// 执行浏览器自定义方案注册回调事件
		/// </summary>
		/// <param name="e">浏览器自定义方案注册事件参数</param>
		internal static void RaiseOnRegisterCustomSchemes(CfxOnRegisterCustomSchemesEventArgs e)
		{
			OnRegisterCustomSchemes?.Invoke(null, e);
		}


		/// <summary>
		/// 渲染进程远程创建回调事件
		/// </summary>
        internal static event EventHandler<RemoteProcessCreatedEventArgs> RemoteProcessCreated;
		/// <summary>
		/// 执行渲染进程远程创建回调事件
		/// </summary>
		/// <param name="renderProcessHandler">渲染进程</param>
		internal static void RaiseRemoteProcessCreated(CfrRenderProcessHandler renderProcessHandler)
		{
			RemoteProcessCreated?.Invoke(null, new RemoteProcessCreatedEventArgs(renderProcessHandler));
		}
		


		/// <summary>
		/// 执行浏览器进程初始化工作
		/// </summary>
		private void Initialize()
		{
			FxCore.BrowserProcess.Initialize();
		}

		#endregion

	}
}
