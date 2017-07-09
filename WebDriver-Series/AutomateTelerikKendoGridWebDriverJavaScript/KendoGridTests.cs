// <copyright file="KendoGridTests.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Ui.Automation.Core.Controls.Controls;
using Ui.Automation.Core.Controls.Enums;

namespace AutomateTelerikKendoGridWebDriverJavaScript
{
    [TestClass]
    public class KendoGridTests
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void SetupTest()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void FilterContactName()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            kendoGrid.Filter("ContactName", FilterOperator.Contains, "Thomas");
            var items = kendoGrid.GetItems<GridItem>();
            Assert.AreEqual(1, items.Count);
        }

        [TestMethod]
        public void SortContactTitleDesc()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            kendoGrid.Sort("ContactTitle", SortType.Desc);
            var items = kendoGrid.GetItems<GridItem>();
            Assert.AreEqual("Sales Representative", items[0]);
            Assert.AreEqual("Sales Representative", items[1]);
        }

        [TestMethod]
        public void TestCurrentPage()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            var pageNumber = kendoGrid.GetCurrentPageNumber();
            Assert.AreEqual(1, pageNumber);
        }

        [TestMethod]
        public void GetPageSize()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            var pageNumber = kendoGrid.GetPageSize();
            Assert.AreEqual(20, pageNumber);
        }

        [TestMethod]
        public void GetAllItems()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var items = kendoGrid.GetItems<GridItem>();
            Assert.AreEqual(91, items.Count);
        }
    }
}