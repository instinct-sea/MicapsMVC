//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：WebEngineException.cs
// 项目名称：Meteo.Breeze.WebEngine
// 创建时间：2018-07-10 17:35:40
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
	/// Web Engine Exception
	/// </summary>
	public class WebEngineException : Exception
	{
		/// <summary>
		/// Web Engine Exception
		/// </summary>
		public WebEngineException(string message) : base(message) {; }

	}
}
