using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public sealed class Storage
    {
        private readonly dynamic storage;

        internal Storage(dynamic storage)
        {
            this.storage = storage;
        }

        public int Length
        {
            get { return (int)ExecuteJavascriptFunction(storage.length, null); }
        }

        public string this[int index]
        {
            get { return (string)ExecuteJavascriptFunction(storage.key, index); }
        }

        public string this[string name]
        {
            get { return (string)ExecuteJavascriptFunction(storage.getItem, name); }
            set { ExecuteJavascriptFunction(storage.setItem, new { name, value }); }
        }

        public void Clear()
        {
            ExecuteJavascriptFunction(storage.clear, null);
        }

        public void Dump()
        {
            ExecuteJavascriptFunction(storage.dump, null);
        }

        public void RemoveItem(string name)
        {
            ExecuteJavascriptFunction(storage.removeItem, null);
        }

        private static object ExecuteJavascriptFunction(object func, object input)
        {
            return Javascript.ExecuteFunction(func, input);
        }
    }
}
