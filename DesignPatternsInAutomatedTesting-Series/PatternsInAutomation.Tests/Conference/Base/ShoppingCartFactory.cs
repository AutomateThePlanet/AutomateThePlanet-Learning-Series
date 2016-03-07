using OpenQA.Selenium;
using PatternsInAutomatedTests.Conference.Pages.Checkout;
using PatternsInAutomatedTests.Conference.Pages.Item;
using PatternsInAutomatedTests.Conference.Pages.ShippingAddress;
using PatternsInAutomatedTests.Conference.Pages.SignIn;

namespace PatternsInAutomatedTests.Conference.Base
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