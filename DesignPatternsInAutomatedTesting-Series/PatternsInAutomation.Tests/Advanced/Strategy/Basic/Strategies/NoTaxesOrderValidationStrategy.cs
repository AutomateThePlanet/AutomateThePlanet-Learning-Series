using PatternsInAutomatedTests.Advanced.Strategy.Basic.Base;
using PatternsInAutomatedTests.Advanced.Strategy.Data;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.PlaceOrderPage;

namespace PatternsInAutomatedTests.Basic.Strategy.Basic.Strategies
{
    class NoTaxesOrderValidationStrategy : IOrderValidationStrategy
    {
        public void ValidateOrderSummary(string itemsPrice, ClientPurchaseInfo clientPurchaseInfo)
        {
            PlaceOrderPage.Instance.Validate().EstimatedTaxPrice("0.00");
        }
    }
}
