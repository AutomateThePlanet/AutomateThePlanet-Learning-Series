using OpenQA.Selenium;

namespace PatternsInAutomatedTests.Advanced.NullObject.Pages.PreviewShoppingCartPage
{
    public partial class PreviewShoppingCartPage
    {
        public IWebElement ProceedToCheckoutButton
        {
            get
            {
                return this.driver.FindElement(By.Id("hlb-ptc-btn-native"));
            }
        }

        public IWebElement ThisOrderContainsGiftCheckbox
        {
            get
            {
                return this.driver.FindElement(By.Id("sc-buy-box-gift-checkbox"));
            }
        }
    }
}
