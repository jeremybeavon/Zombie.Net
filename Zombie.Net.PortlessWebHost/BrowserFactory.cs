using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortlessWebHost;

namespace Zombie.Net.PortlessWebHost
{
    public static class BrowserFactory
    {
        public static Task<Browser> CreateAsync(WebHost host)
        {
            return InitializeBrowser(Browser.Create(), host);
        }

        public static Task<Browser> CreateAsync(WebHost host, BrowserOptions options)
        {
            return InitializeBrowser(new Browser(options), host);
        }

        private static async Task<Browser> InitializeBrowser(Browser browser, WebHost host)
        {
            await (await browser.GetResourcesAsync()).AddRequestHandlerAsync(new RequestHandler(browser, host).HandleRequest);
            return browser;
        }
    }
}
