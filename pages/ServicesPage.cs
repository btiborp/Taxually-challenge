using System.ComponentModel.DataAnnotations;
using Microsoft.Playwright;

namespace PlaywrightTests.pages;

public class ServicesPage {
    private IPage _page;
    private readonly ILocator _pageTitle;
    private readonly ILocator _radioECommerceService;
    private readonly ILocator _comboLegalStatus;
    private readonly ILocator _txtNameOfBusiness;
    private readonly ILocator _comboCountry;
    private readonly ILocator _txtZipPostalCode;
    private readonly ILocator _txtCity;
    private readonly ILocator _txtStreet;
    private readonly ILocator _txtHouseNumber;
    private readonly ILocator _btnNext;
    
    public ServicesPage(IPage page) {
        _page = page;
        _pageTitle = _page
            .GetByRole(AriaRole.Heading, new() { Name = "Tell us about your business" });
        _radioECommerceService = _page
            .Locator("label")
            .Filter( new() { HasText = "E-commerceSelling physical" })
            .Locator("span")
            .Nth(2);
        _comboLegalStatus = _page
            .Locator("#legalStatus")
            .GetByRole(AriaRole.Textbox);
            // .Locator("div")            
            // .Filter( new() { HasText = "/^Select legal statusCompany$/" })
            // .First;
            // .GetByRole(AriaRole.Option, new() { Name = "Partnership" })
        _txtNameOfBusiness = _page
            .GetByRole(AriaRole.Textbox, new() { Name = "Legal name of business" });
        _comboCountry = _page
            .Locator("#establishmentCountryId div")
            .Filter( new() { HasText = "Select country" })
            .First;
        _txtZipPostalCode = _page
            .Locator("form div")
            .Filter( new() { HasText = "ZIP/Post code State/Province" })
            .GetByRole(AriaRole.Textbox)
            .First;
        _txtCity = _page
            .GetByRole(AriaRole.Textbox, new() { Name = "City" });
        _txtStreet = _page
            .GetByRole(AriaRole.Textbox, new() { Name = "Street" });
        _txtHouseNumber = _page
            .GetByRole(AriaRole.Textbox, new() { Name = "House number" });
        _btnNext = _page
            .GetByRole(AriaRole.Button, new() { Name = "Next" });
    }

    public async Task CheckPresence() {
        await _pageTitle.IsVisibleAsync();
    }
    public async Task FillForm() {
        await _radioECommerceService.ClickAsync();
        await _comboLegalStatus.ClickAsync();
        string legalStatus = new string[] {"Company", "Individual", "Partnership"}[Random.Shared.Next(3)];
        bool isSameLegalStatusSelected = await _page
            .GetByRole(AriaRole.Option, new() { Name = legalStatus })
            .Locator("div")
            .IsVisibleAsync();
        if (!isSameLegalStatusSelected) {
        await _page
            .GetByRole(AriaRole.Option, new() { Name = legalStatus })
            .Locator("div")
            .ClickAsync();
        }
        await _pageTitle.ClickAsync();    
        await _txtNameOfBusiness.FillAsync("John Doe " + Random.Shared.Next(10));
        string country = new string[] {"France","Italy", "Germany", "Mexico", "Yemen"}[Random.Shared.Next(5)];
        await _comboCountry.ClickAsync();
        await _page
            .GetByRole(AriaRole.Option, new() { Name = country })
            .Locator("div")
            .ClickAsync();
        string zip = new string[] {"1234","2345", "3456"}[Random.Shared.Next(3)];
        await _txtZipPostalCode.FillAsync(zip);
        string city = new string[] {"Cork", "Mutare", "Puerto Vallarta", "Mokpo"}[Random.Shared.Next(4)];
        await _txtCity.FillAsync(city);
        string street = new string[] {"1468 Blackwell Street", "1594 Brown Avenue", "1605 Upton Avenue", "3214 Buck Drive"}[Random.Shared.Next(4)];
        await _txtStreet.FillAsync(street);
        await _txtHouseNumber.FillAsync(Random.Shared.Next(10000).ToString());
        await _btnNext.ClickAsync();
   }
}