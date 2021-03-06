﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public sealed class Cookies
    {
        private readonly dynamic cookies;

        internal Cookies(dynamic cookies)
        {
            this.cookies = cookies;
        }

        public async Task<string> GetAsync(string name)
        {
            return (string)(await ExecuteJavascriptFunction(cookies.get, name));
        }

        public Task SetAsync(string name, string value)
        {
            return ExecuteJavascriptFunction(cookies.set, new { name, value });
        }

        public Task ClearAsync()
        {
            return ExecuteJavascriptFunction(cookies.clear, null);
        }

        public Task DumpAsync()
        {
            return ExecuteJavascriptFunction(cookies.dump, null);
        }

        public Task RemoveAsync(string name)
        {
            return ExecuteJavascriptFunction(cookies.remove, name);
        }

        public async Task<string> SerializeAsync(Uri url)
        {
            var data = new
            {
                domain = url.Host
            };
            return (string)(await ExecuteJavascriptFunction(cookies.serialize, data));
        }
        
        private static Task<object> ExecuteJavascriptFunction(object func, object input)
        {
            return ((Func<object, Task<object>>)func)(input);
        }
    }
}
