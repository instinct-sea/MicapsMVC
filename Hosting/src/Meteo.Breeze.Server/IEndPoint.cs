/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   IEndPoint.cs"
 * Date:        7/3/2018 6:16:14 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 6:16:14 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Meteo.Breeze.Server
{
    /// <summary>
    /// Contains endpoint information to start channel.
    /// </summary>
    public interface IEndPoint
    {
        IPEndPoint EndPoint
        {
            get;
        }
    }
}
