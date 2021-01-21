// <copyright file="KendoGridTestCasesAutomationTests.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using Ui.Automation.Core.Controls.Controls;
using Ui.Automation.Core.Controls.Enums;

namespace DesignGridControlAutomatedTestsPartTwo
{
    [TestClass]
    public class KendoGridTestCasesAutomationTests
    {
        private IWebDriver _driver;
        private const string OrderIdColumnName = @"OrderID";
        private const string ShipNameColumnName = @"ShipName";
        private const string FreightColumnName = @"Freight";
        private const string OrderDateColumnName = @"OrderDate";

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

        // ** OrderDate Test Cases ** (Date Type Column Test Cases)
        #region OrderDate Test Cases

        [TestMethod]
        public void OrderDateEqualToFilter()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var allItems = GetAllItemsFromDb().OrderBy(x => x.OrderId);
            var lastOrderDate = allItems.Last().OrderDate;

            var newItem = CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(1);
            UpdateItemInDb(newItem);

            kendoGrid.Filter(OrderDateColumnName, FilterOperator.EqualTo, newItem.OrderDate.ToString());
            WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(newItem.OrderDate.ToString(), results[0].OrderDate);
        }

        [TestMethod]
        public void OrderDateNotEqualToFilter()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var allItems = GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.Last().OrderDate;

            var newItem = CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(1);
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(2);
            UpdateItemInDb(secondNewItem);

            // After we filter by the unique shipping name, two items will be displayed in the grid.
            // After we apply the date after filter only the second item should be visible in the grid.
            kendoGrid.Filter(
                new GridFilter(OrderDateColumnName, FilterOperator.NotEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName));
            WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
        }

        [TestMethod]
        public void OrderDateAfterFilter()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var allItems = GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.Last().OrderDate;

            var newItem = CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(1);
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(2);
            UpdateItemInDb(secondNewItem);

            // After we filter by the unique shipping name, two items will be displayed in the grid.
            // After we apply the date after filter only the second item should be visible in the grid.
            kendoGrid.Filter(
                new GridFilter(OrderDateColumnName, FilterOperator.IsAfter, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName));
            WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
        }

        [TestMethod]
        public void OrderDateIsAfterOrEqualToFilter()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var allItems = GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.Last().OrderDate;

            var newItem = CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(1);
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(2);
            UpdateItemInDb(secondNewItem);

            // After we filter by the unique shipping name, two items will be displayed in the grid.
            // After we apply the date after filter only the second item should be visible in the grid.
            kendoGrid.Filter(
                new GridFilter(OrderDateColumnName, FilterOperator.IsAfterOrEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName));
            WaitForGridToLoadAtLeast(2, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
            Assert.AreEqual(newItem.ToString(), results[1].OrderDate);
        }

        [TestMethod]
        public void OrderDateBeforeFilter()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var allItems = GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.First().OrderDate;

            var newItem = CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            UpdateItemInDb(secondNewItem);

            // After we filter by the unique shipping name, two items will be displayed in the grid.
            // After we apply the date after filter only the second item should be visible in the grid.
            kendoGrid.Filter(
                new GridFilter(OrderDateColumnName, FilterOperator.IsBefore, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName));
            WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
        }

        [TestMethod]
        public void OrderDateIsBeforeOrEqualToFilter()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var allItems = GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.First().OrderDate;

            var newItem = CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            UpdateItemInDb(secondNewItem);

            kendoGrid.Filter(
                new GridFilter(OrderDateColumnName, FilterOperator.IsBeforeOrEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName));
            WaitForGridToLoadAtLeast(2, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
            Assert.AreEqual(newItem.ToString(), results[1].OrderDate);
        }

        [TestMethod]
        public void OrderDateClearFilter()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var newItem = CreateNewItemInDb();

            kendoGrid.Filter(OrderDateColumnName, FilterOperator.IsAfter, DateTime.MaxValue.ToString());
            WaitForGridToLoad(0, kendoGrid);
            kendoGrid.RemoveFilters();

            WaitForGridToLoadAtLeast(1, kendoGrid);
        }

        [TestMethod]
        public void OrderDateSortAsc()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var allItems = GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.First().OrderDate;

            var newItem = CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            UpdateItemInDb(secondNewItem);

            kendoGrid.Filter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName);
            WaitForGridToLoadAtLeast(2, kendoGrid);
            kendoGrid.Sort(OrderDateColumnName, SortType.Asc);
            Thread.Sleep(1000);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
            Assert.AreEqual(newItem.ToString(), results[1].OrderDate);
        }

        [TestMethod]
        public void OrderDateSortDesc()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var allItems = GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.First().OrderDate;

            var newItem = CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            UpdateItemInDb(secondNewItem);

            kendoGrid.Filter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName);
            WaitForGridToLoadAtLeast(2, kendoGrid);
            kendoGrid.Sort(OrderDateColumnName, SortType.Desc);
            Thread.Sleep(1000);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            Assert.AreEqual(newItem.ToString(), results[0].OrderDate);
            Assert.AreEqual(secondNewItem.ToString(), results[1].OrderDate);
        }

        // TODO: Add Simulate Real Clicks Demos?
        #endregion

        // ** Freight Test Cases ** (Money Type Column Test Cases)
        #region Freight Test Cases

        [TestMethod]
        public void FreightEqualToFilter()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var newItem = CreateNewItemInDb();
            newItem.Freight = GetUniqueNumberValue();
            UpdateItemInDb(newItem);

            kendoGrid.Filter(FreightColumnName, FilterOperator.EqualTo, newItem.Freight.ToString());
            WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(newItem.Freight.ToString(), results[0].Freight);
        }

        [TestMethod]
        public void FreightGreaterThanOrEqualToFilter()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var allItems = GetAllItemsFromDb().OrderBy(x => x.Freight);
            var biggestFreight = allItems.Last().Freight;

            var newItem = CreateNewItemInDb();
            newItem.Freight = biggestFreight + GetUniqueNumberValue();
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = newItem.Freight + 1;
            UpdateItemInDb(secondNewItem);

            kendoGrid.Filter(FreightColumnName, FilterOperator.GreaterThanOrEqualTo, newItem.Freight.ToString());
            WaitForGridToLoadAtLeast(2, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);

            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
            Assert.AreEqual(1, results.Count(x => x.Freight == newItem.Freight));
        }

        [TestMethod]
        public void FreightGreaterThanFilter()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var allItems = GetAllItemsFromDb().OrderBy(x => x.Freight);
            var biggestFreight = allItems.Last().Freight;

            var newItem = CreateNewItemInDb();
            newItem.Freight = biggestFreight + GetUniqueNumberValue();
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = newItem.Freight + 1;
            UpdateItemInDb(secondNewItem);

            kendoGrid.Filter(FreightColumnName, FilterOperator.GreaterThan, newItem.Freight.ToString());
            WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);

            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
        }

        [TestMethod]
        public void FreightLessThanOrEqualToFilter()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var allItems = GetAllItemsFromDb().OrderBy(x => x.Freight);
            var smallestFreight = allItems.First().Freight;

            var newItem = CreateNewItemInDb();
            newItem.Freight = Math.Round(smallestFreight - GetUniqueNumberValue(), 3, MidpointRounding.AwayFromZero);
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = Math.Round(newItem.Freight - 0.01, 3, MidpointRounding.AwayFromZero);
            UpdateItemInDb(secondNewItem);

            kendoGrid.Filter(FreightColumnName, FilterOperator.LessThanOrEqualTo, newItem.Freight.ToString());
            WaitForGridToLoadAtLeast(2, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);

            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
            Assert.AreEqual(1, results.Count(x => x.Freight == newItem.Freight));
        }

        [TestMethod]
        public void FreightLessThanFilter()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var allItems = GetAllItemsFromDb().OrderBy(x => x.Freight);
            var smallestFreight = allItems.First().Freight;

            var newItem = CreateNewItemInDb();
            newItem.Freight = Math.Round(smallestFreight - GetUniqueNumberValue(), 3, MidpointRounding.AwayFromZero);
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = Math.Round(newItem.Freight - 0.01, 3, MidpointRounding.AwayFromZero);
            UpdateItemInDb(secondNewItem);

            kendoGrid.Filter(FreightColumnName, FilterOperator.LessThan, newItem.Freight.ToString());
            WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);

            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
        }

        [TestMethod]
        public void FreightNotEqualToFilter()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var newItem = CreateNewItemInDb();
            newItem.Freight = GetUniqueNumberValue();
            UpdateItemInDb(newItem);

            // After we apply the orderId filter, only 1 item is displayed in the grid. When we apply the NotEqualTo filter this item will disappear.
            kendoGrid.Filter(
                new GridFilter(FreightColumnName, FilterOperator.NotEqualTo, newItem.Freight.ToString()),
                new GridFilter(OrderIdColumnName, FilterOperator.EqualTo, newItem.OrderId.ToString()));
            WaitForGridToLoad(0, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 0);
        }

        [TestMethod]
        public void FreightClearFilter()
        {
            _driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));

            var allItems = GetAllItemsFromDb().OrderBy(x => x.Freight);
            var biggestFreight = allItems.Last().Freight;

            var newItem = CreateNewItemInDb();
            newItem.Freight = biggestFreight + GetUniqueNumberValue();
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = newItem.Freight + 1;
            UpdateItemInDb(secondNewItem);

            kendoGrid.Filter(FreightColumnName, FilterOperator.EqualTo, newItem.Freight.ToString());
            WaitForGridToLoad(1, kendoGrid);
            kendoGrid.RemoveFilters();

            WaitForGridToLoadAtLeast(2, kendoGrid);
        }

        #endregion

        public void WaitForPageToLoad(int expectedPage, KendoGrid grid)
        {
            Until(() =>
            {
                var currentPage = grid.GetCurrentPageNumber();
                return currentPage == expectedPage;
            });
        }

        private void WaitForGridToLoad(int expectedCount, KendoGrid grid)
        {
            Until(
                () =>
                {
                    var items = grid.GetItems<GridItem>();
                    return expectedCount == items.Count;
                });
        }

        private void WaitForGridToLoadAtLeast(int expectedCount, KendoGrid grid)
        {
            Until(
                () =>
                {
                    var items = grid.GetItems<GridItem>();
                    return items.Count >= expectedCount;
                });
        }

        private void Until(Func<bool> condition, int timeout = 10, string exceptionMessage = "Timeout exceeded.", int retryRateDelay = 50)
        {
            var start = DateTime.Now;
            while (!condition())
            {
                var now = DateTime.Now;
                var totalSeconds = (now - start).TotalSeconds;
                if (totalSeconds >= timeout)
                {
                    throw new TimeoutException(exceptionMessage);
                }

                Thread.Sleep(retryRateDelay);
            }
        }

        private List<Order> GetAllItemsFromDb()
        {
            // Create dummy orders. This logic should be replaced with service oriented call to your DB and get all items that are populated in the grid.
            var orders = new List<Order>();
            for (var i = 0; i < 10; i++)
            {
                orders.Add(new Order());
            }

            return orders;
        }

        private Order CreateNewItemInDb(string shipName = null)
        {
            // Replace it with service oriented call to your DB. Create real enity in DB.
            return new Order(shipName);
        }

        private void UpdateItemInDb(Order order)
        {
            // Replace it with service oriented call to your DB. Update the enity in the DB.
        }

        private int GetUniqueNumberValue()
        {
            var currentTime = DateTime.Now;
            var result = currentTime.Year + currentTime.Month + currentTime.Hour + currentTime.Minute + currentTime.Second + currentTime.Millisecond;
            return result;
        }
    }
}