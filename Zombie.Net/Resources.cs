using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public sealed class Resources
    {
        private readonly dynamic resources;

        internal Resources(dynamic resources)
        {
            this.resources = resources;
        }

        public void AddRequestHandler(Action<Request, Action<Error, Response>> handler)
        {
            Func<object, Task<object>> requestHandler = (dynamic input) =>
            {
                Action<Error, Response> requestCallback = (error, response) =>
                {
                    var nextInput = new 
                    {
                        error = error == null ? null : error.ToDynamic(),
                        response = response == null ? null : response.ToDynamic()
                    };
                    ((Func<object, Task<object>>)input.next)(nextInput);
                };
                handler(new Request(input.request), requestCallback);
                return Task.FromResult<object>(null);
            };
            ExecuteJavascriptFunction(resources.addRequestHandler, requestHandler);
        }

        public void AddResponseHandler(Action<Request, Response, Action<Error>> handler)
        {
            Func<object, Task<object>> responseHandler = (dynamic input) =>
            {
                Action<Error> responseCallback = error => 
                    ((Func<object, Task<object>>)input.next)(error == null ? null : error.ToDynamic()).Wait();
                handler(new Request(input.request), new Response(input.response), responseCallback);
                return Task.FromResult<object>(null);
            };
            ExecuteJavascriptFunction(resources.addResponseHandler, responseHandler);
        }

        public void Delay(Uri url, TimeSpan delay)
        {
            ExecuteJavascriptFunction(resources.delay, new { url = url.AbsoluteUri, delay = delay.TotalMilliseconds });
        }

        public void Dump()
        {
            ExecuteJavascriptFunction(resources.dump, null);
        }

        public void Get(Uri url, Action<Error, Response> callback)
        {
            ExecuteJavascriptFunction(resources.get, new { url = url.AbsoluteUri, callback = GetCallback(callback) });
        }

        public void Mock(Uri url, Response response)
        {
            ExecuteJavascriptFunction(resources.mock, new { url = url.AbsoluteUri, response = response.ToDynamic() });
        }

        public void Post(Uri url, RequestOptions options, Action<Error, Response> callback)
        {
            var input = new
            {
                url = url.AbsoluteUri,
                options = options.ToJavascriptOptions(),
                callback = GetCallback(callback)
            };
            ExecuteJavascriptFunction(resources.post, input);
        }

        public void Request(string method, Uri url, RequestOptions options, Action<Error, Response> callback)
        {
            var input = new
            {
                method,
                url = url.AbsoluteUri,
                options = options.ToJavascriptOptions(),
                callback = GetCallback(callback)
            };
            ExecuteJavascriptFunction(resources.request, input);
        }

        public void Restore(Uri url)
        {
            ExecuteJavascriptFunction(resources.restore, url.AbsoluteUri);
        }

        private static object ExecuteJavascriptFunction(object func, object input)
        {
            return Javascript.ExecuteFunction(func, input);
        }

        private static Func<object, Task<object>> GetCallback(Action<Error, Response> callback)
        {
            return (dynamic input) =>
            {
                Error error = input.error == null ? null : new Error(input.error);
                Response response = input.response == null ? null : new Response(input.response);
                callback(error, response);
                return Task.FromResult<object>(null);
            };
        }
    }
}
