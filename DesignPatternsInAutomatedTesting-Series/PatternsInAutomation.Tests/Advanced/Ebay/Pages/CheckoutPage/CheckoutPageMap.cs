using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.Ebay.Pages.CheckoutPage
{
    public class CheckoutPageMap : BasePageElementMap
    {
        public IWebElement TotalPrice
        {
            get
            {
                return this.browser.FindElement(By.Id("xo_tot_amt"));
            }
        }
    }
}
