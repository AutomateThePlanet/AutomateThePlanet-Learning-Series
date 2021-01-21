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

namespace ExecuteUiTestsCloudCrossBrowserTesting
{
    [TestFixture]
    public class RunTestsInCloud
    {
        private string _username = "autoCloudTester@yahoo.com";
        private string _authkey = "u4f3bb3b861ee342";
        private IWebDriver _driver;

        [SetUp]
        public void SetupTest()
        {
            var options = new ChromeOptions();
            options.AddAdditionalCapability("name", "Basic Example");
            options.AddAdditionalCapability("build", "1.0");
            options.AddAdditionalCapability("browser_api_name", "Chrome58");
            options.AddAdditionalCapability("os_api_name", "Win10-E15");
            options.AddAdditionalCapability("screen_resolution", "1366x768");

            //////caps.SetCapability("browser_api_name", "FF46");
            //////caps.SetCapability("os_api_name", "Mac10.11");
            //////caps.SetCapability("screen_resolution", "1400x900");
            options.AddAdditionalCapability("record_video", "true");
            options.AddAdditionalCapability("record_network", "true");

            options.AddAdditionalCapability("username", _username);
            options.AddAdditionalCapability("password", _authkey);
            
            _driver = new RemoteWebDriver(new Uri("http://hub.crossbrowsertesting.com:80/wd/hub"), options);
        }

        [TearDown]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [Test]
        public void ScrollFocusToControl_InCloud_ShouldFail()
        {
            _driver.Navigate().GoToUrl(@"https://automatetheplanet.com/compelling-sunday-14022016/");
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