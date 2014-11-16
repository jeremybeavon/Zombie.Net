using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zombie.Net.Tests
{
    [TestClass]
    public sealed class ResourcesTests
    {
        [TestMethod]
        public void TestAddRequestHander()
        {
            Browser browser = Browser.Create();
            int callCount = 0;
            Action<Request, Action<Error, Response>> handler = (request, next) =>
            {
                callCount++;
                next(null, null);
            };
            browser.Resources.AddRequestHandler(handler);
            browser.Visit(new Uri("http://localhost:51802/Account/Login"));
            //browser.VisitTestPage();
            callCount.Should().BeGreaterThan(0);
        }
    }
}
