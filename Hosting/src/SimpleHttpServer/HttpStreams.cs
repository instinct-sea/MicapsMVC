/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HttpStreams.cs"
 * Date:        7/5/2018 5:20:37 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/5/2018 5:20:37 PM
 
 * ***********************************************/
using Meteo.Breeze.Server.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Server.Simple
{
    class HttpStreams
    {
        private HttpRequestStream _request;
        private HttpResponseStream _response;


        public HttpStreams(Stream request, IHttpResponseControl httpResponseControl)
        {
            _request = new HttpStreamRequestStream(request);
            _response = new HttpResponseStream(httpResponseControl);
        }

        public void Deconstuct(out Stream request, out Stream response)
        {
            request = _request;
            response = _response;
        }

        public void Start()
        {
            _request.StartAcceptingReads();
            _response.StartAcceptingWrites();
        }

        public void Stop()
        {
            _request.StopAcceptingReads();
            _response.StopAcceptingWrites();
        }

        public void Abort(Exception error)
        {
            _request.Abort(error);
            _response.Abort();
        }
    }
}
