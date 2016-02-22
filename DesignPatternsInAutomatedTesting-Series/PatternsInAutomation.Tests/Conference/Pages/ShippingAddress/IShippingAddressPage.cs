using PatternsInAutomation.Tests.Conference.Data;

namespace PatternsInAutomation.Tests.Conference
{
    public interface IShippingAddressPage : IPage
    {
        void FillShippingInfo(ClientInfo clientInfo);

        void ClickContinueButton();

        double GetSubtotalAmount();
    }
}