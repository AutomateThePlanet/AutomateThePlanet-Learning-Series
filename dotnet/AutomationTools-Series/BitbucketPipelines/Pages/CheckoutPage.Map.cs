using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BitbucketPipelines.Pages
{
    public partial class CheckoutPage
    {
        public IWebElement FirstName => _driver.FindElement(By.Id("firstName"));
        public IWebElement FirstNameValidation => _driver.FindElement(By.CssSelector("#firstName ~ .invalid-feedback"));
        public IWebElement LastName => _driver.FindElement(By.Id("lastName"));
        public IWebElement LastNameValidation => _driver.FindElement(By.CssSelector("#lastName ~ .invalid-feedback"));
        public IWebElement Username => _driver.FindElement(By.Id("username"));
        public IWebElement UsernameValidation => _driver.FindElement(By.CssSelector("#username ~ .invalid-feedback"));
        public IWebElement Email => _driver.FindElement(By.Id("email"));
        public IWebElement EmailValidation => _driver.FindElement(By.CssSelector("#email ~ .invalid-feedback"));
        public IWebElement Address1 => _driver.FindElement(By.Id("address"));
        public IWebElement Address1Validation => _driver.FindElement(By.CssSelector("#address ~ .invalid-feedback"));
        public IWebElement Address2 => _driver.FindElement(By.Id("address2"));
        public SelectElement Country => new SelectElement(_driver.FindElement(By.Id("country")));
        public IWebElement CountryValidation => _driver.FindElement(By.CssSelector("#country ~ .invalid-feedback"));
        public SelectElement State => new SelectElement(_driver.FindElement(By.Id("state")));
        public IWebElement StateValidation => _driver.FindElement(By.CssSelector("#state ~ .invalid-feedback"));
        public IWebElement Zip => _driver.FindElement(By.Id("zip"));
        public IWebElement ZipValidation => _driver.FindElement(By.CssSelector("#zip ~ .invalid-feedback"));
        public IWebElement CardName => _driver.FindElement(By.Id("cc-name"));
        public IWebElement CardNameValidation => _driver.FindElement(By.CssSelector("#cc-name ~ .invalid-feedback"));
        public IWebElement CardNumber => _driver.FindElement(By.Id("cc-number"));
        public IWebElement CardNumberValidation => _driver.FindElement(By.CssSelector("#cc-number ~ .invalid-feedback"));
        public IWebElement CardExpiration => _driver.FindElement(By.Id("cc-expiration"));
        public IWebElement CardExpirationValidation => _driver.FindElement(By.CssSelector("#cc-expiration ~ .invalid-feedback"));
        public IWebElement CardCVV => _driver.FindElement(By.Id("cc-cvv"));
        public IWebElement CardCVVValidation => _driver.FindElement(By.CssSelector("#cc-cvv ~ .invalid-feedback"));
        public IWebElement SubmitButton => _driver.FindElement(By.XPath("//button[text()='Continue to checkout']"));
    }
}
