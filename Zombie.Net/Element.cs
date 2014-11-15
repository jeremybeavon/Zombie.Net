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

        internal static Element Create(dynamic element)
        {
            return element == null ? null : new Element(element);
        }

        private static object ExecuteJavascriptFunction(object func, object input)
        {
            return Javascript.ExecuteFunction(func, input);
        }
    }
}
