using OpenQA.Selenium;
using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Strategy.Pages.SignInPage
{
    public class SignInPageMap : BasePageElementMap
    {
        public IWebElement SignInButton
        {
            get
            {
                return this.browser.FindElement(By.Id("signInSubmit-input"));
            }
        }

        public IWebElement PasswordInput
        {
            get
            {
                return this.browser.FindElement(By.Id("ap_password"));
            }
        }

        public IWebElement EmailInput
        {
            get
            {
                return this.browser.FindElement(By.Id("ap_email"));
            }
        }
    }
}
