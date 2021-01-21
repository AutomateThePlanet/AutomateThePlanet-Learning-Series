// <copyright file="CreatePurchaseSteps - Copy.cs" company="Automate The Planet Ltd.">
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
using SpecflowBehavioursDesignPattern.Behaviours.StepsBehaviours;
using SpecflowBehavioursDesignPattern.Data;
using SpecflowBehavioursDesignPattern.Pages.ShippingAddressPage;
using TechTalk.SpecFlow;

namespace SpecflowBehavioursDesignPattern
{
    [Binding]
    public class CreatePurchaseStepsBehaviours
    {
        [When(@"I navigate to ""([^""]*)""")]
        public void NavigateToItemUrl(string itemUrl)
        {
            new ItemPageNavigationBehaviour(itemUrl).Execute();
        }
        
        [When(@"I click the 'buy now' button")]
        public void ClickBuyNowButtonItemPage()
        {
            new ItemPageBuyBehaviour().Execute();
        }

        [When(@"then I click the 'proceed to checkout' button")]
        public void ClickProceedToCheckoutButtonPreviewShoppingCartPage()
        {
            new PreviewShoppingCartPageProceedBehaviour().Execute();
        }
        
        [When(@"I login with email = ""([^""]*)"" and pass = ""([^""]*)""")]
        public void LoginWithEmailAndPass(string email, string password)
        {
            new SignInPageLoginBehaviour(
                new ClientLoginInfo()
                {
                    Email = email,
                    Password = password
                })
                  .Execute();
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
            new ShippingAddressPageFillShippingBehaviour(clientPurchaseInfo).Execute();
        }
        
        [When(@"I choose to fill different billing, full name = ""([^""]*)"", country = ""([^""]*)"", Adress = ""([^""]*)"", city = ""([^""]*)"", state = ""([^""]*)"", zip = ""([^""]*)"" and phone = ""([^""]*)""")]
        public void FillDifferentBillingInfo(string fullName, string country, string address, string state, string city, string zip, string phone)
        {
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
            new ShippingAddressPageFillDifferentBillingBehaviour(clientPurchaseInfo).Execute();
        }
        
        [When(@"click shipping address page 'continue' button")]
        public void ClickContinueButtonShippingAddressPage()
        {
            new ShippingPaymentPageContinueBehaviour().Execute();
        }
        
        [When(@"click shipping payment top 'continue' button")]
        public void WhenClickTopPaymentButton()
        {
            new ShippingPaymentPageContinueBehaviour().Execute();
        }
        
        [Then(@"assert that order total price = ""([^""]*)""")]
        public void AssertOrderTotalPrice(string itemPrice)
        {
            new PlaceOrderPageAssertFinalAmountsBehaviour(itemPrice).Execute();
        }
    }
}