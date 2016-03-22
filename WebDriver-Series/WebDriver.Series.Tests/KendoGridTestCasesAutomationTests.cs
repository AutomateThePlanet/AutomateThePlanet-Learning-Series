using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using WebDriver.Series.Tests.Controls;
using System.Collections.Generic;
using WebDriver.Series.Tests.GridTestCases.Pages;

namespace WebDriver.Series.Tests
{
    [TestClass]
    public class KendoGridTestCasesAutomationTests
    {
        private IWebDriver driver;
        private const string OrderIdColumnName = @"OrderID";
        private const string ShipNameColumnName = @"ShipName";
        private const string FreightColumnName = @"Freight";
        private const string OrderDateColumnName = @"OrderDate";
        private string uniqueShippingName;
        private List<Order> testPagingItems;

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

            kendoGrid.Filter(OrderIdColumnName, Enums.FilterOperator.EqualTo, newItem.OrderId.ToString());           
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
                new GridFilter(OrderIdColumnName, Enums.FilterOperator.GreaterThanOrEqualTo, newItem.OrderId.ToString()),
                new GridFilter(ShipNameColumnName, Enums.FilterOperator.EqualTo, newItem.ShipName));
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
                new GridFilter(OrderIdColumnName, Enums.FilterOperator.GreaterThan, newItem.OrderId.ToString()),
                new GridFilter(ShipNameColumnName, Enums.FilterOperator.EqualTo, newItem.ShipName));
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
                new GridFilter(OrderIdColumnName, Enums.FilterOperator.LessThanOrEqualTo, secondNewItem.OrderId.ToString()),
                new GridFilter(ShipNameColumnName, Enums.FilterOperator.EqualTo, newItem.ShipName));
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
                new GridFilter(OrderIdColumnName, Enums.FilterOperator.LessThan, secondNewItem.OrderId.ToString()),
                new GridFilter(ShipNameColumnName, Enums.FilterOperator.EqualTo, secondNewItem.ShipName));
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
                new GridFilter(OrderIdColumnName, Enums.FilterOperator.NotEqualTo, secondNewItem.OrderId.ToString()),
                new GridFilter(ShipNameColumnName, Enums.FilterOperator.EqualTo, secondNewItem.ShipName));
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
            
            kendoGrid.Filter(OrderIdColumnName, Enums.FilterOperator.EqualTo, newItem.OrderId.ToString());
            this.WaitForGridToLoad(1, kendoGrid);
            kendoGrid.RemoveFilters();
            
            this.WaitForGridToLoadAtLeast(1, kendoGrid);
            var results = kendoGrid.GetItems<Order>();
            Assert.IsTrue(results.Count() > 1);
        }

        #endregion

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
            newItem.OrderDate = lastOrderDate;
            this.UpdateItemInDb(newItem);

            kendoGrid.Filter(OrderDateColumnName, Enums.FilterOperator.EqualTo, newItem.OrderDate.ToString());
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

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderId);
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
                new GridFilter(OrderDateColumnName, Enums.FilterOperator.NotEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, Enums.FilterOperator.EqualTo, newItem.ShipName));
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

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderId);
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
                new GridFilter(OrderDateColumnName, Enums.FilterOperator.IsAfter, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, Enums.FilterOperator.EqualTo, newItem.ShipName));
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

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderId);
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
                new GridFilter(OrderDateColumnName, Enums.FilterOperator.IsAfterOrEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, Enums.FilterOperator.EqualTo, newItem.ShipName));
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

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderId);
            var lastOrderDate = allItems.Last().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            this.UpdateItemInDb(newItem);

            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            this.UpdateItemInDb(secondNewItem);

            // After we filter by the unique shipping name, two items will be displayed in the grid. 
            // After we apply the date after filter only the second item should be visible in the grid.
            kendoGrid.Filter(
                new GridFilter(OrderDateColumnName, Enums.FilterOperator.IsBefore, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, Enums.FilterOperator.EqualTo, newItem.ShipName));
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

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderId);
            var lastOrderDate = allItems.Last().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            this.UpdateItemInDb(newItem);

            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            this.UpdateItemInDb(secondNewItem);

            kendoGrid.Filter(
                new GridFilter(OrderDateColumnName, Enums.FilterOperator.IsBeforeOrEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(ShipNameColumnName, Enums.FilterOperator.EqualTo, newItem.ShipName));
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

            kendoGrid.Filter(OrderDateColumnName, Enums.FilterOperator.IsAfter, DateTime.MaxValue.ToString());
            this.WaitForGridToLoad(0, kendoGrid);
            kendoGrid.RemoveFilters();

            this.WaitForGridToLoadAtLeast(1, kendoGrid);
        }

        [TestMethod]
        public void OrderDateSortAsc()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/remote-data-binding");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderId);
            var lastOrderDate = allItems.Last().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            this.UpdateItemInDb(newItem);

            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            this.UpdateItemInDb(secondNewItem);

            kendoGrid.Filter(ShipNameColumnName, Enums.FilterOperator.EqualTo, newItem.ShipName);
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

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderId);
            var lastOrderDate = allItems.Last().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            this.UpdateItemInDb(newItem);

            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            this.UpdateItemInDb(secondNewItem);

            kendoGrid.Filter(ShipNameColumnName, Enums.FilterOperator.EqualTo, newItem.ShipName);
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
        
        // ** ShipName Test Cases) ** (Text Type Column Test Cases)
        
        #region ShipName Test Cases
        
        [TestMethod]
        public void ShipNameEqualToFilter()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/filter-row");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            var newItem = this.CreateNewItemInDb();

            kendoGrid.Filter(ShipNameColumnName, Enums.FilterOperator.EqualTo, newItem.ShipName);
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

            kendoGrid.Filter(ShipNameColumnName, Enums.FilterOperator.Contains, newItem.ShipName);
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

            kendoGrid.Filter(ShipNameColumnName, Enums.FilterOperator.EndsWith, newItem.ShipName);
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

            kendoGrid.Filter(ShipNameColumnName, Enums.FilterOperator.StartsWith, newItem.ShipName);
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
                new GridFilter(ShipNameColumnName, Enums.FilterOperator.NotEqualTo, newItem.ShipName),
                new GridFilter(OrderIdColumnName, Enums.FilterOperator.EqualTo, newItem.OrderId.ToString()));
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
                new GridFilter(ShipNameColumnName, Enums.FilterOperator.NotContains, newItem.ShipName),
                new GridFilter(OrderIdColumnName, Enums.FilterOperator.EqualTo, newItem.OrderId.ToString()));
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
            kendoGrid.Filter(ShipNameColumnName, Enums.FilterOperator.StartsWith, Guid.NewGuid().ToString());
            this.WaitForGridToLoad(0, kendoGrid);
            // Remove all filters and we expect that the grid will contain at least 1 item.
            kendoGrid.RemoveFilters();
            WaitForGridToLoadAtLeast(1, kendoGrid);
        }
        
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
            
            kendoGrid.Filter(FreightColumnName, Enums.FilterOperator.EqualTo, newItem.Freight.ToString());
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
            
            kendoGrid.Filter(FreightColumnName, Enums.FilterOperator.GreaterThanOrEqualTo, newItem.Freight.ToString());
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
            
            kendoGrid.Filter(FreightColumnName, Enums.FilterOperator.GreaterThan, newItem.Freight.ToString());
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
            
            kendoGrid.Filter(FreightColumnName, Enums.FilterOperator.LessThanOrEqualTo, newItem.Freight.ToString());
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
            
            kendoGrid.Filter(FreightColumnName, Enums.FilterOperator.LessThan, newItem.Freight.ToString());
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
                new GridFilter(FreightColumnName, Enums.FilterOperator.NotEqualTo, newItem.Freight.ToString()),
                new GridFilter(OrderIdColumnName, Enums.FilterOperator.EqualTo, newItem.OrderId.ToString()));
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
            
            kendoGrid.Filter(FreightColumnName, Enums.FilterOperator.EqualTo, newItem.Freight.ToString());
            this.WaitForGridToLoad(1, kendoGrid);
            kendoGrid.RemoveFilters();
        
            this.WaitForGridToLoadAtLeast(2, kendoGrid);
        }

        #endregion
        
        // ** Paging Test Cases **
        
        #region Paging Test Cases
        
        [TestMethod]
        public void NavigateToFirstPage_GoToFirstPageButton()
        {
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(kendoGrid, 10);
            int targetPage = 1;
            GridFilterPage gridFilterPage = new GridFilterPage(this.driver);
            gridFilterPage.GoToFirstPageButton.Click();
            this.WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();
        
            Assert.AreEqual(this.testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            this.AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, this.testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToLastPage_GoToLastPageButton()
        {
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(kendoGrid, 1);
            int targetPage = 1;
            GridFilterPage gridFilterPage = new GridFilterPage(this.driver);
            gridFilterPage.GoToLastPage.Click();
            this.WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(this.testPagingItems.Last().OrderId, results.First().OrderId);
            this.AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, this.testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToPageNine_GoToPreviousPageButton()
        {
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(kendoGrid, 10);
            int targetPage = 9;
            GridFilterPage gridFilterPage = new GridFilterPage(this.driver);
            gridFilterPage.GoToPreviousPage.Click();
            this.WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(this.testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            this.AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, this.testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToPageTwo_GoToNextPageButton()
        {
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(kendoGrid, 1);
            int targetPage = 2;
            GridFilterPage gridFilterPage = new GridFilterPage(this.driver);
            gridFilterPage.GoToNextPage.Click();
            this.WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(this.testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            this.AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, this.testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToPageTwo_SecondPageButton()
        {
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(kendoGrid, 1);
            int targetPage = 2;
            GridFilterPage gridFilterPage = new GridFilterPage(this.driver);
            gridFilterPage.PageOnSecondPositionButton.Click();
            this.WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(this.testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            this.AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, this.testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToFirstPage_PreviousPageButton()
        {
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(kendoGrid, 2);
            int targetPage = 1;
            GridFilterPage gridFilterPage = new GridFilterPage(this.driver);
            gridFilterPage.GoToPreviousPage.Click();
            this.WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(this.testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            this.AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, this.testPagingItems.Count());
        }

        // Add more test cases for disabled arrow buttons. + more pages button NavigateToLastPage_MorePagesNextButton, NavigateToPage10_MorePagesPreviousButton
        
        #endregion
            
        private void NavigateToGridInitialPage(KendoGrid kendoGrid, int initialPageNumber)
        {
            GridFilterPage gridFilterPage = new GridFilterPage(this.driver);
            gridFilterPage.NavigateTo();
            kendoGrid.Filter(ShipNameColumnName, Enums.FilterOperator.EqualTo, this.uniqueShippingName);
            kendoGrid.ChangePageSize(1);
            this.WaitForGridToLoad(1, kendoGrid);
            kendoGrid.NavigateToPage(initialPageNumber);
            WaitForPageToLoad(initialPageNumber, kendoGrid);
            this.AssertPagerInfoLabel(gridFilterPage, initialPageNumber, initialPageNumber, this.testPagingItems.Count);
        }
            
        private void AssertPagerInfoLabel(GridFilterPage page, int startItems, int endItems, int totalItems)
        {
            string expectedLabel = string.Format("{0} - {1} of {2} items", startItems, endItems, totalItems);
            Assert.AreEqual(expectedLabel, page.PagerInfoLabel.Text);
        }
            
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
            
        private void InitializeInvoicesForPaging()
        {
            int totalOrders = 10;
            if (!string.IsNullOrEmpty(this.uniqueShippingName))
            {
                uniqueShippingName = Guid.NewGuid().ToString();
            }
            this.testPagingItems = new List<Order>();
            for (int i = 0; i < totalOrders; i++)
            {
                var newOrder = this.CreateNewItemInDb(this.uniqueShippingName);
                testPagingItems.Add(newOrder);
            }
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