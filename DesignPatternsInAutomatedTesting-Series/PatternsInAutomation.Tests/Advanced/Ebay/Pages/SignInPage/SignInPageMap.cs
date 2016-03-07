using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.Ebay.Pages.SignInPage
{
    public class SignInPageMap : BasePageElementMap
    {
        public IWebElement ContinueAsGuestButton
        {
            get
            {
                return this.browser.FindElement(By.Id("gtChk"));
            }
        }
    }
}
