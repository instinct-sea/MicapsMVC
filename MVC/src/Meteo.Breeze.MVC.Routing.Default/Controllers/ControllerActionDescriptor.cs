// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Meteo.Breeze.MVC.Routing.Default.Actions;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace Meteo.Breeze.MVC.Routing.Default.Controllers
{
    [DebuggerDisplay("{DisplayName}")]
    public class ControllerActionDescriptor : ActionDescriptor
    {
        public string ControllerName { get; set; }

        public virtual string ActionName { get; set; }

        public MethodInfo MethodInfo { get; set; }

        public TypeInfo ControllerTypeInfo { get; set; }

        public override string DisplayName
        {
            get
            {
                if (base.DisplayName == null && ControllerTypeInfo != null && MethodInfo != null)
                {
                    base.DisplayName = string.Format(
                        CultureInfo.InvariantCulture,
                        "{0}.{1} ({2})",
                        TypeNameHelper.GetTypeDisplayName(ControllerTypeInfo),
                        MethodInfo.Name,
                        ControllerTypeInfo.Assembly.GetName().Name);
                }

                return base.DisplayName;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                base.DisplayName = value;
            }
        }
    }
}
