using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using e2e.Pages;

namespace e2e.Pages
{
    public class AssetsPage(IPage page) : PageTest
    {
        private readonly IPage _page = page;

        public async Task IsAssetCreatedSuccessfully(string assetTag)
        {
            //verify newly created asset has been successfully added
            await Expect(_page.Locator("div[class='alert alert-success fade in']")).ToContainTextAsync(assetTag);
        }

        public async Task ViewAssetAfterCreation()
        {
            await Expect(_page.Locator("//a[contains(text(), 'Click here to view')]")).ToBeVisibleAsync();
            await _page.Locator("//a[contains(text(), 'Click here to view')]").ClickAsync();
        }

        private async Task GoToAssetHistory()
        {
            var tabHistory = _page.Locator("a[href='#history']");

            await Expect(tabHistory).ToBeEnabledAsync();
            await tabHistory.ClickAsync();
        }

        public async Task ValidateAssetInfo(string asset, string status, string checkedOutuser, string model, string note)
        {
            var valAssetTag = _page.Locator("//strong[contains(text(), 'Asset Tag')]//parent::div//following-sibling::div/span");
            var valStatus = _page.Locator("//strong[contains(text(), 'Status')]//parent::div//following-sibling::div");
            var valCheckedOutUser = _page.Locator("//strong[contains(text(), 'Status')]//parent::div//following-sibling::div/a");
            var valModel = _page.Locator("//strong[contains(text(), 'Model')]//parent::div//following-sibling::div/a");
            var valNotes = _page.Locator("//strong[contains(text(), 'Notes')]//parent::div//following-sibling::div");

            //validate actual and expected values in the assets Info tab
            await Expect(valAssetTag).ToHaveTextAsync(asset);
            await Expect(valStatus).ToContainTextAsync(status);
            await Expect(valCheckedOutUser).ToHaveTextAsync(checkedOutuser);
            await Expect(valModel).ToContainTextAsync(model);
            await Expect(valNotes).ToContainTextAsync(note);

        }

        public async Task ValidateHistory(string model)
        {
            //validate history actions in assets history tab
            await GoToAssetHistory();
            await CanSeeActionHistory("create new", model);
            await CanSeeActionHistory("checkout", model);
        }

        private async Task CanSeeActionHistory(string action, string model)
        {
            var history = _page.Locator("//td[contains(text(), '" + action + "')]//following-sibling::td[1]");
            await Expect(history).ToContainTextAsync(model);
        }
    }
}
