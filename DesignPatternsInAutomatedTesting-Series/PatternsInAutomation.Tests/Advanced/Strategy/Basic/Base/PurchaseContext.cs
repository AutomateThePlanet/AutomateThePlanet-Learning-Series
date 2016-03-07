using PatternsInAutomatedTests.Advanced.Strategy.Data;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.ItemPage;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.PreviewShoppingCartPage;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.ShippingAddressPage;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.ShippingPaymentPage;
using PatternsInAutomatedTests.Advanced.Strategy.Pages.SignInPage;

namespace PatternsInAutomatedTests.Advanced.Strategy.Basic.Base
{
    public class PurchaseContext
    {
        private readonly IOrderValidationStrategy orderValidationStrategy;

        public PurchaseContext(IOrderValidationStrategy orderValidationStrategy)
        {
            this.orderValidationStrategy = orderValidationStrategy;
        }

        public void PurchaseItem(string itemUrl, string itemPrice, ClientLoginInfo clientLoginInfo, ClientPurchaseInfo clientPurchaseInfo)
        {
            ItemPage.Instance.Navigate(itemUrl);
            ItemPage.Instance.ClickBuyNowButton();
            PreviewShoppingCartPage.Instance.ClickProceedToCheckoutButton();
            SignInPage.Instance.Login(clientLoginInfo.Email, clientLoginInfo.Password);
            ShippingAddressPage.Instance.FillShippingInfo(clientPurchaseInfo);
            ShippingAddressPage.Instance.ClickContinueButton();
            ShippingPaymentPage.Instance.ClickBottomContinueButton();
            ShippingPaymentPage.Instance.ClickTopContinueButton();
            this.orderValidationStrategy.ValidateOrderSummary(itemPrice, clientPurchaseInfo);
        }
    }
}
