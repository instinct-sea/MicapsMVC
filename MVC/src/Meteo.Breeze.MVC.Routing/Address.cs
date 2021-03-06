﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Meteo.Breeze.MVC.Routing
{
    public class Address
    {
        public Address()
        {
        }

        public Address(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
