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

        public History History
        {
            get { return new History(ExecuteJavascriptFunction(window.history, null)); }
        }

        public Location Location
        {
            get { return new Location(ExecuteJavascriptFunction(window.location, null)); }
            set { ExecuteJavascriptFunction(window.setLocation, value.Url); }
        }

        // document
        // name
        // parent
        // top
        private static object ExecuteJavascriptFunction(object func, object input)
        {
            return Javascript.ExecuteFunction(func, input);
        }
    }
}
