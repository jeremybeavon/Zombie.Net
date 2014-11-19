using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    [DebuggerDisplay("Element")]
    public sealed class Element
    {
        private readonly dynamic element;

        private Element(dynamic element)
        {
            this.element = element;
        }

        public async Task<string> HtmlAsync()
        {
            return (string)(await ExecuteJavascriptFunction(element.html, null));
        }

        public async Task<string> TextAsync()
        {
            return (string)(await ExecuteJavascriptFunction(element.text, null));
        }

        public async Task<string> ValAsync()
        {
            return (string)(await ExecuteJavascriptFunction(element.val, null));
        }

        internal static Element Create(dynamic element)
        {
            return element == null ? null : new Element(element);
        }

        private static Task<object> ExecuteJavascriptFunction(object func, object input)
        {
            return ((Func<object, Task<object>>)func)(input);
        }
    }
}
