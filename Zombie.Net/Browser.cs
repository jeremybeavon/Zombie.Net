using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdgeJs.NativeModuleSupport;
using Zombie.Net.Properties;

namespace Zombie.Net
{
    public sealed class Browser : IDisposable
    {
        private static readonly object initializeLock = new object();
        private static Func<object, Task<object>> browserFactoryFunc;
        private readonly dynamic browser;

        public Browser(BrowserOptions options)
        {
            if (options == null)
            {
                options = new BrowserOptions();
            }

            browser = CreateBrowser(options.ToJavascriptOptions());
        }

        private Browser(dynamic browser)
        {
            this.browser = browser;
        }

        public async Task<Element> GetBodyAsync()
        {
            return Element.Create(await ExecuteJavascriptFunction(browser.body, null));
        }

        public async Task<Cookies> GetCookiesAsync()
        {
            return new Cookies(await ExecuteJavascriptFunction(browser.cookies, null));
        }

        public async Task<Document> GetDocumentAsync()
        {
            return Document.Create(await ExecuteJavascriptFunction(browser.document, null));
        }

        public async Task<Element> GetFocusedAsync()
        {
            return Element.Create(await ExecuteJavascriptFunction(browser.focused, null));
        }

        public async Task<History> GetHistoryAsync()
        {
            return new History(await ExecuteJavascriptFunction(browser.history, null));
        }

        public async Task<Location> GetLocationAsync()
        {
            return new Location(await ExecuteJavascriptFunction(browser.location, null));
        }

        public Task SetLocationAsync(Location location)
        {
            return ExecuteJavascriptFunction(browser.setLocation, location.Url);
        }

        public async Task<bool> GetRedirectedAsync()
        {
            return (bool)(await ExecuteJavascriptFunction(browser.redirected, null));
        }

        public async Task<Resources> GetResourcesAsync()
        {
            return new Resources(await ExecuteJavascriptFunction(browser.resources, null));
        }

        public async Task<int> GetStatusCodeAsync()
        {
            return (int)(await ExecuteJavascriptFunction(browser.statusCode, null));
        }

        public async Task<bool> GetSuccessAsync()
        {
            return (bool)(await ExecuteJavascriptFunction(browser.success, null));
        }

        public async Task<Uri> GetUrlAsync()
        {
            return new Uri((string)(await ExecuteJavascriptFunction(browser.url, null)));
        }

        public static Browser Create()
        {
            return new Browser(CreateBrowser(null));
        }

        public Task AttachAsync(string selector, string fileName)
        {
            return ExecuteJavascriptFunction(browser.attach, new { selector, filename = fileName });
        }

        public Task BackAsync()
        {
            return ExecuteJavascriptFunction(browser.back, null);
        }

        public async Task<Element> GetButtonAsync(string selector)
        {
            return Element.Create(await ExecuteJavascriptFunction(browser.button, selector));
        }

        public Task CheckAsync(string field)
        {
            return ExecuteJavascriptFunction(browser.check, field);
        }

        public Task ChooseAsync(string field)
        {
            return ExecuteJavascriptFunction(browser.choose, field);
        }

        public Task CloseAsync()
        {
            return ExecuteJavascriptFunction(browser.close, null);
        }

        public Task<object> EvaluateAsync(string javascriptExpression)
        {
            return ExecuteJavascriptFunction(browser.evaluate, javascriptExpression);
        }

        public async Task<Element> FieldAsync(string selector)
        {
            return Element.Create(await ExecuteJavascriptFunction(browser.field, selector));
        }

        public Task FillAsync(string field, string value)
        {
            return ExecuteJavascriptFunction(browser.fill, new { field, value });
        }

        public async Task<Browser> ForkAsync()
        {
            return new Browser(await ExecuteJavascriptFunction(browser.fork, null));
        }

        public async Task<string> HtmlAsync()
        {
            return (string)(await ExecuteJavascriptFunction(browser.html, null));
        }

        public async Task<string> HtmlAsync(string selector)
        {
            return (string)(await ExecuteJavascriptFunction(browser.html, new { selector }));
        }

        public Task LoadAsync(string html)
        {
            return ExecuteJavascriptFunction(browser.load, html);
        }

        public Task LoadCookiesAsync(string text)
        {
            return ExecuteJavascriptFunction(browser.loadCookies, null);
        }

        public Task LoadHistoryAsync(string text)
        {
            return ExecuteJavascriptFunction(browser.loadHistory, null);
        }

        public Task LoadStorageAsync(string text)
        {
            return ExecuteJavascriptFunction(browser.loadStorage, null);
        }

        public async Task<Storage> LocalStorage(string host)
        {
            return new Storage(await ExecuteJavascriptFunction(browser.localStorage, host));
        }

        public Task OnAlertAsync(Func<string, Task> function)
        {
            Func<object, Task<object>> javascriptFunction = async input =>
            {
                await function((string)input);
                return Task.FromResult<object>(null);
            };
            return ExecuteJavascriptFunction(browser.onAlert, javascriptFunction);
        }

        public Task OnConfirmAsync(string question, bool response)
        {
            return ExecuteJavascriptFunction(browser.onConfirm, new { question, response });
        }

        public Task OnConfirmAsync(Func<string, Task<bool>> function)
        {
            Func<object, Task<object>> confirmFunction = async input => await function((string)input);
            return ExecuteJavascriptFunction(browser.onConfirm, new { confirmFunction });
        }

        public Task OnPromptAsync(string message, PromptResponse response)
        {
            return ExecuteJavascriptFunction(browser.onPrompt, new { message, response = response.Response });
        }

        public Task OnPromptAsync(Func<string, Task<PromptResponse>> function)
        {
            Func<object, Task<object>> promptFunction = async input =>
            {
                PromptResponse response = await function((string)input);
                return Task.FromResult(response == null ? null : response.Response);
            };
            return ExecuteJavascriptFunction(browser.onPrompt, new { promptFunction });
        }

        public Task PressButtonAsync(string selector)
        {
            return ExecuteJavascriptFunction(browser.pressButton, selector);
        }

        public async Task<bool> PromptedAsync(string message)
        {
            return (bool)(await ExecuteJavascriptFunction(browser.prompted, message));
        }

        public Task<Element[]> QueryAllAsync(string selector)
        {
            return null;
        }

        public async Task<Element> QueryAsync(string selector)
        {
            return Element.Create(await ExecuteJavascriptFunction(browser.query, new { selector }));
        }

        public Task ReloadAsync()
        {
            return ExecuteJavascriptFunction(browser.reload, null);
        }

        public async Task<string> SaveCookiesAsync()
        {
            return (string)(await ExecuteJavascriptFunction(browser.saveCookies, null));
        }

        public async Task<string> SaveHistoryAsync()
        {
            return (string)(await ExecuteJavascriptFunction(browser.saveHistory, null));
        }

        public async Task<string> SaveStorageAsync()
        {
            return (string)(await ExecuteJavascriptFunction(browser.saveStorage, null));
        }

        public Task SelectAsync(string field, string value)
        {
            return ExecuteJavascriptFunction(browser.select, new { field, value });
        }

        public async Task<Storage> SessionStorageAsync(string host)
        {
            return new Storage(await ExecuteJavascriptFunction(browser.sessionStorage, host));
        }

        public async Task<string> TextAsync(string selector)
        {
            return (string)(await ExecuteJavascriptFunction(browser.text, new { selector }));
        }

        public Task UncheckAsync(string field)
        {
            return ExecuteJavascriptFunction(browser.uncheck, field);
        }

        public Task UnselectAsync(string field, string value)
        {
            return ExecuteJavascriptFunction(browser.unselect, new { field, value });
        }

        public Task VisitAsync(Uri url)
        {
            var input = new
            {
                url = url.AbsoluteUri
            };
            return ExecuteJavascriptFunction(browser.visit, input);
        }

        public Task VisitAsync(Uri url, BrowserOptions options)
        {
            var input = new
            {
                url = url.AbsoluteUri,
                options = options.ToJavascriptOptions()
            };
            return ExecuteJavascriptFunction(browser.visit, input);
        }

        public Task WaitAsync()
        {
            return ExecuteJavascriptFunction(browser.wait, null);
        }

        public Task WaitAsync(TimeSpan duration)
        {
            return ExecuteJavascriptFunction(browser.wait, new { duration });
        }

        public void WaitAsync(Func<Window, Task<bool>> function)
        {
            Func<object, Task<object>> done = async input => await function((Window)input);
            ExecuteJavascriptFunction(browser.wait, new { done });
        }

        private static Task<object> ExecuteJavascriptFunction(object func, object input)
        {
            return ((Func<object, Task<object>>)func)(input);
        }

        private static object CreateBrowser(object options)
        {
            if (browserFactoryFunc == null)
            {
                lock (initializeLock)
                {
                    if (browserFactoryFunc == null)
                    {
                        browserFactoryFunc = EdgeWithNativeModules.Func(Properties.Resources.Browser + "return browserFactory;", "zombie");
                    }
                }
            }

            Task<object> createTask = browserFactoryFunc(options);
            createTask.Wait();
            return createTask.Result;
        }

        public void Dispose()
        {
            CloseAsync().Wait();
        }
    }
}

/*
 * Missing:
 * xpath
 * html with context
 * queryAll with context
 * query with context
 * text with context
 * selectOption
 * unselectOption
 * fire
 * wait with function
 * resources.dump with output stream
 * resources.fail
 * resources.pipeline
 */