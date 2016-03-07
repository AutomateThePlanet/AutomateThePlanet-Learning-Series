using PatternsInAutomatedTests.Advanced.Strategy.Data;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.ItemPage;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.PreviewShoppingCartPage;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.ShippingAddressPage;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.ShippingPaymentPage;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.SignInPage;

namespace PatternsInAutomatedTests.Advanced.Strategy.Advanced.Base
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
