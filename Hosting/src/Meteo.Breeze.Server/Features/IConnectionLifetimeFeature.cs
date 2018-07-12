// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading;

namespace Meteo.Breeze.Server.Features
{
    public interface IConnectionLifetimeFeature
    {
        /// <summary>
        /// Signaled when socket finished all read and write opeations.
        /// </summary>
        CancellationToken ConnectionClosed { get; set; }

        void Abort();
    }
}
