/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   IWebHostBuilder.cs"
 * Date:        7/2/2018 5:25:20 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/2/2018 5:25:20 PM
 
 * ***********************************************/
using Meteo.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Hosting
{
    public interface IWebHostBuilder
    {
        /// <summary>
        /// Builds an <see cref="IWebHost"/> which hosts a web application.
        /// </summary>
        IWebHost Build();

        /// <summary>
        /// Adds a delegate for configuring additional services for the host or web application. This may be called
        /// multiple times.
        /// </summary>
        /// <param name="configureServices">A delegate for configuring the <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        IWebHostBuilder ConfigureServices(Action<IServiceCollection> configureServices);

        /// <summary>
        /// Adds a delegate for configuring additional services for the host or web application. This may be called
        /// multiple times.
        /// </summary>
        /// <param name="configureServices">A delegate for configuring the <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IWebHostBuilder"/>.</returns>
        IWebHostBuilder ConfigureServices(Action<WebHostBuilderContext, IServiceCollection> configureServices);

        /// <summary>
        /// Add setting to WebHostBuilder's configuration.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IWebHostBuilder UseSetting(string key, string value);

        /// <summary>
        /// Get the setting value from configuration.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        String GetSetting(string key);
    }
}
