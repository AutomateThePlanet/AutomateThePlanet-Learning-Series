// <copyright file="WorkingWithMobileWebTests.cs" company="Automate The Planet Ltd.">
// Copyright 2018 Automate The Planet Ltd.
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
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System;
using System.Diagnostics;

namespace ExecuteAndroidAppiumDocker
{
    [TestClass]
    public class WorkingWithMobileWebTests
    {
        private static AndroidDriver<AndroidElement> _driver;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, "chrome");
            appiumOptions.AddAdditionalCapability("version", "mobile-79.0");
            appiumOptions.AddAdditionalCapability("enableVNC", true);
            appiumOptions.AddAdditionalCapability("enableVideo", true);
            appiumOptions.AddAdditionalCapability("desired-skin", "WSVGA");
            appiumOptions.AddAdditionalCapability("desired-screen-resolution", "1024x600");

            try
            {
                var timeout = TimeSpan.FromSeconds(120);
                _driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4444/wd/hub"), appiumOptions, timeout);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
                _driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(120);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver?.Quit();
            _driver?.Dispose();
        }

        [TestMethod]
        public void GoToWebSite()
        {
            _driver.Navigate().GoToUrl("https://www.bing.com/");
            Console.WriteLine(_driver.PageSource);
            Assert.IsTrue(_driver.PageSource.Contains("Bing"));
        }
    }
}
