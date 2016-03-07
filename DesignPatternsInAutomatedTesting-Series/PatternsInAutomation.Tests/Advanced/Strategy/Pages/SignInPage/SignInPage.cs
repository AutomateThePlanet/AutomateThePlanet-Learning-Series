using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.Strategy.Pages.SignInPage
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
