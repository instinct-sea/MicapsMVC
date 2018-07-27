//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：ChromiumFxExtensions.cs
// 项目名称：Meteo.Breeze.WebEngine.ChromiumFx
// 创建时间：2018-07-06 16:25:51
// 创建人员：宋杰军
// 负 责 人：北京绘云天科技有限公司
// 参与人员：Breeze 开发组
// ===================================================================
// 修改日期：
// 修改人员：
// 修改内容：
// ===================================================================
using Meteo.Breeze.Hosting;
using Meteo.Extensions.DependencyInjection;

namespace Meteo.Breeze.WebEngine.ChromiumFx
{
	/// <summary>
	/// ChromiumFx Builder 模式扩展类
	/// </summary>
	public static class ChromiumFxExtensions
	{
		/// <summary>
		/// WEB Engine 将采用基于 CEF 框架的 ChromiumFx 实现
		/// </summary>
		/// <param name="builder">The IWebHostBuilder object.</param>
		/// <returns>IWebHostBuilder object.</returns>
		public static IWebHostBuilder UseChromiumFx(this IWebHostBuilder builder)
		{
			return builder.ConfigureServices(services =>
			{
				services.AddSingleton<IWebBrowserFactory>(new WebBrowserFactory());
			});
		}
	}
}
