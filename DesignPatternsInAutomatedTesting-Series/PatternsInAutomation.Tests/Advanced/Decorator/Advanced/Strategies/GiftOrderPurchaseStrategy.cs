using PatternsInAutomation.Tests.Advanced.Decorator.Data;
using PatternsInAutomation.Tests.Advanced.Decorator.Pages.PlaceOrderPage;
using PatternsInAutomation.Tests.Advanced.Decorator.Services;

namespace PatternsInAutomation.Tests.Advanced.Decorator.Advanced.Strategies
{
    public class GiftOrderPurchaseStrategy : OrderPurchaseStrategyDecorator
    {
        private readonly GiftWrappingPriceCalculationService giftWrappingPriceCalculationService;
        private decimal giftWrapPrice;

        public GiftOrderPurchaseStrategy(OrderPurchaseStrategy orderPurchaseStrategy, decimal itemsPrice, ClientPurchaseInfo clientPurchaseInfo) 
            : base(orderPurchaseStrategy, itemsPrice, clientPurchaseInfo)
        {
            this.giftWrappingPriceCalculationService = new GiftWrappingPriceCalculationService();
        }

        public override decimal CalculateTotalPrice()
        {
            this.giftWrapPrice = this.giftWrappingPriceCalculationService.Calculate(clientPurchaseInfo.GiftWrapping);
            return this.orderPurchaseStrategy.CalculateTotalPrice() + this.giftWrapPrice;
        }

        public override void ValidateOrderSummary(decimal totalPrice)
        {
            base.orderPurchaseStrategy.ValidateOrderSummary(totalPrice);
            PlaceOrderPage.Instance.Validate().GiftWrapPrice(giftWrapPrice.ToString());
        }
    }
}