using OpenQA.Selenium;
using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Strategy.Pages.ItemPage
{
    public class ItemPageMap : BasePageElementMap
    {
        public IWebElement AddToCartButton
        {
            get
            {
                return this.browser.FindElement(By.Id("add-to-cart-button"));
            }
        }

        public IWebElement ProductTitle
        {
            get
            {
                return this.browser.FindElement(By.Id("productTitle"));
            }
        }
    }
}
