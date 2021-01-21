// <copyright file="OrderDateColumnAsserter.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ui.Automation.Core.Controls.Enums;
using Ui.Automation.Core.Controls.Controls;

namespace AdvancedReuseTacticsGridControlsAutomatedTests.GridTestCases
{
    public class OrderDateColumnAsserter : GridColumnAsserter
    {
        public OrderDateColumnAsserter(Pages.IGridPage gridPage) : base(gridPage)
        {
        }

        public void OrderDateEqualToFilter()
        {
            GridPage.NavigateTo();

            var allItems = GetAllItemsFromDb().OrderBy(x => x.OrderId);
            var lastOrderDate = allItems.Last().OrderDate;

            var newItem = CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(1);
            UpdateItemInDb(newItem);

            GridPage.Grid.Filter(GridColumns.OrderDate, FilterOperator.EqualTo, newItem.OrderDate.ToString());
            WaitForGridToLoadAtLeast(1, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(newItem.OrderDate.ToString(), results[0].OrderDate);
        }

        public void OrderDateNotEqualToFilter()
        {
            GridPage.NavigateTo();

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
            GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderDate, FilterOperator.NotEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(GridColumns.ShipName, FilterOperator.EqualTo, newItem.ShipName));
            WaitForGridToLoadAtLeast(1, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
        }

        public void OrderDateAfterFilter()
        {
            GridPage.NavigateTo();

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
            GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderDate, FilterOperator.IsAfter, newItem.OrderDate.ToString()),
                new GridFilter(GridColumns.ShipName, FilterOperator.EqualTo, newItem.ShipName));
            WaitForGridToLoadAtLeast(1, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
        }

        public void OrderDateIsAfterOrEqualToFilter()
        {
            GridPage.NavigateTo();

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
            GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderDate, FilterOperator.IsAfterOrEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(GridColumns.ShipName, FilterOperator.EqualTo, newItem.ShipName));
            WaitForGridToLoadAtLeast(2, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
            Assert.AreEqual(newItem.ToString(), results[1].OrderDate);
        }

        public void OrderDateBeforeFilter()
        {
            GridPage.NavigateTo();

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
            GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderDate, FilterOperator.IsBefore, newItem.OrderDate.ToString()),
                new GridFilter(GridColumns.ShipName, FilterOperator.EqualTo, newItem.ShipName));
            WaitForGridToLoadAtLeast(1, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
        }

        public void OrderDateIsBeforeOrEqualToFilter()
        {
            GridPage.NavigateTo();

            var allItems = GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.First().OrderDate;

            var newItem = CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            UpdateItemInDb(secondNewItem);

            GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderDate, FilterOperator.IsBeforeOrEqualTo, newItem.OrderDate.ToString()),
                new GridFilter(GridColumns.ShipName, FilterOperator.EqualTo, newItem.ShipName));
            WaitForGridToLoadAtLeast(2, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
            Assert.AreEqual(newItem.ToString(), results[1].OrderDate);
        }

        public void OrderDateClearFilter()
        {
            GridPage.NavigateTo();

            CreateNewItemInDb();

            GridPage.Grid.Filter(GridColumns.OrderDate, FilterOperator.IsAfter, DateTime.MaxValue.ToString());
            WaitForGridToLoad(0, GridPage.Grid);
            GridPage.Grid.RemoveFilters();

            WaitForGridToLoadAtLeast(1, GridPage.Grid);
        }

        public void OrderDateSortAsc()
        {
            GridPage.NavigateTo();

            var allItems = GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.First().OrderDate;

            var newItem = CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            UpdateItemInDb(secondNewItem);

            GridPage.Grid.Filter(GridColumns.ShipName, FilterOperator.EqualTo, newItem.ShipName);
            WaitForGridToLoadAtLeast(2, GridPage.Grid);
            GridPage.Grid.Sort(GridColumns.OrderDate, SortType.Asc);
            Thread.Sleep(1000);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            Assert.AreEqual(secondNewItem.ToString(), results[0].OrderDate);
            Assert.AreEqual(newItem.ToString(), results[1].OrderDate);
        }

        public void OrderDateSortDesc()
        {
            GridPage.NavigateTo();

            var allItems = GetAllItemsFromDb().OrderBy(x => x.OrderDate);
            var lastOrderDate = allItems.First().OrderDate;

            var newItem = CreateNewItemInDb();
            newItem.OrderDate = lastOrderDate.AddDays(-1);
            UpdateItemInDb(newItem);

            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.OrderDate = lastOrderDate.AddDays(-2);
            UpdateItemInDb(secondNewItem);

            GridPage.Grid.Filter(GridColumns.ShipName, FilterOperator.EqualTo, newItem.ShipName);
            WaitForGridToLoadAtLeast(2, GridPage.Grid);
            GridPage.Grid.Sort(GridColumns.OrderDate, SortType.Desc);
            Thread.Sleep(1000);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.IsTrue(results.Count() == 2);
            Assert.AreEqual(newItem.ToString(), results[0].OrderDate);
            Assert.AreEqual(secondNewItem.ToString(), results[1].OrderDate);
        }
    }
}