using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Decorator.Pages.SignInPage
{
    public class SignInPage : BasePageSingleton<SignInPage, SignInPageMap>
    {
        public void Login(string email, string password)
        {
            this.Map.EmailInput.SendKeys(email);
            this.Map.PasswordInput.SendKeys(password);
            this.Map.SignInButton.Click();
        }
    }
}
