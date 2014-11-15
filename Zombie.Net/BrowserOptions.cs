using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public sealed class BrowserOptions
    {
        public BrowserOptions()
        {
            Debug = true;
            LoadCss = true;
            MaxWaitTime = TimeSpan.FromSeconds(5);
            MaxRedirects = 5;
            RunScripts = true;
        }

        public bool Debug { get; set; }

        public object Headers { get; set; }

        public bool LoadCss { get; set; }

        public TimeSpan MaxWaitTime { get; set; }

        public int MaxRedirects { get; set; }

        public Uri ProxyUrl { get; set; }

        public bool RunScripts { get; set; }

        public string UserAgent { get; set; }

        public bool Silent { get; set; }

        public Uri BaseUrl { get; set; }

        public TimeSpan WaitFor { get; set; }

        //localAddress

        internal object ToJavascriptOptions()
        {
            return new
            {
                debug = Debug,
                headers = Headers,
                loadCss = LoadCss,
                maxRedirects = MaxRedirects,
                maxWait = MaxWaitTime.TotalSeconds,
                proxy = ProxyUrl.AbsoluteUri,
                runScripts = RunScripts,
                silent = Silent,
                userAgent = UserAgent,
                waitFor = WaitFor.TotalSeconds
            };
        }
    }
}
