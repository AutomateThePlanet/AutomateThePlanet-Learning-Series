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
        private static readonly IUnityContainer container = new UnityContainer();

        [TestInitialize]
        public void SetupTest()
        {
            AdvancedBehavioursDesignPatternPartTwo.Core.Driver.StartBrowser();
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
            container.RegisterInstance<IWebDriver>(AdvancedBehavioursDesignPatternPartTwo.Core.Driver.Browser);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            AdvancedBehavioursDesignPatternPartTwo.Core.Driver.StopBrowser();
        }

        [TestMethod]
        public void Purchase_SimpleBehaviourEngine()
        {
            string itemUrl = "/Selenium-Testing-Cookbook-Gundecha-Unmesh/dp/1849515743";
            BehaviorEngine.Execute(
                new NavigatePageBehaviorDefinition(itemUrl));
        }
    }
}