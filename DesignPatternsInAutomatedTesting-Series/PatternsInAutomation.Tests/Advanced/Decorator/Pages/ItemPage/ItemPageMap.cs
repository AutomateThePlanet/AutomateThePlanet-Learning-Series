using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.Decorator.Pages.ItemPage
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
