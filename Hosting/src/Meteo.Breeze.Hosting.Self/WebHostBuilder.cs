/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   WebHostBuilder.cs"
 * Date:        7/3/2018 3:45:25 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 3:45:25 PM
 
 * ***********************************************/
using Meteo.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Hosting.Self
{
    public class WebHostBuilder : IWebHostBuilder
    {
        private WebHostBuilderContext _context;
        private Dictionary<string, string> _settings = new Dictionary<string, string>();
        private List<Action<WebHostBuilderContext, IServiceCollection>> _configServicesDelegates = new List<Action<WebHostBuilderContext, IServiceCollection>>();

        public WebHostBuilder()
        {
            _context = new WebHostBuilderContext();
        }

        class ServiceCollection : List<ServiceDescriptor>, IServiceCollection
        {

        }

        public IWebHost Build()
        {
            ServiceCollection services = new ServiceCollection();
            foreach (var configService in _configServicesDelegates)
            {
                configService.Invoke(_context, services);
            }
            _configServicesDelegates = null;

            return new WebHost(services);
        }

        public IWebHostBuilder ConfigureServices(Action<IServiceCollection> configureServices)
        {
            ConfigureServices((context, services) => configureServices(services));
            return this;
        }

        public IWebHostBuilder ConfigureServices(Action<WebHostBuilderContext, IServiceCollection> configureServices)
        {
            _configServicesDelegates.Add(configureServices);
            return this;
        }

        public string GetSetting(string key)
        {
            _settings.TryGetValue(key, out string value);
            return value;
        }

        public IWebHostBuilder UseSetting(string key, string value)
        {
            _settings[key] = value;
            return this;
        }
    }
}
