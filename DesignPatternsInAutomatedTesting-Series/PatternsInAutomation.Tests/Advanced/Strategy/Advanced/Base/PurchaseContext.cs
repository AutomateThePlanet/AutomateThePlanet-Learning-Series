using PatternsInAutomation.Tests.Advanced.Strategy.Data;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.ItemPage;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.PreviewShoppingCartPage;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.ShippingAddressPage;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.ShippingPaymentPage;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.SignInPage;

namespace PatternsInAutomation.Tests.Advanced.Strategy.Advanced.Base
{
    public class PurchaseContext
    {
        private readonly IOrderPurchaseStrategy[] orderpurchaseStrategies;

        public PurchaseContext(params IOrderPurchaseStrategy[] orderpurchaseStrategies)
        {
            this.orderpurchaseStrategies = orderpurchaseStrategies;
        }

        public void PurchaseItem(string itemUrl, string itemPrice, ClientLoginInfo clientLoginInfo, ClientPurchaseInfo clientPurchaseInfo)
        {
            this.ValidateClientPurchaseInfo(clientPurchaseInfo);

            ItemPage.Instance.Navigate(itemUrl);
            ItemPage.Instance.ClickBuyNowButton();
            PreviewShoppingCartPage.Instance.ClickProceedToCheckoutButton();
            SignInPage.Instance.Login(clientLoginInfo.Email, clientLoginInfo.Password);
            ShippingAddressPage.Instance.FillShippingInfo(clientPurchaseInfo);
            ShippingAddressPage.Instance.ClickDifferentBillingCheckBox(clientPurchaseInfo);
            ShippingAddressPage.Instance.ClickContinueButton();
            ShippingPaymentPage.Instance.ClickBottomContinueButton();
            ShippingAddressPage.Instance.FillBillingInfo(clientPurchaseInfo);
            ShippingAddressPage.Instance.ClickContinueButton();
            ShippingPaymentPage.Instance.ClickTopContinueButton();

            this.ValidateOrderSummary(itemPrice, clientPurchaseInfo);
        }

        public void ValidateClientPurchaseInfo(ClientPurchaseInfo clientPurchaseInfo)
        {
            foreach (var currentStrategy in orderpurchaseStrategies)
            {
                currentStrategy.ValidateClientPurchaseInfo(clientPurchaseInfo);
            }
        }

        public void ValidateOrderSummary(string itemPrice, ClientPurchaseInfo clientPurchaseInfo)
        {
            foreach (var currentStrategy in orderpurchaseStrategies)
            {
                currentStrategy.ValidateOrderSummary(itemPrice, clientPurchaseInfo);
            }
        }
    }
}
