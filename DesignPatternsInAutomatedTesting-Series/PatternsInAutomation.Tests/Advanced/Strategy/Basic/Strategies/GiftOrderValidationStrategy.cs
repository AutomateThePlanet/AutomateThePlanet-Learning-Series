using PatternsInAutomatedTests.Advanced.Strategy.Basic.Base;
using PatternsInAutomatedTests.Advanced.Strategy.Data;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.PlaceOrderPage;
using PatternsInAutomatedTests.Advanced.Strategy.Services;

namespace PatternsInAutomatedTests.Advanced.Strategy.Basic.Strategies
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
