using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebDriver.Series.Tests.Controls;
using WebDriver.Series.Tests.GridTestCases.Pages;

namespace WebDriver.Series.Tests.GridTestCases
{
    public class FreightColumnAsserter : GridColumnAsserter
    {
        public FreightColumnAsserter(IGridPage gridPage) : base(gridPage)
        {
        }

        public void FreightEqualToFilter()
        {
            this.GridPage.NavigateTo();
            
            var newItem = this.CreateNewItemInDb();
            newItem.Freight = this.GetUniqueNumberValue();
            this.UpdateItemInDb(newItem);
            
            this.GridPage.Grid.Filter(GridColumns.Freight, Enums.FilterOperator.EqualTo, newItem.Freight.ToString());
            this.WaitForGridToLoadAtLeast(1, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();
            
            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(newItem.Freight.ToString(), results[0].Freight);
        }
        
        public void FreightGreaterThanOrEqualToFilter()
        {
            this.GridPage.NavigateTo();
            
            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.Freight);
            var biggestFreight = allItems.Last().Freight;
            
            var newItem = this.CreateNewItemInDb();
            newItem.Freight = biggestFreight + this.GetUniqueNumberValue();
            this.UpdateItemInDb(newItem);
            
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = newItem.Freight + 1;
            this.UpdateItemInDb(secondNewItem);
            
            this.GridPage.Grid.Filter(GridColumns.Freight, Enums.FilterOperator.GreaterThanOrEqualTo, newItem.Freight.ToString());
            this.WaitForGridToLoadAtLeast(2, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            
            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
            Assert.AreEqual(1, results.Count(x => x.Freight == newItem.Freight));
        }
        
        public void FreightGreaterThanFilter()
        {
            this.GridPage.NavigateTo();
            
            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.Freight);
            var biggestFreight = allItems.Last().Freight;
            
            var newItem = this.CreateNewItemInDb();
            newItem.Freight = biggestFreight + this.GetUniqueNumberValue();
            this.UpdateItemInDb(newItem);
            
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = newItem.Freight + 1;
            this.UpdateItemInDb(secondNewItem);
            
            this.GridPage.Grid.Filter(GridColumns.Freight, Enums.FilterOperator.GreaterThan, newItem.Freight.ToString());
            this.WaitForGridToLoadAtLeast(1, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            
            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
        }
        
        public void FreightLessThanOrEqualToFilter()
        {
            this.GridPage.NavigateTo();
            
            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.Freight);
            var smallestFreight = allItems.First().Freight;
            
            var newItem = this.CreateNewItemInDb();
            newItem.Freight = Math.Round(smallestFreight - this.GetUniqueNumberValue(), 3, MidpointRounding.AwayFromZero);
            this.UpdateItemInDb(newItem);
            
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = Math.Round((newItem.Freight - 0.01), 3, MidpointRounding.AwayFromZero);
            this.UpdateItemInDb(secondNewItem);
            
            this.GridPage.Grid.Filter(GridColumns.Freight, Enums.FilterOperator.LessThanOrEqualTo, newItem.Freight.ToString());
            this.WaitForGridToLoadAtLeast(2, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            
            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
            Assert.AreEqual(1, results.Count(x => x.Freight == newItem.Freight));
        }
        
        public void FreightLessThanFilter()
        {
            this.GridPage.NavigateTo();
            
            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.Freight);
            var smallestFreight = allItems.First().Freight;
            
            var newItem = this.CreateNewItemInDb();
            newItem.Freight = Math.Round(smallestFreight - this.GetUniqueNumberValue(), 3, MidpointRounding.AwayFromZero);
            this.UpdateItemInDb(newItem);
            
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = Math.Round((newItem.Freight - 0.01), 3, MidpointRounding.AwayFromZero);
            this.UpdateItemInDb(secondNewItem);
            
            this.GridPage.Grid.Filter(GridColumns.Freight, Enums.FilterOperator.LessThan, newItem.Freight.ToString());
            this.WaitForGridToLoadAtLeast(1, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            
            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
        }
        
        public void FreightNotEqualToFilter()
        {
            this.GridPage.NavigateTo();
            
            var newItem = this.CreateNewItemInDb();
            newItem.Freight = this.GetUniqueNumberValue();
            this.UpdateItemInDb(newItem);
            
            // After we apply the orderId filter, only 1 item is displayed in the grid. When we apply the NotEqualTo filter this item will disappear.
            this.GridPage.Grid.Filter(
                new GridFilter(GridColumns.Freight, Enums.FilterOperator.NotEqualTo, newItem.Freight.ToString()),
                new GridFilter(GridColumns.OrderID, Enums.FilterOperator.EqualTo, newItem.OrderId.ToString()));
            this.WaitForGridToLoad(0, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();
        
            Assert.IsTrue(results.Count() == 0);
        }
        
        public void FreightClearFilter()
        {
            this.GridPage.NavigateTo();
            
            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.Freight);
            var biggestFreight = allItems.Last().Freight;
            
            var newItem = this.CreateNewItemInDb();
            newItem.Freight = biggestFreight + this.GetUniqueNumberValue();
            this.UpdateItemInDb(newItem);
            
            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = newItem.Freight + 1;
            this.UpdateItemInDb(secondNewItem);
            
            this.GridPage.Grid.Filter(GridColumns.Freight, Enums.FilterOperator.EqualTo, newItem.Freight.ToString());
            this.WaitForGridToLoad(1, this.GridPage.Grid);
            this.GridPage.Grid.RemoveFilters();
        
            this.WaitForGridToLoadAtLeast(2, this.GridPage.Grid);
        }
    }
}