using PatternsInAutomatedTests.Advanced.Decorator.Data;
using PatternsInAutomatedTests.Advanced.Decorator.Pages.PlaceOrderPage;

namespace PatternsInAutomatedTests.Advanced.Decorator.Advanced.Strategies
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
