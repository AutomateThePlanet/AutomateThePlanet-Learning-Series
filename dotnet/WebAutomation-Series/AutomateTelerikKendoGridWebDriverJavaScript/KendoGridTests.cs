// <copyright file="KendoGridTests.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using Ui.Automation.Core.Controls.Controls;
using Ui.Automation.Core.Controls.Enums;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace AutomateTelerikKendoGridWebDriverJavaScript
{
    [TestClass]
    public class KendoGridTests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
        }

        [TestInitialize]
        public void SetupTest()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));

            _driver.Navigate().GoToUrl("https://demos.telerik.com/kendo-ui/grid/basic-usage");
            var consentButton = _driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            consentButton.Click();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void FilterContactName()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            kendoGrid.Filter("ContactName", FilterOperator.Contains, "Thomas");
            var items = kendoGrid.GetItems<GridItem>();

            Assert.AreEqual(1, items.Count);
        }

        [TestMethod]
        public void SortContactTitleDesc()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            kendoGrid.Sort("ContactTitle", SortType.Desc);
            var items = kendoGrid.GetItems<GridItem>();

            Assert.AreEqual("Sales Representative", items[0].ContactTitle);
            Assert.AreEqual("Sales Representative", items[1].ContactTitle);
        }

        [TestMethod]
        public void TestCurrentPage()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            var pageNumber = kendoGrid.GetCurrentPageNumber();

            Assert.AreEqual(1, pageNumber);
        }

        [TestMethod]
        public void GetPageSize()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            var pageNumber = kendoGrid.GetPageSize();

            Assert.AreEqual(20, pageNumber);
        }

        [TestMethod]
        public void GetAllItems()
        {
            _wait.Until(ExpectedConditions.ElementExists(By.Id("grid")));
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            var items = kendoGrid.GetItems<GridItem>();

            Assert.AreEqual(20, items.Count);
        }
    }
}