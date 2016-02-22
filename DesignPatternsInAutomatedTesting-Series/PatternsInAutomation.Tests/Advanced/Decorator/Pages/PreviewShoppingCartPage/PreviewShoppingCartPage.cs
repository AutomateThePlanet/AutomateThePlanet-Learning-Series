using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Decorator.Pages.PreviewShoppingCartPage
{
    public class PreviewShoppingCartPage : BasePageSingleton<PreviewShoppingCartPage, PreviewShoppingCartPageMap>
    {
        public void ClickProceedToCheckoutButton()
        {
            this.Map.ProceedToCheckoutButton.Click();
        }

        public void CheckOrderContainsGift()
        {
            this.Map.ThisOrderContainsGiftCheckbox.Click();
        }
    }
}
