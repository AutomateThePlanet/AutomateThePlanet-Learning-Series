using PatternsInAutomatedTests.Advanced.Strategy.Data;

namespace PatternsInAutomatedTests.Advanced.Strategy.Advanced.Base
{
    public interface IOrderPurchaseStrategy
    {
        void AssertOrderSummary(string itemPrice, ClientPurchaseInfo clientPurchaseInfo);

        void ValidateClientPurchaseInfo(ClientPurchaseInfo clientPurchaseInfo);
    }
}