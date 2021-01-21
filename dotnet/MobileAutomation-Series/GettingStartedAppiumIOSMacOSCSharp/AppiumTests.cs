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
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using OpenQA.Selenium.Appium;

namespace GettingStartedAppiumIOSMacOS
{
    [TestClass]
    public class AppiumTests
    {
        private static IOSDriver<IOSElement> _driver;
        private static AppiumLocalService _appiumLocalService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // There is a bug in current version of Appium and it is not working on MacOS.
            ////var args = new OptionCollector().AddArguments(GeneralOptionList.PreLaunch());
            ////_appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            ////_appiumLocalService.Start();
            string testAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "TestApp.app.zip");
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone 6");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "iOS");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "11.3");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, testAppPath);

            ////_driver = new IOSDriver<IOSElement>(_appiumLocalService, appiumOptions);
            _driver = new IOSDriver<IOSElement>(new Uri("http://127.0.0.1:4723/wd/hub"), appiumOptions);
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
            ////_appiumLocalService.Dispose();
        }

        [TestMethod]
        public void AddTwoNumbersTest()
        {
            IOSElement numberOne = _driver.FindElementById("IntegerA");
            var numberTwo = _driver.FindElementById("IntegerB");
            var compute = _driver.FindElementByName("ComputeSumButton");
            var answer = _driver.FindElementByName("Answer");

            numberOne.Clear();
            numberOne.SetImmediateValue("5");
            numberTwo.Clear();
            numberTwo.SetImmediateValue("6");
            compute.Click();

            Assert.AreEqual("11", answer.GetAttribute("value"));
        }

        [TestMethod]
        public void LocatingElementInsideAnotherElementTest()
        {
            var mainElement = _driver.FindElementByIosNsPredicate("type == \"XCUIElementTypeApplication\" AND name == \"TestApp\"");

            var numberOne = mainElement.FindElementById("IntegerA");
            var numberTwo = mainElement.FindElementById("IntegerB");
            var compute = mainElement.FindElementByName("ComputeSumButton");
            var answer = mainElement.FindElementByName("Answer");

            numberOne.Clear();
            numberOne.SetImmediateValue("5");
            numberTwo.Clear();
            numberTwo.SetImmediateValue("6");
            compute.Click();

            Assert.AreEqual("11", answer.GetAttribute("value"));
        }
    }
}
