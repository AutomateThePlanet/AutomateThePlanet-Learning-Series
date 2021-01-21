// <copyright file="DriverAdapter.cs" company="Automate The Planet Ltd.">
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
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CompositeDesignPattern
{
    public class DriverAdapter : IDriver
    {
        private readonly IWebDriver _driver;

        public DriverAdapter(IWebDriver driver)
        {
            _driver = driver;
        }

        public void GoToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public Uri Url 
        { 
            get => new Uri(_driver.Url);
            set => _driver.Url = value.ToString();
        }

        public IElement Create(By locator)
        {
            return new ElementAdapter(_driver, locator);
        }

        public IElementsList CreateElements(By locator)
        {
            return new ElementsList(_driver, locator);
        }

        public void WaitForAjax()
        {
            var timeout = TimeSpan.FromSeconds(30);
            var sleepInterval = TimeSpan.FromSeconds(2);
            var webDriverWait = new WebDriverWait(new SystemClock(), _driver, timeout, sleepInterval);
            var js = (IJavaScriptExecutor)_driver;
            webDriverWait.Until(wd => js.ExecuteScript("return jQuery.active").ToString() == "0");
        }

        public void Close()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
