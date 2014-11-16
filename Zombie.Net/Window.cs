using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public sealed class Window
    {
        private readonly dynamic window;

        internal Window(dynamic window)
        {
            this.window = window;
        }

        public async Task<History> GetHistoryAsync()
        {
            return new History(await ExecuteJavascriptFunction(window.history, null));
        }

        public async Task<Location> GetLocationAsync()
        {
            return new Location(await ExecuteJavascriptFunction(window.location, null));
        }

        public Task SetLocationAsync(Location location)
        {
            return ExecuteJavascriptFunction(window.setLocation, location.Url);
        }

        // document
        // name
        // parent
        // top
        private static Task<object> ExecuteJavascriptFunction(object func, object input)
        {
            return ((Func<object, Task<object>>)func)(input);
        }
    }
}
