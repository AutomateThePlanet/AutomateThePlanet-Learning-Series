// <copyright file="SpecflowHooks.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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

using OpenQA.Selenium;
using SpecflowBehavioursDesignPattern.Base;
using SpecflowBehavioursDesignPattern.Behaviours.StepsBehaviours;
using SpecflowBehavioursDesignPattern.Core;
using SpecflowBehavioursDesignPattern.Data;
using SpecflowBehavioursDesignPattern.Pages.ItemPage;
using SpecflowBehavioursDesignPattern.Pages.PlaceOrderPage;
using SpecflowBehavioursDesignPattern.Pages.PreviewShoppingCartPage;
using SpecflowBehavioursDesignPattern.Pages.ShippingAddressPage;
using SpecflowBehavioursDesignPattern.Pages.ShippingPaymentPage;
using SpecflowBehavioursDesignPattern.Pages.SignInPage;
using TechTalk.SpecFlow;
using Unity;
using Unity.Lifetime;

namespace PerfectSystemTestsDesign.Specflow
{
    [Binding]
    public sealed class SpecflowHooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Driver.StartBrowser(BrowserTypes.Chrome);
            UnityContainerFactory.GetContainer().RegisterType<ItemPage>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<PreviewShoppingCartPage>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<SignInPage>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<ShippingAddressPage>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<ShippingPaymentPage>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<PlaceOrderPage>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<ItemPageBuyBehaviour>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<ItemPageNavigationBehaviour>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<PlaceOrderPageAssertFinalAmountsBehaviour>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<PreviewShoppingCartPageProceedBehaviour>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<ShippingAddressPageContinueBehaviour>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<ShippingAddressPageFillDifferentBillingBehaviour>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<ShippingAddressPageFillShippingBehaviour>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<ShippingPaymentPageContinueBehaviour>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterType<SignInPageLoginBehaviour>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterInstance<IWebDriver>(Driver.Browser);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Driver.StopBrowser();
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
        }

        [AfterScenario]
        public static void AfterScenario()
        {
        }
    }
}