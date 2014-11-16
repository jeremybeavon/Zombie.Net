using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public sealed class History
    {
        private readonly dynamic history;

        internal History(dynamic history)
        {
            this.history = history;
        }

        public Task BackAsync()
        {
            return ExecuteJavascriptFunction(history.back, null);
        }

        public Task ForwardAsync()
        {
            return ExecuteJavascriptFunction(history.forward, null);
        }

        public Task GoAsync(int number)
        {
            return ExecuteJavascriptFunction(history.go, number);
        }

        private static Task<object> ExecuteJavascriptFunction(object func, object input)
        {
            return ((Func<object, Task<object>>)func)(input);
        }
    }
}
