// <copyright file="KendoGridTestCasesAutomationTests.cs" company="Automate The Planet Ltd.">
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
// <site>http://automatetheplanet.com/</site>
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Ui.Automation.Core.Controls.Controls;
using Ui.Automation.Core.Controls.Enums;

namespace DesignGridControlAutomatedTestsPartOne
{
    [TestClass]
    public class KendoGridTestCasesAutomationTests
    {
        private IWebDriver driver;
        private const string OrderIdColumnName = @"OrderID";
        private const string ShipNameColumnName = @"ShipName";

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

        // ** OrderID Test Cases (Unique Identifier Type Column Test Cases) ** 
        
        #region OrderID Test Cases 
        
        [TestMethod]
        public void OrderIdEqualToFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/frozen-columns");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            var newItem = this.CreateNewItemInDb();

            kendoGrid.Filter(OrderIdColumnName, FilterOperator.EqualTo, newItem.OrderId.ToString());           
            this.WaitForGridToLoad(1, kendoGrid);
            var items = kendoGrid.GetItems<GridItem>();
        
            Assert.AreEqual(1, items.Count);
        }
        
        [TestMethod]
        public void OrderIdGreaterThanOrEqualToFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/frozen-columns");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            // Create new item with unique ship name;
            var newItem = this.CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            
            // When we filter by the second unique column ShippingName, only one item will be displayed. Once we apply the second not equal to filter the grid should be empty.
            kendoGrid.Filter(
                new GridFilter(OrderIdColumnName, FilterOperator.GreaterThanOrEqualTo, newItem.OrderId.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(2, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(secondNewItem.OrderId, results.FirstOrDefault(x => x.ShipName == secondNewItem.ShipName).OrderId);
            Assert.AreEqual(newItem.OrderId, results.FirstOrDefault(x => x.ShipName == newItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 2);
        }
        
        [TestMethod]
        public void OrderIdGreaterThanFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/frozen-columns");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            
            // Create new item with unique ship name;
            var newItem = this.CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            
            // Filter by the smaller orderId but also by the second unique column in this case shipping name
            kendoGrid.Filter(
                new GridFilter(OrderIdColumnName, FilterOperator.GreaterThan, newItem.OrderId.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();
            
            Assert.AreEqual(secondNewItem.OrderId, results.FirstOrDefault(x => x.ShipName == secondNewItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 1);
        }
        
        [TestMethod]
        public void OrderIdLessThanOrEqualToFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/frozen-columns");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            
            // Create new item with unique ship name;
            var newItem = this.CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            
            // Filter by the larger orderId but also by the second unique column in this case shipping name
            kendoGrid.Filter(
                new GridFilter(OrderIdColumnName, FilterOperator.LessThanOrEqualTo, secondNewItem.OrderId.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(2, kendoGrid);
            var results = kendoGrid.GetItems<Order>();
            
            Assert.AreEqual(newItem.OrderId, results.FirstOrDefault(x => x.ShipName == newItem.ShipName).OrderId);
            Assert.AreEqual(secondNewItem.OrderId, results.Last(x => x.ShipName == secondNewItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 2);
        }
        
        [TestMethod]
        public void OrderIdLessThanFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/frozen-columns");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            
            // Create new item with unique ship name;
            var newItem = this.CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            
            // Filter by the larger orderId but also by the second unique column in this case shipping name
            kendoGrid.Filter(
                new GridFilter(OrderIdColumnName, FilterOperator.LessThan, secondNewItem.OrderId.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, secondNewItem.ShipName));
            this.WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();
            
            Assert.AreEqual(newItem.OrderId, results.FirstOrDefault(x => x.ShipName == newItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 1);
        }
        
        [TestMethod]
        public void OrderIdNotEqualToFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/frozen-columns");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            
            // Create new item with unique ship name;
            var newItem = this.CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            
            // Filter by the larger orderId but also by the second unique column in this case shipping name
            kendoGrid.Filter(
                new GridFilter(OrderIdColumnName, FilterOperator.NotEqualTo, secondNewItem.OrderId.ToString()),
                new GridFilter(ShipNameColumnName, FilterOperator.EqualTo, secondNewItem.ShipName));
            this.WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();
            
            Assert.AreEqual(newItem.OrderId, results.FirstOrDefault(x => x.ShipName == newItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 1);
        }
        
        [TestMethod]
        public void OrderIdClearFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/frozen-columns");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            // Create new item with unique ship name;
            var newItem = this.CreateNewItemInDb();
            // Make sure that we have at least 2 items if the grid is empty. The tests are designed to run against empty DB.
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            
            kendoGrid.Filter(OrderIdColumnName, FilterOperator.EqualTo, newItem.OrderId.ToString());
            this.WaitForGridToLoad(1, kendoGrid);
            kendoGrid.RemoveFilters();
            
            this.WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();
            Assert.IsTrue(results.Count() > 1);
        }

        #endregion
        
        // ** ShipName Test Cases) ** (Text Type Column Test Cases)
        
        #region ShipName Test Cases
        
        [TestMethod]
        public void ShipNameEqualToFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            var newItem = this.CreateNewItemInDb();

            kendoGrid.Filter(ShipNameColumnName, FilterOperator.EqualTo, newItem.ShipName);
            this.WaitForGridToLoad(1, kendoGrid);
            var items = kendoGrid.GetItems<GridItem>();
            
            Assert.AreEqual(1, items.Count);
        }
        
        [TestMethod]
        public void ShipNameContainsFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            string shipName = Guid.NewGuid().ToString();
            // Remove first and last letter
            shipName = shipName.TrimStart(shipName.First()).TrimEnd(shipName.Last());
            var newItem = this.CreateNewItemInDb(shipName);          

            kendoGrid.Filter(ShipNameColumnName, FilterOperator.Contains, newItem.ShipName);
            this.WaitForGridToLoad(1, kendoGrid);
            var items = kendoGrid.GetItems<GridItem>();
            
            Assert.AreEqual(1, items.Count);
        }
        
        [TestMethod]
        public void ShipNameEndsWithFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            // Remove first letter 
            string shipName = Guid.NewGuid().ToString();
            shipName = shipName.TrimStart(shipName.First());
            var newItem = this.CreateNewItemInDb(shipName);

            kendoGrid.Filter(ShipNameColumnName, FilterOperator.EndsWith, newItem.ShipName);
            this.WaitForGridToLoad(1, kendoGrid);
            var items = kendoGrid.GetItems<GridItem>();
            
            Assert.AreEqual(1, items.Count);
        }
        
        [TestMethod]
        public void ShipNameStartsWithFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            // Remove last letter
            string shipName = Guid.NewGuid().ToString();
            shipName = shipName.TrimEnd(shipName.Last());
            var newItem = this.CreateNewItemInDb(shipName);

            kendoGrid.Filter(ShipNameColumnName, FilterOperator.StartsWith, newItem.ShipName);
            this.WaitForGridToLoad(1, kendoGrid);
            var items = kendoGrid.GetItems<GridItem>();
            
            Assert.AreEqual(1, items.Count);
        }
        
        [TestMethod]
        public void ShipNameNotEqualToFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            
            // Apply combined filter. First filter by ID and than by ship name (not equal filter). 
            // After the first filter there is only one element when we apply the second we expect 0 elements.
            var newItem = this.CreateNewItemInDb();
            
            kendoGrid.Filter(
                new GridFilter(ShipNameColumnName, FilterOperator.NotEqualTo, newItem.ShipName),
                new GridFilter(OrderIdColumnName, FilterOperator.EqualTo, newItem.OrderId.ToString()));
            this.WaitForGridToLoad(0, kendoGrid);
            var items = kendoGrid.GetItems<GridItem>();
            
            Assert.AreEqual(0, items.Count);
        }
        
        [TestMethod]
        public void ShipNameNotContainsFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));            
          
            // Remove first and last letter
            string shipName = Guid.NewGuid().ToString();
            shipName = shipName.TrimStart(shipName.First()).TrimEnd(shipName.Last());
            var newItem = this.CreateNewItemInDb(shipName);

            // Apply combined filter. First filter by ID and than by ship name (not equal filter). 
            // After the first filter there is only one element when we apply the second we expect 0 elements.
            kendoGrid.Filter(
                new GridFilter(ShipNameColumnName, FilterOperator.NotContains, newItem.ShipName),
                new GridFilter(OrderIdColumnName, FilterOperator.EqualTo, newItem.OrderId.ToString()));
            this.WaitForGridToLoad(0, kendoGrid);
            var items = kendoGrid.GetItems<GridItem>();
            
            Assert.AreEqual(0, items.Count);
        }
        
        [TestMethod]
        public void ShipNameClearFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            var newItem = this.CreateNewItemInDb();

            // Filter by GUID and we know we wait the grid to be empty
            kendoGrid.Filter(ShipNameColumnName, FilterOperator.StartsWith, Guid.NewGuid().ToString());
            this.WaitForGridToLoad(0, kendoGrid);
            // Remove all filters and we expect that the grid will contain at least 1 item.
            kendoGrid.RemoveFilters();
            WaitForGridToLoadAtLeast(1, kendoGrid);
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
            
       
        private Order CreateNewItemInDb(string shipName = null)
        {
            // Replace it with service oriented call to your DB. Create real enity in DB.
            return new Order(shipName);
        }
    }
}