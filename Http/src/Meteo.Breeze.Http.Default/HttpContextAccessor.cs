/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HttpContextAccessor.cs"
 * Date:        7/10/2018 3:38:04 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 3:38:04 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace Meteo.Breeze.Http.Default
{
    class HttpContextAccessor : IHttpContextAccessor
    {
        public static readonly IHttpContextAccessor Null = new NullAccessor();

        class NullAccessor : IHttpContextAccessor
        {
            public HttpContext HttpContext { get => null; set { } }
        }

        private static readonly string LogicalDataKey = "__HttpContext_Current__" + AppDomain.CurrentDomain.Id;
        public HttpContext HttpContext
        {
            get
            {
                var handle = CallContext.LogicalGetData(LogicalDataKey) as ObjectHandle;
                return handle?.Unwrap() as HttpContext;
            }
            set
            {
                CallContext.LogicalSetData(LogicalDataKey, new ObjectHandle(value));
            }
        }

    }
}
