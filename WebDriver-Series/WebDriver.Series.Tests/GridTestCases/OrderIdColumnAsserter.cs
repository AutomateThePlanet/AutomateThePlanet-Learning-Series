using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebDriver.Series.Tests.Controls;
using WebDriver.Series.Tests.GridTestCases.Pages;

namespace WebDriver.Series.Tests.GridTestCases
{
    public class OrderIdColumnAsserter : GridColumnAsserter
    {
        public OrderIdColumnAsserter(IGridPage gridPage) : base(gridPage)
        {
        }

        public void OrderIdEqualToFilter()
        {
            this.GridPage.NavigateTo();
            var newItem = this.CreateNewItemInDb();

            this.GridPage.Grid.Filter(GridColumns.OrderID, Enums.FilterOperator.EqualTo, newItem.OrderId.ToString());           
            this.WaitForGridToLoad(1, this.GridPage.Grid);
            var items = this.GridPage.Grid.GetItems<GridItem>();
        
            Assert.AreEqual(1, items.Count);
        }
        
        public void OrderIdGreaterThanOrEqualToFilter()
        {
            this.GridPage.NavigateTo();

            // Create new item with unique ship name;
            var newItem = this.CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            
            // When we filter by the second unique column ShippingName, only one item will be displayed. Once we apply the second not equal to filter the grid should be empty.
            this.GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderID, Enums.FilterOperator.GreaterThanOrEqualTo, newItem.OrderId.ToString()),
                new GridFilter(GridColumns.ShipName, Enums.FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(2, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.AreEqual(secondNewItem.OrderId, results.FirstOrDefault(x => x.ShipName == secondNewItem.ShipName).OrderId);
            Assert.AreEqual(newItem.OrderId, results.FirstOrDefault(x => x.ShipName == newItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 2);
        }
        
        public void OrderIdGreaterThanFilter()
        {
            this.GridPage.NavigateTo();
            
            // Create new item with unique ship name;
            var newItem = this.CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            
            // Filter by the smaller orderId but also by the second unique column in this case shipping name
            this.GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderID, Enums.FilterOperator.GreaterThan, newItem.OrderId.ToString()),
                new GridFilter(GridColumns.ShipName, Enums.FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(1, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();
            
            Assert.AreEqual(secondNewItem.OrderId, results.FirstOrDefault(x => x.ShipName == secondNewItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 1);
        }
        
        public void OrderIdLessThanOrEqualToFilter()
        {
            this.GridPage.NavigateTo();
            
            // Create new item with unique ship name;
            var newItem = this.CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            
            // Filter by the larger orderId but also by the second unique column in this case shipping name
            this.GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderID, Enums.FilterOperator.LessThanOrEqualTo, secondNewItem.OrderId.ToString()),
                new GridFilter(GridColumns.ShipName, Enums.FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(2, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();
            
            Assert.AreEqual(newItem.OrderId, results.FirstOrDefault(x => x.ShipName == newItem.ShipName).OrderId);
            Assert.AreEqual(secondNewItem.OrderId, results.Last(x => x.ShipName == secondNewItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 2);
        }
        
        public void OrderIdLessThanFilter()
        {
            this.GridPage.NavigateTo();
            
            // Create new item with unique ship name;
            var newItem = this.CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            
            // Filter by the larger orderId but also by the second unique column in this case shipping name
            this.GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderID, Enums.FilterOperator.LessThan, secondNewItem.OrderId.ToString()),
                new GridFilter(GridColumns.ShipName, Enums.FilterOperator.EqualTo, secondNewItem.ShipName));
            this.WaitForGridToLoadAtLeast(1, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();
            
            Assert.AreEqual(newItem.OrderId, results.FirstOrDefault(x => x.ShipName == newItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 1);
        }
        
        public void OrderIdNotEqualToFilter()
        {
            this.GridPage.NavigateTo();
            
            // Create new item with unique ship name;
            var newItem = this.CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            
            // Filter by the larger orderId but also by the second unique column in this case shipping name
            this.GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderID, Enums.FilterOperator.NotEqualTo, secondNewItem.OrderId.ToString()),
                new GridFilter(GridColumns.ShipName, Enums.FilterOperator.EqualTo, secondNewItem.ShipName));
            this.WaitForGridToLoadAtLeast(1, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();
            
            Assert.AreEqual(newItem.OrderId, results.FirstOrDefault(x => x.ShipName == newItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 1);
        }
        
        public void OrderIdClearFilter()
        {
            this.GridPage.NavigateTo();
            // Create new item with unique ship name;
            var newItem = this.CreateNewItemInDb();
            // Make sure that we have at least 2 items if the grid is empty. The tests are designed to run against empty DB.
            this.CreateNewItemInDb(newItem.ShipName);
            
            this.GridPage.Grid.Filter(GridColumns.OrderID, Enums.FilterOperator.EqualTo, newItem.OrderId.ToString());
            this.WaitForGridToLoad(1, this.GridPage.Grid);
            this.GridPage.Grid.RemoveFilters();
            
            this.WaitForGridToLoadAtLeast(1, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();
            Assert.IsTrue(results.Count() > 1);
        }
    }
}