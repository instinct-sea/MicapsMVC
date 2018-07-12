/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   ITransportFactory.cs"
 * Date:        7/3/2018 6:08:41 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 6:08:41 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Server
{
    public interface ITransportFactory
    {
        ITransport Create(IEndPoint endpoint, IConnectionHandler connectionHandler);
    }
}
