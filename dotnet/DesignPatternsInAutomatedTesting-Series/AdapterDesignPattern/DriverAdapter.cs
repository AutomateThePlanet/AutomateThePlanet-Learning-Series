// <copyright file="DriverAdapter.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AdapterDesignPattern
{
    public class DriverAdapter : IDriver
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _webDriverWait;

        public DriverAdapter(IWebDriver driver)
        {
            _driver = driver;
            var timeout = TimeSpan.FromSeconds(30);
            var sleepInterval = TimeSpan.FromSeconds(2);
            _webDriverWait = new WebDriverWait(new SystemClock(), _driver, timeout, sleepInterval);
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

        public IElement FindElement(By locator)
        {
            IWebElement nativeElement =
                _webDriverWait.Until(ExpectedConditions.ElementExists(locator));

            return new ElementAdapter(_driver, nativeElement, locator);
        }

        public IEnumerable<IElement> FindElements(By locator)
        {
            ReadOnlyCollection<IWebElement> nativeElements = 
                _webDriverWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
             var elements = new List<IElement>();
             foreach (var nativeElement in nativeElements)
             {
                 IElement element = new ElementAdapter(_driver, nativeElement, locator);
                 elements.Add(element);
             }

             return elements;
        }

        public void WaitForAjax()
        {
            var js = (IJavaScriptExecutor)_driver;
            _webDriverWait.Until(wd => js.ExecuteScript("return jQuery.active").ToString() == "0");
        }

        public void Close()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
