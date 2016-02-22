using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Ebay.Pages.ShippingAddressPage
{
    public class ShippingAddressPageMap : BasePageElementMap
    {
        public SelectElement CountryDropDown
        {
            get
            {
                this.browserWait.Until<IWebElement>((d) => { return d.FindElement(By.Name("country")); });
                return new SelectElement(this.browser.FindElement(By.Name("country")));
            }
        }

        public IWebElement FirstName
        {
            get
            {
                return this.browser.FindElement(By.Id("firstName"));
            }
        }

        public IWebElement LastName
        {
            get
            {
                return this.browser.FindElement(By.Id("lastName"));
            }
        }

        public IWebElement Address1
        {
            get
            {
                return this.browser.FindElement(By.Id("address1"));
            }
        }

        public IWebElement City
        {
            get
            {
                return this.browser.FindElement(By.Id("city"));
            }
        }

        public IWebElement Zip
        {
            get
            {
                return this.browser.FindElement(By.Id("zip"));
            }
        }

        public IWebElement Phone
        {
            get
            {
                return this.browser.FindElement(By.Id("dayphone1"));
            }
        }

        public IWebElement Email
        {
            get
            {
                return this.browser.FindElement(By.Id("email"));
            }
        }

        public IWebElement Subtotal
        {
            get
            {
                return this.browser.FindElement(By.Id("xo_tot_amt"));
            }
        }

        public IWebElement ContinueButton
        {
            get
            {
                return this.browser.FindElement(By.Id("but_address_continue"));
            }
        }

        public void SwitchToShippingFrame()
        {
            this.WaitForLogo();
            this.browser.SwitchTo().Frame("shpFrame");
        }

        private void WaitForLogo()
        {
            this.browserWait.Until<IWebElement>((d) => { return d.FindElement(By.Id("gh-logo")); });
        }
    }
}
