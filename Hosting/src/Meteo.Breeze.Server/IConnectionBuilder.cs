/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   IPipeBuilder.cs"
 * Date:        7/3/2018 5:08:13 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 5:08:13 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server
{
    public interface IConnectionBuilder
    {
        IConnectionBuilder Use(Func<ConnectionDelegate, ConnectionDelegate> connectionMiddleware);

        ConnectionDelegate Build();
    }
}
