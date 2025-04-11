namespace PlaywrightTests;

using Microsoft.Playwright;
using PlaywrightTests.pages;

public class Tests
{
    private const string email = "btiborp@gmail.com";
    private const string password = "AdvantageWS_123";
    private IPlaywright _playwright;
    private IBrowser _browser;
    private IPage _page;

    [SetUp]
    public async Task Setup() {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions {
            Headless = false
        });
        _page = await _browser.NewPageAsync();
        await _page.GotoAsync("https://app.taxually.com/");

        LoginPage loginPage = new LoginPage(_page);
        await loginPage.Login(email, password);
        ServicesPage servicesPage = new ServicesPage(_page);
        await servicesPage.CheckPresence();
        await servicesPage.FillForm();
        CountryPage countryPage = new CountryPage(_page);
        await countryPage.CheckPresence();
        await countryPage.SelectTargetCountry("France");
        // await countryPage.SelectTargetCountries(9);

    }

    [Test]
    public async Task TestCheckServiceWithNoVatNo() {
        ServiceDetailsPage serviceDetailsPage = new ServiceDetailsPage(_page);
        await serviceDetailsPage.CheckPresence();
        await serviceDetailsPage.CheckVatNumberNo();
        await serviceDetailsPage.AssureMonthlyFeeAndPayments();
   }

    [Test]
    public async Task TestCheckServiceWithVatYesOutNo() {
        ServiceDetailsPage serviceDetailsPage = new ServiceDetailsPage(_page);
        await serviceDetailsPage.CheckPresence();
        await serviceDetailsPage.CheckVatNumberYesOutNo();
        await serviceDetailsPage.AssureMonthlyFeeAndPayments();
    }

    [Test]
    public async Task TestCheckServiceWithVatYesOutYes() {
        ServiceDetailsPage serviceDetailsPage = new ServiceDetailsPage(_page);
        await serviceDetailsPage.CheckPresence();
        await serviceDetailsPage.CheckVatNumberYesOutYes();
        await serviceDetailsPage.AssureMonthlyFeeAndPayments();
   }
}
