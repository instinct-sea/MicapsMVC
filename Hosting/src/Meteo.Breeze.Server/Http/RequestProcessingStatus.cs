﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Meteo.Breeze.Server.Http
{
    public enum RequestProcessingStatus
    {
        RequestPending,
        ParsingRequestLine,
        ParsingHeaders,
        AppStarted,
        ResponseStarted
    }
}