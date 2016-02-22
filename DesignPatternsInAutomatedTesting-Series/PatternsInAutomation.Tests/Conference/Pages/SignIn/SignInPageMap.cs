using OpenQA.Selenium;
using PatternsInAutomation.Tests.Conference.Base;

namespace PatternsInAutomation.Tests.Conference.Pages.SignIn
{
    public class SignInPageMap : BaseElementMap
    {
        public SignInPageMap(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement ContinueAsGuestButton
        {
            get
            {
                return this.driver.FindElement(By.Id("gtChk"));
            }
        }
    }
}
