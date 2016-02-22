using PatternsInAutomation.Tests.Advanced.Strategy.Data;

namespace PatternsInAutomation.Tests.Advanced.Strategy.Basic.Base
{
    public interface IOrderValidationStrategy
    {
        void ValidateOrderSummary(string itemPrice, ClientPurchaseInfo clientPurchaseInfo);
    }
}
