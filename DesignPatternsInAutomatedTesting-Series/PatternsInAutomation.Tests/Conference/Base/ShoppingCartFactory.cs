using OpenQA.Selenium;
using PatternsInAutomation.Tests.Conference.Pages.Checkout;
using PatternsInAutomation.Tests.Conference.Pages.Item;
using PatternsInAutomation.Tests.Conference.Pages.ShippingAddress;
using PatternsInAutomation.Tests.Conference.Pages.SignIn;

namespace PatternsInAutomation.Tests.Conference.Base
{
    public class ShoppingCartFactory : IFactory<ShoppingCart>
    {
        private readonly IWebDriver driver;

        public ShoppingCartFactory(IWebDriver driver)
        {
            this.driver = driver;
        }

        public ShoppingCart Create()
        {
            var itemPage = new ItemPage(this.driver);
            var signInPage = new SignInPage(this.driver);
            var checkoutPage = new CheckoutPage(this.driver);
            var shippingAddressPage = new ShippingAddressPage(this.driver);
            var purchaseFacade = new ShoppingCart(itemPage, signInPage, checkoutPage, shippingAddressPage);
            return purchaseFacade;
        }
    }    
}