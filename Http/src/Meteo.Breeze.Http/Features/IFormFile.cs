/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   IFormFile.cs"
 * Date:        7/2/2018 1:53:45 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/2/2018 1:53:45 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Breeze.Http.Features
{
    /// <summary>
    /// Represents a file send with request.
    /// </summary>
    public interface IFormFile
    {
        /// <summary>
        /// Gets the content-type header of the uploaded file.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Gets the raw content-disposition header of the uploaded file.
        /// </summary>
        string ContentDisposition { get; }

        /// <summary>
        /// Gets the file length in bytes.
        /// </summary>
        long Length { get; }

        /// <summary>
        /// Gets the form field name from content-disposition header.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the file name from the content-disposition header.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Opens the request stream for  reading content of file.
        /// </summary>
        /// <returns></returns>
        Stream OpenRead();

        /// <summary>
        /// Copies the contents of the uploaded file to the <paramref name="target"/> stream.
        /// </summary>
        /// <param name="target">The stream to copy the file contents to.</param>
        void CopyTo(Stream target);

        /// <summary>
        /// Asynchronously copies the contents of the uploaded file to the <paramref name="target"/> stream.
        /// </summary>
        /// <param name="target">The stream to copy the file contents to.</param>
        /// <param name="cancellationToken"></param>
        Task CopyToAsync(Stream target, CancellationToken cancellationToken = default(CancellationToken));
    }
}
