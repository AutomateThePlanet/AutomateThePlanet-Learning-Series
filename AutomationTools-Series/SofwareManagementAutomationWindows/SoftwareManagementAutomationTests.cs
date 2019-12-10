// <copyright file="SoftwareManagementAutomationTests.cs" company="Automate The Planet Ltd.">
// Copyright 2017 Automate The Planet Ltd.
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
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SoftwareManagementAutomationWindows
{
    [TestClass]
    public class SoftwareManagementAutomationTests
    {
        private static readonly string AssemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static IWebDriver _driver;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            SoftwareAutomationService.InstallRequiredSoftware();
        }

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _driver = new ChromeDriver(AssemblyFolder);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _driver?.Dispose();
        }

        [TestMethod]
        public void CheckCurrentIpAddressEqualToSetProxyIp()
        {
            _driver.Navigate().GoToUrl("https://whatismyipaddress.com/");
            var element = _driver.FindElement(By.XPath("//*[@id=\"ipv4\"]/a"));

            Console.WriteLine(element.Text);
        }
    }
}