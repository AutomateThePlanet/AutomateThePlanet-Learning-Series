using PatternsInAutomation.Tests.Advanced.Strategy.Enums;

namespace PatternsInAutomation.Tests.Advanced.Strategy.Data
{
    public class ClientPurchaseInfo
    {
        public ClientPurchaseInfo(ClientAddressInfo addressInfo)
        {
            this.BillingInfo = addressInfo;
            this.ShippingInfo = addressInfo;
        }

        public ClientPurchaseInfo(ClientAddressInfo billingInfo, ClientAddressInfo shippingInfo)
        {
            this.BillingInfo = billingInfo;
            this.ShippingInfo = shippingInfo;
        }

        public ClientAddressInfo BillingInfo { get; set; }

        public ClientAddressInfo ShippingInfo { get; set; }

        public string DeliveryType { get; set; }

        public GiftWrappingStyles GiftWrapping { get; set; }
    }
}