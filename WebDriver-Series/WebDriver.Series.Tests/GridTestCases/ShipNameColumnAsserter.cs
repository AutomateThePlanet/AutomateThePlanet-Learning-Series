using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebDriver.Series.Tests.Controls;
using WebDriver.Series.Tests.GridTestCases.Pages;

namespace WebDriver.Series.Tests.GridTestCases
{
    public class ShipNameColumnAsserter : GridColumnAsserter
    {
        public ShipNameColumnAsserter(IGridPage gridPage) : base(gridPage)
        {
        }

        public void ShipNameEqualToFilter()
        {
            this.GridPage.NavigateTo();
            var newItem = this.CreateNewItemInDb();

            this.GridPage.Grid.Filter(GridColumns.ShipName, Enums.FilterOperator.EqualTo, newItem.ShipName);
            this.WaitForGridToLoad(1, this.GridPage.Grid);
            var items = this.GridPage.Grid.GetItems<GridItem>();
            
            Assert.AreEqual(1, items.Count);
        }
        
        public void ShipNameContainsFilter()
        {
            this.GridPage.NavigateTo();
            string shipName = Guid.NewGuid().ToString();
            // Remove first and last letter
            shipName = shipName.TrimStart(shipName.First()).TrimEnd(shipName.Last());
            var newItem = this.CreateNewItemInDb(shipName);          

            this.GridPage.Grid.Filter(GridColumns.ShipName, Enums.FilterOperator.Contains, newItem.ShipName);
            this.WaitForGridToLoad(1, this.GridPage.Grid);
            var items = this.GridPage.Grid.GetItems<GridItem>();
            
            Assert.AreEqual(1, items.Count);
        }
        
        public void ShipNameEndsWithFilter()
        {
            this.GridPage.NavigateTo();

            // Remove first letter 
            string shipName = Guid.NewGuid().ToString();
            shipName = shipName.TrimStart(shipName.First());
            var newItem = this.CreateNewItemInDb(shipName);

            this.GridPage.Grid.Filter(GridColumns.ShipName, Enums.FilterOperator.EndsWith, newItem.ShipName);
            this.WaitForGridToLoad(1, this.GridPage.Grid);
            var items = this.GridPage.Grid.GetItems<GridItem>();
            
            Assert.AreEqual(1, items.Count);
        }
        
        public void ShipNameStartsWithFilter()
        {
            this.GridPage.NavigateTo();

            // Remove last letter
            string shipName = Guid.NewGuid().ToString();
            shipName = shipName.TrimEnd(shipName.Last());
            var newItem = this.CreateNewItemInDb(shipName);

            this.GridPage.Grid.Filter(GridColumns.ShipName, Enums.FilterOperator.StartsWith, newItem.ShipName);
            this.WaitForGridToLoad(1, this.GridPage.Grid);
            var items = this.GridPage.Grid.GetItems<GridItem>();
            
            Assert.AreEqual(1, items.Count);
        }
        
        public void ShipNameNotEqualToFilter()
        {
            this.GridPage.NavigateTo();
            
            // Apply combined filter. First filter by ID and than by ship name (not equal filter). 
            // After the first filter there is only one element when we apply the second we expect 0 elements.
            var newItem = this.CreateNewItemInDb();
            
            this.GridPage.Grid.Filter(
                new GridFilter(GridColumns.ShipName, Enums.FilterOperator.NotEqualTo, newItem.ShipName),
                new GridFilter(GridColumns.OrderID, Enums.FilterOperator.EqualTo, newItem.OrderId.ToString()));
            this.WaitForGridToLoad(0, this.GridPage.Grid);
            var items = this.GridPage.Grid.GetItems<GridItem>();
            
            Assert.AreEqual(0, items.Count);
        }
        
        public void ShipNameNotContainsFilter()
        {
            this.GridPage.NavigateTo();
          
            // Remove first and last letter
            string shipName = Guid.NewGuid().ToString();
            shipName = shipName.TrimStart(shipName.First()).TrimEnd(shipName.Last());
            var newItem = this.CreateNewItemInDb(shipName);

            // Apply combined filter. First filter by ID and than by ship name (not equal filter). 
            // After the first filter there is only one element when we apply the second we expect 0 elements.
            this.GridPage.Grid.Filter(
                new GridFilter(GridColumns.ShipName, Enums.FilterOperator.NotContains, newItem.ShipName),
                new GridFilter(GridColumns.OrderID, Enums.FilterOperator.EqualTo, newItem.OrderId.ToString()));
            this.WaitForGridToLoad(0, this.GridPage.Grid);
            var items = this.GridPage.Grid.GetItems<GridItem>();
            
            Assert.AreEqual(0, items.Count);
        }
        
        public void ShipNameClearFilter()
        {
            this.GridPage.NavigateTo();
            this.CreateNewItemInDb();

            // Filter by GUID and we know we wait the grid to be empty
            this.GridPage.Grid.Filter(GridColumns.ShipName, Enums.FilterOperator.StartsWith, Guid.NewGuid().ToString());
            this.WaitForGridToLoad(0, this.GridPage.Grid);
            // Remove all filters and we expect that the grid will contain at least 1 item.
            this.GridPage.Grid.RemoveFilters();
            WaitForGridToLoadAtLeast(1, this.GridPage.Grid);
        }
    }
}