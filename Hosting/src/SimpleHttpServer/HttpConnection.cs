/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   HttpConnection.cs"
 * Date:        7/5/2018 9:55:42 AM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/5/2018 9:55:42 AM
 
 * ***********************************************/
using Meteo.Breeze.Server.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server.Simple
{
    partial class HttpConnection : IHttpResponseControl
    {
        private RequestProcessingStatus _requestProcessStatus;
        private long _responseBytesWritten;
        private readonly HttpConnectionContext _context;
        private Task _requestLifetimeTask;
        private Exception _applicationException;

        public HttpConnection(HttpConnectionContext context)
        {
            _context = context;
            InitializeFeatures();
        }

        public string ConnectionId => _context.ConnectionId;

        public Task StartProcessRequest<TContext>(IHttpApplication<TContext> application)
        {
            return _requestLifetimeTask = ProcessRequestsAsync(application);
        }

        private async Task ProcessRequestsAsync<TContext>(IHttpApplication<TContext> application)
        {
            try
            {
                HttpStreams streams = StartStreams();

                OnRequestProcessingStarting();

                var httpContext = application.CreateContext(this);

                try
                {
                    // Run the application code for this request
                    await application.ProcessRequestAsync(httpContext);
                }
                catch (Exception e)
                {
                    ReportApplicationError(e);
                    //Unexpect exception occurs while processing http request.
                }
                finally
                {
                    // // At this point all user code that needs use to the request or response streams has completed.
                    // Using these streams in the OnCompleted callback is not allowed.
                    StopStreams(streams);

                    //await connection to close.
                    await _context.ConnectionContext.CloseAsync();

                    application.DisposeContext(httpContext, _applicationException);
                }
            }
            catch(BadHttpRequestException e)
            {
                //bad http request exception.
            }
            catch(ConnectionResetException e)
            {
                //connection reset exception.
            }
            catch(Exception ex)
            {
                //other request process exception occurs.
            }
            finally
            {
                this.Reset();
                OnRequestProcessingEnded();
            }
        }

        protected virtual void OnRequestProcessingStarting()
        {

        }

        protected virtual void OnRequestProcessingEnded()
        {

        }

        protected void ReportApplicationError(Exception ex)
        {
            if (_applicationException == null)
            {
                _applicationException = ex;
            }
            else if (_applicationException is AggregateException)
            {
                _applicationException = new AggregateException(_applicationException, ex).Flatten();
            }
            else
            {
                _applicationException = new AggregateException(_applicationException, ex);
            }
        }

        private HttpStreams StartStreams()
        {
            var streams = new HttpStreams(_httpContext.Request.InputStream, this);
            streams.Start();
            streams.Deconstuct(out Stream request, out Stream response);
            RequestBody = request;
            ResponseBody = response;
            return streams;
        }

        private void StopStreams(HttpStreams streams)
        {
            streams?.Stop();
        }

        public void ProduceContinue()
        {

        }

        public Stream RequestBody
        {
            get;
            set;
        }

        public Stream ResponseBody
        {
            get;
            set;
        }

        public bool HasResponseStarted => _requestProcessStatus == RequestProcessingStatus.ResponseStarted;

        public Task StopProcessingNextRequestAsync()
        {
            Abort(null);
            return _requestLifetimeTask;
        }

        public void Abort(ConnectionAbortedException ex)
        {
            _context.ConnectionContext.Abort(abortReason: ex);
        }

        public Task AbortAsync(ConnectionAbortedException e)
        {
            Abort(e);
            return _context.ConnectionContext.CloseAsync();
        }

        public Task FlushAsync(CancellationToken cancellationToken)
        {
            if (!HasResponseStarted)
            {
                var initializeTask = InitializeResponseAsync();
                // If return is Task.CompletedTask no awaiting is required
                if (!initializeTask.IsCompleted)
                {
                    return FlushAsyncAwaited(initializeTask, cancellationToken);
                }
            }
            //如果没有初始任务或者任务已经完成，不使用await 避免上下文切换，提高性能
            return _httpContext.Response.OutputStream.FlushAsync(cancellationToken);
        }

        private async Task FlushAsyncAwaited(Task initializeTask, CancellationToken cancellationToken)
        {
            await initializeTask;
            await _httpContext.Response.OutputStream.FlushAsync(cancellationToken);
        }

        public Task WriteAsync(byte[] data, int offset, int count, CancellationToken cancellationToken)
        {
            var firstWrite = !HasResponseStarted;
            if (firstWrite)
            {
                var startingTask = InitializeResponseAsync();
                if (!startingTask.IsCompleted)
                {
                    return WriteAsyncAwaited(startingTask, data, offset, count, cancellationToken);
                }
            }
            //如果没有初始任务或者任务已经完成，不使用await 避免上下文切换，提高性能
            VerifyAndUpdateWrite(count);
            return _httpContext.Response.OutputStream.WriteAsync(data, offset, count, cancellationToken);
        }

        private async Task WriteAsyncAwaited(Task initializeTask, byte[] data, int offset, int count, CancellationToken cancellationToken)
        {
            await initializeTask;

            VerifyAndUpdateWrite(count);
            await _httpContext.Response.OutputStream.WriteAsync(data, offset, count, cancellationToken);
        }

        private Task InitializeResponseAsync()
        {
            //fire on start.
            //call reponse starting call back.

            _requestProcessStatus = RequestProcessingStatus.ResponseStarted;
            return Task.FromResult<object>(null);
        }


        private void VerifyAndUpdateWrite(int count)
        {
            var responseHeaders = _httpContext.Response.Headers;

            if (responseHeaders != null &&
                _httpContext.Response.ContentEncoding == null &&
                _httpContext.Response.ContentLength64 > 0 &&
                _responseBytesWritten + count > _httpContext.Response.ContentLength64)
            {
                ThrowTooManyBytesWritten(count);
            }
            _responseBytesWritten += count;
        }

        private void ThrowTooManyBytesWritten(int count)
        {
            throw new InvalidOperationException(string.Format("Response bytes written {0} exceeds content length {1}",
                _responseBytesWritten + count,
                _httpContext.Response.ContentLength64));
        }

        public void Reset()
        {
            _responseBytesWritten = 0;
        }
    }
}
