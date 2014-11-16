using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public sealed class Request
    {
        private readonly dynamic request;

        public Request(dynamic request)
        {
            this.request = request;
        }

        public string Method
        {
            get { return request.method; }
        }

        public Uri Url
        {
            get { return new Uri(request.url); }
        }

        public IReadOnlyDictionary<string, string> Headers
        {
            get
            {
                IDictionary<string, object> headerDictionary = (IDictionary<string, object>)request.headers;
                return headerDictionary.ToDictionary(header => header.Key, header => (string)header.Value);
            }
        }

        public string Body
        {
            get { return request.body; }
        }

        public string MultiPart
        {
            get { return request.multipart; }
        }

        public object Time
        {
            get { return request.time; }
        }

        public TimeSpan Timeout
        {
            get { return TimeSpan.FromMilliseconds(request.timeout); }
        }
    }
}
