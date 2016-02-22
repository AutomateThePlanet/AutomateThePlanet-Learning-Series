using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PatternsInAutomation.Tests.Conference.Base;

namespace PatternsInAutomation.Tests.Conference.Pages.ShippingAddress
{
    public class ShippingAddressPageMap : BaseElementMap
    {
        private readonly WebDriverWait driverWait;

        public ShippingAddressPageMap(IWebDriver driver) : base(driver)
        {
            driverWait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 30));
        }

        public SelectElement CountryDropDown
        {
            get
            {
                this.driverWait.Until<IWebElement>((d) => { return d.FindElement(By.Name("country")); });
                return new SelectElement(this.driver.FindElement(By.Name("country")));
            }
        }

        public IWebElement FirstName
        {
            get
            {
                return this.driver.FindElement(By.Id("firstName"));
            }
        }

        public IWebElement LastName
        {
            get
            {
                return this.driver.FindElement(By.Id("lastName"));
            }
        }

        public IWebElement Address1
        {
            get
            {
                return this.driver.FindElement(By.Id("address1"));
            }
        }

        public IWebElement City
        {
            get
            {
                return this.driver.FindElement(By.Id("city"));
            }
        }

        public IWebElement Zip
        {
            get
            {
                return this.driver.FindElement(By.Id("zip"));
            }
        }

        public IWebElement Phone
        {
            get
            {
                return this.driver.FindElement(By.Id("dayphone1"));
            }
        }

        public IWebElement Email
        {
            get
            {
                return this.driver.FindElement(By.Id("email"));
            }
        }

        public IWebElement Subtotal
        {
            get
            {
                return this.driver.FindElement(By.Id("xo_tot_amt"));
            }
        }

        public IWebElement ContinueButton
        {
            get
            {
                return this.driver.FindElement(By.Id("but_address_continue"));
            }
        }

        public void SwitchToShippingFrame()
        {
            this.WaitForLogo();
            this.driver.SwitchTo().Frame("shpFrame");
        }

        private void WaitForLogo()
        {
            this.driverWait.Until<IWebElement>((d) => { return d.FindElement(By.Id("gh-logo")); });
        }
    }
}
