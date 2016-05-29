// <copyright file="AmazonPurchaseTests.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PerfectSystemTestsDesign.Base;
using PerfectSystemTestsDesign.Behaviours;
using PerfectSystemTestsDesign.Core;
using PerfectSystemTestsDesign.Data;
using PerfectSystemTestsDesign.Pages.ItemPage;
using PerfectSystemTestsDesign.Pages.PlaceOrderPage;
using PerfectSystemTestsDesign.Pages.PreviewShoppingCartPage;
using PerfectSystemTestsDesign.Pages.ShippingAddressPage;
using PerfectSystemTestsDesign.Pages.ShippingPaymentPage;
using PerfectSystemTestsDesign.Pages.SignInPage;

namespace PerfectSystemTestsDesign
{
    [TestClass]
    public class AmazonPurchaseTests
    {
        private static readonly IUnityContainer container = new UnityContainer();

        [TestInitialize]
        public void SetupTest()
        {
            Driver.StartBrowser();
            container.RegisterType<ItemPage>(new ContainerControlledLifetimeManager());
            container.RegisterType<PreviewShoppingCartPage>(new ContainerControlledLifetimeManager());
            container.RegisterType<SignInPage>(new ContainerControlledLifetimeManager());
            container.RegisterType<ShippingAddressPage>(new ContainerControlledLifetimeManager());
            container.RegisterType<ShippingPaymentPage>(new ContainerControlledLifetimeManager());
            container.RegisterType<PlaceOrderPage>(new ContainerControlledLifetimeManager());
            container.RegisterType<ItemPageBuyBehaviour>(new ContainerControlledLifetimeManager());
            container.RegisterType<ItemPageNavigationBehaviour>(new ContainerControlledLifetimeManager());
            container.RegisterType<PlaceOrderPageAssertFinalAmountsBehaviour>(new ContainerControlledLifetimeManager());
            container.RegisterType<PreviewShoppingCartPageProceedBehaviour>(new ContainerControlledLifetimeManager());
            container.RegisterType<ShippingAddressPageContinueBehaviour>(new ContainerControlledLifetimeManager());
            container.RegisterType<ShippingAddressPageFillDifferentBillingBehaviour>(new ContainerControlledLifetimeManager());
            container.RegisterType<ShippingAddressPageFillShippingBehaviour>(new ContainerControlledLifetimeManager());
            container.RegisterType<ShippingPaymentPageContinueBehaviour>(new ContainerControlledLifetimeManager());
            container.RegisterType<SignInPageLoginBehaviour>(new ContainerControlledLifetimeManager());
            container.RegisterType<ShoppingCart>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<IWebDriver>(PerfectSystemTestsDesign.Core.Driver.Browser);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            Driver.StopBrowser();
        }

        [TestMethod]
        public void Purchase_SimpleBehaviourEngine()
        {
            var itemUrl = "/Selenium-Testing-Cookbook-Gundecha-Unmesh/dp/1849515743";
            var itemPrice = "40.49";
            var clientPurchaseInfo = new ClientPurchaseInfo(
                new ClientAddressInfo()
                {
                    FullName = "John Smith",
                    Country = "United States",
                    Address1 = "950 Avenue of the Americas",
                    State = "New York",
                    City = "New York City",
                    Zip = "10001-2121",
                    Phone = "00164644885569"
                });
            clientPurchaseInfo.CouponCode = "99PERDIS";
            var clientLoginInfo = new ClientLoginInfo()
            {
                Email = "g3984159@trbvm.com",
                Password = "ASDFG_12345"
            };
            PerfectSystemTestsDesign.Behaviours.Core.BehaviourExecutor.Execute(
                new ItemPageNavigationBehaviour(itemUrl),
                new ItemPageBuyBehaviour(),
                new PreviewShoppingCartPageProceedBehaviour(),
                new SignInPageLoginBehaviour(clientLoginInfo),
                new ShippingAddressPageFillShippingBehaviour(clientPurchaseInfo),
                new ShippingAddressPageFillDifferentBillingBehaviour(clientPurchaseInfo),
                new ShippingAddressPageContinueBehaviour(),
                new ShippingPaymentPageContinueBehaviour(),
                new PlaceOrderPageAssertFinalAmountsBehaviour(itemPrice));
        }

        [TestMethod]
        public void Purchase_ShoppingCartFacade()
        {
            var itemUrl = "/Selenium-Testing-Cookbook-Gundecha-Unmesh/dp/1849515743";
            var itemPrice = "40.49";
            var clientPurchaseInfo = new ClientPurchaseInfo(
                new ClientAddressInfo()
                {
                    FullName = "John Smith",
                    Country = "United States",
                    Address1 = "950 Avenue of the Americas",
                    State = "New York",
                    City = "New York City",
                    Zip = "10001-2121",
                    Phone = "00164644885569"
                });
            clientPurchaseInfo.CouponCode = "99PERDIS";
            var clientLoginInfo = new ClientLoginInfo()
            {
                Email = "g3984159@trbvm.com",
                Password = "ASDFG_12345"
            };
            var shoppingCart = container.Resolve<ShoppingCart>();
            shoppingCart.PurchaseItem(itemUrl, itemPrice, clientLoginInfo, clientPurchaseInfo);
        }
    }
}