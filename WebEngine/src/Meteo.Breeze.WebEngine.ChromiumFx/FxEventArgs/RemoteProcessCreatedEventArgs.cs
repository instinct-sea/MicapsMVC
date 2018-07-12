//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：RemoteProcessCreatedEventArgs.cs
// 项目名称：Meteo.Breeze.WebEngine.ChromiumFx
// 创建时间：2018-07-11 11:15:51
// 创建人员：宋杰军
// 负 责 人：北京绘云天科技有限公司
// 参与人员：Breeze 开发组
// ===================================================================
// 修改日期：
// 修改人员：
// 修改内容：
// ===================================================================
using Chromium.Remote;
using System;

namespace Meteo.Breeze.WebEngine.ChromiumFx
{
	/// <summary>
	/// 远程进程创建事件参数类
	/// </summary>
	public class RemoteProcessCreatedEventArgs : EventArgs
	{
		/// <summary>
		/// 远程进程创建事件参数类
		/// </summary>
		/// <param name="renderProcessHandler">渲染进程处理</param>
        internal RemoteProcessCreatedEventArgs(CfrRenderProcessHandler renderProcessHandler) {
            RenderProcessHandler = renderProcessHandler;
        }

		/// <summary>
		/// 渲染进程处理
		/// </summary>
		public CfrRenderProcessHandler RenderProcessHandler { get; private set; }
	}
}
