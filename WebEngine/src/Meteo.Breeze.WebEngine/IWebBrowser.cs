//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：IWebBrowser.cs
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

namespace Meteo.Breeze.WebEngine
{
	/// <summary>
	/// 浏览器常用操作接口类
	/// </summary>
	public interface IWebBrowser : IWebFrame
	{
		/// <summary>
		/// 向后导航
		/// </summary>
		void GoBack();
		/// <summary>
		/// 向前导航
		/// </summary>
		void GoForward();
		/// <summary>
		/// 重新加载当前页面
		/// </summary>
		void Reload();
		/// <summary>
		/// 重新加载当前页面，并忽略任何缓存数据
		/// </summary>
		void ReloadIgnoreCache();
	}
}
