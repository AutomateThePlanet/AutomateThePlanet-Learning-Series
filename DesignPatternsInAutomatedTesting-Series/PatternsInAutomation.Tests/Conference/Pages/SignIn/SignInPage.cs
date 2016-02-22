using OpenQA.Selenium;
using PatternsInAutomation.Tests.Conference.Base;

namespace PatternsInAutomation.Tests.Conference.Pages.SignIn
{
    public class SignInPage : BasePage<SignInPageMap>, ISignInPage
    {
        public SignInPage(IWebDriver driver)
            : base(driver, new SignInPageMap(driver))
        {
        }

        public override string Url
        {
            get
            {
                return string.Empty;
            }
        }

        public void ClickContinueAsGuestButton()
        {
            this.Map.ContinueAsGuestButton.Click();
        }

    }
}
