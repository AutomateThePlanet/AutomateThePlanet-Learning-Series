using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Controls;
using HybridTestFramework.UITests.Selenium.Controls;
using HybridTestFramework.UITests.Selenium.Engine;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SeleniumDriverImplementation
{
    [TestClass]
    public class BingTests
    {
        private IDriver driver;
        private IUnityContainer container;

        [TestInitialize]
        public void SetupTest()
        {
            this.container = new UnityContainer();
            this.container.RegisterType<IDriver, SeleniumDriver>();
            this.container.RegisterType<INavigationService, SeleniumDriver>();
            this.container.RegisterType<IBrowser, SeleniumDriver>();
            this.container.RegisterType<ICookieService, SeleniumDriver>();
            this.container.RegisterType<IDialogService, SeleniumDriver>();
            this.container.RegisterType<IElementFinder, SeleniumDriver>();
            this.container.RegisterType<IJavaScriptInvoker, SeleniumDriver>();
            this.container.RegisterType<IElement, Element>();
            this.container.RegisterType<IButton, Button>();
            this.container.RegisterInstance<IUnityContainer>(this.container);
            this.container.RegisterInstance<BrowserSettings>(BrowserSettings.DefaultFirefoxSettings);
            this.driver = this.container.Resolve<IDriver>();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.driver.Quit();
        }

        [TestMethod]
        public void NavigateToAutomateThePlanet()
        {
            this.driver.NavigateByAbsoluteUrl(@"http://automatetheplanet.com/");
            var blogButton = this.driver.Find<IButton>(By.Xpath("//*[@id='tve_editor']/div[2]/div[4]/div/div/div/div/div/a"));
            blogButton.Hover();
            Console.WriteLine(blogButton.Content);
            this.driver.NavigateByAbsoluteUrl(@"http://automatetheplanet.com/download-source-code/");
            this.driver.ClickBackButton();
            Console.WriteLine(this.driver.Title);
        }
    }
}