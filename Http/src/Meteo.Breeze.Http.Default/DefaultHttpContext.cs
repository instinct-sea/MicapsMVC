/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   DefaultHttpContext.cs"
 * Date:        7/10/2018 2:30:40 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 2:30:40 PM
 
 * ***********************************************/
using Meteo.Breeze.Http.Default.Features;
using Meteo.Breeze.Http.Default.Internal;
using Meteo.Breeze.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace Meteo.Breeze.Http.Default
{
    public class DefaultHttpContext : HttpContext
    {
        private HttpRequest _request;
        private HttpResponse _response;

        private ConnectionInfo _connection;

        private IFeatureCollection _features;



        public DefaultHttpContext(IFeatureCollection features)
        {
            _features = features;
            Initialize(features);
        }

        private IItemsFeature ItemsFeature { get; set; }

        private IServiceProvidersFeature ServiceProvidersFeature { get; set; }

        private IHttpRequestLifetimeFeature LifetimeFeature { get; set; }

        private ISessionFeature SessionFeature { get; set; }

        public virtual void Initialize(IFeatureCollection features)
        {
            ItemsFeature = features.Get<IItemsFeature>() ?? new ItemsFeature();
            ServiceProvidersFeature = features.Get<IServiceProvidersFeature>();
            LifetimeFeature = features.Get<IHttpRequestLifetimeFeature>();
            SessionFeature = features.Get<ISessionFeature>();

            _request = InitializeHttpRequest();
            _response = InitializeHttpResponse();
        }

        public virtual void Uninitialize()
        {
            _features = null;

            ItemsFeature = null;
            ServiceProvidersFeature = null;
            LifetimeFeature = null;
            SessionFeature = null;

            if (_request != null)
            {
                UninitializeHttpRequest(_request);
                _request = null;
            }
            if (_response != null)
            {
                UninitializeHttpResponse(_response);
                _response = null;
            }
          
            if (_connection != null)
            {
                UninitializeConnectionInfo(_connection);
                _connection = null;
            }
        }

        public override IFeatureCollection Features => _features;

        public override HttpRequest Request => _request;

        public override HttpResponse Response => _response;

        public override CancellationToken RequestAborted
        {
            get => LifetimeFeature?.RequestAborted ?? CancellationToken.None;
            set => LifetimeFeature.RequestAborted = value;
        }

        public override IServiceProvider RequestServices
        {
            get => ServiceProvidersFeature?.RequestServices;
            set => ServiceProvidersFeature.RequestServices = value;
        }

        public override ISession Session
        {
            get
            {
                var feature = SessionFeature;
                if (feature == null)
                {
                    throw new InvalidOperationException("Session has not been configured for this application " +
                        "or request.");
                }
                return feature.Session;
            }
            set
            {
                SessionFeature.Session = value;
            }
        }

        public override ConnectionInfo Connection => _connection ?? (_connection = InitializeConnectionInfo());

        public override ClaimsPrincipal User
        {
            get;
            set;
        }

        public override IDictionary<object, object> Items
        {
            get { return ItemsFeature.Items; }
            set { ItemsFeature.Items = value; }
        }

        public override void Abort()
        {
            LifetimeFeature?.Abort();
        }

        protected virtual HttpRequest InitializeHttpRequest() => new DefaultHttpRequest(this);
        protected virtual void UninitializeHttpRequest(HttpRequest instance) { }

        protected virtual HttpResponse InitializeHttpResponse() => new DefaultHttpResponse(this);
        protected virtual void UninitializeHttpResponse(HttpResponse instance) { }

        protected virtual ConnectionInfo InitializeConnectionInfo() => new DefaultConnectionInfo(Features);
        protected virtual void UninitializeConnectionInfo(ConnectionInfo instance) { }
    }
}
