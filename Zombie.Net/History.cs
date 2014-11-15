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

        public void Back()
        {
            ExecuteJavascriptFunction(history.back, null);
        }

        public void Forward()
        {
            ExecuteJavascriptFunction(history.forward, null);
        }

        public void Go(int number)
        {
            ExecuteJavascriptFunction(history.go, number);
        }

        private static object ExecuteJavascriptFunction(object func, object input)
        {
            return Javascript.ExecuteFunction(func, input);
        }
    }
}
