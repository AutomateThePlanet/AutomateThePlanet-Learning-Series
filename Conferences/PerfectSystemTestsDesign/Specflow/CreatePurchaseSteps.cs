using Microsoft.Practices.Unity;
using PerfectSystemTestsDesign.Base;
using PerfectSystemTestsDesign.Data;
using PerfectSystemTestsDesign.Pages.ItemPage;
using PerfectSystemTestsDesign.Pages.PlaceOrderPage;
using PerfectSystemTestsDesign.Pages.PreviewShoppingCartPage;
using PerfectSystemTestsDesign.Pages.ShippingAddressPage;
using PerfectSystemTestsDesign.Pages.ShippingPaymentPage;
using PerfectSystemTestsDesign.Pages.SignInPage;
using TechTalk.SpecFlow;

namespace PerfectSystemTestsDesign.Specflow
{
    //[Binding]
    public class CreatePurchaseSteps
    {
        [When(@"I navigate to ""([^""]*)""")]
        public void NavigateToItemUrl(string itemUrl)
        {
            var itemPage = UnityContainerFactory.GetContainer().Resolve<ItemPage>();
            itemPage.Navigate(itemUrl);
        }
        
        [When(@"I click the 'buy now' button")]
        public void ClickBuyNowButtonItemPage()
        {
            var itemPage = UnityContainerFactory.GetContainer().Resolve<ItemPage>();
            itemPage.ClickBuyNowButton();
        }

        [When(@"then I click the 'proceed to checkout' button")]
        public void ClickProceedToCheckoutButtonPreviewShoppingCartPage()
        {
            var previewShoppingCartPage = UnityContainerFactory.GetContainer().Resolve<PreviewShoppingCartPage>();
            previewShoppingCartPage.ClickProceedToCheckoutButton();
        }
        
        [When(@"the login page loads")]
        public void SignInPageLoads()
        {
            var signInPage = UnityContainerFactory.GetContainer().Resolve<SignInPage>();
            signInPage.WaitForPageToLoad();
        }
        
        [When(@"I login with email = ""([^""]*)"" and pass = ""([^""]*)""")]
        public void LoginWithEmailAndPass(string email, string password)
        {
            var signInPage = UnityContainerFactory.GetContainer().Resolve<SignInPage>();
            signInPage.Login(email, password);
        }

        [When(@"the shipping address page loads")]
        public void ShippingPageLoads()
        {
            var shippingAddressPage = UnityContainerFactory.GetContainer().Resolve<ShippingAddressPage>();
            shippingAddressPage.WaitForPageToLoad();
        }
        
        [When(@"I type full name = ""([^""]*)"", country = ""([^""]*)"", Adress = ""([^""]*)"", city = ""([^""]*)"", state = ""([^""]*)"", zip = ""([^""]*)"" and phone = ""([^""]*)""")]
        public void FillShippingInfo(string fullName, string country, string address, string state, string city, string zip, string phone)
        {
            var shippingAddressPage = UnityContainerFactory.GetContainer().Resolve<ShippingAddressPage>();
            var clientPurchaseInfo = new ClientPurchaseInfo(
                new ClientAddressInfo()
                {
                    FullName = fullName,
                    Country = country,
                    Address1 = address,
                    State = state,
                    City = city,
                    Zip = zip,
                    Phone = phone
                });
            shippingAddressPage.FillShippingInfo(clientPurchaseInfo);
        }
        
        [When(@"I choose to fill different billing, full name = ""([^""]*)"", country = ""([^""]*)"", Adress = ""([^""]*)"", city = ""([^""]*)"", state = ""([^""]*)"", zip = ""([^""]*)"" and phone = ""([^""]*)""")]
        public void FillDifferentBillingInfo(string fullName, string country, string address, string state, string city, string zip, string phone)
        {
            var shippingAddressPage = UnityContainerFactory.GetContainer().Resolve<ShippingAddressPage>();
            var shippingPaymentPage = UnityContainerFactory.GetContainer().Resolve<ShippingPaymentPage>();
            var clientPurchaseInfo = new ClientPurchaseInfo(
                new ClientAddressInfo()
                {
                    FullName = fullName,
                    Country = country,
                    Address1 = address,
                    State = state,
                    City = city,
                    Zip = zip,
                    Phone = phone
                });
            shippingAddressPage.ClickDifferentBillingCheckBox(clientPurchaseInfo);
            shippingAddressPage.ClickContinueButton();
            shippingPaymentPage.ClickBottomContinueButton();
            shippingAddressPage.FillBillingInfo(clientPurchaseInfo);
        }
        
        [When(@"click shipping address page 'continue' button")]
        public void ClickContinueButtonShippingAddressPage()
        {
            var shippingAddressPage = UnityContainerFactory.GetContainer().Resolve<ShippingAddressPage>();
            shippingAddressPage.ClickContinueButton();
        }
        
        [When(@"click shipping payment top 'continue' button")]
        public void WhenClickTopPaymentButton()
        {
            var shippingPaymentPage = PerfectSystemTestsDesign.Base.UnityContainerFactory.GetContainer().Resolve<ShippingPaymentPage>();
            shippingPaymentPage.ClickTopContinueButton();
        }
        
        [Then(@"assert that order total price = ""([^""]*)""")]
        public void AssertOrderTotalPrice(string itemPrice)
        {
            var placeOrderPage = PerfectSystemTestsDesign.Base.UnityContainerFactory.GetContainer().Resolve<PlaceOrderPage>();
            double totalPrice = double.Parse(itemPrice);
            placeOrderPage.AssertOrderTotalPrice(totalPrice);
        }
    }
}