// <copyright file="CaptureHttpTrafficDevToolsTests.cs" company="Automate The Planet Ltd.">
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
// <site>https://automatetheplanet.com/</site>

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebDriverFour
{
    [TestClass]
    public class CaptureHttpTrafficDevToolsTests
    {
        private ChromeDriver _driver;

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = new ChromeDriver();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void FontRequestsNotMade_When_FontRequestSetToBeBlocked_DevTools()
        {
            var devToolssession = _driver.CreateDevToolsSession();
            var blockedUrlSettings = new SetBlockedURLsCommandSettings();
            blockedUrlSettings.Urls = new string[] { "http://demos.bellatrix.solutions/wp-content/themes/storefront/assets/fonts/fontawesome-webfont.woff2?v=4.7.0" };
            devToolssession.Network.SetBlockedURLs(blockedUrlSettings);

            _driver.Navigate().GoToUrl("http://demos.bellatrix.solutions/");
            IWebElement imageTitle = _driver.FindElement(By.XPath("//h2[text()='Falcon 9']"));
            IWebElement falconSalesButton = _driver.FindElement(RelativeBy.WithTagName("span").Below(imageTitle));
            falconSalesButton.Click();
        }
    }
}