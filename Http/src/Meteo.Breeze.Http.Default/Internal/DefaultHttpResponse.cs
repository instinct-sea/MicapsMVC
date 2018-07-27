/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   DefaultHttpResponse.cs"
 * Date:        7/10/2018 2:43:21 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 2:43:21 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meteo.Breeze.Http.Default.Features;
using Meteo.Breeze.Http.Features;
using Meteo.Breeze.Http.Headers;

namespace Meteo.Breeze.Http.Default.Internal
{
    class DefaultHttpResponse : HttpResponse
    {
        private HttpContext _context;

        private IHttpResponseFeature _responseFeature;
        private IResponseCookiesFeature _responseCookiesFeature;

        public DefaultHttpResponse(HttpContext context)
        {
            this.Initialize(context);
        }

        public virtual void Initialize(HttpContext context)
        {
            _responseFeature = context.Features.Get<IHttpResponseFeature>();
            _responseCookiesFeature = context.Features.Get<IResponseCookiesFeature>() ?? new ResponseCookiesFeature(context.Features);
        }

        public virtual void Uninitialize()
        {
            _context = null;
            _responseFeature = null;
            _responseCookiesFeature = null;
        }

        private IHttpResponseFeature HttpResponseFeature => _responseFeature;
        private IResponseCookiesFeature ResponseCookiesFeature => _responseCookiesFeature;

        public override HttpContext HttpContext { get { return _context; } }

        public override int StatusCode
        {
            get { return HttpResponseFeature.StatusCode; }
            set { HttpResponseFeature.StatusCode = value; }
        }

        public override IHeaderDictionary Headers
        {
            get { return HttpResponseFeature.Headers; }
        }

        public override Stream Body
        {
            get { return HttpResponseFeature.Body; }
            set { HttpResponseFeature.Body = value; }
        }

        public override long? ContentLength
        {
            get { return Headers.ContentLength; }
            set { Headers.ContentLength = value; }
        }

        public override string ContentType
        {
            get
            {
                return Headers[HeaderNames.ContentType];
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    HttpResponseFeature.Headers.Remove(HeaderNames.ContentType);
                }
                else
                {
                    HttpResponseFeature.Headers[HeaderNames.ContentType] = value;
                }
            }
        }

        public override IResponseCookies Cookies
        {
            get { return ResponseCookiesFeature.Cookies; }
        }

        public override bool HasStarted
        {
            get { return HttpResponseFeature.HasStarted; }
        }

        public override void OnStarting(Func<object, Task> callback, object state)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            HttpResponseFeature.OnStarting(callback, state);
        }

        public override void OnCompleted(Func<object, Task> callback, object state)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            HttpResponseFeature.OnCompleted(callback, state);
        }

        public override void Redirect(string location, bool permanent)
        {
            if (permanent)
            {
                HttpResponseFeature.StatusCode = 301;
            }
            else
            {
                HttpResponseFeature.StatusCode = 302;
            }

            Headers[HeaderNames.Location] = location;
        }
    }
}
