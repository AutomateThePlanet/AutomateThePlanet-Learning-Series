// <copyright file="AppiumTests.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdbCommandsLibraryAppium
{
    [TestClass]
    public class AppiumTests
    {
        private static AndroidDriver<AndroidElement> _driver;
        private static AppiumLocalService _appiumLocalService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var args = new OptionCollector()
                .AddArguments(GeneralOptionList.PreLaunch())
                .AddArguments(new KeyValuePair<string, string>("--relaxed-security", string.Empty));
            _appiumLocalService = new AppiumServiceBuilder().WithArguments(args).UsingAnyFreePort().Build();
            _appiumLocalService.Start();
            string testAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ApiDemos-debug.apk");
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "android25-test");
            appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.example.android.apis");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "7.1");
            appiumOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, ".ApiDemos");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, testAppPath);

            _driver = new AndroidDriver<AndroidElement>(_appiumLocalService, appiumOptions);
            _driver.CloseApp();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _driver?.LaunchApp();
            _driver?.StartActivity("com.example.android.apis", ".ApiDemos");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver?.CloseApp();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _appiumLocalService.Dispose();
        }

        [TestMethod]
        public void PerformRandomShellCommandAsJson()
        {
            string result = _driver.ExecuteScript("mobile: shell", "{\"command\": \"dumpsys\", \"args\": [\"battery\", \"reset\"]}").ToString();
            Debug.WriteLine(result);
        }

        [TestMethod]
        public void PerformRandomShellCommand()
        {
            string result = _driver.ExecuteScript("mobile: shell", new AdbCommand("dumpsys", "battery", "reset").ToString()).ToString();
            Debug.WriteLine(result);
        }

        [TestMethod]
        public void PerformShellCommandViaExtensionMethod()
        {
            _driver.ResetBattery();
        }
    }
}
