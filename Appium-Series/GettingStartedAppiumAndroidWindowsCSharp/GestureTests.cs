// <copyright file="GestureTests.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium.Appium.Service.Options;
using OpenQA.Selenium.Remote;
using System;
using System.Drawing;
using System.IO;

namespace GettingStartedAppiumAndroidWindows
{
    [TestClass]
    public class GestureTests
    {
        private static AndroidDriver<AppiumWebElement> _driver;
        private static AppiumLocalService _appiumLocalService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var args = new OptionCollector().AddArguments(GeneralOptionList.PreLaunch());
            _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            _appiumLocalService.Start();
            string testAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ApiDemos-debug.apk");
            var desiredCaps = new DesiredCapabilities();
            desiredCaps.SetCapability(MobileCapabilityType.DeviceName, "Android_Accelerated_x86_Oreo");
            desiredCaps.SetCapability(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
            desiredCaps.SetCapability(MobileCapabilityType.PlatformName, "Android");
            desiredCaps.SetCapability(MobileCapabilityType.PlatformVersion, "7.1");
            desiredCaps.SetCapability(AndroidMobileCapabilityType.AppActivity, ".graphics.TouchRotateActivity");
            desiredCaps.SetCapability(MobileCapabilityType.App, testAppPath);

            _driver = new AndroidDriver<AppiumWebElement>(_appiumLocalService, desiredCaps);
            _driver.CloseApp();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            if (_driver != null)
            {
                _driver.LaunchApp();
                _driver.StartActivity("io.appium.android.apis", ".graphics.TouchRotateActivity");
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
        public void SwipeTest()
        {
            _driver.StartActivity("io.appium.android.apis", ".graphics.FingerPaint");
            var element = _driver.FindElementById("android:id/content");
            Point point = element.Coordinates.LocationInDom;
            Size size = element.Size;
            _driver.Swipe
            (
                point.X + 5,
                point.Y + 5,
                point.X + size.Width - 5,
                point.Y + size.Height - 5,
                200
            );

            _driver.Swipe
            (
                point.X + size.Width - 5,
                point.Y + 5,
                point.X + 5,
                point.Y + size.Height - 5,
                2000
            );
        }

        [TestMethod]
        public void PincTest()
        {
            var element = _driver.FindElementById("android:id/content");
            _driver.Pinch(element);
        }

        [TestMethod]
        public void ZoomTest()
        {
            var element = _driver.FindElementById("android:id/content");
            _driver.Zoom(element);
        }
    }
}
