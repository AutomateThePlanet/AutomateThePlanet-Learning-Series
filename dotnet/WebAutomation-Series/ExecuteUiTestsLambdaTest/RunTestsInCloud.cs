// <copyright file="RunTestsInCloud.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
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

using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using OpenQA.Selenium.Support.UI;

namespace ExecuteUiTestsLambdaTest
{
    [TestFixture]
    public class RunTestsInCloud
    {
        private const string UserName = "angelov.anton";
        private const string AccessKey = "OmG5Nt6iB0WqGxVPueEuVFXu6WX9U6bMzqZLelUuxi9vyOY28K";
        private IWebDriver _driver;

        [SetUp]
        public void SetupTest()
        {
            var desiredCapabilities = new DesiredCapabilities();
            desiredCapabilities.SetCapability("build", "1.0");
            desiredCapabilities.SetCapability("browserName", "Chrome");
            desiredCapabilities.SetCapability("platform", "win8");
            desiredCapabilities.SetCapability("version", "76.0");
            desiredCapabilities.SetCapability("screenResolution", "1280x800");
            desiredCapabilities.SetCapability("visual", "true");
            desiredCapabilities.SetCapability("video", "true");
            desiredCapabilities.SetCapability("console", "true");
            desiredCapabilities.SetCapability("network", "true");
            desiredCapabilities.SetCapability("name", TestContext.CurrentContext.Test.Name);

            desiredCapabilities.SetCapability("user", UserName);
            desiredCapabilities.SetCapability("accessKey", AccessKey);

            _driver = new RemoteWebDriver(new Uri("https://hub.lambdatest.com/wd/hub"), desiredCapabilities);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }

        [TearDown]
        public void TeardownTest()
        {
            var passed = TestContext.CurrentContext.Result.Outcome == ResultState.Success;
            try
            {
                // Logs the result to LambdaTest
                ((IJavaScriptExecutor)_driver).ExecuteScript("lambda-status=" + (passed ? "passed" : "failed"));
            }
            finally
            {
                _driver.Quit();
            }
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