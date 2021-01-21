using Microsoft.Practices.Unity;
using OpenQA.Selenium;
using PerfectSystemTestsDesign.Base;
using PerfectSystemTestsDesign.Behaviours;
using PerfectSystemTestsDesign.Core;
using PerfectSystemTestsDesign.Pages.ItemPage;
using PerfectSystemTestsDesign.Pages.PlaceOrderPage;
using PerfectSystemTestsDesign.Pages.PreviewShoppingCartPage;
using PerfectSystemTestsDesign.Pages.ShippingAddressPage;
using PerfectSystemTestsDesign.Pages.ShippingPaymentPage;
using PerfectSystemTestsDesign.Pages.SignInPage;
using TechTalk.SpecFlow;

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
            UnityContainerFactory.GetContainer().RegisterType<ShoppingCart>(new ContainerControlledLifetimeManager());
            UnityContainerFactory.GetContainer().RegisterInstance<IWebDriver>(PerfectSystemTestsDesign.Core.Driver.Browser);
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