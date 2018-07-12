using Chromium;
using Chromium.Event;
using Chromium.WebBrowser;
using Chromium.WebBrowser.Event;
using DawnXZ.FileUtility;
using System;
using System.IO;
using System.Windows.Forms;

namespace Meteo.Breeze.WebEngine.ConsoleApp
{
	class Program
	{
		private static readonly string _basePath = @"cef";

		static void Main(string[] args)
		{
			Environment.CurrentDirectory = FileHelper.AppPath;

			CfxRuntime.LibCefDirPath = Path.GetFullPath($@"{_basePath}\{CfxRuntime.PlatformArch.ToString()}");
			CfxRuntime.LibCfxDirPath = Path.GetFullPath($"{_basePath}");

			if (!Directory.Exists(CfxRuntime.LibCefDirPath))
			{
				throw new DirectoryNotFoundException("Sorry! The CEF configuration directory was not found. Please confirm and try again.");
			}

			ChromiumWebBrowser.OnBeforeCfxInitialize += new OnBeforeCfxInitializeEventHandler(ChromiumWebBrowser_OnBeforeCfxInitialize);
			ChromiumWebBrowser.OnBeforeCommandLineProcessing += new OnBeforeCommandLineProcessingEventHandler(ChromiumWebBrowser_OnBeforeCommandLineProcessing);		
			ChromiumWebBrowser.Initialize();

			Application.EnableVisualStyles();
            var f = new BrowserForm();
            f.Show();
            Application.Run(f);

            CfxRuntime.Shutdown();
		}

		static void ChromiumWebBrowser_OnBeforeCfxInitialize(OnBeforeCfxInitializeEventArgs e)
		{
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
			e.Settings.LogSeverity = Chromium.CfxLogSeverity.Disable;
			//指定中文为当前CEF环境的默认语言
			e.Settings.AcceptLanguageList = "zh-CN";
			e.Settings.Locale = "zh-CN";
			//资源文件及语言环境目录
			e.Settings.LocalesDirPath = Path.GetFullPath($@"{_basePath}\Resources\locales");
			e.Settings.ResourcesDirPath = Path.GetFullPath($@"{_basePath}\Resources");
		}

		static void ChromiumWebBrowser_OnBeforeCommandLineProcessing(CfxOnBeforeCommandLineProcessingEventArgs e)
		{
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
			

            Console.WriteLine("ChromiumWebBrowser_OnBeforeCommandLineProcessing");
            Console.WriteLine(e.CommandLine.CommandLineString);
		}
	}
}
