using OpenQA.Selenium;
using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Decorator.Pages.PreviewShoppingCartPage
{
    public class PreviewShoppingCartPageMap : BasePageElementMap
    {
        public IWebElement ProceedToCheckoutButton
        {
            get
            {
                return this.browser.FindElement(By.Id("hlb-ptc-btn-native"));
            }
        }

        public IWebElement EditYourCartButton
        {
            get
            {
                return this.browser.FindElement(By.Id("a-autoid-0-announce"));
            }
        }

        public IWebElement ThisOrderContainsGiftCheckbox
        {
            get
            {
                return this.browser.FindElement(By.Id("sc-buy-box-gift-checkbox"));
            }
        }
    }
}
