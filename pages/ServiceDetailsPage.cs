using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace PlaywrightTests.pages;

public class ServiceDetailsPage {
    private IPage _page;
    private readonly ILocator _pageTitle;
    private readonly ILocator _btnVatNo;
    private readonly ILocator _btnVatYes;
    private readonly ILocator _btnVatNoCheckLabel;
    private readonly ILocator _btnVatOutstandingNo;
    private readonly ILocator _btnVatOutstandingYes;
    private readonly ILocator _retroactivePeriodForYes;
    private readonly ILocator _subscriptionType;
    private readonly ILocator _monthlyFee;
    private readonly ILocator _anualFee;

    public ServiceDetailsPage(IPage page) {
        _page = page;
        _pageTitle = _page
            .GetByRole(AriaRole.Heading, new() { Name = "Select services" });
        _btnVatNo = _page
            // .Locator("div")
            // .Filter(new() { HasText = "/^No$/" })
            // .First;
            .Locator("//*[@id='main']/app-registration/div/app-service-selector/div/div/form/div[1]/tuui-accordion/div/div[2]/div/div[1]/app-decision-box-group/div/tuui-decision-box[1]/div");
        _btnVatYes = _page
            // .Locator("div")
            // .Filter(new() { HasText = "/^Yes$/" })
            // .First;
            .Locator("//*[@id='main']/app-registration/div/app-service-selector/div/div/form/div[1]/tuui-accordion/div/div[2]/div/div[1]/app-decision-box-group/div/tuui-decision-box[2]/div");
        _btnVatNoCheckLabel = _page
            .GetByText("We have added the VAT registration to your cart.");
        _btnVatOutstandingNo = _page
            .Locator("//*[@id='retroactive_question_FR']/div/tuui-extended-radio[2]");
        _btnVatOutstandingYes = _page
            .Locator("//*[@id='retroactive_question_FR']/div/tuui-extended-radio[1]");
        _retroactivePeriodForYes = page
            .GetByRole(AriaRole.Combobox, new() { Name = "YYYY-MM" });
        _subscriptionType = _page
            .Locator("//*[@id='main']/app-registration/div/app-service-selector/div/app-subscription-summary/div/div[1]/div[2]/tuui-toggle/div/label/span");
        _monthlyFee = _page
            .Locator("//div[contains(text(), 'Monthly fee')]/div");
        _anualFee = _page
            .Locator("//div[contains(text(), 'Anual fee')]/div");
    }

    public async Task CheckPresence() {
        await _pageTitle.IsVisibleAsync();
    }
    public async Task CheckVatNumberNo() {
        await _btnVatNo.ClickAsync();
        await _btnVatNoCheckLabel.IsVisibleAsync();
    }

    public async Task CheckVatNumberYesOutNo() {
        await _btnVatYes.ClickAsync();
        await _btnVatNoCheckLabel.IsVisibleAsync();
        await _btnVatOutstandingNo.ClickAsync();
    }

    public async Task CheckVatNumberYesOutYes() {
            
        await _btnVatYes.ClickAsync();
        await _btnVatNoCheckLabel.IsVisibleAsync();
        await _btnVatOutstandingYes.ClickAsync();
        await _retroactivePeriodForYes.ClickAsync();
        await _page
            .GetByText("Jan")
            .ClickAsync();
    }

    public async Task AssureMonthlyFeeAndPayments() {
        // Assure Monthly fee is selected    
        var backgroundColor = await _subscriptionType
            .EvaluateAsync<string>("el => getComputedStyle(el).backgroundColor");
        if (backgroundColor.Equals("rgb(17, 113, 255)")) {
            await _subscriptionType.ClickAsync();
        }
        // Assure Payment values
        await Assertions.Expect(_monthlyFee).ToHaveTextAsync(new Regex("^(?!€0)$"));
        await Assertions.Expect(_anualFee).ToHaveTextAsync("€0");
    }

}