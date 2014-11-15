using System;
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
            Browser browser = Browser.Create();
            browser.VisitTestPage();
            string html = browser.Html();
            int statusCode = browser.StatusCode;
            bool isAlertCallbackCalled = false;
            Action<string> callback = input => 
            {
                input.Should().Be("test alert message");
                isAlertCallbackCalled = true;
            };
            browser.OnAlert(callback);
            browser.PressButton("Display Alert");
            isAlertCallbackCalled.Should().BeTrue();
        }
    }
}
