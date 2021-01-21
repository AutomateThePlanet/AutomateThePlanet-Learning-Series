// <copyright file="BingMainPage.Actions.cs" company="Automate The Planet Ltd.">
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
// <site>https://automatetheplanet.com/</site>
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace TestProjectPageObjectsSingleton
{
    public class BingMainPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = @"http://www.bing.com/";

        public BingMainPage(IWebDriver browser)
        {
            _driver = browser;
        }

        public void Navigate()
        {
            _driver.Navigate().GoToUrl(_url);
        }

        public void Search(string textToType)
        {
            GetSearchBox().Clear();
            GetSearchBox().SendKeys(textToType);
            GetGoButton().Click();
        }

        public void AssertResultsCount(string expectedCount)
        {
            Assert.AreEqual(GetResultsCountDiv().Text, expectedCount);
        }

        private IWebElement GetSearchBox()
        {
            return _driver.FindElement(By.Id("sb_form_q"));
        }

        private IWebElement GetGoButton()
        {
            return _driver.FindElement(By.Id("sb_form_go"));
        }

        private IWebElement GetResultsCountDiv()
        {
            return _driver.FindElement(By.Id("b_tween"));
        }
    }
}
