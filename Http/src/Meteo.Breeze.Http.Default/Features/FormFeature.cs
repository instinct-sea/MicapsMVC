/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   FormFeature.cs"
 * Date:        7/10/2018 3:30:13 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 3:30:13 PM
 
 * ***********************************************/
using Meteo.Breeze.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Breeze.Http.Default.Features
{
    class FormFeature : IFormFeature
    {
        private readonly HttpRequest _request;

        public FormFeature(HttpRequest request) => _request = request;

        public bool HasFormContentType => throw new NotImplementedException();

        public IFormCollection Form { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IFormCollection ReadForm()
        {
            throw new NotImplementedException();
        }

        public Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
