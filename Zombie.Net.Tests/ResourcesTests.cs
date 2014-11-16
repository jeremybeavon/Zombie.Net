using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zombie.Net.Tests
{
    [TestClass]
    public sealed class ResourcesTests
    {
        [TestMethod]
        public async Task TestAddRequestHander()
        {
            Browser browser = Browser.Create();
            int callCount = 0;
            Func<Request, Func<Error, Response, Task>, Task> handler = async (request, next) =>
            {
                callCount++;
                await next(null, null);
            };
            await (await browser.GetResourcesAsync()).AddRequestHandlerAsync(handler);
            await browser.VisitAsync(new Uri("http://localhost:51802/Account/Login"));
            //browser.VisitTestPage();
            callCount.Should().BeGreaterThan(0);
        }
    }
}
