using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ProxyDesignPattern
{
    public class WebDriverProxy : IWebDriver
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _webDriverWait;

        public WebDriverProxy(IWebDriver driver)
        {
            _driver = driver;
            var timeout = TimeSpan.FromSeconds(30);
            var sleepInterval = TimeSpan.FromSeconds(2);
            _webDriverWait = new WebDriverWait(new SystemClock(), _driver, timeout, sleepInterval);
        }

        public IWebElement FindElement(By @by)
        {
            return _webDriverWait.Until(ExpectedConditions.ElementExists(@by));
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            return _webDriverWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(@by));

        }

        public void Dispose()
        {
            _driver.Dispose();
        }

        public void Close()
        {
            _driver.Close();
        }

        public void Quit()
        {
            _driver.Quit();
        }

        public IOptions Manage()
        {
            return _driver.Manage();
        }

        public INavigation Navigate()
        {
            return _driver.Navigate();
        }

        public ITargetLocator SwitchTo()
        {
            return _driver.SwitchTo();
        }

        public string Url 
        { 
            get => _driver.Url;
            set => _driver.Url = value;
        }

        public string Title
        {
            get => _driver.Title;
        }

        public string PageSource
        {
            get => _driver.PageSource;
        }

        public string CurrentWindowHandle
        {
            get => _driver.CurrentWindowHandle;
        }

        public ReadOnlyCollection<string> WindowHandles
        {
            get => _driver.WindowHandles;
        }
    }
}
