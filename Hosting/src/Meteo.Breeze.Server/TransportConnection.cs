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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Meteo.Breeze.Server
{
    public abstract class TransportConnection : ConnectionContext
    {
        private IDictionary<object, object> _items;
        public IPAddress RemoteAddress { get; set; }
        public int RemotePort { get; set; }
        public IPAddress LocalAddress { get; set; }
        public int LocalPort { get; set; }

        public override string ConnectionId { get; set; }

        public override IDictionary<object, object> Items
        {
            get
            {
                // Lazily allocate connection metadata
                return _items ?? (_items = new ConnectionItems());
            }
            set
            {
                _items = value;
            }
        }

        public override void Abort(ConnectionAbortedException abortReason)
        {
            base.Abort(abortReason);
            DoAbort(abortReason);
        }

        protected abstract void DoAbort(ConnectionAbortedException abortReason);
    }
}
