using Microsoft.Playwright;

namespace e2e.Drivers;
public class TestBase : IAsyncLifetime
{
    public IPlaywright? _playwright;
    public IBrowser? _browser;
    public IBrowserContext? _context;
    public IPage? Page;

    public async Task InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = false });
        _context = await _browser.NewContextAsync();
        Page = await _context.NewPageAsync();
    }

    public async Task DisposeAsync()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}