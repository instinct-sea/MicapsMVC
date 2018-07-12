using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteo.Breeze.Server.Simple
{
    static class UriHelper
    {
        public static string GetPathBase(Uri uri)
        {
            string path = uri.AbsolutePath;
            int index = path.IndexOf('/', 1);
            if (index >= 0)
                return path.Substring(0, index);
            return path;
        }

        public static string GetPath(Uri uri)
        {
            return uri.AbsolutePath;
        }

        public static string GetQueryString(NameValueCollection queryStrings)
        {
            if (queryStrings.Count == 0)
                return string.Empty;

            StringBuilder query = new StringBuilder("?");
            foreach(var name in queryStrings.AllKeys)
            {
                query.Append(name).Append('=').Append(queryStrings.Get(name));
                query.Append('&');
            }
            query.Remove(query.Length - 1, 1);
            return query.ToString();
        }
    }
}
