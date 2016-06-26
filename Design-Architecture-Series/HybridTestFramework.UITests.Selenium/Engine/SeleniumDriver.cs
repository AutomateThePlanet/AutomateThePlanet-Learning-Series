using HybridTestFramework.UITests.Core;
using Microsoft.Practices.Unity;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace HybridTestFramework.UITests.Selenium.Engine
{
    public partial class SeleniumDriver : IDriver
    {
        private IWebDriver driver;
        private IUnityContainer container;
        private BrowserSettings browserSettings;
        private readonly ElementFinderService elementFinderService;

        public SeleniumDriver(IUnityContainer container, BrowserSettings browserSettings)
        {
            this.container = container;
            this.browserSettings = browserSettings;
            this.ResolveBrowser(browserSettings);
            this.elementFinderService = new ElementFinderService(container);
            driver.Manage().Timeouts().ImplicitlyWait(
                TimeSpan.FromSeconds(browserSettings.ElementsWaitTimeout));
        }

        private void ResolveBrowser(BrowserSettings browserSettings)
        {
            switch (browserSettings.Type)
            {
                case Browsers.NotSet:
                    break;
                case Browsers.Chrome:
                    break;
                case Browsers.Firefox:
                    this.driver = new FirefoxDriver();
                    break;
                case Browsers.InternetExplorer:
                    break;
                case Browsers.Safari:
                    break;
                case Browsers.NoBrowser:
                    break;
                default:
                    break;
            }
        }       
    }
}