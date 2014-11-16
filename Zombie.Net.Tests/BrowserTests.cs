using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zombie.Net.Tests
{
    [TestClass]
    public class BrowserTests
    {


        [TestMethod]
        public async Task TestOnAlertCallsCallback()
        {
            Browser browser = Browser.Create();
            await browser.VisitTestPage();
            string html = await browser.HtmlAsync();
            int statusCode = await browser.GetStatusCodeAsync();
            bool isAlertCallbackCalled = false;
            Func<string, Task> callback = input => 
            {
                input.Should().Be("test alert message");
                isAlertCallbackCalled = true;
                return Task.FromResult<object>(null);
            };
            await browser.OnAlertAsync(callback);
            await browser.PressButtonAsync("Display Alert");
            isAlertCallbackCalled.Should().BeTrue();
        }
    }
}
