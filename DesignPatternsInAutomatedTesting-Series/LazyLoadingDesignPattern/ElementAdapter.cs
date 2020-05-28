// <copyright file="ElementAdapter.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LazyLoadingDesignPattern
{
    public class ElementAdapter : IElement
    {
        private readonly IWebDriver _driver;
        private readonly ElementFinderService _elementFinder;

        public ElementAdapter(IWebDriver driver, By by)
        {
            _driver = driver;
            By = by;
            _elementFinder = new ElementFinderService(driver);
        }

        public IWebElement NativeWebElement
        {
            get => _elementFinder.Find(By);
        }

        public By By { get; }

        public string Text => NativeWebElement?.Text;

        public bool? Enabled => NativeWebElement?.Enabled;

        public bool? Displayed => NativeWebElement?.Displayed;

        public void Click()
        {
            WaitToBeClickable(By);
            NativeWebElement?.Click();
        }

        public IElement CreateElement(By locator)
        {
            return new ElementAdapter(_driver, locator);
        }

        public IElementsList CreateElements(By locator)
        {
            return new ElementsList(_driver, locator);
        }

        public void TypeText(string text)
        {
            var webElement = NativeWebElement;
            webElement?.Clear();
            webElement?.SendKeys(text);
        }

        private void WaitToBeClickable(By by)
        {
            var webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }
    }
}
