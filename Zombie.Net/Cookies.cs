using System;
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

        public string this[string name]
        {
            get { return (string)ExecuteJavascriptFunction(cookies.get, name); }
            set { ExecuteJavascriptFunction(cookies.set, new { name, value }); }
        }

        public Cookie[] All()
        {
            return (Cookie[])ExecuteJavascriptFunction(cookies.all, null);
        }

        public void Clear()
        {
            ExecuteJavascriptFunction(cookies.clear, null);
        }

        public void Dump()
        {
            ExecuteJavascriptFunction(cookies.dump, null);
        }

        public void Remove(string name)
        {
            ExecuteJavascriptFunction(cookies.remove, name);
        }
        
        private static object ExecuteJavascriptFunction(object func, object input)
        {
            return Javascript.ExecuteFunction(func, input);
        }
    }
}
