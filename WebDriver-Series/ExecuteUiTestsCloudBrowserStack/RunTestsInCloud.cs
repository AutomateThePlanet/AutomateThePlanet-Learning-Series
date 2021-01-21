// <copyright file="RunTestsInCloud.cs" company="Automate The Planet Ltd.">
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

using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace ExecuteUiTestsCloudBrowserStack
{
    [TestFixture]
    public class BrowserStackRunTestsInCloud
    {
        private string _username = "soioa1";
        private string _authkey = "pnFG3Ky2yLZ5muB1p46P";
        private IWebDriver _driver;

        [OneTimeSetUp]
        public void SetupTest()
        {
            var options = new ChromeOptions();
            options.AddAdditionalCapability("browserstack.debug", "true");
            options.AddAdditionalCapability("build", "1.0");

            options.AddAdditionalCapability("os", "Windows");
            options.AddAdditionalCapability("os_version", "10");
            options.AddAdditionalCapability("browser", "Chrome");
            options.AddAdditionalCapability("browser_version", "65.0");
            options.AddAdditionalCapability("resolution", "1366x768");
            options.AddAdditionalCapability("browserstack.video", "false");
            
            options.AddAdditionalCapability("build", "version1");
            options.AddAdditionalCapability("project", "AutomateThePlanet");

            options.AddAdditionalCapability("browserstack.user", _username);
            options.AddAdditionalCapability("browserstack.key", _authkey);

            _driver = new RemoteWebDriver(new Uri("http://hub-cloud.browserstack.com/wd/hub/"), options);
            //_driver = new ChromeDriver();
            ////_driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }

        [OneTimeTearDown]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [Test]
        public void ScrollFocusToControl_InCloud_ShouldFail()
        {
            _driver.Navigate().GoToUrl(@"https://www.automatetheplanet.com/qa-process-architecture-business-services-part-three/");
            var link = _driver.FindElement(By.PartialLinkText("Previous post"));
            var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
            ((IJavaScriptExecutor)_driver).ExecuteScript(jsToBeExecuted);
            link.Click();

            Assert.AreEqual("10 Advanced WebDriver Tips and Tricks - Part 1", _driver.Title);
        }

        [Test]
        public void ScrollFocusToControl_InCloud_ShouldPass()
        {
            _driver.Navigate().GoToUrl(@"https://automatetheplanet.com/multiple-files-page-objects-item-templates/");
            var link = _driver.FindElement(By.PartialLinkText("TFS Test API"));
            var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
            ((IJavaScriptExecutor)_driver).ExecuteScript(jsToBeExecuted);
            var wait = new WebDriverWait(_driver, TimeSpan.FromMinutes(1));
            var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("TFS Test API")));
            clickableElement.Click();

            Assert.AreEqual("TFS Test API Archives - Automate The Planet", _driver.Title);
        }
    }
}