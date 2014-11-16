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

        public async Task<string> GetHashAsync()
        {
            return (string)(await ExecuteJavascriptFunction(location.hash, null));
        }

        public async Task<string> GetHostAsync()
        {
            return (string)(await ExecuteJavascriptFunction(location.host, null));
        }

        public async Task<string> GetHostNameAsync()
        {
            return (string)(await ExecuteJavascriptFunction(location.hostname, null));
        }

        public async Task<string> GetHrefAsync()
        {
            return (string)(await ExecuteJavascriptFunction(location.href, null));
        }

        public async Task<string> GetOriginAsync()
        {
            return (string)(await ExecuteJavascriptFunction(location.origin, null));
        }

        public async Task<string> GetPathNameAsync()
        {
            return (string)(await ExecuteJavascriptFunction(location.pathname, null));
        }

        public async Task<string> GetPortAsync()
        {
            return (string)(await ExecuteJavascriptFunction(location.port, null));
        }

        public async Task<string> GetProtocolAsync()
        {
            return (string)(await ExecuteJavascriptFunction(location.protocol, null));
        }

        public async Task<string> GetSearchAsync()
        {
            return (string)(await ExecuteJavascriptFunction(location.search, null));
        }

        internal Uri Url { get; private set; }

        public Task AssignAsync(Uri url)
        {
            return ExecuteJavascriptFunction(location.assign, url.AbsoluteUri);
        }

        public Task ReloadAsync()
        {
            return ExecuteJavascriptFunction(location.reload, null);
        }

        public Task Reload(bool forceGet)
        {
            return ExecuteJavascriptFunction(location.reload, null);
        }

        public Task Replace(Uri url)
        {
            return ExecuteJavascriptFunction(location.replace, url.AbsoluteUri);
        }

        public static implicit operator Location(Uri url)
        {
            return new Location(url);
        }

        private static Task<object> ExecuteJavascriptFunction(object func, object input)
        {
            return ((Func<object, Task<object>>)func)(input);
        }
    }
}
