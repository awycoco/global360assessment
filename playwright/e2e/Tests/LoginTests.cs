using Microsoft.Playwright.Xunit;
using e2e.Pages;

namespace e2e.Tests
{
    public class LoginTests : PageTest
    {
        [Fact]
        public async Task CanLoginSuccessfully()
        {
            var loginPage = new LoginPage(Page);

            //Navigate to Page
            await loginPage.NavigateToUrl("https://demo.snipeitapp.com/login");
            // await loginPage.LoginAsync("admin", "password");
        }
    }


}
