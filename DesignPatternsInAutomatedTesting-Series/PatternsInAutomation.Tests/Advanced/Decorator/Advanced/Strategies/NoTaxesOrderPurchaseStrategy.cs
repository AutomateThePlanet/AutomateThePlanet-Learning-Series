using PatternsInAutomation.Tests.Advanced.Decorator.Data;
using PatternsInAutomation.Tests.Advanced.Decorator.Pages.PlaceOrderPage;

namespace PatternsInAutomation.Tests.Advanced.Decorator.Advanced.Strategies
{
    public class NoTaxesOrderPurchaseStrategy : OrderPurchaseStrategyDecorator
    {
        public NoTaxesOrderPurchaseStrategy(OrderPurchaseStrategy orderPurchaseStrategy, decimal itemsPrice, ClientPurchaseInfo clientPurchaseInfo) 
            : base(orderPurchaseStrategy, itemsPrice, clientPurchaseInfo)
        {
        }

        public override void ValidateOrderSummary(decimal totalPrice)
        {
            base.orderPurchaseStrategy.ValidateOrderSummary(totalPrice);
            PlaceOrderPage.Instance.Validate().EstimatedTaxPrice("0.00");
        }
    }
}
