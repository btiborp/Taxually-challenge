using System.ComponentModel.DataAnnotations;
using Microsoft.Playwright;

namespace PlaywrightTests.pages;

public class LoginPage {
    private IPage _page;
    private readonly ILocator _txtEmail;
    private readonly ILocator _txtPassword;
    private readonly ILocator _btnLogin;
    
    public LoginPage(IPage page) {
        _page = page;
        _txtEmail = _page.Locator("#email");
        _txtPassword = _page.Locator("#password");
        _btnLogin = _page.Locator("#next");
        // _txtEmail = _page.GetByRole(AriaRole.Textbox, new() { Name = "Email Address" });
        // _txtPassword = _page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
        // _btnLogin = _page.GetByRole(AriaRole.Button, new() { Name = "Sign in" });
    }

    public async Task Login(string email, string password) {
        await _txtEmail.FillAsync(email);
        await _txtPassword.FillAsync(password);
        await _btnLogin.ClickAsync();
    }
}