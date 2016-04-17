using OpenQA.Selenium;

namespace PatternsInAutomatedTests.Advanced.NullObject.Pages.ItemPage
{
    public partial class ItemPage
    {
        public IWebElement AddToCartButton
        {
            get
            {
                return this.driver.FindElement(By.Id("add-to-cart-button"));
            }
        }

        public IWebElement ProductTitle
        {
            get
            {
                return this.driver.FindElement(By.Id("productTitle"));
            }
        }
    }
}
