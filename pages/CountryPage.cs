using Microsoft.Playwright;

namespace PlaywrightTests.pages;

public class CountryPage {
    private IPage _page;
    private readonly ILocator _pageTitle;
    private readonly ILocator _all_regions;
    private readonly ILocator _all_countries;
    private readonly ILocator _eu_ioss;
    private readonly ILocator _eu_oss;
    private readonly ILocator _czech_republic;
    private readonly ILocator _france;
    private readonly ILocator _germany;
    private readonly ILocator _italy;
    private readonly ILocator _poland;
    private readonly ILocator _spain;
    private readonly ILocator _united_kingdom;
    private readonly ILocator _btnNext;
    private string[] targetCountries = {"EU-IOSS", "EU-OSS", "Czech Republic", "France", "Germany", "Italy", "Poland", "Spain", "United Kingdom"};
    private string[] selectedCountries;

    public CountryPage(IPage page) {
        _page = page;
        _pageTitle = _page
            .GetByRole(AriaRole.Heading, new() { Name = "Country selection" });
        _all_regions = _page
            .Locator("//*[@id='main']/app-registration/div/app-country-selector/div/div[2]/section[1]/div/tuui-extended-checkbox");
        _all_countries = _page
            .Locator("//*[@id='main']/app-registration/div/app-country-selector/div/div[2]/section[2]/div/tuui-extended-checkbox");
        _eu_ioss = _page
            .Locator("label")
            .Filter(new() { HasText = "EU - IOSS" });
        _eu_oss = _page
            .Locator("label")
            .Filter(new() { HasText = "EU - OSS" });
        _czech_republic = _page
            .Locator("label")
            .Filter(new() { HasText = "Czech Republic" });
        _france = _page
            .Locator("label")
            .Filter(new() { HasText = "France" });
        _germany = _page
            .Locator("label")
            .Filter(new() { HasText = "Germany" });
        _italy = _page
            .Locator("label")
            .Filter(new() { HasText = "Italy" });
        _poland = _page
            .Locator("label")
            .Filter(new() { HasText = "Poland" });
        _spain = _page
            .Locator("label")
            .Filter(new() { HasText = "Spain" });
        _united_kingdom = _page
            .Locator("label")
            .Filter(new() { HasText = "United Kingdom" });
        _btnNext = _page
            .GetByRole(AriaRole.Button, new() { Name = "Next" });
    }

    public async Task CheckPresence() {
        await _pageTitle.IsVisibleAsync();
    }

    private async Task SelectCountries() {

        foreach (var region in await _all_regions.AllAsync()) {
            // Console.WriteLine(await region.GetAttributeAsync("data-unique-meta_selected"));
            if (await region.GetAttributeAsync("data-unique-meta_selected") == "true" ) {
                await region.ClickAsync();
            };
        };
        foreach (var country in await _all_countries.AllAsync()) {
            // Console.WriteLine(await country.GetAttributeAsync("data-unique-meta_selected"));
            if (await country.GetAttributeAsync("data-unique-meta_selected") == "true" ) {
                await country.ClickAsync();
            };
        };
        if (Array.IndexOf(selectedCountries, "EU-IOSS") >= 0) {
            await _eu_ioss.CheckAsync();
        };
        if (Array.IndexOf(selectedCountries, "EU-OSS") >= 0) {
            await _eu_oss.CheckAsync();
        };
        if (Array.IndexOf(selectedCountries, "Czech Republic") >= 0) {
            await _czech_republic.CheckAsync();
        };
        if (Array.IndexOf(selectedCountries, "France") >= 0) {
            await _france.CheckAsync();
        };
        if (Array.IndexOf(selectedCountries, "Germany") >= 0) {
            await _germany.CheckAsync();
        };
        if (Array.IndexOf(selectedCountries, "Italy") >= 0) {
            await _italy.CheckAsync();
        };
        if (Array.IndexOf(selectedCountries, "Poland") >= 0) {
            await _poland.CheckAsync();
        };
        if (Array.IndexOf(selectedCountries, "Spain") >= 0) {
            await _spain.CheckAsync();
        };
        if (Array.IndexOf(selectedCountries, "United Kingdom") >= 0) {
            await _united_kingdom.CheckAsync();
        };
        await _btnNext.ClickAsync();
    }

    public async Task SelectTargetCountries(int targetCount) {
        string[] shuffledTargetCountries = targetCountries.ToArray();
        Random.Shared.Shuffle(shuffledTargetCountries);
        foreach (string contry in shuffledTargetCountries) {
            Console.WriteLine(contry);
        }
        if (targetCount > 0 && targetCount < 10 ) {
            selectedCountries = shuffledTargetCountries.Take(targetCount).ToArray();
        } else {
            selectedCountries = shuffledTargetCountries.Take(1).ToArray();
        }
        await SelectCountries();
    }

    public async Task SelectTargetCountry(string country) {
        string[] shuffledTargetCountries = { country };
        selectedCountries = shuffledTargetCountries;
        await SelectCountries();
    }
}
