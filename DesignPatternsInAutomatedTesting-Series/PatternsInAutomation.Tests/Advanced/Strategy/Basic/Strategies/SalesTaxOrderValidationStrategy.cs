using System;
using PatternsInAutomatedTests.Advanced.Strategy.Basic.Base;
using PatternsInAutomatedTests.Advanced.Strategy.Data;
using PatternsInAutomatedTests.Advanced.Strategy.Enums;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.PlaceOrderPage;
using PatternsInAutomatedTests.Advanced.Strategy.Services;

namespace PatternsInAutomatedTests.Basic.Strategy.Basic.Strategies
{
    public class SalesTaxOrderValidationStrategy : IOrderValidationStrategy
    {
        public SalesTaxOrderValidationStrategy()
        {
            this.SalesTaxCalculationService = new SalesTaxCalculationService();
        }

        public SalesTaxCalculationService SalesTaxCalculationService { get; set; }

        public void ValidateOrderSummary(string itemsPrice, ClientPurchaseInfo clientPurchaseInfo)
        {
            States currentState = (States)Enum.Parse(typeof(States), clientPurchaseInfo.ShippingInfo.State);
            decimal currentItemPrice = decimal.Parse(itemsPrice);
            decimal salesTax = this.SalesTaxCalculationService.Calculate(currentItemPrice, currentState, clientPurchaseInfo.ShippingInfo.Zip);

            PlaceOrderPage.Instance.Validate().EstimatedTaxPrice(salesTax.ToString());
        }
    }
}
