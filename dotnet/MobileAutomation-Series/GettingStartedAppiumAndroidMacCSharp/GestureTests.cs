// <copyright file="GestureTests.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Drawing;
using System.IO;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;

namespace GettingStartedAppiumAndroidMacCSharp
{
    [TestClass]
    public class GestureTests
    {
        private static AndroidDriver<AndroidElement> _driver;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            string testAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ApiDemos-debug.apk");
            var desiredCaps = new AppiumOptions();
            desiredCaps.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android_Accelerated_x86_Oreo");
            desiredCaps.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
            desiredCaps.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            desiredCaps.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "7.1");
            desiredCaps.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, ".ApiDemos");
            desiredCaps.AddAdditionalCapability(MobileCapabilityType.App, testAppPath);

            _driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), desiredCaps);
            _driver.CloseApp();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            if (_driver != null)
            {
                _driver.LaunchApp();
                _driver.StartActivity("io.appium.android.apis", ".ApiDemos");
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver?.CloseApp();
        }

        [TestMethod]
        public void SwipeTest()
        {
            _driver.StartActivity("io.appium.android.apis", ".graphics.FingerPaint");
            ITouchAction touchAction = new TouchAction(_driver);
            var element = _driver.FindElementById("android:id/content");
            Point point = element.Coordinates.LocationInDom;
            Size size = element.Size;
            touchAction
                .Press(point.X + 5, point.Y + 5)
                .Wait(200).MoveTo(point.X + size.Width - 5, point.Y + size.Height - 5)
                .Release()
                .Perform();
        }

        [TestMethod]
        public void MoveToTest()
        {
            ITouchAction touchAction = new TouchAction(_driver);
            var element = _driver.FindElementById("android:id/content");
            Point point = element.Coordinates.LocationInDom;

            touchAction.MoveTo(point.X, point.Y).Perform();
        }

        [TestMethod]
        public void TapTest()
        {
            ITouchAction touchAction = new TouchAction(_driver);
            var element = _driver.FindElementById("android:id/content");
            Point point = element.Coordinates.LocationInDom;

            touchAction.Tap(point.X, point.Y, 2).Perform();
        }
    }
}
