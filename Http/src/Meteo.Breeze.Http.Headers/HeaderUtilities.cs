/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HeaderUtilities.cs"
 * Date:        7/11/2018 1:17:00 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/11/2018 1:17:00 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Http.Headers
{
    public static class HeaderUtilities
    {
        internal static void ThrowIfReadOnly(bool isReadOnly)
        {
            if (isReadOnly)
            {
                throw new InvalidOperationException("The object cannot be modified because it is read-only.");
            }
        }
    }
}
