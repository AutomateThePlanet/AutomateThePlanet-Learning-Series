﻿using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.Strategy.Pages.PreviewShoppingCartPage
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
