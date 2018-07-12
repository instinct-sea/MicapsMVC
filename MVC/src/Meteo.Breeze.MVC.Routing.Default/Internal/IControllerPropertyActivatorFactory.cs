// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Meteo.Breeze.MVC.Routing.Default.Controllers;

namespace Meteo.Breeze.MVC.Routing.Default.Internal
{
    public interface IControllerPropertyActivator
    {
        void Activate(ControllerContext context, object controller);

        Action<ControllerContext, object> GetActivatorDelegate(ControllerActionDescriptor actionDescriptor);
    }
}
