using System;
using PatternsInAutomation.Tests.Advanced.Strategy.Advanced.Base;
using PatternsInAutomation.Tests.Advanced.Strategy.Data;
using PatternsInAutomation.Tests.Advanced.Strategy.Enums;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.PlaceOrderPage;
using PatternsInAutomation.Tests.Advanced.Strategy.Services;

namespace PatternsInAutomation.Tests.Advanced.Strategy.Advanced.Strategies
{
    public class SalesTaxOrderPurchaseStrategy : IOrderPurchaseStrategy
    {
        public SalesTaxOrderPurchaseStrategy()
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

        public void ValidateClientPurchaseInfo(ClientPurchaseInfo clientPurchaseInfo)
        {
            if (!clientPurchaseInfo.ShippingInfo.Country.Equals("United States"))
            {
                throw new ArgumentException("If the NoTaxesOrderPurchaseStrategy is used, the country should be set to United States because otherwise no sales tax is going to be applied.");
            }
        }
    }
}
