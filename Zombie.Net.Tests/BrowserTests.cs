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
        public void TestOnAlertCallsCallback()
        {
            ((Func<Task>)(async () =>
            {
                // Act
                Browser browser = Browser.Create();
                await browser.VisitTestPage();
                string html = await browser.HtmlAsync();
                int statusCode = await browser.GetStatusCodeAsync();
                string alertMessage = string.Empty;
                Func<string, Task> callback = input =>
                {
                    alertMessage = input;
                    return Task.FromResult<object>(null);
                };
                await browser.OnAlertAsync(callback);
                await browser.PressButtonAsync("Display Alert");

                // Assert
                statusCode.Should().Be(200);
                alertMessage.Should().Be("test alert message");
            }))().Wait();
        }
    }
}
