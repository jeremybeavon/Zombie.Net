using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net.Tests
{
    public static class BrowserExtensions
    {
        public static async Task VisitTestPage(this Browser browser)
        {
            Resources resources = await browser.GetResourcesAsync();
            await resources.MockAsync(new Uri("http://localhost.test/default.html"), new Response(Website.DefaultHtml));
            await resources.MockAsync(new Uri("http://localhost.test/default.js"), new Response(Website.DefaultJs));
            await resources.MockAsync(new Uri("http://localhost.test/jquery-2.1.1.min.js"), new Response(Website.JQueryJs));
            await browser.VisitAsync(new Uri("http://localhost.test/default.html"));
        }
    }
}
