using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    internal static class Javascript
    {
        public static object ExecuteFunction(object func, object input)
        {
            return ExecuteFunction((Func<object, Task<object>>)func, input);
        }

        public static object ExecuteFunction(Func<object, Task<object>> func, object input)
        {
            Task<object> output = func(input);
            output.Wait();
            return output.Result;
        }
    }
}
