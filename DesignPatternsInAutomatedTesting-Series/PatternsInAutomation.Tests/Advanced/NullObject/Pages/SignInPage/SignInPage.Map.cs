using OpenQA.Selenium;

namespace PatternsInAutomatedTests.Advanced.NullObject.Pages.SignInPage
{
    public partial class SignInPage
    {
        public IWebElement SignInButton
        {
            get
            {
                return this.driver.FindElement(By.Id("signInSubmit-input"));
            }
        }

        public IWebElement PasswordInput
        {
            get
            {
                return this.driver.FindElement(By.Id("ap_password"));
            }
        }

        public IWebElement EmailInput
        {
            get
            {
                return this.driver.FindElement(By.Id("ap_email"));
            }
        }
    }
}
