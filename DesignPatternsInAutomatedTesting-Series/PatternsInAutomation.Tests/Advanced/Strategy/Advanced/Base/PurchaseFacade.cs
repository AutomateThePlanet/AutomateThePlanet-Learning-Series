using PatternsInAutomation.Tests.Advanced.Strategy.Data;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.ItemPage;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.PlaceOrderPage;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.PreviewShoppingCartPage;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.ShippingAddressPage;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.ShippingPaymentPage;
using PatternsInAutomation.Tests.Advanced.Strategy.Pages.SignInPage;

namespace PatternsInAutomation.Tests.Advanced.Strategy.Advanced.Base
{
    public class PurchaseFacade
    {
        public void PurchaseItemSalesTax(string itemUrl, string itemPrice, string taxAmount, ClientLoginInfo clientLoginInfo, ClientPurchaseInfo clientPurchaseInfo)
        {
            PurchaseItemInternal(itemUrl, clientLoginInfo, clientPurchaseInfo);
            PlaceOrderPage.Instance.Validate().EstimatedTaxPrice(taxAmount);
        }

        public void PurchaseItemGiftWrapping(string itemUrl, string itemPrice, string giftWrapTax, ClientLoginInfo clientLoginInfo, ClientPurchaseInfo clientPurchaseInfo)
        {
            PurchaseItemInternal(itemUrl, clientLoginInfo, clientPurchaseInfo);
            PlaceOrderPage.Instance.Validate().GiftWrapPrice(giftWrapTax);
        }

        public void PurchaseItemShippingTax(string itemUrl, string itemPrice, string shippingTax, ClientLoginInfo clientLoginInfo, ClientPurchaseInfo clientPurchaseInfo)
        {
            PurchaseItemInternal(itemUrl, clientLoginInfo, clientPurchaseInfo);
            PlaceOrderPage.Instance.Validate().ShippingTaxPrice(shippingTax);
        }

        private void PurchaseItemInternal(string itemUrl, ClientLoginInfo clientLoginInfo, ClientPurchaseInfo clientPurchaseInfo)
        {
            ItemPage.Instance.Navigate(itemUrl);
            ItemPage.Instance.ClickBuyNowButton();
            PreviewShoppingCartPage.Instance.ClickProceedToCheckoutButton();
            SignInPage.Instance.Login(clientLoginInfo.Email, clientLoginInfo.Password);
            ShippingAddressPage.Instance.FillShippingInfo(clientPurchaseInfo);
            ShippingAddressPage.Instance.ClickContinueButton();
            ShippingPaymentPage.Instance.ClickBottomContinueButton();
            ShippingPaymentPage.Instance.ClickTopContinueButton();
        }
    }
}
