/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   ConnectionHandler.cs"
 * Date:        7/3/2018 6:28:29 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 6:28:29 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server.Simple
{
    class ConnectionHandler : IConnectionHandler
    {
        private readonly ConnectionDelegate _application;
        public ConnectionHandler(ConnectionDelegate app)
        {
            _application = app;
        }

        public Task OnConnection(TransportConnection connection)
        {
            connection.ConnectionId = ConnectionIdGenerator.GetNextId();
            return _application(connection);
        }
    }
}
