// <copyright file="ShipNameColumnAsserter.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ui.Automation.Core.Controls.Controls;
using Ui.Automation.Core.Controls.Enums;

namespace AdvancedReuseTacticsGridControlsAutomatedTests.GridTestCases
{
    public class ShipNameColumnAsserter : GridColumnAsserter
    {
        public ShipNameColumnAsserter(Pages.IGridPage gridPage) : base(gridPage)
        {
        }

        public void ShipNameEqualToFilter()
        {
            GridPage.NavigateTo();
            var newItem = CreateNewItemInDb();

            GridPage.Grid.Filter(GridColumns.ShipName, FilterOperator.EqualTo, newItem.ShipName);
            WaitForGridToLoad(1, GridPage.Grid);
            var items = GridPage.Grid.GetItems<GridItem>();
            
            Assert.AreEqual(1, items.Count);
        }
        
        public void ShipNameContainsFilter()
        {
            GridPage.NavigateTo();
            var shipName = Guid.NewGuid().ToString();
            // Remove first and last letter
            shipName = shipName.TrimStart(shipName.First()).TrimEnd(shipName.Last());
            var newItem = CreateNewItemInDb(shipName);          

            GridPage.Grid.Filter(GridColumns.ShipName, FilterOperator.Contains, newItem.ShipName);
            WaitForGridToLoad(1, GridPage.Grid);
            var items = GridPage.Grid.GetItems<GridItem>();
            
            Assert.AreEqual(1, items.Count);
        }
        
        public void ShipNameEndsWithFilter()
        {
            GridPage.NavigateTo();

            // Remove first letter 
            var shipName = Guid.NewGuid().ToString();
            shipName = shipName.TrimStart(shipName.First());
            var newItem = CreateNewItemInDb(shipName);

            GridPage.Grid.Filter(GridColumns.ShipName, FilterOperator.EndsWith, newItem.ShipName);
            WaitForGridToLoad(1, GridPage.Grid);
            var items = GridPage.Grid.GetItems<GridItem>();
            
            Assert.AreEqual(1, items.Count);
        }
        
        public void ShipNameStartsWithFilter()
        {
            GridPage.NavigateTo();

            // Remove last letter
            var shipName = Guid.NewGuid().ToString();
            shipName = shipName.TrimEnd(shipName.Last());
            var newItem = CreateNewItemInDb(shipName);

            GridPage.Grid.Filter(GridColumns.ShipName, FilterOperator.StartsWith, newItem.ShipName);
            WaitForGridToLoad(1, GridPage.Grid);
            var items = GridPage.Grid.GetItems<GridItem>();
            
            Assert.AreEqual(1, items.Count);
        }
        
        public void ShipNameNotEqualToFilter()
        {
            GridPage.NavigateTo();
            
            // Apply combined filter. First filter by ID and than by ship name (not equal filter). 
            // After the first filter there is only one element when we apply the second we expect 0 elements.
            var newItem = CreateNewItemInDb();
            
            GridPage.Grid.Filter(
                new GridFilter(GridColumns.ShipName, FilterOperator.NotEqualTo, newItem.ShipName),
                new GridFilter(GridColumns.OrderId, FilterOperator.EqualTo, newItem.OrderId.ToString()));
            WaitForGridToLoad(0, GridPage.Grid);
            var items = GridPage.Grid.GetItems<GridItem>();
            
            Assert.AreEqual(0, items.Count);
        }
        
        public void ShipNameNotContainsFilter()
        {
            GridPage.NavigateTo();
          
            // Remove first and last letter
            var shipName = Guid.NewGuid().ToString();
            shipName = shipName.TrimStart(shipName.First()).TrimEnd(shipName.Last());
            var newItem = CreateNewItemInDb(shipName);

            // Apply combined filter. First filter by ID and than by ship name (not equal filter). 
            // After the first filter there is only one element when we apply the second we expect 0 elements.
            GridPage.Grid.Filter(
                new GridFilter(GridColumns.ShipName, FilterOperator.NotContains, newItem.ShipName),
                new GridFilter(GridColumns.OrderId, FilterOperator.EqualTo, newItem.OrderId.ToString()));
            WaitForGridToLoad(0, GridPage.Grid);
            var items = GridPage.Grid.GetItems<GridItem>();
            
            Assert.AreEqual(0, items.Count);
        }
        
        public void ShipNameClearFilter()
        {
            GridPage.NavigateTo();
            CreateNewItemInDb();

            // Filter by GUID and we know we wait the grid to be empty
            GridPage.Grid.Filter(GridColumns.ShipName, FilterOperator.StartsWith, Guid.NewGuid().ToString());
            WaitForGridToLoad(0, GridPage.Grid);
            // Remove all filters and we expect that the grid will contain at least 1 item.
            GridPage.Grid.RemoveFilters();
            WaitForGridToLoadAtLeast(1, GridPage.Grid);
        }
    }
}