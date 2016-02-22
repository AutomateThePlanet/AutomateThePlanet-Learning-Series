using System;
using PatternsInAutomation.Tests.Advanced.Decorator.Data;
using PatternsInAutomation.Tests.Advanced.Decorator.Enums;
using PatternsInAutomation.Tests.Advanced.Decorator.Pages.PlaceOrderPage;
using PatternsInAutomation.Tests.Advanced.Decorator.Services;

namespace PatternsInAutomation.Tests.Advanced.Decorator.Advanced.Strategies
{
    public class SalesTaxOrderPurchaseStrategy : OrderPurchaseStrategyDecorator
    {
        private readonly SalesTaxCalculationService salesTaxCalculationService;
        private decimal salesTax;        

        public SalesTaxOrderPurchaseStrategy(OrderPurchaseStrategy orderPurchaseStrategy, decimal itemsPrice, ClientPurchaseInfo clientPurchaseInfo)
            : base(orderPurchaseStrategy, itemsPrice, clientPurchaseInfo)
        {
            this.salesTaxCalculationService = new SalesTaxCalculationService();
        }

        public SalesTaxCalculationService SalesTaxCalculationService { get; set; }

        public override decimal CalculateTotalPrice()
        {
            States currentState = (States)Enum.Parse(typeof(States), clientPurchaseInfo.ShippingInfo.State);
            this.salesTax = this.salesTaxCalculationService.Calculate(this.itemsPrice, currentState, clientPurchaseInfo.ShippingInfo.Zip);
            return this.orderPurchaseStrategy.CalculateTotalPrice() + this.salesTax;
        }

        public override void ValidateOrderSummary(decimal totalPrice)
        {
            base.orderPurchaseStrategy.ValidateOrderSummary(totalPrice);
            PlaceOrderPage.Instance.Validate().EstimatedTaxPrice(salesTax.ToString());
        }
    }
}
