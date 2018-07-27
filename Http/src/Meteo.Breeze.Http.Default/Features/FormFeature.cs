﻿/* ***********************************************
 * Copyright (c) 2018 luoshasha. All rights reserved";
 * CLR version: 4.0.30319.42000"
 * File name:   FormFeature.cs"
 * Date:        7/10/2018 3:30:13 PM
 * Author :  sand
 * Email  :  luoshasha@foxmail.com
 * Description: 
	
 * History:  created by sand 7/10/2018 3:30:13 PM
 
 * ***********************************************/
using Meteo.Breeze.Http.Default.Internal;
using Meteo.Breeze.Http.Features;
using Meteo.Breeze.Http.Headers;
using Meteo.Common.Types;
using Meteo.Common.WebUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Meteo.Breeze.Http.Default.Features
{
    class FormFeature : IFormFeature
    {
        private static readonly FormOptions DefaultFormOptions = new FormOptions();

        private readonly HttpRequest _request;
        private readonly FormOptions _options;
        private Task<IFormCollection> _parsedFormTask;
        private IFormCollection _form;

        public FormFeature(IFormCollection form)
        {
            Form = form ?? throw new ArgumentNullException(nameof(form));
        }


        public FormFeature(HttpRequest request)
            : this(request, DefaultFormOptions)
        {
        }

        public FormFeature(HttpRequest request, FormOptions options)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _request = request;
            _options = options;
        }

        public IFormCollection Form
        {
            get { return _form; }
            set
            {
                _parsedFormTask = null;
                _form = value;
            }
        }

        private MediaTypeHeaderValue ContentType
        {
            get
            {
                MediaTypeHeaderValue mt;
                MediaTypeHeaderValue.TryParse(_request.ContentType, out mt);
                return mt;
            }
        }

        public bool HasFormContentType
        {
            get
            {
                // Set directly
                if (Form != null)
                {
                    return true;
                }

                var contentType = ContentType;
                return HasApplicationFormContentType(contentType) || HasMultipartFormContentType(contentType);
            }
        }

        public IFormCollection ReadForm()
        {
            if (Form != null)
            {
                return Form;
            }

            if (!HasFormContentType)
            {
                throw new InvalidOperationException("Incorrect Content-Type: " + _request.ContentType);
            }

            // TODO: Issue #456 Avoid Sync-over-Async http://blogs.msdn.com/b/pfxteam/archive/2012/04/13/10293638.aspx
            // TODO: How do we prevent thread exhaustion?
            return ReadFormAsync().GetAwaiter().GetResult();
        }

        public Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default)
        {
            if (_parsedFormTask == null)
            {
                if (Form != null)
                {
                    _parsedFormTask = Task.FromResult(Form);
                }
                else
                {
                    _parsedFormTask = ReadFormAsyncInternal(cancellationToken);
                }
            }
            return _parsedFormTask;
        }

        private Task<IFormCollection> ReadFormAsyncInternal(CancellationToken cancellationToken)
        {
            // Avoid state machine and task allocation for repeated reads
            if (_parsedFormTask == null)
            {
                if (Form != null)
                {
                    _parsedFormTask = Task.FromResult(Form);
                }
                else
                {
                    _parsedFormTask = InnerReadFormAsync(cancellationToken);
                }
            }
            return _parsedFormTask;
        }

        private async Task<IFormCollection> InnerReadFormAsync(CancellationToken cancellationToken)
        {
            if (!HasFormContentType)
            {
                throw new InvalidOperationException("Incorrect Content-Type: " + _request.ContentType);
            }

            cancellationToken.ThrowIfCancellationRequested();

            //if (_options.BufferBody)
            //{
            //    _request.EnableRewind(_options.MemoryBufferThreshold, _options.BufferBodyLengthLimit);
            //}

            FormCollection formFields = null;
            FormFileCollection files = null;

            // Some of these code paths use StreamReader which does not support cancellation tokens.
            using (cancellationToken.Register((state) => ((HttpContext)state).Abort(), _request.HttpContext))
            {
                var contentType = ContentType;
                // Check the content-type
                if (HasApplicationFormContentType(contentType))
                {
                    var encoding = FilterEncoding(contentType.Encoding);
                    using (var formReader = new FormReader(_request.Body, encoding)
                    {
                        ValueCountLimit = _options.ValueCountLimit,
                        KeyLengthLimit = _options.KeyLengthLimit,
                        ValueLengthLimit = _options.ValueLengthLimit,
                    })
                    {
                        formFields = new FormCollection(await formReader.ReadFormAsync(cancellationToken));
                    }
                }
                else if (HasMultipartFormContentType(contentType))
                {
                    var formAccumulator = new KeyValueAccumulator();

                    var boundary = GetBoundary(contentType, _options.MultipartBoundaryLengthLimit);
                    var multipartReader = new MultipartReader(boundary, _request.Body)
                    {
                        HeadersCountLimit = _options.MultipartHeadersCountLimit,
                        HeadersLengthLimit = _options.MultipartHeadersLengthLimit,
                        BodyLengthLimit = _options.MultipartBodyLengthLimit,
                    };
                    var section = await multipartReader.ReadNextSectionAsync(cancellationToken);
                    while (section != null)
                    {
                        // Parse the content disposition here and pass it further to avoid reparsings
                        ContentDispositionHeaderValue contentDisposition;
                        ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                        if (contentDisposition.IsFileDisposition())
                        {
                            var fileSection = new FileMultipartSection(section, contentDisposition);

                            FormFile file;
                            if (section.Body.CanSeek)
                            {
                                // Find the end
                                await section.Body.ConsumeAsync(cancellationToken);

                                var name = fileSection.Name;
                                var fileName = fileSection.FileName;

                             
                                if (section.BaseStreamOffset.HasValue)
                                {
                                    // Relative reference to buffered request body
                                    file = new StreamFormFile(_request.Body, section.BaseStreamOffset.Value, section.Body.Length, name, fileName);
                                }
                                else
                                {
                                    // Individually buffered file body
                                    file = new StreamFormFile(section.Body, 0, section.Body.Length, name, fileName);
                                }
                                file.Headers = new HeaderDictionary(section.Headers);
                            }
                            else
                            {
                                //// Enable buffering for the file if not already done for the full body
                                //section.EnableRewind(
                                //    _request.HttpContext.Response.RegisterForDispose,
                                //    _options.MemoryBufferThreshold, _options.MultipartBodyLengthLimit);

                                //read all bytes.
                                var bytes = await section.Body.ReadAllBytesAsync(cancellationToken);
                                var name = fileSection.Name;
                                var fileName = fileSection.FileName;

                                if (section.BaseStreamOffset.HasValue)
                                {
                                    // Relative reference to buffered request body
                                    file = new BufferedFormFile(new ArraySegment<byte>(bytes, (int)section.BaseStreamOffset.Value, (int)section.Body.Length), name, fileName);
                                }
                                else
                                {
                                    // Individually buffered file body
                                    file = new BufferedFormFile(new ArraySegment<byte>(bytes, 0, (int)section.Body.Length), name, fileName);
                                }
                                file.Headers = new HeaderDictionary(section.Headers);
                            }

                            if (files == null)
                            {
                                files = new FormFileCollection();
                            }
                            if (files.Count >= _options.ValueCountLimit)
                            {
                                throw new InvalidDataException($"Form value count limit {_options.ValueCountLimit} exceeded.");
                            }
                            files.Add(file);
                        }
                        else if (contentDisposition.IsFormDisposition())
                        {
                            var formDataSection = new FormMultipartSection(section, contentDisposition);

                            // Content-Disposition: form-data; name="key"
                            //
                            // value

                            // Do not limit the key name length here because the mulipart headers length limit is already in effect.
                            var key = formDataSection.Name;
                            var value = await formDataSection.GetValueAsync();

                            formAccumulator.Append(key, value);
                            if (formAccumulator.ValueCount > _options.ValueCountLimit)
                            {
                                throw new InvalidDataException($"Form value count limit {_options.ValueCountLimit} exceeded.");
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.Assert(false, "Unrecognized content-disposition for this section: " + section.ContentDisposition);
                        }

                        section = await multipartReader.ReadNextSectionAsync(cancellationToken);
                    }

                    if (formAccumulator.HasValues)
                    {
                        formFields = new FormCollection(formAccumulator.GetResults(), files);
                    }
                }
            }

            // Rewind so later readers don't have to.
            if (_request.Body.CanSeek)
            {
                _request.Body.Seek(0, SeekOrigin.Begin);
            }

            if (formFields != null)
            {
                Form = formFields;
            }
            else if (files != null)
            {
                Form = new FormCollection(null, files);
            }
            else
            {
                Form = FormCollection.Empty;
            }

            return Form;
        }

        private Encoding FilterEncoding(Encoding encoding)
        {
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed for most cases.
            if (encoding == null || Encoding.UTF7.Equals(encoding))
            {
                return Encoding.UTF8;
            }
            return encoding;
        }

        private bool HasApplicationFormContentType(MediaTypeHeaderValue contentType)
        {
            // Content-Type: application/x-www-form-urlencoded; charset=utf-8
            return contentType != null && contentType.MediaType.Equals("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase);
        }

        private bool HasMultipartFormContentType(MediaTypeHeaderValue contentType)
        {
            // Content-Type: multipart/form-data; boundary=----WebKitFormBoundarymx2fSWqWSd0OxQqq
            return contentType != null && contentType.MediaType.Equals("multipart/form-data", StringComparison.OrdinalIgnoreCase);
        }

        private bool HasFormDataContentDisposition(ContentDispositionHeaderValue contentDisposition)
        {
            // Content-Disposition: form-data; name="key";
            return contentDisposition != null && contentDisposition.DispositionType.Equals("form-data")
                && StringSegment.IsNullOrEmpty(contentDisposition.FileName) && StringSegment.IsNullOrEmpty(contentDisposition.FileNameStar);
        }

        private bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition)
        {
            // Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
            return contentDisposition != null && contentDisposition.DispositionType.Equals("form-data")
                && (!StringSegment.IsNullOrEmpty(contentDisposition.FileName) || !StringSegment.IsNullOrEmpty(contentDisposition.FileNameStar));
        }

        // Content-Type: multipart/form-data; boundary="----WebKitFormBoundarymx2fSWqWSd0OxQqq"
        // The spec says 70 characters is a reasonable limit.
        private static string GetBoundary(Headers.MediaTypeHeaderValue contentType, int lengthLimit)
        {
            var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary);
            if (StringSegment.IsNullOrEmpty(boundary))
            {
                throw new InvalidDataException("Missing content-type boundary.");
            }
            if (boundary.Length > lengthLimit)
            {
                throw new InvalidDataException($"Multipart boundary length limit {lengthLimit} exceeded.");
            }
            return boundary.ToString();
        }
    }
}
