//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：FindEventArgs.cs
// 项目名称：Meteo.Breeze.WebEngine
// 创建时间：2018-07-10 14:34:24
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
	/// 浏览器框架内容查找事件参数类
	/// </summary>
	public class FindEventArgs : EventArgs
	{
		/// <summary>
		/// 浏览器框架内容查找事件参数类
		/// </summary>
		/// <param name="finalUpdate">是否为最后一次查找</param>
		/// <param name="identifier">查找到的匹配 Id</param>
		/// <param name="count">查找到的匹配数量</param>
		public FindEventArgs(bool finalUpdate, int identifier, int count)
		{
			FinalUpdate = finalUpdate;
			Identifier = identifier;
			Count = count;
		}

		/// <summary>
		/// 是否为最后一次查找
		/// </summary>
		public bool FinalUpdate { get; }
		/// <summary>
		/// 当前查找到的匹配 Id
		/// </summary>
		public int Identifier { get; }
		/// <summary>
		/// 当前查找到的匹配数量
		/// </summary>
		public int Count { get; }
	}
}
