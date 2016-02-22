using System;
using PatternsInAutomation.Tests.Advanced.Strategy.Basic.Base;
using PatternsInAutomation.Tests.Advanced.Strategy.Data;
using PatternsInAutomation.Tests.Advanced.Strategy.Enums;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.PlaceOrderPage;
using PatternsInAutomation.Tests.Advanced.Strategy.Services;

namespace PatternsInAutomation.Tests.Basic.Strategy.Basic.Strategies
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
