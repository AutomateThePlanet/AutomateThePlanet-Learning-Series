// <copyright file="OrderIdColumnAsserter.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ui.Automation.Core.Controls.Controls;
using Ui.Automation.Core.Controls.Enums;

namespace AdvancedReuseTacticsGridControlsAutomatedTests.GridTestCases
{
    public class OrderIdColumnAsserter : GridColumnAsserter
    {
        public OrderIdColumnAsserter(Pages.IGridPage gridPage) : base(gridPage)
        {
        }

        public void OrderIdEqualToFilter()
        {
            GridPage.NavigateTo();
            var newItem = CreateNewItemInDb();

            GridPage.Grid.Filter(GridColumns.OrderId, FilterOperator.EqualTo, newItem.OrderId.ToString());           
            WaitForGridToLoad(1, GridPage.Grid);
            var items = GridPage.Grid.GetItems<GridItem>();
        
            Assert.AreEqual(1, items.Count);
        }
        
        public void OrderIdGreaterThanOrEqualToFilter()
        {
            GridPage.NavigateTo();

            // Create new item with unique ship name;
            var newItem = CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            
            // When we filter by the second unique column ShippingName, only one item will be displayed. Once we apply the second not equal to filter the grid should be empty.
            GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderId, FilterOperator.GreaterThanOrEqualTo, newItem.OrderId.ToString()),
                new GridFilter(GridColumns.ShipName, FilterOperator.EqualTo, newItem.ShipName));
            WaitForGridToLoadAtLeast(2, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.AreEqual(secondNewItem.OrderId, results.FirstOrDefault(x => x.ShipName == secondNewItem.ShipName).OrderId);
            Assert.AreEqual(newItem.OrderId, results.FirstOrDefault(x => x.ShipName == newItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 2);
        }
        
        public void OrderIdGreaterThanFilter()
        {
            GridPage.NavigateTo();
            
            // Create new item with unique ship name;
            var newItem = CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            
            // Filter by the smaller orderId but also by the second unique column in this case shipping name
            GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderId, FilterOperator.GreaterThan, newItem.OrderId.ToString()),
                new GridFilter(GridColumns.ShipName, FilterOperator.EqualTo, newItem.ShipName));
            WaitForGridToLoadAtLeast(1, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();
            
            Assert.AreEqual(secondNewItem.OrderId, results.FirstOrDefault(x => x.ShipName == secondNewItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 1);
        }
        
        public void OrderIdLessThanOrEqualToFilter()
        {
            GridPage.NavigateTo();
            
            // Create new item with unique ship name;
            var newItem = CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            
            // Filter by the larger orderId but also by the second unique column in this case shipping name
            GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderId, FilterOperator.LessThanOrEqualTo, secondNewItem.OrderId.ToString()),
                new GridFilter(GridColumns.ShipName, FilterOperator.EqualTo, newItem.ShipName));
            WaitForGridToLoadAtLeast(2, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();
            
            Assert.AreEqual(newItem.OrderId, results.FirstOrDefault(x => x.ShipName == newItem.ShipName).OrderId);
            Assert.AreEqual(secondNewItem.OrderId, results.Last(x => x.ShipName == secondNewItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 2);
        }
        
        public void OrderIdLessThanFilter()
        {
            GridPage.NavigateTo();
            
            // Create new item with unique ship name;
            var newItem = CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            
            // Filter by the larger orderId but also by the second unique column in this case shipping name
            GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderId, FilterOperator.LessThan, secondNewItem.OrderId.ToString()),
                new GridFilter(GridColumns.ShipName, FilterOperator.EqualTo, secondNewItem.ShipName));
            WaitForGridToLoadAtLeast(1, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();
            
            Assert.AreEqual(newItem.OrderId, results.FirstOrDefault(x => x.ShipName == newItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 1);
        }
        
        public void OrderIdNotEqualToFilter()
        {
            GridPage.NavigateTo();
            
            // Create new item with unique ship name;
            var newItem = CreateNewItemInDb();
            // Create second new item with the same unique shipping name 
            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            
            // Filter by the larger orderId but also by the second unique column in this case shipping name
            GridPage.Grid.Filter(
                new GridFilter(GridColumns.OrderId, FilterOperator.NotEqualTo, secondNewItem.OrderId.ToString()),
                new GridFilter(GridColumns.ShipName, FilterOperator.EqualTo, secondNewItem.ShipName));
            WaitForGridToLoadAtLeast(1, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();
            
            Assert.AreEqual(newItem.OrderId, results.FirstOrDefault(x => x.ShipName == newItem.ShipName).OrderId);
            Assert.IsTrue(results.Count() == 1);
        }
        
        public void OrderIdClearFilter()
        {
            GridPage.NavigateTo();
            // Create new item with unique ship name;
            var newItem = CreateNewItemInDb();
            // Make sure that we have at least 2 items if the grid is empty. The tests are designed to run against empty DB.
            CreateNewItemInDb(newItem.ShipName);
            
            GridPage.Grid.Filter(GridColumns.OrderId, FilterOperator.EqualTo, newItem.OrderId.ToString());
            WaitForGridToLoad(1, GridPage.Grid);
            GridPage.Grid.RemoveFilters();
            
            WaitForGridToLoadAtLeast(1, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();
            Assert.IsTrue(results.Count() > 1);
        }
    }
}