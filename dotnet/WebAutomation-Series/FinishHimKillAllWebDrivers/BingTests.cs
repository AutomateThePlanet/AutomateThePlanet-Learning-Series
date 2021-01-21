﻿// <copyright file="BingTests.cs" company="Automate The Planet Ltd.">
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
using System;
using FinishHimKillAllWebDrivers;
using FinishHimKillAllWebDrivers.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HuddlePageObjectsPartialClassesCSharpSeven
{
    [TestClass]
    public class BingTests
    {
        private static IWebDriver _driver;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            DisposeDriverService.TestRunStartTime = DateTime.Now;
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            DisposeDriverService.FinishHim(_driver);
        }

        [TestMethod]
        public void SearchTextInBing_First()
        {
            var bingMainPage = new BingMainPage(_driver);
            bingMainPage.Navigate();
            bingMainPage.Search("Automate The Planet");
            bingMainPage.AssertResultsCount("236,000 RESULTS");
        }
    }
}
