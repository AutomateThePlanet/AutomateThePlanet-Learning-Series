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
using OpenQA.Selenium.Appium.Service;
using System;

namespace GettingStartedAppiumAndroidWindows
{
    [TestClass]
    public class WorkingWithMobileWebTests
    {
private static AndroidDriver<AppiumWebElement> _driver;
private static AppiumLocalService _appiumLocalService;

[ClassInitialize]
public static void ClassInitialize(TestContext context)
{
    _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
    _appiumLocalService.Start();
    var appiumOptions = new AppiumOptions();
    appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android_Accelerated_x86_Oreo");
    appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
    appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "7.1");
    appiumOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Chrome");

    _driver = new AndroidDriver<AppiumWebElement>(_appiumLocalService, appiumOptions);
    _driver.CloseApp();
}

[TestInitialize]
public void TestInitialize()
{
    _driver?.LaunchApp();
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
        public void GoToWebSite()
        {
            _driver.Navigate().GoToUrl("http://demos.bellatrix.solutions/");
            Console.WriteLine(_driver.PageSource);
            Assert.IsTrue(_driver.PageSource.Contains("Shop"));
        }
    }
}
