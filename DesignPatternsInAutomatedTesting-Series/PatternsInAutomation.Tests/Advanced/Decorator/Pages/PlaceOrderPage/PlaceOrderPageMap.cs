using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.Decorator.Pages.PlaceOrderPage
{
    public class PlaceOrderPageMap : BasePageElementMap
    {
        public IWebElement TotalPrice
        {
            get
            {
                return this.browser.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[7]/td[2]/strong"));
            }
        }

        public IWebElement EstimatedTaxPrice
        {
            get
            {
                return this.browser.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[5]/td[2]"));
            }
        }

        public IWebElement ItemsPrice
        {
            get
            {
                return this.browser.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[1]/td[2]"));
            }
        }

        public IWebElement TotalBeforeTaxPrice
        {
            get
            {
                return this.browser.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[4]/td[2]"));
            }
        }

        public IWebElement GiftWrapPrice
        {
            get
            {
                return this.browser.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[2]/td[2]"));
            }
        }

        public IWebElement ShippingTax
        {
            get
            {
                return this.browser.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[2]/td[2]"));
            }
        }
    }
}
