// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Meteo.Breeze.MVC.Routing.Default.Pool
{
    public abstract class PooledObjectPolicy<T> : IPooledObjectPolicy<T>
    {
        public abstract T Create();

        public abstract bool Return(T obj);
    }
}
