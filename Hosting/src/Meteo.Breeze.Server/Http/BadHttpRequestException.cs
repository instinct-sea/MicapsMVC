// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Meteo.Breeze.Http;
using Meteo.Common.Types;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace Meteo.Breeze.Server.Simple
{
    public sealed class BadHttpRequestException : IOException
    {
        private BadHttpRequestException(string message, int statusCode, RequestRejectionReason reason)
            : this(message, statusCode, reason, null)
        {

        }

        private BadHttpRequestException(string message, int statusCode, RequestRejectionReason reason, string requiredMethod)
            : base(message)
        {
            StatusCode = statusCode;
            Reason = reason;
            AllowedHeader = requiredMethod;
        }

        internal int StatusCode { get; }

        internal StringValues AllowedHeader { get; }

        internal RequestRejectionReason Reason { get; }

        internal static void Throw(RequestRejectionReason reason)
        {
            throw GetException(reason);
        }

        public static void Throw(RequestRejectionReason reason, string method)
            => throw GetException(reason, method.ToUpperInvariant());

        private static string GetRejectionReasonMessage(RequestRejectionReason reason)
        {
            return reason.ToString();
        }

        private static string GetRejectionReasonMessage(RequestRejectionReason reason, string detail)
        {
            return $"{reason.ToString()} : {detail}";
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static BadHttpRequestException GetException(RequestRejectionReason reason)
        {
            BadHttpRequestException ex;
            switch (reason)
            {
                case RequestRejectionReason.InvalidRequestHeadersNoCRLF:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.InvalidRequestLine:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.MalformedRequestInvalidHeaders:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.MultipleContentLengths:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.UnexpectedEndOfRequestContent:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.BadChunkSuffix:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.BadChunkSizeData:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.ChunkedRequestIncomplete:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.InvalidCharactersInHeaderName:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.RequestLineTooLong:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status414UriTooLong, reason);
                    break;
                case RequestRejectionReason.HeadersExceedMaxTotalSize:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status431RequestHeaderFieldsTooLarge, reason);
                    break;
                case RequestRejectionReason.TooManyHeaders:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status431RequestHeaderFieldsTooLarge, reason);
                    break;
                case RequestRejectionReason.RequestBodyTooLarge:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status413PayloadTooLarge, reason);
                    break;
                case RequestRejectionReason.RequestHeadersTimeout:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status408RequestTimeout, reason);
                    break;
                case RequestRejectionReason.RequestBodyTimeout:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status408RequestTimeout, reason);
                    break;
                case RequestRejectionReason.OptionsMethodRequired:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status405MethodNotAllowed, reason, HttpMethods.Options);
                    break;
                case RequestRejectionReason.ConnectMethodRequired:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status405MethodNotAllowed, reason, HttpMethods.Connect);
                    break;
                case RequestRejectionReason.MissingHostHeader:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.MultipleHostHeaders:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.InvalidHostHeader:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.UpgradeRequestCannotHavePayload:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status400BadRequest, reason);
                    break;
                default:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason), StatusCodes.Status400BadRequest, reason);
                    break;
            }
            return ex;
        }

        internal static void Throw(RequestRejectionReason reason, in StringValues detail)
        {
            throw GetException(reason, detail.ToString());
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static BadHttpRequestException GetException(RequestRejectionReason reason, string detail)
        {
            BadHttpRequestException ex;
            switch (reason)
            {
                case RequestRejectionReason.InvalidRequestLine:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason, detail), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.InvalidRequestTarget:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason, detail), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.InvalidRequestHeader:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason, detail), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.InvalidContentLength:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason, detail), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.UnrecognizedHTTPVersion:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason, detail), StatusCodes.Status505HttpVersionNotsupported, reason);
                    break;
                case RequestRejectionReason.FinalTransferCodingNotChunked:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason, detail), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.LengthRequired:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason, detail), StatusCodes.Status411LengthRequired, reason);
                    break;
                case RequestRejectionReason.LengthRequiredHttp10:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason, detail), StatusCodes.Status400BadRequest, reason);
                    break;
                case RequestRejectionReason.InvalidHostHeader:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason, detail), StatusCodes.Status400BadRequest, reason);
                    break;
                default:
                    ex = new BadHttpRequestException(GetRejectionReasonMessage(reason, detail), StatusCodes.Status400BadRequest, reason);
                    break;
            }
            return ex;
        }
    }
}