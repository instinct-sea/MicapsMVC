/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   ITransport.cs"
 * Date:        7/3/2018 6:10:25 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 6:10:25 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server
{
    /// <summary>
    /// Transport layer used to communication with client.
    /// </summary>
    public interface ITransport
    {
        /// <summary>
        /// Start to transport to accept connections.
        /// </summary>
        /// <returns></returns>
        Task StartAsync();
        /// <summary>
        /// Stop transport to accept incoming connections.
        /// </summary>
        /// <returns></returns>
        Task StopAsync();

        /// <summary>
        /// Shutdown tranport and release all resources.
        /// </summary>
        /// <returns></returns>
        Task CloseAsync();
    }
}
