using PatternsInAutomatedTests.Conference.Data;

namespace PatternsInAutomatedTests.Conference
{
    public interface IShippingAddressPage : IPage
    {
        void FillShippingInfo(ClientInfo clientInfo);

        void ClickContinueButton();

        double GetSubtotalAmount();
    }
}