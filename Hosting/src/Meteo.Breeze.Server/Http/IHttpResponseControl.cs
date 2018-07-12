// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server.Http
{
    public interface IHttpResponseControl
    {
        void ProduceContinue();
        Task WriteAsync(byte[] data, int offset, int count, CancellationToken cancellationToken);
        Task FlushAsync(CancellationToken cancellationToken);
    }
}
