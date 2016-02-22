namespace PatternsInAutomation.Tests.Advanced.Decorator.Services
{
    public class ShippingCostsCalculationService
    {
        public decimal Calculate(string country, string state, string address1, string zip)
        {
            // Call real WebSerive to calculate the shipping costs.
            return 4.98M;
        }
    }
}
