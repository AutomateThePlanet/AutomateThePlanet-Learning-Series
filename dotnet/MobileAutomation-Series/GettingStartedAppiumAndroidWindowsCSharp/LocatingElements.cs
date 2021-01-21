// <copyright file="LocatingElements.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium.Appium.Service;
using System;
using System.IO;

namespace GettingStartedAppiumAndroidWindows
{
    [TestClass]
    public class LocatingElements
    {
        private static AndroidDriver<AndroidElement> _driver;
        private static AppiumLocalService _appiumLocalService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            _appiumLocalService.Start();
            string testAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ApiDemos-debug.apk");
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android_Accelerated_x86_Oreo");
            appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.example.android.apis");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "7.1");
            appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, ".view.ControlsMaterialDark");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, testAppPath);

            _driver = new AndroidDriver<AndroidElement>(_appiumLocalService, appiumOptions);
            _driver.CloseApp();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            if (_driver != null)
            {
                _driver.LaunchApp();
                _driver.StartActivity("com.example.android.apis", ".view.ControlsMaterialDark");
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (_driver != null)
            {
                _driver.CloseApp();
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _appiumLocalService.Dispose();
        }

        [TestMethod]
        public void LocatingElementsTest()
        {
            AndroidElement button = _driver.FindElementById("button");
            button.Click();

            AndroidElement checkBox = _driver.FindElementByClassName("android.widget.CheckBox");
            checkBox.Click();

            AndroidElement secondButton = _driver.FindElementByAndroidUIAutomator("new UiSelector().textContains(\"BUTTO\");");
            secondButton.Click();

            AndroidElement thirdButton = _driver.FindElementByXPath("//*[@resource-id='com.example.android.apis:id/button']");
            thirdButton.Click();
        }

        [TestMethod]
        public void LocatingElementInsideAnotherElementTest()
        {
            var mainElement = _driver.FindElementById("decor_content_parent");

            var button = mainElement.FindElementById("button");
            button.Click();

            var checkBox = mainElement.FindElementByClassName("android.widget.CheckBox");
            checkBox.Click();

            var thirdButton = mainElement.FindElementByXPath("//*[@resource-id='com.example.android.apis:id/button']");
            thirdButton.Click();
        }
    }
}
