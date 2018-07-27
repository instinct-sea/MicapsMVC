/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HttpListenConnection.cs"
 * Date:        7/3/2018 5:53:57 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/3/2018 5:53:57 PM
 
 * ***********************************************/
using Meteo.Breeze.Http.Features;
using Meteo.Breeze.Server.Features;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server.Simple
{
    partial class HttpConnection : IFeatureCollection,
        IHttpConnectionFeature,
        IHttpRequestFeature,
        IHttpRequestLifetimeFeature,
        IHttpResponseFeature,
        IServiceProvidersFeature
    {
        private HashSet<Type> _supportedFeatures = new HashSet<Type>();
        private IHeaderDictionary _requestHeaders;
        private IHeaderDictionary _responseHeaders;
        private HttpListenerContext _httpContext;
        private IConnectionLifetimeFeature _connectionFeature;

        private void InitializeFeatures()
        {
            _httpContext = _context.ConnectionFeatures.Get<HttpListenerContext>();
            _connectionFeature = _context.ConnectionFeatures.Get<IConnectionLifetimeFeature>();

            var interfaces = this.GetType().GetInterfaces();
            foreach (var @interface in interfaces)
            {
                if (@interface is IFeatureCollection) continue;
                _supportedFeatures.Add(@interface);
            }
            _requestHeaders = new RequestHeaderDictionary(_httpContext.Request.Headers, 
                _httpContext.Request.ContentLength64);
            _responseHeaders = new ResponseHeaderDictionary(_httpContext.Response);
        }

        public bool IsReadOnly => true;

        string IHttpConnectionFeature.ConnectionId
        {
            get => _context.ConnectionId;
            set => throw new NotImplementedException();
        }

        IPAddress IHttpConnectionFeature.RemoteAddress
        {
            get => _context.RemoteEndPoint.Address;
            set => throw new NotImplementedException();
        }

        IPAddress IHttpConnectionFeature.LocalAddress
        {
            get => _context.LocalEndPoint.Address;
            set => throw new NotImplementedException();
        }

        int IHttpConnectionFeature.RemotePort
        {
            get => _context.RemoteEndPoint.Port;
            set => throw new NotImplementedException();
        }

        int IHttpConnectionFeature.LocalPort
        {
            get => _context.LocalEndPoint.Port;
            set => throw new NotImplementedException();
        }

        string IHttpRequestFeature.Protocol
        {
            get => _httpContext.Request.ProtocolVersion.ToString();
            set => throw new NotImplementedException();
        }

        string IHttpRequestFeature.Scheme
        {
            get => _httpContext.Request.Url.Scheme;
            set => throw new NotImplementedException();
        }

        string IHttpRequestFeature.Method
        {
            get => _httpContext.Request.HttpMethod;
            set => throw new NotImplementedException();
        }

        string IHttpRequestFeature.PathBase
        {
            get => UriHelper.GetPathBase(_httpContext.Request.Url);
            set => throw new NotImplementedException();
        }

        string IHttpRequestFeature.Path
        {
            get => UriHelper.GetPath(_httpContext.Request.Url);
            set => throw new NotImplementedException();
        }

        string IHttpRequestFeature.QueryString
        {
            get => UriHelper.GetQueryString(_httpContext.Request.QueryString);
            set => throw new NotImplementedException();
        }

        string IHttpRequestFeature.RawTarget
        {
            get => _httpContext.Request.Url.OriginalString;
            set => throw new NotImplementedException();
        }

        IHeaderDictionary IHttpRequestFeature.Headers
        {
            get => _requestHeaders;
            set => throw new NotImplementedException();
        }

        Stream IHttpRequestFeature.Body
        {
            get => RequestBody;
            set => throw new NotImplementedException();
        }

        int IHttpResponseFeature.StatusCode
        {
            get => _httpContext.Response.StatusCode;
            set => _httpContext.Response.StatusCode = value;
        }

        string IHttpResponseFeature.ReasonPhrase
        {
            get;
            set;
        }

        IHeaderDictionary IHttpResponseFeature.Headers
        {
            get => _responseHeaders;
            set => throw new NotImplementedException();
        }

        Stream IHttpResponseFeature.Body
        {
            get => ResponseBody;
            set => throw new NotImplementedException();
        }

        bool IHttpResponseFeature.HasStarted => HasResponseStarted;

        public CancellationToken RequestAborted
        {
            get => _connectionFeature.ConnectionClosed;
            set => throw new NotImplementedException();
        }

        public IServiceProvider RequestServices
        {
            get => _context.ServerContext.ApplicationServices;
            set { }
        }

        public object this[Type key]
        {
            get
            {
                if (_supportedFeatures.Contains(key))
                    return this;
                return null;
            }
            set => throw new NotImplementedException();
        }

        public void Set<TFeature>(TFeature feature) where TFeature : class
        {
            throw new InvalidOperationException();
        }

        private IEnumerable<KeyValuePair<Type, object>> FeatureEnumerable()
        {
            foreach (var type in _supportedFeatures)
            {
                yield return new KeyValuePair<Type, object>(type, this);
            }
        }

        public IEnumerator<KeyValuePair<Type, object>> GetEnumerator()
        {
            return FeatureEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return FeatureEnumerable().GetEnumerator();
        }

        void IHttpResponseFeature.OnStarting(Func<object, Task> callback, object state)
        {
            OnStarting(callback, state);
        }

        void IHttpResponseFeature.OnCompleted(Func<object, Task> callback, object state)
        {
            OnCompleted(callback, state);
        }

        public TFeature Get<TFeature>() where TFeature : class
        {
            if (_supportedFeatures.Contains(typeof(TFeature)))
                return this as TFeature;
            return null;
        }

        void IHttpRequestLifetimeFeature.Abort()
        {
            this.Abort(null);
        }
    }
}
