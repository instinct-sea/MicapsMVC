/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HttpConnectionContext.cs"
 * Date:        7/5/2018 10:04:15 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/5/2018 10:04:15 AM
 
 * ***********************************************/
using System;

namespace Meteo.Breeze.Server.Simple
{
    [Flags]
    public enum HttpVersion
    {
        None = 0x0,
        Http1 = 0x1,
        Http2 = 0x2,
        Http1AndHttp2 = Http1 | Http2,
    }
}
