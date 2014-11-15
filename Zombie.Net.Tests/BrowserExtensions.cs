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
        public static void VisitTestPage(this Browser browser)
        {
            Resources resources = browser.Resources;
            resources.Mock(new Uri("http://localhost.test/default.html"), new Response(Website.DefaultHtml));
            resources.Mock(new Uri("http://localhost.test/default.js"), new Response(Website.DefaultJs));
            resources.Mock(new Uri("http://localhost.test/jquery-2.1.1.min.js"), new Response(Website.JQueryJs));
            browser.Visit(new Uri("http://localhost.test/default.html"));
        }
    }
}
