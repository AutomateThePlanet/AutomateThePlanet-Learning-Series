// <copyright file="CreatePurchaseSteps.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>

using Microsoft.Practices.Unity;
using SpecflowBehavioursDesignPattern.Base;
using SpecflowBehavioursDesignPattern.Data;
using SpecflowBehavioursDesignPattern.Pages.ItemPage;
using SpecflowBehavioursDesignPattern.Pages.PlaceOrderPage;
using SpecflowBehavioursDesignPattern.Pages.PreviewShoppingCartPage;
using SpecflowBehavioursDesignPattern.Pages.ShippingAddressPage;
using SpecflowBehavioursDesignPattern.Pages.ShippingPaymentPage;
using SpecflowBehavioursDesignPattern.Pages.SignInPage;
using TechTalk.SpecFlow;

namespace SpecflowBehavioursDesignPattern
{
    [Binding]
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
            var shippingPaymentPage = UnityContainerFactory.GetContainer().Resolve<ShippingPaymentPage>();
            shippingPaymentPage.ClickTopContinueButton();
        }
        
        [Then(@"assert that order total price = ""([^""]*)""")]
        public void AssertOrderTotalPrice(string itemPrice)
        {
            var placeOrderPage = UnityContainerFactory.GetContainer().Resolve<PlaceOrderPage>();
            double totalPrice = double.Parse(itemPrice);
            placeOrderPage.AssertOrderTotalPrice(totalPrice);
        }
    }
}