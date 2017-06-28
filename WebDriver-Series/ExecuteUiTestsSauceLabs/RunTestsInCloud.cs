// <copyright file="RunTestsInCloud.cs" company="Automate The Planet Ltd.">
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

using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace ExecuteUiTestsSauceLabs
{
    [TestFixture]
    public class RunTestsInCloud
    {
        private string _username = "autoCloudTester";
        private string _authkey = "70dccdcf-a9fd-4f55-aa07-12b051f6c83e";
        private IWebDriver _driver;

        [SetUp]
        public void SetupTest()
        {
            var caps = new DesiredCapabilities();
            caps.SetCapability("browserstack.debug", "true");
            caps.SetCapability("build", "1.0");

            caps.SetCapability("browserName", "Chrome");
            caps.SetCapability("platform", "Windows 8.1");
            caps.SetCapability("version", "49.0");
            caps.SetCapability("screenResolution", "1280x800");

            caps.SetCapability("username", _username);
            caps.SetCapability("accessKey", _authkey);
            caps.SetCapability("name", TestContext.CurrentContext.Test.Name);

            _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(180));
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }

        [TearDown]
        public void TeardownTest()
        {
            bool passed = TestContext.CurrentContext.Result.Outcome == ResultState.Success;
            try
            {
                // Logs the result to Sauce Labs
                ((IJavaScriptExecutor)_driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            }
            finally
            {
                _driver.Quit();
            }
        }

        [Test]
        public void ScrollFocusToControl_InCloud_ShouldFail()
        {
            _driver.Navigate().GoToUrl(@"http://automatetheplanet.com/compelling-sunday-14022016/");
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