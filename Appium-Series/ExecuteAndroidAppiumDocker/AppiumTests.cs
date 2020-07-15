// <copyright file="AppiumTests.cs" company="Automate The Planet Ltd.">
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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace ExecuteAndroidAppiumDocker
{
    [TestClass]
    public class AppiumTests
    {
        private static AndroidDriver<AndroidElement> _driver;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("deviceName", "android");
            appiumOptions.AddAdditionalCapability("appPackage", "io.appium.android.apis");
            appiumOptions.AddAdditionalCapability("version", "6.0");
            appiumOptions.AddAdditionalCapability("appActivity", ".ApiDemos");
            appiumOptions.AddAdditionalCapability("app", "https://exampleFileshare.com/ApiDemos-debug.apk"); // upload ApiDemos-debug.apk from Resources folder to public file share.
            appiumOptions.AddAdditionalCapability("enableVNC", true);
            appiumOptions.AddAdditionalCapability("enableVideo", true);

            var timeout = TimeSpan.FromSeconds(120);
            // Change with your Selenoid hub instance URL.
            _driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4444/wd/hub"), appiumOptions, timeout);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
            _driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(120);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver?.CloseApp();
            _driver?.Quit();
            _driver?.Dispose();
        }

        [TestMethod]
        public void PerformActionsButtons()
        {
            By byScrollLocator = new ByAndroidUIAutomator("new UiSelector().text(\"Views\");");
            var viewsButton = _driver.FindElement(byScrollLocator);
            viewsButton.Click();

            var controlsViewButton = _driver.FindElementByXPath("//*[@text='Controls']");
            controlsViewButton.Click();

            var lightThemeButton = _driver.FindElementByXPath("//*[@text='1. Light Theme']");
            lightThemeButton.Click();
            var saveButton = _driver.FindElementByXPath("//*[@text='Save']");

            Assert.IsTrue(saveButton.Enabled);
        }
    }
}
