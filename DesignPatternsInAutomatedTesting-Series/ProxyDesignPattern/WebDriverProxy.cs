// <copyright file="WebDriverProxy.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
