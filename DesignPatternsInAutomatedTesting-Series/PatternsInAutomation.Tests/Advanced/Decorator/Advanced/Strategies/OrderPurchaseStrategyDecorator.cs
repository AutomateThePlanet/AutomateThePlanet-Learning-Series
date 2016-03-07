using System;
using PatternsInAutomatedTests.Advanced.Decorator.Data;

namespace PatternsInAutomatedTests.Advanced.Decorator.Advanced.Strategies
{
    public abstract class OrderPurchaseStrategyDecorator : OrderPurchaseStrategy
    {
        protected readonly OrderPurchaseStrategy orderPurchaseStrategy;
        protected readonly ClientPurchaseInfo clientPurchaseInfo;
        protected readonly decimal itemsPrice;

        public OrderPurchaseStrategyDecorator(OrderPurchaseStrategy orderPurchaseStrategy, decimal itemsPrice, ClientPurchaseInfo clientPurchaseInfo)
        {
            this.orderPurchaseStrategy = orderPurchaseStrategy;
            this.itemsPrice = itemsPrice;
            this.clientPurchaseInfo = clientPurchaseInfo;
        }

        public override decimal CalculateTotalPrice()
        {
            this.ValidateOrderStrategy();

            return this.orderPurchaseStrategy.CalculateTotalPrice();
        }

        public override void ValidateOrderSummary(decimal totalPrice)
        {
            this.ValidateOrderStrategy();
            this.orderPurchaseStrategy.ValidateOrderSummary(totalPrice);
        }

        private void ValidateOrderStrategy()
        {
            if (this.orderPurchaseStrategy == null)
            {
                throw new Exception("The OrderPurchaseStrategy should be first initialized.");
            }
        }
    }
}