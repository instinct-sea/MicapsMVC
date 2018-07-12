//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：ITaskDispatcher.cs
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
using System.Threading.Tasks;

namespace Meteo.Breeze.WebEngine
{
	/// <summary>
	/// UI 线程之间的任务投递员
	/// </summary>
	public interface ITaskDispatcher
	{
		/// <summary>
		/// 是否在当前的上下文中
		/// </summary>
		/// <returns>true 是 / false 否</returns>
		bool IsInContext();

		/// <summary>
		/// 任务投递
		/// </summary>
		/// <param name="task">需要投递的任务</param>
		void Dispatch(Action task);

		/// <summary>
		/// 运行指定的任务
		/// <para>异步的</para>
		/// </summary>
		/// <param name="task">需要运行的任务</param>
		/// <returns>异步任务</returns>
		Task RunAsync(Action task);
		/// <summary>
		/// 运行指定的任务
		/// <para>阻塞的</para>
		/// </summary>
		/// <param name="task">需要运行的任务</param>
		void Run(Action task);

		/// <summary>
		/// 运行指定的函数任务
		/// <para>异步的</para>
		/// </summary>
		/// <typeparam name="T">泛型对象</typeparam>
		/// <param name="compute">需要运行的函数任务</param>
		/// <returns>包含泛型对象的异步任务</returns>
		Task<T> EvaluateAsync<T>(Func<T> compute);
		/// <summary>
		/// 运行指定的函数任务
		/// <para>阻塞的</para>
		/// </summary>
		/// <typeparam name="T">泛型对象</typeparam>
		/// <param name="compute">需要运行的函数任务</param>
		/// <returns>泛型对象</returns>
		T Evaluate<T>(Func<T> compute);
	}
}
