using PatternsInAutomation.Tests.Advanced.Decorator.Advanced.Strategies;
using PatternsInAutomation.Tests.Advanced.Decorator.Data;
using PatternsInAutomation.Tests.Advanced.Decorator.Pages.ItemPage;
using PatternsInAutomation.Tests.Advanced.Decorator.Pages.PreviewShoppingCartPage;
using PatternsInAutomation.Tests.Advanced.Decorator.Pages.ShippingAddressPage;
using PatternsInAutomation.Tests.Advanced.Decorator.Pages.ShippingPaymentPage;
using PatternsInAutomation.Tests.Advanced.Decorator.Pages.SignInPage;

namespace PatternsInAutomation.Tests.Advanced.Decorator.Advanced.Base
{
    public class PurchaseContext
    {
        private readonly OrderPurchaseStrategy orderPurchaseStrategy;

        public PurchaseContext(OrderPurchaseStrategy orderPurchaseStrategy)
        {
            this.orderPurchaseStrategy = orderPurchaseStrategy;
        }

        public void PurchaseItem(string itemUrl, string itemPrice, ClientLoginInfo clientLoginInfo, ClientPurchaseInfo clientPurchaseInfo)
        {
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
            decimal expectedTotalPrice = this.orderPurchaseStrategy.CalculateTotalPrice();
            this.orderPurchaseStrategy.ValidateOrderSummary(expectedTotalPrice);
        }
    }
}
