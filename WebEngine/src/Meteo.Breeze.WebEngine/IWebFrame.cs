//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：IWebFrame.cs
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
	/// 浏览器内部框架常用操作接口类
	/// </summary>
	public interface IWebFrame
	{
		/// <summary>
		/// 执行 JavaScript 脚本
		/// </summary>
		/// <param name="javascriptString">JavaScript 脚本字符串</param>
		void ExecuteJavaScript(string javascriptString);
		/// <summary>
		/// 加载指定的字符串文本内容并展示
		/// </summary>
		/// <param name="stringValue">字符串文本内容</param>
		/// <param name="urlString">URL超链接</param>
		void LoadString(string stringValue, string urlString);
		/// <summary>
		/// 加载指定的URL超链接并展示
		/// </summary>
		/// <param name="urlString">URL超链接</param>
		void LoadURL(string urlString);
		/// <summary>
		/// 显示当前框架的 HTML 源代码
		/// </summary>
		void ViewSource();
	}
}
