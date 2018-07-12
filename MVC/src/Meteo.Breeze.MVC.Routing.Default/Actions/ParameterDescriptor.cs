// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Meteo.Breeze.MVC.Routing.Default.Actions
{
    public class ParameterDescriptor
    {
        public string Name { get; set; }

        public Type ParameterType { get; set; }

        public Meteo.Breeze.MVC.Routing.Default.ModelBinding.BindingInfo BindingInfo { get; set; }
    }
}
