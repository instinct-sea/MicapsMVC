//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：IWebBrowserWindow.cs
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
	/// 浏览器窗口常用操作接口类
	/// </summary>
	public interface IWebBrowserWindow
	{
		/// <summary>
		/// 浏览器框架内容查找事件
		/// </summary>
		event EventHandler<FindEventArgs> FindHandler;

		/// <summary>
		/// 浏览器是否创建
		/// </summary>
		bool IsBrowserCreated { get; }
		/// <summary>
		/// 浏览器 Win32 窗口句柄号
		/// </summary>
		IntPtr WindowHandle { get; }
		/// <summary>
		/// 浏览器
		/// </summary>
		IWebBrowser Browser { get; }

		#region Bworser methods

		/// <summary>
		/// 关闭浏览器
		/// </summary>
		/// <param name="isPromptUser">是否提示用户即将关闭浏览器</param>
		void CloseBrowser(bool isPromptUser);
		/// <summary>
		/// 关闭浏览器
		/// </summary>
		void TryCloseBrowser();

		#endregion

		#region DevTools methods

		/// <summary>
		/// 显示开发者调试工具
		/// </summary>
		void ShowDevTools();
		/// <summary>
		/// 关闭开发者调试工具
		/// </summary>
		void CloseDevTools();

		#endregion

		#region Basic methods

		/// <summary>
		/// 窗口大小发生变化时
		/// </summary>
		/// <param name="width">宽度</param>
		/// <param name="height">高度</param>
		void OnResize(int width, int height);
		/// <summary>
		/// 取消当前正在进行的所有搜索
		/// </summary>
		/// <param name="clearSelection">是否清空所有选择</param>
		void StopFinding(bool clearSelection);
		/// <summary>
		/// 浏览器框架内容查找
		/// </summary>
		/// <param name="identifier">查找ID</param>
		/// <param name="searchText">要查找的内容</param>
		/// <param name="forward">是否查找上一个</param>
		/// <param name="matchCase">是否区分大小写</param>
		/// <param name="findNext">是否查找下一个</param>
		void Find(int identifier, string searchText, bool forward, bool matchCase, bool findNext);

		#endregion

	}
}
