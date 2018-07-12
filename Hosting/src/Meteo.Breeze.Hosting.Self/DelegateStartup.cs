/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   DelegateStartup.cs"
 * Date:        7/10/2018 11:42:01 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 11:42:01 AM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Meteo.Breeze.Http;

namespace Meteo.Breeze.Hosting.Self
{
    class DelegateStartup : IStartup
    {
        private Action<IApplicationBuilder> _configure;

        public DelegateStartup(Action<IApplicationBuilder> configure)
        {
            _configure = configure;
        }

        public void Configure(IApplicationBuilder app) => _configure(app);
    }
}
