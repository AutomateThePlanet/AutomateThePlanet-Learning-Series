using PatternsInAutomatedTests.Advanced.Decorator.Data;

namespace PatternsInAutomatedTests.Advanced.Decorator.Services
{
    public class OrderSummaryCalculator
    {
        public OrderSummaryCalculator()
        {
            ShippingCostsCalculationService = new ShippingCostsCalculationService();
        }

        public ShippingCostsCalculationService ShippingCostsCalculationService { get; set; }

        public decimal CalculateTotalPrice(decimal itemsPrice, decimal estimatedTax, ClientPurchaseInfo clientPurchaseInfo)
        {
            decimal totalPrice = default(decimal);
            decimal shippingCosts = CalculateShippingPrice(clientPurchaseInfo);
            totalPrice = itemsPrice + estimatedTax + shippingCosts;

            return totalPrice;
        }

        public decimal CalculateBeforeTaxPrice(decimal itemsPrice, ClientPurchaseInfo clientPurchaseInfo)
        {
            decimal beforeTaxPrice = default(decimal);
            decimal shippingCosts = CalculateShippingPrice(clientPurchaseInfo);
            beforeTaxPrice = itemsPrice + shippingCosts;

            return beforeTaxPrice;
        }

        public decimal CalculateShippingPrice(ClientPurchaseInfo clientPurchaseInfo)
        {
            decimal shippingCosts = this.ShippingCostsCalculationService.Calculate(clientPurchaseInfo.ShippingInfo.Country, clientPurchaseInfo.ShippingInfo.State, clientPurchaseInfo.ShippingInfo.Address1, clientPurchaseInfo.ShippingInfo.Zip);
            return shippingCosts;
        }
    }
}
