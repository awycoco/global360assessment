using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using e2e.Pages;
using e2e.helpers;

namespace e2e.Pages
{
    public class CreateAssetsPage(IPage page) : PageTest
    {
        private readonly IPage _page = page;
        private string serialTag = "", company = "", checkoutuser = "";

        public class AssetInfo
        {
            public string? Company { get; set; }
            public string? AssetTag { get; set; }
            public string? SerialTag { get; set; }
            public string? Model { get; set; }
            public string? Status { get; set; }
            public string? CheckOutUser { get; set; }
            public string? Notes { get; set; }
        }
        public async Task<AssetInfo> CanCreateAsset(string model, string status, string note)
        {
            
            await SelectCompany();
            var assetTag = await GetAssetTag();
            await InputSerialTag();
            await SearchAndSelectModel(model);
            await SelectStatus(status);
            await CanBeCheckedOut();
            await SelectUser();
            await InputNotes(note);
            await SelectLocation();
            await SaveAsset();

            return new AssetInfo
            {
                Company = company,
                AssetTag = assetTag,
                SerialTag = serialTag,
                Model = model,
                Status = status,
                CheckOutUser = checkoutuser,
                Notes = note,
            };

        }

        private async Task SelectCompany()
        {
            var slctCompany = _page.Locator("#select2-company_select-container");

            //select company
            await Expect(slctCompany).ToBeVisibleAsync();
            await slctCompany.ClickAsync();

            //selecting random company
            await page.Locator("#select2-company_select-results li:nth-of-type(1)").ClickAsync();

            company = await _page.Locator("span#select2-company_select-container").GetAttributeAsync("title");

        }

        private async Task<String> GetAssetTag()
        {
            var inpAssetTag = _page.Locator("#asset_tag");
            //get asset tag value
            return await inpAssetTag.GetAttributeAsync("value");
        }

        private async Task InputSerialTag()
        {
            var inpSerial = _page.Locator("//input[@id='serials[1]']");

            //input serial
            serialTag = Randomizer.GenerateGuid();
            await Expect(inpSerial).ToBeVisibleAsync();
            await inpSerial.FillAsync(serialTag.ToUpper());
        }

        private async Task SearchAndSelectModel(string model)
        {
            var slctModel = _page.Locator("#select2-model_select_id-container");

            //search and select model
            await Expect(slctModel).ToBeVisibleAsync();
            await slctModel.ClickAsync();
            await _page.Locator("input.select2-search__field").FillAsync(model);
            await _page.Locator("ul#select2-model_select_id-results li").ClickAsync();
        }

        private async Task SelectStatus(string status)
        {
            var slctStatus = _page.Locator("#select2-status_select_id-container");

            //select status
            await Expect(slctStatus).ToBeVisibleAsync();
            await slctStatus.ClickAsync();
            await _page.Locator("input.select2-search__field").FillAsync(status);
            await _page.Locator("ul#select2-status_select_id-results li").ClickAsync();

        }

        private async Task SelectUser()
        {
            var slctUser = _page.Locator("#select2-assigned_user_select-container");

            //checkout to user is preselected so just verify if checkout to field has users list
            await Expect(slctUser).ToBeVisibleAsync();
            await Expect(slctUser.Locator("span")).ToHaveTextAsync("Select a User");
            await slctUser.ClickAsync();
            await _page.Locator("#select2-assigned_user_select-results li:nth-of-type(1)").ClickAsync();

            checkoutuser = await _page.Locator("#select2-assigned_user_select-container").GetAttributeAsync("title");
        }

        private async Task InputNotes(string note)
        {
            var inpNotes = _page.Locator("#notes");

            //fill notes
            await Expect(inpNotes).ToBeVisibleAsync();
            await inpNotes.FillAsync(note);
        }

        private async Task SelectLocation()
        { 
            var slctLocation = _page.Locator("#select2-rtd_location_id_location_select-container");

            //select location
            await Expect(slctLocation).ToBeVisibleAsync();
            await slctLocation.ClickAsync();
            await _page.Locator("#select2-rtd_location_id_location_select-results li:nth-of-type(1)").ClickAsync();
        }

        private async Task SaveAsset()
        {
            var btnSave = _page.Locator("#submit_button");

            //save asset
            await Expect(btnSave).ToBeVisibleAsync();
            await Expect(btnSave).ToBeEnabledAsync();
            await btnSave.ClickAsync();
        }

        private async Task CanBeCheckedOut()
        {
            //check asset can be checked out
            await Expect(_page.Locator("p#selected_status_status")).ToBeVisibleAsync();
        }
    }
}
