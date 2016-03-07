using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.Ebay.Pages.ItemPage
{
    public class ItemPageMap : BasePageElementMap
    {
        public IWebElement BuyNowButton
        {
            get
            {
                return this.browser.FindElement(By.Id("binBtn_btn"));
            }
        }

        public IWebElement Price
        {
            get
            {
                return this.browser.FindElement(By.Id("prcIsum"));
            }
        }
    }
}
