//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：WebWindowFactory.cs
// 项目名称：Meteo.Breeze.WebEngine.WinForm
// 创建时间：2018-07-04 17:42:31
// 创建人员：宋杰军
// 负 责 人：北京绘云天科技有限公司
// 参与人员：Breeze 开发组
// ===================================================================
// 修改日期：
// 修改人员：
// 修改内容：
// ===================================================================
using System;

namespace Meteo.Breeze.WebEngine.WinForm
{
	/// <summary>
	/// 浏览器窗口创建工厂类
	/// </summary>
	public class WebWindowFactory
	{
		private static IWebBrowserFactory _factory = new ChromiumFx.WebBrowserFactory();
		/// <summary>
		/// 获得一个浏览器窗口对象
		/// </summary>
		/// <param name="formHandle">当前承载浏览器窗口的 Form 窗体的句柄号(Handle)</param>
		/// <param name="defWidth">宽度</param>
		/// <param name="defHeight">高度</param>
		/// <param name="initialUrl">初始超链接</param>
		/// <returns>浏览器窗口</returns>
		internal static IWebBrowserWindow GetBrowserWindow(IntPtr formHandle, int defWidth, int defHeight, string initialUrl = "about:blank")
		{
			return _factory.CreateBrowser(formHandle, 800, 600);
		}
	}
}
