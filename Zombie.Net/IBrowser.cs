using System;
using System.Threading.Tasks;

namespace Zombie.Net
{
    public interface IBrowser : IDisposable
    {
        Task<Element> GetBodyAsync();

        Task<Cookies> GetCookiesAsync();

        Task<Document> GetDocumentAsync();

        Task<Element> GetFocusedAsync();

        Task<History> GetHistoryAsync();

        Task<Location> GetLocationAsync();

        Task SetLocationAsync(Location location);

        Task<bool> GetRedirectedAsync();

        Task<Resources> GetResourcesAsync();

        Task<int> GetStatusCodeAsync();

        Task<bool> GetSuccessAsync();

        Task<Uri> GetUrlAsync();

        Task AttachAsync(string selector, string fileName);

        Task BackAsync();

        Task<Element> GetButtonAsync(string selector);

        Task CheckAsync(string field);

        Task ChooseAsync(string field);

        Task CloseAsync();

        Task<object> EvaluateAsync(string javascriptExpression);

        Task<Element> FieldAsync(string selector);

        Task FillAsync(string field, string value);

        Task<Browser> ForkAsync();

        Task<string> HtmlAsync();

        Task<string> HtmlAsync(string selector);

        Task LoadAsync(string html);

        Task LoadCookiesAsync(string text);

        Task LoadHistoryAsync(string text);

        Task LoadStorageAsync(string text);

        Task<Storage> LocalStorage(string host);

        Task OnAlertAsync(Func<string, Task> function);

        Task OnConfirmAsync(string question, bool response);

        Task OnConfirmAsync(Func<string, Task<bool>> function);

        Task OnPromptAsync(string message, PromptResponse response);

        Task OnPromptAsync(Func<string, Task<PromptResponse>> function);

        Task PressButtonAsync(string selector);

        Task<bool> PromptedAsync(string message);

        Task<Element[]> QueryAllAsync(string selector);

        Task<Element> QueryAsync(string selector);

        Task ReloadAsync();

        Task<string> SaveCookiesAsync();

        Task<string> SaveHistoryAsync();

        Task<string> SaveStorageAsync();

        Task SelectAsync(string field, string value);

        Task<Storage> SessionStorageAsync(string host);

        Task<string> TextAsync(string selector);

        Task UncheckAsync(string field);

        Task UnselectAsync(string field, string value);

        Task VisitAsync(Uri url);

        Task VisitAsync(Uri url, BrowserOptions options);

        Task WaitAsync();

        Task WaitAsync(TimeSpan duration);

        void WaitAsync(Func<Window, Task<bool>> function);
    }
}
