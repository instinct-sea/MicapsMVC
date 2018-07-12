/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   IFormCollection.cs"
 * Date:        7/2/2018 1:54:18 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/2/2018 1:54:18 PM
 
 * ***********************************************/
using System.Collections.Generic;

namespace Meteo.Breeze.Http.Features
{
    /// <summary>
    /// Represents the collection of files sent with the HttpRequest.
    /// </summary>
    public interface IFormFileCollection : IReadOnlyList<IFormFile>
    {
        IFormFile this[string name] { get; }

        IFormFile GetFile(string name);

        IReadOnlyList<IFormFile> GetFiles(string name);
    }
}
