// <copyright file="BingMainPage.Actions.cs" company="Automate The Planet Ltd.">
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
// <site>https://automatetheplanet.com/</site>
using OpenQA.Selenium;

namespace HuddlePageObjectsElementsAccessStyles.ElementsExposedAsProperties
{
    public partial class BingMainPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = @"http://www.bing.com/";

        public BingMainPage(IWebDriver driver)
        {
            _driver = driver;
            Elements = new BingMainPageElements(_driver);
        }

        public void Navigate() => _driver.Navigate().GoToUrl(_url);
        
        public void Search(string textToType)
        {
            Elements.SearchBox.Clear();
            Elements.SearchBox.SendKeys(textToType);
            Elements.GoButton.Click();
        }

        public BingMainPageElements Elements { get; set; }
    }
}
