
using Meteo.Common.Types;
using System.Collections.Generic;
using System.IO;

namespace Meteo.Common.WebUtilities
{
    public class MultipartSection
    {
        public string ContentType
        {
            get
            {
                StringValues values;
                if (Headers.TryGetValue("Content-Type", out values))
                {
                    return values;
                }
                return null;
            }
        }

        public string ContentDisposition
        {
            get
            {
                StringValues values;
                if (Headers.TryGetValue("Content-Disposition", out values))
                {
                    return values;
                }
                return null;
            }
        }

        public IDictionary<string, StringValues> Headers { get; set; }

        public Stream Body { get; set; }

        /// <summary>
        /// The position where the body starts in the total multipart body.
        /// This may not be available if the total multipart body is not seekable.
        /// </summary>
        public long? BaseStreamOffset { get; set; }
    }
}
