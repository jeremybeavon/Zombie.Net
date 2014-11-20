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
        private static WebHost host;
        private static WebHost simpleHost;

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string physicalPath = Path.Combine(baseDirectory, @"..\..\..\Zombie.Net.TestWebSite\");
            host = new WebHost("/", Path.GetFullPath(physicalPath));
            physicalPath = Path.Combine(baseDirectory, @"..\..\Website\");
            simpleHost = new WebHost("/", Path.GetFullPath(physicalPath));
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            host.Dispose();
            simpleHost.Dispose();
            host = null;
            simpleHost = null;
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

        [TestMethod]
        public async Task TestCreateWithAngularJs()
        {
            using (Browser browser = await BrowserFactory.CreateAsync(simpleHost))
            {
                // Act
                await browser.VisitAsync(new Uri("http://test/angular_test.html"));

                // Assert
                string text = await browser.TextAsync("div");
                text.Should().Be("This is a test");
            }
        }
    }
}
