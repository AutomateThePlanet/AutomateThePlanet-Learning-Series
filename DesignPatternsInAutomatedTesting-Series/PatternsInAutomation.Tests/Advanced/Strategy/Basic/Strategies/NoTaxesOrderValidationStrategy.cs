using PatternsInAutomation.Tests.Advanced.Strategy.Basic.Base;
using PatternsInAutomation.Tests.Advanced.Strategy.Data;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.PlaceOrderPage;

namespace PatternsInAutomation.Tests.Basic.Strategy.Basic.Strategies
{
    class NoTaxesOrderValidationStrategy : IOrderValidationStrategy
    {
        public void ValidateOrderSummary(string itemsPrice, ClientPurchaseInfo clientPurchaseInfo)
        {
            PlaceOrderPage.Instance.Validate().EstimatedTaxPrice("0.00");
        }
    }
}
