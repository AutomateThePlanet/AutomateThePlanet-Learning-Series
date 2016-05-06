// <copyright file="ShipNameColumnAsserter.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ui.Automation.Core.Controls.Controls;
using Ui.Automation.Core.Controls.Enums;

namespace AdvancedReuseTacticsGridControlsAutomatedTests.GridTestCases
{
    public class ShipNameColumnAsserter : GridColumnAsserter
    {
        public ShipNameColumnAsserter(AdvancedReuseTacticsGridControlsAutomatedTests.GridTestCases.Pages.IGridPage gridPage) : base(gridPage)
        {
        }

        public void ShipNameEqualToFilter()
        {
            this.GridPage.NavigateTo();
            var newItem = this.CreateNewItemInDb();

            this.GridPage.Grid.Filter(GridColumns.ShipName, FilterOperator.EqualTo, newItem.ShipName);
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

            this.GridPage.Grid.Filter(GridColumns.ShipName, FilterOperator.Contains, newItem.ShipName);
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

            this.GridPage.Grid.Filter(GridColumns.ShipName, FilterOperator.EndsWith, newItem.ShipName);
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

            this.GridPage.Grid.Filter(GridColumns.ShipName, FilterOperator.StartsWith, newItem.ShipName);
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
                new GridFilter(GridColumns.ShipName, FilterOperator.NotEqualTo, newItem.ShipName),
                new GridFilter(GridColumns.OrderID, FilterOperator.EqualTo, newItem.OrderId.ToString()));
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
                new GridFilter(GridColumns.ShipName, FilterOperator.NotContains, newItem.ShipName),
                new GridFilter(GridColumns.OrderID, FilterOperator.EqualTo, newItem.OrderId.ToString()));
            this.WaitForGridToLoad(0, this.GridPage.Grid);
            var items = this.GridPage.Grid.GetItems<GridItem>();
            
            Assert.AreEqual(0, items.Count);
        }
        
        public void ShipNameClearFilter()
        {
            this.GridPage.NavigateTo();
            this.CreateNewItemInDb();

            // Filter by GUID and we know we wait the grid to be empty
            this.GridPage.Grid.Filter(GridColumns.ShipName, FilterOperator.StartsWith, Guid.NewGuid().ToString());
            this.WaitForGridToLoad(0, this.GridPage.Grid);
            // Remove all filters and we expect that the grid will contain at least 1 item.
            this.GridPage.Grid.RemoveFilters();
            WaitForGridToLoadAtLeast(1, this.GridPage.Grid);
        }
    }
}