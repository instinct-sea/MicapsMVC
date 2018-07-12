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
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server.Simple
{
    partial class HttpListenerConnection : IFeatureCollection,
        IHttpConnectionFeature,
        IHttpRequestFeature,
        IHttpResponseFeature
    {
        private HashSet<Type> _supportedFeatures = new HashSet<Type>();
        private IHeaderDictionary _requestHeaders;
        private IHeaderDictionary _responseHeaders;

        private void InitializeFeatures()
        {
            var interfaces = this.GetType().GetInterfaces();
            foreach (var @interface in interfaces)
            {
                if (@interface is IFeatureCollection) continue;
                _supportedFeatures.Add(@interface);
            }

            _requestHeaders = new RequestHeaderDictionary(_context.httpContext.Request.Headers, 
                _context.httpContext.Request.ContentLength64);
            _responseHeaders = new ResponseHeaderDictionary(_context.httpContext.Response);

            RemoteAddress = _context.httpContext.Request.RemoteEndPoint.Address;
            RemotePort = _context.httpContext.Request.RemoteEndPoint.Port;
            LocalAddress = _context.httpContext.Request.LocalEndPoint.Address;
            LocalPort = _context.httpContext.Request.LocalEndPoint.Port;
        }

        public bool IsReadOnly => true;

        string IHttpConnectionFeature.ConnectionId
        {
            get => ConnectionId;
            set => throw new NotImplementedException();
        }

        IPAddress IHttpConnectionFeature.RemoteIpAddress
        {
            get => RemoteAddress;
            set => RemoteAddress = value;
        }

        IPAddress IHttpConnectionFeature.LocalIpAddress
        {
            get => LocalAddress;
            set => LocalAddress = value;
        }

        int IHttpConnectionFeature.RemotePort
        {
            get => RemotePort;
            set => RemotePort = value;
        }

        int IHttpConnectionFeature.LocalPort
        {
            get => LocalPort;
            set => LocalPort = value;
        }

        string IHttpRequestFeature.Protocol
        {
            get => _context.httpContext.Request.ProtocolVersion.ToString();
            set => throw new NotImplementedException();
        }

        string IHttpRequestFeature.Scheme
        {
            get => _context.httpContext.Request.Url.Scheme;
            set => throw new NotImplementedException();
        }

        string IHttpRequestFeature.Method
        {
            get => _context.httpContext.Request.HttpMethod;
            set => throw new NotImplementedException();
        }

        string IHttpRequestFeature.PathBase
        {
            get => UriHelper.GetPathBase(_context.httpContext.Request.Url);
            set => throw new NotImplementedException();
        }

        string IHttpRequestFeature.Path
        {
            get => UriHelper.GetPath(_context.httpContext.Request.Url);
            set => throw new NotImplementedException();
        }

        string IHttpRequestFeature.QueryString
        {
            get => UriHelper.GetQueryString(_context.httpContext.Request.QueryString);
            set => throw new NotImplementedException();
        }

        string IHttpRequestFeature.RawTarget
        {
            get => _context.httpContext.Request.Url.OriginalString;
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
            get => _context.httpContext.Response.StatusCode;
            set => _context.httpContext.Response.StatusCode = value;
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
            throw new NotImplementedException();
        }

        void IHttpResponseFeature.OnCompleted(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public TFeature Get<TFeature>() where TFeature : class
        {
            if (_supportedFeatures.Contains(typeof(TFeature)))
                return this as TFeature;
            return null;
        }
    }
}
