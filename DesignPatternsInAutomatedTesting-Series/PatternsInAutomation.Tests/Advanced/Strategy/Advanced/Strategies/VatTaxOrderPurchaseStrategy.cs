using System;
using PatternsInAutomation.Tests.Advanced.Strategy.Advanced.Base;
using PatternsInAutomation.Tests.Advanced.Strategy.Data;
using PatternsInAutomation.Tests.Advanced.Strategy.Enums;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.PlaceOrderPage;

namespace PatternsInAutomation.Tests.Advanced.Strategy.Advanced.Strategies
{
    public class VatTaxOrderPurchaseStrategy : IOrderPurchaseStrategy
    {
        public VatTaxOrderPurchaseStrategy()
        {
            this.VatTaxCalculationService = new VatTaxCalculationService();
        }

        public VatTaxCalculationService VatTaxCalculationService { get; set; }

        public void ValidateOrderSummary(string itemsPrice, ClientPurchaseInfo clientPurchaseInfo)
        {
            Countries currentCountry = (Countries)Enum.Parse(typeof(Countries), clientPurchaseInfo.BillingInfo.Country);
            decimal currentItemPrice = decimal.Parse(itemsPrice);
            decimal vatTax = this.VatTaxCalculationService.Calculate(currentItemPrice, currentCountry);

            PlaceOrderPage.Instance.Validate().EstimatedTaxPrice(vatTax.ToString());
        }
        public void ValidateClientPurchaseInfo(ClientPurchaseInfo clientPurchaseInfo)
        {
           // Throw a new Argument exection if the country is not part of the EU Union.
        }
    }
}
