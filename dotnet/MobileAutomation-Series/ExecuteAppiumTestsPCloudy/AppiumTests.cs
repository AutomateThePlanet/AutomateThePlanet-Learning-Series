// <copyright file="AppiumTests.cs" company="Automate The Planet Ltd.">
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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace ExecuteAppiumTestsPCloudy
{
    [TestClass]
    public class AppiumTests
    {
        private static AndroidDriver<AndroidElement> _driver;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("pCloudy_Username", "yourUserName");
            appiumOptions.AddAdditionalCapability("pCloudy_ApiKey", "yourKey");
            appiumOptions.AddAdditionalCapability("pCloudy_DurationInMinutes", 10);
            appiumOptions.AddAdditionalCapability("newCommandTimeout", 600);
            appiumOptions.AddAdditionalCapability("launchTimeout", 90000);
            appiumOptions.AddAdditionalCapability("pCloudy_DeviceFullName", "SAMSUNG_GalaxyJ52016_Android_7.1.1_09c99");
            appiumOptions.AddAdditionalCapability("platformVersion", "09c99");
            appiumOptions.AddAdditionalCapability("platformName", "Android");
            appiumOptions.AddAdditionalCapability("pCloudy_ApplicationName", "ApiDemos-debug.apk");
            appiumOptions.AddAdditionalCapability("appPackage", "io.appium.android.apis");
            appiumOptions.AddAdditionalCapability("appActivity", ".ApiDemos");
            appiumOptions.AddAdditionalCapability("pCloudy_WildNet", "true");

            var timeout = TimeSpan.FromSeconds(120);
            _driver = new AndroidDriver<AndroidElement>(new Uri("https://device.pcloudy.com/appiumcloud/wd/hub"), appiumOptions, timeout);
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
