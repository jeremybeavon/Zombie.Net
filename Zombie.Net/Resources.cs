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

        public Task AddRequestHandlerAsync(Func<Request, Func<Error, Response, Task>, Task> handler)
        {
            Func<object, Task<object>> requestHandler = async (dynamic input) =>
            {
                Func<Error, Response, Task> requestCallback = (error, response) =>
                {
                    var nextInput = new 
                    {
                        error = error == null ? null : error.ToDynamic(),
                        response = response == null ? null : response.ToDynamic()
                    };
                    return ((Func<object, Task<object>>)input.next)(nextInput);
                };
                await handler(new Request(input.request), requestCallback);
                return Task.FromResult<object>(null);
            };
            return ExecuteJavascriptFunction(resources.addRequestHandler, requestHandler);
        }

        public Task AddResponseHandlerAsync(Func<Request, Response, Func<Error, Task>, Task> handler)
        {
            Func<object, Task<object>> responseHandler = async (dynamic input) =>
            {
                Func<Error, Task> responseCallback = error => 
                    ((Func<object, Task<object>>)input.next)(error == null ? null : error.ToDynamic());
                await handler(new Request(input.request), new Response(input.response), responseCallback);
                return Task.FromResult<object>(null);
            };
            return ExecuteJavascriptFunction(resources.addResponseHandler, responseHandler);
        }

        public Task DelayAsync(Uri url, TimeSpan delay)
        {
            return ExecuteJavascriptFunction(resources.delay, new { url = url.AbsoluteUri, delay = delay.TotalMilliseconds });
        }

        public Task DumpAsync()
        {
            return ExecuteJavascriptFunction(resources.dump, null);
        }

        public Task GetAsync(Uri url, Func<Error, Response, Task> callback)
        {
            return ExecuteJavascriptFunction(resources.get, new { url = url.AbsoluteUri, callback = GetCallback(callback) });
        }

        public Task MockAsync(Uri url, Response response)
        {
            return ExecuteJavascriptFunction(resources.mock, new { url = url.AbsoluteUri, response = response.ToDynamic() });
        }

        public Task PostAsync(Uri url, RequestOptions options, Func<Error, Response, Task> callback)
        {
            var input = new
            {
                url = url.AbsoluteUri,
                options = options.ToJavascriptOptions(),
                callback = GetCallback(callback)
            };
            return ExecuteJavascriptFunction(resources.post, input);
        }

        public Task RequestAsync(string method, Uri url, RequestOptions options, Func<Error, Response, Task> callback)
        {
            var input = new
            {
                method,
                url = url.AbsoluteUri,
                options = options.ToJavascriptOptions(),
                callback = GetCallback(callback)
            };
            return ExecuteJavascriptFunction(resources.request, input);
        }

        public Task RestoreAsync(Uri url)
        {
            return ExecuteJavascriptFunction(resources.restore, url.AbsoluteUri);
        }

        private static object ExecuteJavascriptFunction(object func, object input)
        {
            return ((Func<object, Task<object>>)func)(input);
        }

        private static Func<object, Task<object>> GetCallback(Func<Error, Response, Task> callback)
        {
            return async (dynamic input) =>
            {
                Error error = input.error == null ? null : new Error(input.error);
                Response response = input.response == null ? null : new Response(input.response);
                await callback(error, response);
                return Task.FromResult<object>(null);
            };
        }
    }
}
