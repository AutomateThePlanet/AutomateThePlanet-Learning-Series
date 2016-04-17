using PatternsInAutomatedTests.Advanced.NullObject.Data;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.ItemPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.PlaceOrderPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.PreviewShoppingCartPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.ShippingAddressPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.ShippingPaymentPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.SignInPage;

namespace PatternsInAutomatedTests.Advanced.NullObject.Base
{
    public class PurchaseContext
    {
        private readonly IPurchasePromotionalCodeStrategy purchasePromotionalCodeStrategy;
        private readonly ItemPage itemPage;
        private readonly PreviewShoppingCartPage previewShoppingCartPage;
        private readonly SignInPage signInPage;
        private readonly ShippingAddressPage shippingAddressPage;
        private readonly ShippingPaymentPage shippingPaymentPage;
        private readonly PlaceOrderPage placeOrderPage;

        public PurchaseContext(
            IPurchasePromotionalCodeStrategy purchasePromotionalCodeStrategy,
            ItemPage itemPage, 
            PreviewShoppingCartPage previewShoppingCartPage, 
            SignInPage signInPage, 
            ShippingAddressPage shippingAddressPage,
            ShippingPaymentPage shippingPaymentPage,
            PlaceOrderPage placeOrderPage)
        {
            this.purchasePromotionalCodeStrategy = purchasePromotionalCodeStrategy;
            this.itemPage = itemPage;
            this.previewShoppingCartPage = previewShoppingCartPage;
            this.signInPage = signInPage;
            this.shippingAddressPage = shippingAddressPage;
            this.shippingPaymentPage = shippingPaymentPage;
            this.placeOrderPage = placeOrderPage;
        }

        public void PurchaseItem(string itemUrl, string itemPrice, ClientLoginInfo clientLoginInfo, ClientPurchaseInfo clientPurchaseInfo)
        {
            this.itemPage.Navigate(itemUrl);
            this.itemPage.ClickBuyNowButton();
            this.previewShoppingCartPage.ClickProceedToCheckoutButton();
            this.signInPage.Login(clientLoginInfo.Email, clientLoginInfo.Password);
            this.shippingAddressPage.FillShippingInfo(clientPurchaseInfo);
            this.shippingAddressPage.ClickDifferentBillingCheckBox(clientPurchaseInfo);
            this.shippingAddressPage.ClickContinueButton();
            this.shippingPaymentPage.ClickBottomContinueButton();
            this.shippingAddressPage.FillBillingInfo(clientPurchaseInfo);
            this.shippingAddressPage.ClickContinueButton();
            this.shippingPaymentPage.ClickTopContinueButton();
            this.purchasePromotionalCodeStrategy.AssertPromotionalCodeDiscount();
            var couponDiscount = this.purchasePromotionalCodeStrategy.GetPromotionalCodeDiscountAmount();
            double totalPrice = double.Parse(itemPrice);
            this.placeOrderPage.AssertOrderTotalPrice(totalPrice, couponDiscount);
            this.purchasePromotionalCodeStrategy.AssertPromotionalCodeDiscount();
        }
    }
}
