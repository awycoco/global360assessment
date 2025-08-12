using System.Text.RegularExpressions;
using System.Threading.Tasks;
using e2e.helpers;
using e2e.Pages;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

namespace e2e.Tests
{
    public class AssetTests : PageTest
    {
        [Fact]
        public async Task CanAddAssets()
        {

            var loginPage = new LoginPage(Page);
            var assetsPage = new AssetsPage(Page);
            var createAssetPage = new CreateAssetsPage(Page);

            //Navigate to Page
            await loginPage.NavigateToUrl("https://demo.snipeitapp.com/login");

            //Login
            await loginPage.Login("admin", "password");
            await Expect(Page).ToHaveURLAsync("https://demo.snipeitapp.com/");

            await Page.Locator("li[class='dropdown']").ClickAsync();
            await Page.Locator("a[href='https://demo.snipeitapp.com/hardware/create']").ClickAsync();

            //Run Test
            var assetInfo = await createAssetPage.CanCreateAsset("Macbook Pro 13", "Ready to Deploy", "Assessment");
            await assetsPage.IsAssetCreatedSuccessfully(assetInfo.AssetTag!);
            await assetsPage.ViewAssetAfterCreation();
            string username = StringCleaner.GetCleanName(assetInfo.CheckOutUser!);
            await assetsPage.ValidateAssetInfo(assetInfo.AssetTag!, assetInfo.Status!, username, assetInfo.Model!, assetInfo.Notes!);
            await assetsPage.ValidateHistory(assetInfo.Model!);
    
            await Page.WaitForTimeoutAsync(5000);

        }
    }

}
