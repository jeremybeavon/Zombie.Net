using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public sealed class Document
    {
        private readonly dynamic document;

        private Document(dynamic document)
        {
            this.document = document;
        }

        internal static Document Create(dynamic document)
        {
            return document == null ? null : new Document(document);
        }

        private static object ExecuteJavascriptFunction(object func, object input)
        {
            return Javascript.ExecuteFunction(func, input);
        }
    }
}
