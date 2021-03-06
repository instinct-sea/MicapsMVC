﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Meteo.Breeze.MVC.Routing.Default.Pool
{
    public abstract class ObjectPool<T> where T : class
    {
        public abstract T Get();

        public abstract void Return(T obj);
    }
}
