using OpenQA.Selenium;
using PatternsInAutomatedTests.Conference.Base;

namespace PatternsInAutomatedTests.Conference.Pages.SignIn
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
