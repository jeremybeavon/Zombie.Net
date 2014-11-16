using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortlessWebHost;

namespace Zombie.Net.PortlessWebHost
{
    internal sealed class RequestHandler
    {
        private readonly Browser browser;
        private readonly WebHost host;

        public RequestHandler(Browser browser, WebHost host)
        {
            this.browser = browser;
            this.host = host;
        }

        public void HandleRequest(Request request, Action<Error, Response> callback)
        {
            PortlessWebRequest webRequest = host.CreateRequest(request.Url);
            webRequest.Method = request.Method;
            using (PortlessWebResponse webResponse = webRequest.GetPortlessResponse())
            {
                Response response = new Response();
                response.Url = request.Url;
                using (Stream stream = webResponse.GetResponseStream())
                {
                    byte[] responseBytes = new byte[stream.Length];
                    stream.Read(responseBytes, 0, (int)stream.Length);
                    response.Body = Encoding.Default.GetString(responseBytes);
                }

                callback(null, response);
            }
        }
    }
}
