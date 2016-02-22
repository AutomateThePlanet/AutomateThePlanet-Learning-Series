using PatternsInAutomation.Tests.Advanced.Strategy.Basic.Base;
using PatternsInAutomation.Tests.Advanced.Strategy.Data;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.PlaceOrderPage;
using PatternsInAutomation.Tests.Advanced.Strategy.Services;

namespace PatternsInAutomation.Tests.Advanced.Strategy.Basic.Strategies
{
    public class GiftOrderValidationStrategy : IOrderValidationStrategy
    {
        public GiftOrderValidationStrategy()
        {
            this.GiftWrappingPriceCalculationService = new GiftWrappingPriceCalculationService();
        }

        public GiftWrappingPriceCalculationService GiftWrappingPriceCalculationService { get; set; }

        public void ValidateOrderSummary(string itemsPrice, ClientPurchaseInfo clientPurchaseInfo)
        {
            decimal giftWrapPrice = this.GiftWrappingPriceCalculationService.Calculate(clientPurchaseInfo.GiftWrapping);

            PlaceOrderPage.Instance.Validate().GiftWrapPrice(giftWrapPrice.ToString());
        }
    }
}
