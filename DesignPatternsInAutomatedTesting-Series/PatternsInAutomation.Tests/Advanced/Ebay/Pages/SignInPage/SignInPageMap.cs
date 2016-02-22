using OpenQA.Selenium;
using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Ebay.Pages.SignInPage
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
