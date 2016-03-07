using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.Strategy.Pages.ShippingAddressPage
{
    public class ShippingAddressPageMap : BasePageElementMap
    {
        public SelectElement CountryDropDown
        {
            get
            {
                this.browserWait.Until<IWebElement>((d) => { return d.FindElement(By.Name("enterAddressCountryCode")); });
                return new SelectElement(this.browser.FindElement(By.Name("enterAddressCountryCode")));
            }
        }

        public IWebElement FullNameInput
        {
            get
            {
                return this.browser.FindElement(By.Id("enterAddressFullName"));
            }
        }

        public IWebElement Address1Input
        {
            get
            {
                return this.browser.FindElement(By.Id("enterAddressAddressLine1"));
            }
        }

        public IWebElement CityInput
        {
            get
            {
                return this.browser.FindElement(By.Id("enterAddressCity"));
            }
        }

        public IWebElement ZipInput
        {
            get
            {
                return this.browser.FindElement(By.Id("enterAddressPostalCode"));
            }
        }

        public IWebElement PhoneInput
        {
            get
            {
                return this.browser.FindElement(By.Id("enterAddressPhoneNumber"));
            }
        }

        public SelectElement DeliveryPreferenceDropDown
        {
            get
            {
                return new SelectElement(this.browser.FindElement(By.Name("AddressType")));
            }
        }
      

        public IWebElement ContinueButton
        {
            get
            {
                return this.browser.FindElement(By.XPath("//input[@value='Continue']"));
            }
        }

        public IWebElement ShipToThisAddress
        {
            get
            {
                return this.browser.FindElement(By.CssSelector("input.a-button-text"));
            }
        }

        public IWebElement DifferemtFromBillingCheckbox
        {
            get
            {
                return this.browser.FindElement(By.Id("isBillingAddress"));
            }
        }
    }
}
