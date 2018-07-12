using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meteo.Breeze.Http.Features
{
    public interface ISession
    {
        /// <summary>
        /// Indicate whether the current session has loaded
        /// </summary>
        bool IsAvalilable { get; }

        /// <summary>
        /// A unique identifier for the current session.This is not the same as the session cookie
        /// since the cookie lifetime may not be the same as the session entry lieftime in the data store
        /// </summary>
        string Id { get; }

        void Set(string key, byte[] value);

        void Remove(string key);

        void Clear();
    }
}
