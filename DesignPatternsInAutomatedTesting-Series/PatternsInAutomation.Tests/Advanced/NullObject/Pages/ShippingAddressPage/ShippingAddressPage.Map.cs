using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PatternsInAutomatedTests.Advanced.NullObject.Pages.ShippingAddressPage
{
    public partial class ShippingAddressPage
    {
        public SelectElement CountryDropDown
        {
            get
            {
                this.driverWait.Until<IWebElement>((d) => { return d.FindElement(By.Name("enterAddressCountryCode")); });
                return new SelectElement(this.driver.FindElement(By.Name("enterAddressCountryCode")));
            }
        }

        public IWebElement FullNameInput
        {
            get
            {
                return this.driver.FindElement(By.Id("enterAddressFullName"));
            }
        }

        public IWebElement Address1Input
        {
            get
            {
                return this.driver.FindElement(By.Id("enterAddressAddressLine1"));
            }
        }

        public IWebElement CityInput
        {
            get
            {
                return this.driver.FindElement(By.Id("enterAddressCity"));
            }
        }

        public IWebElement ZipInput
        {
            get
            {
                return this.driver.FindElement(By.Id("enterAddressPostalCode"));
            }
        }

        public IWebElement PhoneInput
        {
            get
            {
                return this.driver.FindElement(By.Id("enterAddressPhoneNumber"));
            }
        }

        public SelectElement DeliveryPreferenceDropDown
        {
            get
            {
                return new SelectElement(this.driver.FindElement(By.Name("AddressType")));
            }
        }
      

        public IWebElement ContinueButton
        {
            get
            {
                return this.driver.FindElement(By.XPath("//input[@value='Continue']"));
            }
        }

        public IWebElement ShipToThisAddress
        {
            get
            {
                return this.driver.FindElement(By.CssSelector("input.a-button-text"));
            }
        }

        public IWebElement DifferemtFromBillingCheckbox
        {
            get
            {
                return this.driver.FindElement(By.Id("isBillingAddress"));
            }
        }
    }
}
