using System;
using System.Linq;
using PatternsInAutomatedTests.Advanced.Ebay.Data;
using PatternsInAutomatedTests.Advanced.Ebay.Pages.CheckoutPage;
using PatternsInAutomatedTests.Advanced.Ebay.Pages.ItemPage;
using PatternsInAutomatedTests.Advanced.Ebay.Pages.ShippingAddressPage;
using PatternsInAutomatedTests.Advanced.Ebay.Pages.SignInPage;

namespace PatternsInAutomatedTests.Advanced.Ebay
{
    public class PurchaseFacade
    {
        private ItemPage itemPage;
        private CheckoutPage checkoutPage;
        private ShippingAddressPage shippingAddressPage;
        private SignInPage signInPage;

        public ItemPage ItemPage 
        {
            get
            {
                if (itemPage == null)
                {
                    itemPage = new ItemPage();
                }
                return itemPage;
            }
        }

        public SignInPage SignInPage
        {
            get
            {
                if (signInPage == null)
                {
                    signInPage = new SignInPage();
                }
                return signInPage;
            }
        }

        public CheckoutPage CheckoutPage
        {
            get
            {
                if (checkoutPage == null)
                {
                    checkoutPage = new CheckoutPage();
                }
                return checkoutPage;
            }
        }

        public ShippingAddressPage ShippingAddressPage
        {
            get
            {
                if (shippingAddressPage == null)
                {
                    shippingAddressPage = new ShippingAddressPage();
                }
                return shippingAddressPage;
            }
        }

        public void PurchaseItem(string item, string itemPrice, ClientInfo clientInfo)
        {
            this.ItemPage.Navigate(item);
            this.ItemPage.Validate().Price(itemPrice);
            this.ItemPage.ClickBuyNowButton();
            this.SignInPage.ClickContinueAsGuestButton();
            this.ShippingAddressPage.FillShippingInfo(clientInfo);
            this.ShippingAddressPage.Validate().Subtotal(itemPrice);
            this.ShippingAddressPage.ClickContinueButton();
            this.CheckoutPage.Validate().Subtotal(itemPrice);
        }
    }
}
