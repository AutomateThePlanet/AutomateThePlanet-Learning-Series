// <copyright file="AmazonPurchaseTests.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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

using AdvancedBehavioursDesignPattern.Behaviours;
using AdvancedBehavioursDesignPattern.Behaviours.Core;
using AdvancedBehavioursDesignPattern.Core;
using AdvancedBehavioursDesignPattern.Data;
using AdvancedBehavioursDesignPattern.Pages.ItemPage;
using AdvancedBehavioursDesignPattern.Pages.PlaceOrderPage;
using AdvancedBehavioursDesignPattern.Pages.PreviewShoppingCartPage;
using AdvancedBehavioursDesignPattern.Pages.ShippingAddressPage;
using AdvancedBehavioursDesignPattern.Pages.ShippingPaymentPage;
using AdvancedBehavioursDesignPattern.Pages.SignInPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Unity;
using Unity.Lifetime;

namespace AdvancedBehavioursDesignPattern;

[TestClass]
public class AmazonPurchaseTests
{
    private static readonly IUnityContainer Container = new UnityContainer();

    [TestInitialize]
    public void SetupTest()
    {
        Driver.StartBrowser();
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
        Container.RegisterInstance<IWebDriver>(Driver.Browser);
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
            })
        {
            CouponCode = "99PERDIS"
        };
        var clientLoginInfo = new ClientLoginInfo()
        {
            Email = "g3984159@trbvm.com",
            Password = "ASDFG_12345"
        };
        BehaviourExecutor.Execute(
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
}