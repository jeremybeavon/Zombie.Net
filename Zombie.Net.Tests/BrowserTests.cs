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
            using (Browser browser = Browser.Create())
            {
                // Act
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
            }
        }

        [TestMethod]
        public async Task TestFillWithValidInput()
        {
            using (Browser browser = Browser.Create())
            {
                // Act
                await browser.VisitTestPage();
                await browser.FillAsync("userName", "TestUser");

                // Assert
                Element input = await browser.FieldAsync("userName");
                string value = await input.ValAsync();
                value.Should().Be("TestUser");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task TestFillWithNonExistentInput()
        {
            using (Browser browser = Browser.Create())
            {
                // Act
                await browser.VisitTestPage();
                await browser.FillAsync("userName2", "TestUser");
            }
        }

        [TestMethod]
        public async Task TestVisitWithAngularJs()
        {
            using (Browser browser = Browser.Create())
            {
                // Act
                Resources resources = await browser.GetResourcesAsync();
                await resources.MockAsync(new Uri("http://test/angular_test.html"), new Response(Website.AngularTestHtml));
                await resources.MockAsync(new Uri("http://test/angular.js"), new Response(Website.AngularJs));
                await resources.MockAsync(new Uri("http://test/angular_test.js"), new Response(Website.AngularTestJs));
                await browser.VisitAsync(new Uri("http://test/angular_test.html"));
                
                // Assert
                string text = await browser.TextAsync("div");
                text.Should().Be("This is a test");
            }
        }
    }
}
