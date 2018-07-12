//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：OnBeforeCfxInitializeEventArgs.cs
// 项目名称：Meteo.Breeze.WebEngine.ChromiumFx
// 创建时间：2018-07-10 16:57:49
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
	/// 浏览器初始化之前执行事件参数类
	/// </summary>
	internal class OnBeforeCfxInitializeEventArgs : EventArgs
	{
		/// <summary>
		/// 浏览器初始化之前执行事件参数类
		/// </summary>
		/// <param name="settings">全局参数设置</param>
		/// <param name="processHandler">浏览器进程处理</param>
		internal OnBeforeCfxInitializeEventArgs(CfxSettings settings, CfxBrowserProcessHandler processHandler)
		{
			Settings = settings;
			ProcessHandler = processHandler;
		}

		/// <summary>
		/// 全局参数设置
		/// </summary>
		public CfxSettings Settings { get; private set; }
		/// <summary>
		/// 浏览器进程处理
		/// </summary>
		public CfxBrowserProcessHandler ProcessHandler { get; private set; }
	}
}
