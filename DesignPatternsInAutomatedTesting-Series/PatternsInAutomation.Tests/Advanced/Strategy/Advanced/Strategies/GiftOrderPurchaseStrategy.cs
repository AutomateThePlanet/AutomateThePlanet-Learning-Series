using System;
using PatternsInAutomatedTests.Advanced.Strategy.Advanced.Base;
using PatternsInAutomatedTests.Advanced.Strategy.Data;
using PatternsInAutomatedTests.Advanced.Strategy.Enums;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.PlaceOrderPage;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.PreviewShoppingCartPage;
using PatternsInAutomatedTests.Advanced.Strategy.Services;

namespace PatternsInAutomatedTests.Advanced.Strategy.Advanced.Strategies
{
    public class GiftOrderPurchaseStrategy : IOrderPurchaseStrategy
    {
        public GiftOrderPurchaseStrategy()
        {
            this.GiftWrappingPriceCalculationService = new GiftWrappingPriceCalculationService();
        }

        public GiftWrappingPriceCalculationService GiftWrappingPriceCalculationService { get; set; }

        public void ValidateOrderSummary(string itemsPrice, ClientPurchaseInfo clientPurchaseInfo)
        {
            decimal giftWrapPrice = this.GiftWrappingPriceCalculationService.Calculate(clientPurchaseInfo.GiftWrapping);

            PlaceOrderPage.Instance.Validate().GiftWrapPrice(giftWrapPrice.ToString());
        }
        public void ValidateClientPurchaseInfo(ClientPurchaseInfo clientPurchaseInfo)
        {
            if (clientPurchaseInfo.GiftWrapping.Equals(GiftWrappingStyles.None))
            {
                throw new ArgumentException("The gift wrapping style cannot be set to None if the GiftOrderPurchaseStrategy should be executed.");
            }
        }
    }
}
