﻿// <copyright file="SimpleFactoryTests.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium;

namespace SimpleFactoryDesignPatternProxies
{
    [TestClass]
    public class SimpleFactoryTests
    {
        private IWebDriver _driver;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            ProxyService.GetListProxies();
            ProxyService.CheckProxiesStatus();
        }

        [TestInitialize]
        public void TestInit()
        {
            _driver = WebDriverFactory.CreateDriver(Browser.Chrome);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver?.Dispose();
        }

        [TestMethod]
        public void CheckCurrentIpAddressEqualToSetProxyIp()
        {
            _driver.Navigate().GoToUrl("https://whatismyipaddress.com/");
            var element = _driver.FindElement(By.XPath("//*[@id='ipv4']/a"));

            Assert.AreEqual(ProxyService.CurrentProxyIp, element.Text);
        }
    }
}
