/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   IStartup.cs"
 * Date:        7/10/2018 11:40:02 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 11:40:02 AM
 
 * ***********************************************/
using Meteo.Breeze.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Hosting.Self
{
    interface IStartup
    {
        void Configure(IApplicationBuilder app);
    }
}
