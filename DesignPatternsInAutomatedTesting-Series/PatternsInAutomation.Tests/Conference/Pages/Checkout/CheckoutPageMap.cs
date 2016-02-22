using OpenQA.Selenium;
using PatternsInAutomation.Tests.Conference.Base;

namespace PPatternsInAutomation.Tests.Conference.Pages.Checkout
{
    public class CheckoutPageMap : BaseElementMap
    {
        public CheckoutPageMap(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement TotalPrice
        {
            get
            {
                return this.driver.FindElement(By.Id("xo_tot_amt"));
            }
        }
    }
}
