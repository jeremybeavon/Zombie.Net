using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PortlessWebHost;
using WebCookie = System.Net.Cookie;

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

        public async Task HandleRequest(Request request, Func<Error, Response, Task> callback)
        {
            PortlessWebRequest webRequest = host.CreateRequest(request.Url);
            webRequest.Method = request.Method;
            await AddHeaders(request, webRequest);
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

                await callback(null, response);
            }
        }

        private async Task AddHeaders(Request request, PortlessWebRequest webRequest)
        {
            foreach (KeyValuePair<string, string> header in request.Headers)
            {
                webRequest.Headers[header.Key] = header.Value;
            }

            Cookies cookies = await browser.CookiesAsync(request.Url);
            Cookie[] allCookies = await cookies.AllAsync();
            CookieContainer cookieContainer = new CookieContainer();
            foreach (Cookie cookie in allCookies)
            {
            }
        }
    }
}
