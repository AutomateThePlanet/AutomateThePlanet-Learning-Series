using System;
using System.Linq;
using PatternsInAutomation.Tests.Advanced.Ebay.Data;
using PatternsInAutomation.Tests.Advanced.Ebay.Pages.CheckoutPage;
using PatternsInAutomation.Tests.Advanced.Ebay.Pages.ItemPage;
using PatternsInAutomation.Tests.Advanced.Ebay.Pages.ShippingAddressPage;
using PatternsInAutomation.Tests.Advanced.Ebay.Pages.SignInPage;

namespace PatternsInAutomation.Tests.Advanced.Ebay
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
