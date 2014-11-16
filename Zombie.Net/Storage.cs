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

        public async Task<int> GetLengthAsync()
        {
            return (int)(await ExecuteJavascriptFunction(storage.length, null));
        }

        public async Task<string> KeyAsync(int index)
        {
            return (string)(await ExecuteJavascriptFunction(storage.key, index));
        }

        public async Task<string> GetItemAsync(string name)
        {
            return (string)(await ExecuteJavascriptFunction(storage.getItem, name));
        }

        public Task SetItemAsync(string name, string value)
        {
            return ExecuteJavascriptFunction(storage.setItem, new { name, value });
        }
        
        public Task ClearAsync()
        {
            return ExecuteJavascriptFunction(storage.clear, null);
        }

        public Task DumpAsync()
        {
            return ExecuteJavascriptFunction(storage.dump, null);
        }

        public Task RemoveItem(string name)
        {
            return ExecuteJavascriptFunction(storage.removeItem, null);
        }

        private static Task<object> ExecuteJavascriptFunction(object func, object input)
        {
            return ((Func<object, Task<object>>)func)(input);
        }
    }
}
