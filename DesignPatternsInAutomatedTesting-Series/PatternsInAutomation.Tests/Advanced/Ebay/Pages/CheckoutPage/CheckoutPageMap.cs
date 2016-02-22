using OpenQA.Selenium;
using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Ebay.Pages.CheckoutPage
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
