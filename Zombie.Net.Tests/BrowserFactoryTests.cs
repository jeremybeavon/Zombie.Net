using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortlessWebHost;
using Zombie.Net.PortlessWebHost;

namespace Zombie.Net.Tests
{
    [TestClass]
    public sealed class BrowserFactoryTests
    {
        private WebHost host;

        [TestInitialize]
        public void SetUp()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string physicalPath = Path.Combine(baseDirectory, @"..\..\..\Zombie.Net.TestWebSite\");
            host = new WebHost("/", Path.GetFullPath(physicalPath));
        }

        [TestCleanup]
        public void CleanUp()
        {
            host.Dispose();
        }

        [TestMethod]
        public async Task TestCreate()
        {
            //EdgeJs.NativeModuleSupport.EdgeWithNativeModules.RegisterPreCompiledModules("zombie");
            BrowserOptions options = new BrowserOptions()
            {
                WaitDuration = TimeSpan.FromMinutes(10)
            };
            using (Browser browser = await BrowserFactory.CreateAsync(host, options))
            {
                await browser.VisitAsync(new Uri("http://localhost.test/Account/Login"));
                string html = await browser.HtmlAsync();
                /*string cookies = await browser.SaveCookiesAsync();
                string test = cookies;*/
                html.Should().Contain("Zombie.Net tests");
            }
        }
    }
}
