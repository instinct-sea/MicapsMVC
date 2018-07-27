//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：IWebBrowserFactory.cs
// 项目名称：Meteo.Breeze.WebEngine
// 创建时间：2018-06-27 14:54:25
// 创建人员：宋杰军
// 负 责 人：北京绘云天科技有限公司
// 参与人员：Breeze 开发组
// ===================================================================
// 修改日期：
// 修改人员：
// 修改内容：
// ===================================================================
using System;

namespace Meteo.Breeze.WebEngine
{
	/// <summary>
	/// 浏览器工厂类
	/// <para>创建事务</para>
	/// <para>操作事务</para>
	/// <para>管理事务</para>
	/// </summary>
	public interface IWebBrowserFactory
	{
		/// <summary>
		/// 创建一个浏览器窗口
		/// </summary>
		/// <param name="formHandle">当前承载浏览器窗口的 Form 窗体的句柄号(Handle)</param>
		/// <param name="defWidth">默认宽度</param>
		/// <param name="defHeight">默认高度</param>
		/// <param name="initialUrl">初始的超链接，默认为空白页</param>
		/// <returns>浏览器窗口常用操作对象</returns>
		IWebBrowserWindow CreateBrowser(IntPtr formHandle, int defWidth, int defHeight, string initialUrl = "about:blank");
	}
}
