namespace PatternsInAutomatedTests.Advanced.NullObject.Data
{
    public class ClientPurchaseInfo
    {
        public ClientPurchaseInfo(ClientAddressInfo addressInfo)
        {
            this.BillingInfo = addressInfo;
            this.ShippingInfo = addressInfo;
        }

        public ClientAddressInfo BillingInfo { get; set; }

        public ClientAddressInfo ShippingInfo { get; set; }

        public string CouponCode { get; set; }
    }
}