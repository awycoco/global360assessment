using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using e2e.Pages;


namespace e2e.Pages
{
    public class LoginPage(IPage page)
    {
        private readonly IPage _page = page;

        public async Task NavigateToUrl(string url)
        {
            await _page.GotoAsync(url);
        }

        public async Task Login(string username, string password)
        {
            await _page.FillAsync("input[name='username']", username);
            await _page.FillAsync("input[name='password']", password);
            await _page.ClickAsync("button[type='submit']");
        }
    }
}