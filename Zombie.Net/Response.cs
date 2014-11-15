using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public sealed class Response
    {
        private readonly dynamic response;

        public Response()
        {
            response = new ExpandoObject();
            StatusCode = 200;
            StatusText = "OK";
        }

        public Response(string body)
            : this()
        {
            Body = body;
        }

        internal Response(dynamic response)
        {
            this.response = response;
        }

        public Uri Url
        {
            get { return new Uri(response.url); }
            set { response.url = value == null ? null : value.AbsoluteUri; }
        }

        public int StatusCode
        {
            get { return response.statusCode; }
            set { response.statusCode = value; }
        }

        public string StatusText
        {
            get { return response.statusText; }
            set { response.statusText = value; }
        }

        public object Headers
        {
            get { return response.headers; }
            set { response.headers = value; }
        }

        public string Body
        {
            get { return response.body; }
            set { response.body = value; }
        }

        public int Redirects
        {
            get { return response.redirects; }
            set { response.redirects = value; }
        }

        public object Time
        {
            get { return response.time; }
            set { response.time = value; }
        }

        internal dynamic ToDynamic()
        {
            return response;
        }
    }
}
