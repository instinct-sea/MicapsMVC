/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   StreamExtensions.cs"
 * Date:        7/10/2018 6:05:07 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 6:05:07 PM
 
 * ***********************************************/
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Common.WebUtilities
{
    static class StreamExtensions
    {

        private const int _maxReadBufferSize = 1024 * 4;

        public static Task ConsumeAsync(this Stream stream, CancellationToken cancellationToken)
        {
            return stream.ConsumeAsync(ArrayPool<byte>.Shared, null, cancellationToken);
        }

        public static Task ConsumeAsync(this Stream stream, long? limit, CancellationToken cancellationToken)
        {
            return stream.ConsumeAsync(ArrayPool<byte>.Shared, limit, cancellationToken);
        }

        public static async Task ConsumeAsync(this Stream stream, ArrayPool<byte> bytePool, long? limit, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var buffer = bytePool.Rent(_maxReadBufferSize);
            long total = 0;
            try
            {
                var read = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                while (read > 0)
                {
                    // Not all streams support cancellation directly.
                    cancellationToken.ThrowIfCancellationRequested();
                    if (limit.HasValue && limit.Value - total < read)
                    {
                        throw new InvalidDataException($"The stream exceeded the data limit {limit.Value}.");
                    }
                    total += read;
                    read = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                }
            }
            finally
            {
                bytePool.Return(buffer);
            }
        }

    }
}
