/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   CancallationTokenExtensions.cs"
 * Date:        7/5/2018 1:31:56 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/5/2018 1:31:56 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server.Simple
{
    public static class CancellationTokenExtensions
    {
        public static Task AsTask(this CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return Task.FromResult<object>(null);
            }

            var tcs = new TaskCompletionSource<object>();
            token.Register(() => tcs.SetResult(null));
            return tcs.Task;
        }
    }
}
