using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdgeJs.NativeModuleSupport;
using Zombie.Net.Properties;

namespace Zombie.Net
{
    public sealed class Browser
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

        public Element Body
        {
            get { return Element.Create(ExecuteJavascriptFunction(browser.body, null)); }
        }

        public Document Document
        {
            get { return Document.Create(ExecuteJavascriptFunction(browser.document, null)); }
        }

        public Element Focused
        {
            get { return Element.Create(ExecuteJavascriptFunction(browser.focused, null)); }
        }

        public History History
        {
            get { return new History(ExecuteJavascriptFunction(browser.history, null)); }
        }

        public Location Location
        {
            get { return new Location(ExecuteJavascriptFunction(browser.location, null)); }
            set { ExecuteJavascriptFunction(browser.setLocation, value.Url); }
        }

        public bool Redirected
        {
            get { return (bool)ExecuteJavascriptFunction(browser.redirected, null); }
        }

        public Resources Resources
        {
            get { return new Resources(ExecuteJavascriptFunction(browser.resources, null)); }
        }

        public int StatusCode
        {
            get { return (int)ExecuteJavascriptFunction(browser.statusCode, null); }
        }

        public bool Success
        {
            get { return (bool)ExecuteJavascriptFunction(browser.success, null); }
        }

        public Uri Url
        {
            get { return new Uri((string)ExecuteJavascriptFunction(browser.url, null)); }
        }

        public static Browser Create()
        {
            return new Browser(CreateBrowser(null));
        }

        public Browser Attach(string selector, string fileName)
        {
            ExecuteJavascriptFunction(browser.attach, new { selector, filename = fileName });
            return this;
        }

        public void Back()
        {
            ExecuteJavascriptFunction(browser.back, null);
        }

        public Element Button(string selector)
        {
            return Element.Create(ExecuteJavascriptFunction(browser.button, selector));
        }

        public Browser Check(string field)
        {
            ExecuteJavascriptFunction(browser.check, field);
            return this;
        }

        public Browser Choose(string field)
        {
            ExecuteJavascriptFunction(browser.choose, field);
            return this;
        }

        public void Close()
        {
            ExecuteJavascriptFunction(browser.close, null);
        }

        public Cookies Cookies()
        {
            return new Cookies(ExecuteJavascriptFunction(browser.cookies, null));
        }

        public Cookies Cookies(string domain)
        {
            return new Cookies(ExecuteJavascriptFunction(browser.cookies, new { domain }));
        }

        public Cookies Cookies(string domain, string path)
        {
            return new Cookies(ExecuteJavascriptFunction(browser.cookies, new { domain, path }));
        }

        public object Evaluate(string javascriptExpression)
        {
            return ExecuteJavascriptFunction(browser.evaluate, javascriptExpression);
        }

        public Element Field(string selector)
        {
            return Element.Create(ExecuteJavascriptFunction(browser.field, selector));
        }

        public Browser Fill(string field, string value)
        {
            ExecuteJavascriptFunction(browser.fill, new { field, value });
            return this;
        }

        public Browser Fork()
        {
            return new Browser(ExecuteJavascriptFunction(browser.fork, null));
        }

        public string Html()
        {
            return (string)ExecuteJavascriptFunction(browser.html, null);
        }

        public string Html(string selector)
        {
            return (string)ExecuteJavascriptFunction(browser.html, new { selector });
        }

        public void Load(string html)
        {
            ExecuteJavascriptFunction(browser.load, html);
        }

        public void LoadCookies(string text)
        {
            ExecuteJavascriptFunction(browser.loadCookies, null);
        }

        public void LoadHistory(string text)
        {
            ExecuteJavascriptFunction(browser.loadHistory, null);
        }

        public void LoadStorage(string text)
        {
            ExecuteJavascriptFunction(browser.loadStorage, null);
        }

        public Storage LocalStorage(string host)
        {
            return new Storage(ExecuteJavascriptFunction(browser.localStorage, host));
        }

        public void OnAlert(Action<string> function)
        {
            Func<object, Task<object>> javascriptFunction = input =>
            {
                function((string)input);
                return Task.FromResult<object>(null);
            };
            ExecuteJavascriptFunction(browser.onAlert, javascriptFunction);
        }

        public void OnConfirm(string question, bool response)
        {
            ExecuteJavascriptFunction(browser.onConfirm, new { question, response });
        }

        public void OnConfirm(Func<string, bool> function)
        {
            Func<object, Task<object>> confirmFunction = input => Task.FromResult<object>(function((string)input));
            ExecuteJavascriptFunction(browser.onConfirm, new { confirmFunction });
        }

        public void OnPrompt(string message, PromptResponse response)
        {
            ExecuteJavascriptFunction(browser.onPrompt, new { message, response = response.Response });
        }

        public void OnPrompt(Func<string, PromptResponse> function)
        {
            Func<object, Task<object>> promptFunction = input =>
            {
                PromptResponse response = function((string)input);
                return Task.FromResult(response == null ? null : response.Response);
            };
            ExecuteJavascriptFunction(browser.onPrompt, new { promptFunction });
        }

        public void PressButton(string selector)
        {
            ExecuteJavascriptFunction(browser.pressButton, selector);
        }

        public bool Prompted(string message)
        {
            return (bool)ExecuteJavascriptFunction(browser.prompted, message);
        }

        public Element[] QueryAll(string selector)
        {
            return null;
        }

        public Element Query(string selector)
        {
            return Element.Create(ExecuteJavascriptFunction(browser.query, new { selector }));
        }

        public void Reload()
        {
            ExecuteJavascriptFunction(browser.reload, null);
        }

        public string SaveCookies()
        {
            return (string)ExecuteJavascriptFunction(browser.saveCookies, null);
        }

        public string SaveHistory()
        {
            return (string)ExecuteJavascriptFunction(browser.saveHistory, null);
        }

        public string SaveStorage()
        {
            return (string)ExecuteJavascriptFunction(browser.saveStorage, null);
        }

        public Browser Select(string field, string value)
        {
            ExecuteJavascriptFunction(browser.select, new { field, value });
            return this;
        }

        public Storage SessionStorage(string host)
        {
            return new Storage(ExecuteJavascriptFunction(browser.sessionStorage, host));
        }

        public string Text(string selector)
        {
            return (string)ExecuteJavascriptFunction(browser.text, new { selector });
        }

        public Browser Uncheck(string field)
        {
            ExecuteJavascriptFunction(browser.uncheck, field);
            return this;
        }

        public Browser Unselect(string field, string value)
        {
            ExecuteJavascriptFunction(browser.unselect, new { field, value });
            return this;
        }

        public Browser Visit(Uri url)
        {
            var input = new
            {
                url = url.AbsoluteUri
            };
            ExecuteJavascriptFunction(browser.visit, input);
            return this;
        }

        public Browser Visit(Uri url, BrowserOptions options)
        {
            var input = new
            {
                url = url.AbsoluteUri,
                options = options.ToJavascriptOptions()
            };
            ExecuteJavascriptFunction(browser.visit, input);
            return this;
        }

        public void Wait()
        {
            ExecuteJavascriptFunction(browser.wait, null);
        }

        public void Wait(TimeSpan duration)
        {
            ExecuteJavascriptFunction(browser.wait, new { duration });
        }

        public void Wait(Func<Window, bool> function)
        {
            Func<object, Task<object>> done = input => Task.FromResult<object>(function((Window)input));
            ExecuteJavascriptFunction(browser.wait, new { done });
        }

        private static object ExecuteJavascriptFunction(object func, object input)
        {
            return Javascript.ExecuteFunction(func, input);
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

            return Javascript.ExecuteFunction(browserFactoryFunc, options);
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