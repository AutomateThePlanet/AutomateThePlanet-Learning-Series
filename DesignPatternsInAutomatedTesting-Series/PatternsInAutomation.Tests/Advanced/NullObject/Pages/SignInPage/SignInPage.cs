using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.NullObject.Base;

namespace PatternsInAutomatedTests.Advanced.NullObject.Pages.SignInPage
{
    public partial class SignInPage : BasePage
    {
        public SignInPage(IWebDriver driver)
            : base(driver)
        {
        }

        public void Login(string email, string password)
        {
            this.EmailInput.SendKeys(email);
            this.PasswordInput.SendKeys(password);
            this.SignInButton.Click();
        }
    }
}
