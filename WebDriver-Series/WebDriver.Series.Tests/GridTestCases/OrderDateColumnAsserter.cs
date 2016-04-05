using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebDriver.Series.Tests.Controls;
using WebDriver.Series.Tests.GridTestCases.Pages;

namespace WebDriver.Series.Tests.GridTestCases
{
    public class OrderDateColumnAsserter : GridColumnAsserter
    {
        public OrderDateColumnAsserter(IGridPage gridPage) : base(gridPage)
        {
        }

        public void OrderDateEqualToFilter()
        {
            this.GridPage.NavigateTo();

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderId);
            var lastOrderDate = allItems.Last().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(1);
            this.UpdateItemInDb(newItem);

            this.GridPage.Grid.Filter(GridColumns.OrderDate, Enums.FilterOperator.EqualTo, newItem.OrderDate.ToString());
            this.WaitForGridToLoadAtLeast(1, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(newItem.OrderDate.ToString(), results[0].OrderDate);
        }

        public void OrderDateNotEqualToFilter()
        {
            this.GridPage.NavigateTo();

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
            this.GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderDate, Enums.FilterOperator.NotEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(GridColumns.ShipName, Enums.FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(1, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
        }

        public void OrderDateAfterFilter()
        {
            this.GridPage.NavigateTo();

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
            this.GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderDate, Enums.FilterOperator.IsAfter, newItem.OrderDate.ToString()),
                new GridFilter(GridColumns.ShipName, Enums.FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(1, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
        }

        public void OrderDateIsAfterOrEqualToFilter()
        {
            this.GridPage.NavigateTo();

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
            this.GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderDate, Enums.FilterOperator.IsAfterOrEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(GridColumns.ShipName, Enums.FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(2, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
            Assert.AreEqual(newItem.ToString(), results[1].OrderDate);
        }

        public void OrderDateBeforeFilter()
        {
            this.GridPage.NavigateTo();

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
            this.GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderDate, Enums.FilterOperator.IsBefore, newItem.OrderDate.ToString()),
                new GridFilter(GridColumns.ShipName, Enums.FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(1, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
        }

        public void OrderDateIsBeforeOrEqualToFilter()
        {
            this.GridPage.NavigateTo();

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.First().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            this.UpdateItemInDb(newItem);

            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            this.UpdateItemInDb(secondNewItem);

            this.GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderDate, Enums.FilterOperator.IsBeforeOrEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(GridColumns.ShipName, Enums.FilterOperator.EqualTo, newItem.ShipName));
            this.WaitForGridToLoadAtLeast(2, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
            Assert.AreEqual(newItem.ToString(), results[1].OrderDate);
        }

        public void OrderDateClearFilter()
        {
            this.GridPage.NavigateTo();

            this.CreateNewItemInDb();

            this.GridPage.Grid.Filter(GridColumns.OrderDate, Enums.FilterOperator.IsAfter, DateTime.MaxValue.ToString());
            this.WaitForGridToLoad(0, this.GridPage.Grid);
            this.GridPage.Grid.RemoveFilters();

            this.WaitForGridToLoadAtLeast(1, this.GridPage.Grid);
        }

        public void OrderDateSortAsc()
        {
            this.GridPage.NavigateTo();

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.First().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            this.UpdateItemInDb(newItem);

            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            this.UpdateItemInDb(secondNewItem);

            this.GridPage.Grid.Filter(GridColumns.ShipName, Enums.FilterOperator.EqualTo, newItem.ShipName);
            this.WaitForGridToLoadAtLeast(2, this.GridPage.Grid);
            this.GridPage.Grid.Sort(GridColumns.OrderDate, SortType.Asc);
            Thread.Sleep(1000);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
            Assert.AreEqual(newItem.ToString(), results[1].OrderDate);
        }

        public void OrderDateSortDesc()
        {
            this.GridPage.NavigateTo();

            var allItems = this.GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.First().OrderDate;

            var newItem = this.CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            this.UpdateItemInDb(newItem);

            var secondNewItem = this.CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            this.UpdateItemInDb(secondNewItem);

            this.GridPage.Grid.Filter(GridColumns.ShipName, Enums.FilterOperator.EqualTo, newItem.ShipName);
            this.WaitForGridToLoadAtLeast(2, this.GridPage.Grid);
            this.GridPage.Grid.Sort(GridColumns.OrderDate, SortType.Desc);
            Thread.Sleep(1000);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            Assert.AreEqual(newItem.ToString(), results[0].OrderDate);
            Assert.AreEqual(secondNewItem.ToString(), results[1].OrderDate);
        }
    }
}