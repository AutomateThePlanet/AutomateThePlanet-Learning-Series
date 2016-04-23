// <copyright file="AmazonPurchase_UnityNullObjectTests.cs" company="Automate The Planet Ltd.">
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.Core;
using Microsoft.Practices.Unity;
using PatternsInAutomatedTests.Advanced.NullObject.Base;
using PatternsInAutomatedTests.Advanced.NullObject.Data;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.ItemPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.PlaceOrderPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.PreviewShoppingCartPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.ShippingAddressPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.ShippingPaymentPage;
using PatternsInAutomatedTests.Advanced.NullObject.Pages.SignInPage;
using PatternsInAutomatedTests.Advanced.NullObject.Strategies;

namespace PatternsInAutomatedTests.Advanced.NullObject.Tests
{
    [TestClass]
    public class AmazonPurchase_UnityNullObjectTests
    {
        private static IUnityContainer container = new UnityContainer();

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
            container.RegisterType<PurchaseContext>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPurchasePromotionalCodeStrategy, NullPurchasePromotionalCodeStrategy>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<IWebDriver>(Driver.Browser);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            Driver.StopBrowser();
        }

        [TestMethod]
        public void Purchase_SeleniumTestingToolsCookbook()
        {
            string itemUrl = "/Selenium-Testing-Cookbook-Gundecha-Unmesh/dp/1849515743";
            string itemPrice = "40.49";
            ClientPurchaseInfo clientPurchaseInfo = new ClientPurchaseInfo(
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
            ClientLoginInfo clientLoginInfo = new ClientLoginInfo()
            {
                Email = "g3984159@trbvm.com",
                Password = "ASDFG_12345"
            };
            container.RegisterInstance<IPurchasePromotionalCodeStrategy>(
                new UiPurchasePromotionalCodeStrategy(container.Resolve<PlaceOrderPage>(), 40.49));
            var purchaseContext = container.Resolve<PurchaseContext>();
            purchaseContext.PurchaseItem(itemUrl, itemPrice, clientLoginInfo, clientPurchaseInfo);
        }
    }
}