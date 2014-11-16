using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainAspects;
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
            string physicalPath = Path.Combine(baseDirectory, @"..\..\..\Zombie.Net.TestWebSite");
            host = new WebHost("/", Path.GetFullPath(physicalPath));
            DefaultAppDomainProvider.AppDomain = host.Domain;
        }

        [TestCleanup]
        public void CleanUp()
        {
            DefaultAppDomainProvider.AppDomain = null;
            host.Dispose();
        }

        [TestMethod]
        [RunInDifferentAppDomain]
        public void TestCreate()
        {
            EdgeJs.NativeModuleSupport.EdgeWithNativeModules.RegisterPreCompiledModules("zombie");
            Browser browser = BrowserFactory.Create(WebHost.Current);
            browser.Visit(new Uri("http://localhost.test/Account/Login"));
            string html = browser.Html();
            html.Should().Contain("Zombie.Net tests");
        }
    }
}
