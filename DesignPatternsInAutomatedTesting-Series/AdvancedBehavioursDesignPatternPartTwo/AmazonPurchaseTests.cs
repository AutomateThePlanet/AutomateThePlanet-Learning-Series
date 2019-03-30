// <copyright file="AmazonPurchaseTests.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
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

using AdvancedBehavioursDesignPatternPartTwo.Behaviours;
using AdvancedBehavioursDesignPatternPartTwo.Behaviours.Core;
using AdvancedBehavioursDesignPatternPartTwo.Data;
using AdvancedBehavioursDesignPatternPartTwo.Pages.ItemPage;
using AdvancedBehavioursDesignPatternPartTwo.Pages.PlaceOrderPage;
using AdvancedBehavioursDesignPatternPartTwo.Pages.PreviewShoppingCartPage;
using AdvancedBehavioursDesignPatternPartTwo.Pages.ShippingAddressPage;
using AdvancedBehavioursDesignPatternPartTwo.Pages.ShippingPaymentPage;
using AdvancedBehavioursDesignPatternPartTwo.Pages.SignInPage;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace AdvancedBehavioursDesignPatternPartTwo
{
    [TestClass]
    public class AmazonPurchaseTests
    {
        private static readonly IUnityContainer Container = new UnityContainer();

        [TestInitialize]
        public void SetupTest()
        {
            Core.Driver.StartBrowser();
            Container.RegisterType<ItemPage>(new ContainerControlledLifetimeManager());
            Container.RegisterType<PreviewShoppingCartPage>(new ContainerControlledLifetimeManager());
            Container.RegisterType<SignInPage>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ShippingAddressPage>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ShippingPaymentPage>(new ContainerControlledLifetimeManager());
            Container.RegisterType<PlaceOrderPage>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ItemPageBuyBehaviour>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ItemPageNavigationBehaviour>(new ContainerControlledLifetimeManager());
            Container.RegisterType<PlaceOrderPageAssertFinalAmountsBehaviour>(new ContainerControlledLifetimeManager());
            Container.RegisterType<PreviewShoppingCartPageProceedBehaviour>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ShippingAddressPageContinueBehaviour>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ShippingAddressPageFillDifferentBillingBehaviour>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ShippingAddressPageFillShippingBehaviour>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ShippingPaymentPageContinueBehaviour>(new ContainerControlledLifetimeManager());
            Container.RegisterType<SignInPageLoginBehaviour>(new ContainerControlledLifetimeManager());
            Container.RegisterInstance<IWebDriver>(Core.Driver.Browser);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            Core.Driver.StopBrowser();
        }

        [TestMethod]
        public void Purchase_SimpleBehaviourEngine()
        {
            var itemUrl = "/Selenium-Testing-Cookbook-Gundecha-Unmesh/dp/1849515743";
            BehaviorEngine.Execute(
                new NavigatePageBehaviorDefinition(itemUrl));
        }
    }
}