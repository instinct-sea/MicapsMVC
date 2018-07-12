/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   ChannelConnection.cs"
 * Date:        7/3/2018 6:15:33 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 6:15:33 PM
 
 * ***********************************************/
using Meteo.Breeze.Http.Features;
using Meteo.Breeze.Server.Features;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server
{
    public abstract class ConnectionContext
    {
        public abstract string ConnectionId { get; set; }

        public abstract IFeatureCollection Features { get; }

        public abstract IDictionary<object, object> Items { get; set; }

        public virtual void Abort(ConnectionAbortedException abortReason)
        {
            Features.Get<IConnectionLifetimeFeature>()?.Abort();
        }

        /// <summary>
        /// Close connection gracefully. wait all pending io operations finish before close.
        /// </summary>
        /// <returns></returns>
        public abstract Task CloseAsync();
    }
}
