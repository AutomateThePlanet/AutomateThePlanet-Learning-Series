using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.NullObject.Base;

namespace PatternsInAutomatedTests.Advanced.NullObject.Pages.PreviewShoppingCartPage
{
    public partial class PreviewShoppingCartPage : BasePage
    {
        public PreviewShoppingCartPage(IWebDriver driver)
            : base(driver)
        {
        }

        public void ClickProceedToCheckoutButton()
        {
            this.ProceedToCheckoutButton.Click();
        }

        public void CheckOrderContainsGift()
        {
            this.ThisOrderContainsGiftCheckbox.Click();
        }
    }
}
