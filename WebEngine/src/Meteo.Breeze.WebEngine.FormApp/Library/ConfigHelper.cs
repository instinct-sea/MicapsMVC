//==================================================================== 
//**   晨曦小竹软件企业集团有限公司
//====================================================================
//**   Copyright © DawnXZ.com 2016 -- QQ：6808240 -- 请保留此注释
//====================================================================
// 文件名称：ConfigHelper.cs
// 项目名称：晨曦通用应用程序模板
// 创建时间：2016-04-25 16:39:16
// 创建人员：宋杰军
// 负 责 人：宋杰军
// 参与人员：宋杰军
// ===================================================================
// 修改日期：
// 修改人员：
// 修改内容：
// ===================================================================
using System;
using System.Configuration;
using DawnXZ.DawnUtility;

namespace Meteo.Breeze.WebEngine.FormApp.Library
{
	/// <summary>
	/// 配置参数帮助类
	/// </summary>
	public class ConfigHelper
	{
		/// <summary>
		/// 应用名称
		/// </summary>
		public static string AppName
		{
			get
			{
				var result = ConfigurationManager.AppSettings["AppName"];
				return CryptoHelper.Decrypt(result);
			}
		}
		/// <summary>
		/// 应用版本号
		/// </summary>
		public static string AppVersion
		{
			get
			{
				var result = ConfigurationManager.AppSettings["AppVersion"];
				return CryptoHelper.Decrypt(result);
			}
		}
	}
}
