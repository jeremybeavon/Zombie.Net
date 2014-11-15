using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public sealed class RequestOptions
    {
        public string Body { get; set; }

        public object Headers { get; set; }

        public object Params { get; set; }

        public TimeSpan? Timeout { get; set; }

        internal object ToJavascriptOptions()
        {
            return new
            {
                body = Body,
                timeout = Timeout.HasValue ? Timeout.Value.TotalMilliseconds : 0
            };
        }
    }
}
