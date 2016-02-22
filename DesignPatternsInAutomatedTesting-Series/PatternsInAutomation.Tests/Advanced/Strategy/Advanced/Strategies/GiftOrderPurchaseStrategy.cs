using System;
using PatternsInAutomation.Tests.Advanced.Strategy.Advanced.Base;
using PatternsInAutomation.Tests.Advanced.Strategy.Data;
using PatternsInAutomation.Tests.Advanced.Strategy.Enums;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.PlaceOrderPage;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.PreviewShoppingCartPage;
using PatternsInAutomation.Tests.Advanced.Strategy.Services;

namespace PatternsInAutomation.Tests.Advanced.Strategy.Advanced.Strategies
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
