/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   DefaultHttpRequest.cs"
 * Date:        7/10/2018 2:43:08 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 2:43:08 PM
 
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Meteo.Breeze.Http.Default.Features;
using Meteo.Breeze.Http.Features;

namespace Meteo.Breeze.Http.Default.Internal
{
    class DefaultHttpRequest : HttpRequest
    {
        struct FeatureInterfaces
        {
            public IHttpRequestFeature Request;
            public IQueryFeature Query;
            public IFormFeature Form;
            public IRequestCookiesFeature Cookies;
        };

        private FeatureInterfaces _cachedFeatures;

        public DefaultHttpRequest(HttpContext context)
        {
            HttpContext = context;
            Initialize(context);
        }

        private IHttpRequestFeature HttpRequestFeature => _cachedFeatures.Request;

        public virtual void Initialize(HttpContext context)
        {
            _cachedFeatures.Request = context.Features.Get<IHttpRequestFeature>() ?? throw new InvalidOperationException("Context doesn't contains a http request feature.");
            _cachedFeatures.Query = context.Features.Get<IQueryFeature>() ?? InitializeQueryFeature(context);
            _cachedFeatures.Form = context.Features.Get<IFormFeature>() ?? InitializeFormFeature(context);
            _cachedFeatures.Cookies = context.Features.Get<IRequestCookiesFeature>() ?? InitializeCookiesFeature(context);
        }

        public virtual void Uninitialize()
        {
            _cachedFeatures.Request = null;
            _cachedFeatures.Query = null;
            _cachedFeatures.Form = null;
            _cachedFeatures.Cookies = null;
        }

        protected virtual IFormFeature InitializeFormFeature(HttpContext context)
        {
            return new FormFeature(this);
        }

        protected virtual IQueryFeature InitializeQueryFeature(HttpContext context)
        {
            return new QueryFeature(context.Features);
        }

        protected virtual IRequestCookiesFeature InitializeCookiesFeature(HttpContext context)
        {
            return new RequestCookiesFeature(context.Features);
        }

        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default)
        {
            if (_cachedFeatures.Form == null)
                return Task.FromResult<IFormCollection>(null);
            return _cachedFeatures.Form.ReadFormAsync(cancellationToken);
        }

        public override HttpContext HttpContext
        {
            get;
        }

        public override string Method
        {
            get => _cachedFeatures.Request.Method;
            set => _cachedFeatures.Request.Method = value;
        }

        public override string Scheme
        {
            get => _cachedFeatures.Request.Scheme;
            set => _cachedFeatures.Request.Scheme = value;
        }

        public override bool IsHttps
        {
            get { return string.Equals(Constants.Https, Scheme, StringComparison.OrdinalIgnoreCase); }
            set { Scheme = value ? Constants.Https : Constants.Http; }
        }

        public override HostString Host
        {
            get { return HostString.FromUriComponent(Headers["Host"]); }
            set { Headers["Host"] = value.ToUriComponent(); }
        }

        public override PathString PathBase
        {
            get { return new PathString(HttpRequestFeature.PathBase); }
            set { HttpRequestFeature.PathBase = value.Value; }
        }

        public override PathString Path
        {
            get { return new PathString(HttpRequestFeature.Path); }
            set { HttpRequestFeature.Path = value.Value; }
        }

        public override QueryString QueryString
        {
            get { return new QueryString(HttpRequestFeature.QueryString); }
            set { HttpRequestFeature.QueryString = value.Value; }
        }

        public override IQueryCollection Query
        {
            get => _cachedFeatures.Query.Query;
            set => _cachedFeatures.Query.Query = value;
        }

        public override string Protocol
        {
            get => HttpRequestFeature.Protocol;
            set => HttpRequestFeature.Protocol = value;
        }

        public override IHeaderDictionary Headers => HttpRequestFeature.Headers;

        public override IRequestCookieCollection Cookies
        {
            get => _cachedFeatures.Cookies.Cookies;
            set => _cachedFeatures.Cookies.Cookies = value;
        }

        public override long? ContentLength
        {
            get => Headers.ContentLength;
            set => Headers.ContentLength = value;
        }

        public override string ContentType
        {
            get;
            set;
        }

        public override Stream Body
        {
            get => HttpRequestFeature.Body;
            set => HttpRequestFeature.Body = value;
        }

        public override bool HasFormContentType => _cachedFeatures.Form.HasFormContentType;

        public override IFormCollection Form
        {
            get => _cachedFeatures.Form.ReadForm();
            set => _cachedFeatures.Form.Form = value;
        }
    }
}
