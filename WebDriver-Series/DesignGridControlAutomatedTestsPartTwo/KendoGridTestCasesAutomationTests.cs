// <copyright file="KendoGridTestCasesAutomationTests.cs" company="Automate The Planet Ltd.">
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
        private IWebDriver driver;
        private const string OrderIdColumnName = @"OrderID";
        private const string ShipNameColumnName = @"ShipName";
        private const string FreightColumnName = @"Freight";
        private const string OrderDateColumnName = @"OrderDate";

        [TestInitialize]
        public void SetupTest()
        {
            this.driver = new FirefoxDriver();
            this.driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(5));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.driver.Quit();
        }
     
        // ** OrderDate Test Cases ** (Date Type Column Test Cases)
        
        #region OrderDate Test Cases

        [TestMethod]
        public void OrderDateEqualToFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderId);
            var lastOrderDate = allItems.Last().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(1);
            this.UpdateItemInDb(newItem);

            kendoGrid.Filter(OrderDateColumnName, FilterOperator.EqualTo, newItem.OrderDate.ToString());
            this.WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(newItem.OrderDate.ToString(), results[0].OrderDate);
        }

        [TestMethod]
        public void OrderDateNotEqualToFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.Last().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(1);
            this.UpdateItemInDb(newItem);

            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(2);
            this.UpdateItemInDb(secondNewItem);

            // After we filter by the unique shipping name, two items will be displayed in the grid. 
            // After we apply the date after filter only the second item should be visible in the grid.
            kendoGrid.Filter(
                new GridFilter(OrderDateColumnName, FilterOperator.NotEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
        }

        [TestMethod]
        public void OrderDateAfterFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.Last().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(1);
            this.UpdateItemInDb(newItem);

            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(2);
            this.UpdateItemInDb(secondNewItem);

            // After we filter by the unique shipping name, two items will be displayed in the grid. 
            // After we apply the date after filter only the second item should be visible in the grid.
            kendoGrid.Filter(
                new GridFilter(OrderDateColumnName, FilterOperator.IsAfter, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
        }

        [TestMethod]
        public void OrderDateIsAfterOrEqualToFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.Last().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(1);
            this.UpdateItemInDb(newItem);

            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(2);
            this.UpdateItemInDb(secondNewItem);

            // After we filter by the unique shipping name, two items will be displayed in the grid. 
            // After we apply the date after filter only the second item should be visible in the grid.
            kendoGrid.Filter(
                new GridFilter(OrderDateColumnName, FilterOperator.IsAfterOrEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(2, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
            Assert.AreEqual(newItem.ToString(), results[1].OrderDate);
        }

        [TestMethod]
        public void OrderDateBeforeFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.First().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            this.UpdateItemInDb(newItem);

            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            this.UpdateItemInDb(secondNewItem);

            // After we filter by the unique shipping name, two items will be displayed in the grid. 
            // After we apply the date after filter only the second item should be visible in the grid.
            kendoGrid.Filter(
                new GridFilter(OrderDateColumnName, FilterOperator.IsBefore, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
        }

        [TestMethod]
        public void OrderDateIsBeforeOrEqualToFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.First().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            this.UpdateItemInDb(newItem);

            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            this.UpdateItemInDb(secondNewItem);

            kendoGrid.Filter(
                new GridFilter(OrderDateColumnName, FilterOperator.IsBeforeOrEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(2, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
            Assert.AreEqual(newItem.ToString(), results[1].OrderDate);
        }

        [TestMethod]
        public void OrderDateClearFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            var newItem = this.CreateNewItemInDb();

            kendoGrid.Filter(OrderDateColumnName, FilterOperator.IsAfter, DateTime.MaxValue.ToString());
            this.WaitForGridToLoad(0, kendoGrid);
            kendoGrid.RemoveFilters();

            this.WaitForGridToLoadAtLeast(1, kendoGrid);
        }

        [TestMethod]
        public void OrderDateSortAsc()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.First().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            this.UpdateItemInDb(newItem);

            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            this.UpdateItemInDb(secondNewItem);

            kendoGrid.Filter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName);
            this.WaitForGridToLoadAtLeast(2, kendoGrid);
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
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.First().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            this.UpdateItemInDb(newItem);

            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            this.UpdateItemInDb(secondNewItem);

            kendoGrid.Filter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName);
            this.WaitForGridToLoadAtLeast(2, kendoGrid);
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
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            
            var newItem = this.CreateNewItemInDb();
            newItem.Freight = this.GetUniqueNumberValue();
            this.UpdateItemInDb(newItem);
            
            kendoGrid.Filter(FreightColumnName, FilterOperator.EqualTo, newItem.Freight.ToString());
            this.WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();
            
            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(newItem.Freight.ToString(), results[0].Freight);
        }
        
        [TestMethod]
        public void FreightGreaterThanOrEqualToFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            
            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.Freight);
            var biggestFreight = allItems.Last().Freight;
            
            var newItem = this.CreateNewItemInDb();
            newItem.Freight = biggestFreight + this.GetUniqueNumberValue();
            this.UpdateItemInDb(newItem);
            
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = newItem.Freight + 1;
            this.UpdateItemInDb(secondNewItem);
            
            kendoGrid.Filter(FreightColumnName, FilterOperator.GreaterThanOrEqualTo, newItem.Freight.ToString());
            this.WaitForGridToLoadAtLeast(2, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            
            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
            Assert.AreEqual(1, results.Count(x => x.Freight == newItem.Freight));
        }
        
        [TestMethod]
        public void FreightGreaterThanFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            
            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.Freight);
            var biggestFreight = allItems.Last().Freight;
            
            var newItem = this.CreateNewItemInDb();
            newItem.Freight = biggestFreight + this.GetUniqueNumberValue();
            this.UpdateItemInDb(newItem);
            
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = newItem.Freight + 1;
            this.UpdateItemInDb(secondNewItem);
            
            kendoGrid.Filter(FreightColumnName, FilterOperator.GreaterThan, newItem.Freight.ToString());
            this.WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            
            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
        }
        
        [TestMethod]
        public void FreightLessThanOrEqualToFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            
            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.Freight);
            var smallestFreight = allItems.First().Freight;
            
            var newItem = this.CreateNewItemInDb();
            newItem.Freight = Math.Round(smallestFreight - this.GetUniqueNumberValue(), 3, MidpointRounding.AwayFromZero);
            this.UpdateItemInDb(newItem);
            
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = Math.Round((newItem.Freight - 0.01), 3, MidpointRounding.AwayFromZero);
            this.UpdateItemInDb(secondNewItem);
            
            kendoGrid.Filter(FreightColumnName, FilterOperator.LessThanOrEqualTo, newItem.Freight.ToString());
            this.WaitForGridToLoadAtLeast(2, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            
            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
            Assert.AreEqual(1, results.Count(x => x.Freight == newItem.Freight));
        }
        
        [TestMethod]
        public void FreightLessThanFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            
            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.Freight);
            var smallestFreight = allItems.First().Freight;
            
            var newItem = this.CreateNewItemInDb();
            newItem.Freight = Math.Round(smallestFreight - this.GetUniqueNumberValue(), 3, MidpointRounding.AwayFromZero);
            this.UpdateItemInDb(newItem);
            
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = Math.Round((newItem.Freight - 0.01), 3, MidpointRounding.AwayFromZero);
            this.UpdateItemInDb(secondNewItem);
            
            kendoGrid.Filter(FreightColumnName, FilterOperator.LessThan, newItem.Freight.ToString());
            this.WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            
            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
        }
        
        [TestMethod]
        public void FreightNotEqualToFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            
            var newItem = this.CreateNewItemInDb();
            newItem.Freight = this.GetUniqueNumberValue();
            this.UpdateItemInDb(newItem);
            
            // After we apply the orderId filter, only 1 item is displayed in the grid. When we apply the NotEqualTo filter this item will disappear.
            kendoGrid.Filter(
                new GridFilter(FreightColumnName, FilterOperator.NotEqualTo, newItem.Freight.ToString()),
                new GridFilter(OrderIdColumnName, FilterOperator.EqualTo, newItem.OrderId.ToString()));
            this.WaitForGridToLoad(0, kendoGrid);
            var results = kendoGrid.GetItems<Order>();
        
            Assert.IsTrue(results.Count() == 0);
        }
        
        [TestMethod]
        public void FreightClearFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            
            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.Freight);
            var biggestFreight = allItems.Last().Freight;
            
            var newItem = this.CreateNewItemInDb();
            newItem.Freight = biggestFreight + this.GetUniqueNumberValue();
            this.UpdateItemInDb(newItem);
            
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = newItem.Freight + 1;
            this.UpdateItemInDb(secondNewItem);
            
            kendoGrid.Filter(FreightColumnName, FilterOperator.EqualTo, newItem.Freight.ToString());
            this.WaitForGridToLoad(1, kendoGrid);
            kendoGrid.RemoveFilters();
        
            this.WaitForGridToLoadAtLeast(2, kendoGrid);
        }

        #endregion
        
            
        public void WaitForPageToLoad(int expectedPage, KendoGrid grid)
        {
            this.Until(() =>
            {
                int currentPage = grid.GetCurrentPageNumber();
                return currentPage == expectedPage;
            });
        }

        private void WaitForGridToLoad(int expectedCount, KendoGrid grid)
        {
            this.Until(
                () =>
                {
                    var items = grid.GetItems<GridItem>();
                    return expectedCount == items.Count;
                });
        }
            
        private void WaitForGridToLoadAtLeast(int expectedCount, KendoGrid grid)
        {
            this.Until(
                () =>
                {
                    var items = grid.GetItems<GridItem>();
                    return items.Count >= expectedCount;
                });
        }
            
        private void Until(Func<bool> condition, int timeout = 10, string exceptionMessage = "Timeout exceeded.", int retryRateDelay = 50)
        {
            DateTime start = DateTime.Now;
            while (!condition())
            {
                DateTime now = DateTime.Now;
                double totalSeconds = (now - start).TotalSeconds;
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
            List<Order> orders = new List<Order>();
            for (int i = 0; i < 10; i++)
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
            int result = currentTime.Year + currentTime.Month + currentTime.Hour + currentTime.Minute + currentTime.Second + currentTime.Millisecond;
            return result;
        }
    }
}