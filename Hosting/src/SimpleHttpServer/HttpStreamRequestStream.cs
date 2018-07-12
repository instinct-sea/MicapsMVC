/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HttpStreamRequestStream.cs"
 * Date:        7/5/2018 5:04:46 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/5/2018 5:04:46 PM
 
 * ***********************************************/
using Meteo.Breeze.Server.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server.Simple
{
    class HttpStreamRequestStream : HttpRequestStream
    {
        private readonly Stream _stream;

        public HttpStreamRequestStream(Stream baseStream)
        {
            _stream = baseStream;
        }

        protected override Task CopyToAsyncInternal(Stream destination, CancellationToken cancellationToken)
        {
            return _stream.CopyToAsync(destination, 81920, cancellationToken);
        }

        protected override Task<int> ReadAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _stream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override void Close()
        {
            base.Close();
            _stream.Close();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _stream.Dispose();
        }
    }
}
