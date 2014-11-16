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
        public static Browser Create(WebHost host)
        {
            return InitializeBrowser(Browser.Create(), host);
        }

        public static Browser Create(WebHost host, BrowserOptions options)
        {
            return InitializeBrowser(new Browser(options), host);
        }

        private static Browser InitializeBrowser(Browser browser, WebHost host)
        {
            browser.Resources.AddRequestHandler(new RequestHandler(browser, host).HandleRequest);
            return browser;
        }
    }
}
