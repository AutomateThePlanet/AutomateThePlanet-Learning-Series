using PatternsInAutomatedTests.Advanced.Strategy.Data;

namespace PatternsInAutomatedTests.Advanced.Strategy.Basic.Base
{
    public interface IOrderValidationStrategy
    {
        void ValidateOrderSummary(string itemPrice, ClientPurchaseInfo clientPurchaseInfo);
    }
}
