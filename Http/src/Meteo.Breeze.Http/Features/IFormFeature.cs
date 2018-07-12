/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   IFormFeature.cs"
 * Date:        7/2/2018 1:54:05 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/2/2018 1:54:05 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Breeze.Http.Features
{
    /// <summary>
    /// Indicates if the request has a supported form content-type.
    /// </summary>
    public interface IFormFeature
    {
        /// <summary>
        /// Indicates if the request has a supported form content-type.
        /// </summary>
        bool HasFormContentType { get; }

        /// <summary>
        /// The parsed form, if any.
        /// </summary>
        IFormCollection Form { get; set; }

        /// <summary>
        /// Parses the request body as a form.
        /// </summary>
        /// <returns></returns>
        IFormCollection ReadForm();

        /// <summary>
        /// Parses the request body as a form.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken);
    }
}
