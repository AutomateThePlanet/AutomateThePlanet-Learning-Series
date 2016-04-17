using OpenQA.Selenium;

namespace PatternsInAutomatedTests.Advanced.NullObject.Pages.PlaceOrderPage
{
    public class PlaceOrderPage
    {
        public IWebElement TotalPrice
        {
            get
            {
                return this.driver.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[7]/td[2]/strong"));
            }
        }
     
        public IWebElement PromotionalDiscountPrice
        {
            get
            {
                return this.driver.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[5]/td[2]"));
            }

        }

        public IWebElement PromotionalCode
        {
            get
            {
                return this.driver.FindElement(By.Id("xocpnety_cnt"));
            }
        }
    }
}
