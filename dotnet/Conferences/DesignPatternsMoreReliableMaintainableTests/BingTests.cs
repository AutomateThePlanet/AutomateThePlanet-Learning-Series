﻿// <copyright file="BingTests.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
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
// <site>http://automatetheplanet.com/</site>

using DesignPatternsMoreReliableMaintainableTests.Pages.BingMain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace DesignPatternsMoreReliableMaintainableTests
{
    [TestClass]
    public class BingTests 
    {
        private IBingMainPage bingMainPage;
        private IWebDriver driver;

        [TestInitialize]
        public void SetupTest()
        {
            this.driver = new FirefoxDriver();
            this.bingMainPage = new BingMainPage(this.driver);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.driver.Quit();
        }

        [TestMethod]
        public void SearchForAutomateThePlanet()
        {
            this.bingMainPage.Open();
            this.bingMainPage.Search("Automate The Planet");
            this.bingMainPage.AssertResultsCountIsAsExpected(264);
        }
    }
}