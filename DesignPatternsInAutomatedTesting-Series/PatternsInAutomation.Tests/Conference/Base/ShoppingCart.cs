using PatternsInAutomation.Tests.Conference.Data;
using PatternsInAutomation.Tests.Conference.Pages.Checkout;
using PatternsInAutomation.Tests.Conference.Pages.ShippingAddress;
using PatternsInAutomation.Tests.Conference.Pages.Item;

namespace PatternsInAutomation.Tests.Conference.Base
{
    public class ShoppingCart
    {
        private readonly IItemPage itemPage;

        private readonly ISignInPage signInPage;

        private readonly ICheckoutPage checkoutPage;

        private readonly IShippingAddressPage shippingAddressPage;

        public ShoppingCart(IItemPage itemPage, ISignInPage signInPage, ICheckoutPage checkoutPage, IShippingAddressPage shippingAddressPage)
        {
            this.itemPage = itemPage;
            this.signInPage = signInPage;
            this.checkoutPage = checkoutPage;
            this.shippingAddressPage = shippingAddressPage;
        }

        public void PurchaseItem(string item, double itemPrice, ClientInfo clientInfo)
        {
            this.itemPage.Open(item);
            this.itemPage.AssertPrice(itemPrice);
            this.itemPage.ClickBuyNowButton();
            this.signInPage.ClickContinueAsGuestButton();
            this.shippingAddressPage.FillShippingInfo(clientInfo);
            this.shippingAddressPage.AssertSubtotalAmount(itemPrice);
            this.shippingAddressPage.ClickContinueButton();
            this.checkoutPage.AssertSubtotal(itemPrice);
        }
    }
}
