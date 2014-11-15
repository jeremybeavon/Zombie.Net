using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public sealed class Location
    {
        private readonly dynamic location;

        internal Location(dynamic location)
        {
            this.location = location;
        }

        internal Location(Uri url)
        {
            Url = url;
        }

        public string Hash
        {
            get { return (string)ExecuteJavascriptFunction(location.hash, null); }
        }

        public string Host
        {
            get { return (string)ExecuteJavascriptFunction(location.host, null); }
        }

        public string HostName
        {
            get { return (string)ExecuteJavascriptFunction(location.hostname, null); }
        }

        public string Href
        {
            get { return (string)ExecuteJavascriptFunction(location.href, null); }
        }

        public string Origin
        {
            get { return (string)ExecuteJavascriptFunction(location.origin, null); }
        }

        public string PathName
        {
            get { return (string)ExecuteJavascriptFunction(location.pathname, null); }
        }

        public string Port
        {
            get { return (string)ExecuteJavascriptFunction(location.port, null); }
        }

        public string Protocol
        {
            get { return (string)ExecuteJavascriptFunction(location.protocol, null); }
        }

        public string Search
        {
            get { return (string)ExecuteJavascriptFunction(location.search, null); }
        }

        internal Uri Url { get; private set; }

        public void Assign(Uri url)
        {
            ExecuteJavascriptFunction(location.assign, url.AbsoluteUri);
        }

        public void Reload()
        {
            ExecuteJavascriptFunction(location.reload, null);
        }

        public void Reload(bool forceGet)
        {
            ExecuteJavascriptFunction(location.reload, null);
        }

        public void Replace(Uri url)
        {
            ExecuteJavascriptFunction(location.replace, url.AbsoluteUri);
        }

        public static implicit operator Location(Uri url)
        {
            return new Location(url);
        }

        private static object ExecuteJavascriptFunction(object func, object input)
        {
            return Javascript.ExecuteFunction(func, input);
        }
    }
}
