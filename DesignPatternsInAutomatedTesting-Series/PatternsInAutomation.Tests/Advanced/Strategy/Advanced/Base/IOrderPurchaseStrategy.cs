using PatternsInAutomation.Tests.Advanced.Strategy.Data;

namespace PatternsInAutomation.Tests.Advanced.Strategy.Advanced.Base
{
    public interface IOrderPurchaseStrategy
    {
        void ValidateOrderSummary(string itemPrice, ClientPurchaseInfo clientPurchaseInfo);

        void ValidateClientPurchaseInfo(ClientPurchaseInfo clientPurchaseInfo);
    }
}