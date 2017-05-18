// <copyright file="BingMainPage.cs" company="Automate The Planet Ltd.">
// Copyright 2017 Automate The Planet Ltd.
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
// <site>http://automatetheplanet.com/</site>
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace HuddlePageObjectsPartialClassesProperties
{
    public class BingMainPage
    {
        private readonly IWebDriver _driver;

        public BingMainPage(IWebDriver browser)
        {
            this._driver = browser;
            PageFactory.InitElements(browser, this);
        }

        public string Url
        {
            get
            {
                return @"http://www.bing.com/";
            }
        }

        [FindsBy(How = How.Id, Using = "sb_form_q")]
        public IWebElement SearchBox { get; set; }

        [FindsBy(How = How.Id, Using = "sb_form_go")]
        public IWebElement GoButton { get; set; }

        [FindsBy(How = How.Id, Using = "b_tween")]
        public IWebElement ResultsCountDiv { get; set; }

        public void Navigate()
        {
            this._driver.Navigate().GoToUrl(this.Url);
        }

        public void Search(string textToType)
        {
            this.SearchBox.Clear();
            this.SearchBox.SendKeys(textToType);
            this.GoButton.Click();
        }

        public void AssertResultsCount(string expectedCount)
        {
            Assert.AreEqual(this.ResultsCountDiv.Text, expectedCount);
        }
    }
}
