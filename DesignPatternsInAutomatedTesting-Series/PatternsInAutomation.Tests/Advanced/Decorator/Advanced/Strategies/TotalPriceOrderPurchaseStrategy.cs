using PatternsInAutomatedTests.Advanced.Decorator.Pages.PlaceOrderPage;

namespace PatternsInAutomatedTests.Advanced.Decorator.Advanced.Strategies
{
    public class TotalPriceOrderPurchaseStrategy : OrderPurchaseStrategy
    {
        private readonly decimal itemsPrice;

        public TotalPriceOrderPurchaseStrategy(decimal itemsPrice)
        {
            this.itemsPrice = itemsPrice;
        }

        public override decimal CalculateTotalPrice()
        {
            return itemsPrice;
        }

        public override void ValidateOrderSummary(decimal totalPrice)
        {
            PlaceOrderPage.Instance.Validate().OrderTotalPrice(totalPrice.ToString());
        }
    }
}
