using HybridTestFramework.UITests.Core;
using HybridTestFramework.UITests.Core.Controls;
using HybridTestFramework.UITests.Core.Extensions;
using HybridTestFramework.UITests.Selenium.Controls;
using HybridTestFramework.UITests.Selenium.Engine;
using Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SeleniumDriverImplementation
{
    [TestClass]
    public class BingTests
    {
        private IDriver _driver;
        private IUnityContainer _container;

        [TestInitialize]
        public void SetupTest()
        {
            _container = new UnityContainer();
            _container.RegisterType<IDriver, SeleniumDriver>();
            _container.RegisterType<INavigationService, SeleniumDriver>();
            _container.RegisterType<IBrowser, SeleniumDriver>();
            _container.RegisterType<ICookieService, SeleniumDriver>();
            _container.RegisterType<IDialogService, SeleniumDriver>();
            _container.RegisterType<IElementFinder, SeleniumDriver>();
            _container.RegisterType<IJavaScriptInvoker, SeleniumDriver>();
            _container.RegisterType<IElement, Element>();
            _container.RegisterType<IButton, Button>();
            _container.RegisterInstance(_container);
            _container.RegisterInstance(BrowserSettings.DefaultFirefoxSettings);
            _driver = _container.Resolve<IDriver>();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void NavigateToAutomateThePlanet()
        {
            _driver.NavigateByAbsoluteUrl(@"http://automatetheplanet.com/");
            var blogButton = _driver.Find<IButton>(AdvancedBy.Xpath("//*[@id='tve_editor']/div[2]/div[4]/div/div/div/div/div/a"));
            blogButton.Hover();
            Console.WriteLine(blogButton.Content);
            _driver.NavigateByAbsoluteUrl(@"http://automatetheplanet.com/download-source-code/");
            _driver.ClickBackButton();
            Console.WriteLine(_driver.Title);
        }
    }
}