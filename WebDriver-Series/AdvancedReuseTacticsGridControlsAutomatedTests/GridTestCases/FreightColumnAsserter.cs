// <copyright file="FreightColumnAsserter.cs" company="Automate The Planet Ltd.">
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
// <site>https://automatetheplanet.com/</site>
using System;
using System.Linq;
using AdvancedReuseTacticsGridControlsAutomatedTests.GridTestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ui.Automation.Core.Controls.Enums;
using Ui.Automation.Core.Controls.Controls;

namespace WebDriver.Series.Tests.GridTestCases
{
    public class FreightColumnAsserter : GridColumnAsserter
    {
        public FreightColumnAsserter(AdvancedReuseTacticsGridControlsAutomatedTests.GridTestCases.Pages.IGridPage gridPage) : base(gridPage)
        {
        }

        public void FreightEqualToFilter()
        {
            GridPage.NavigateTo();
            
            var newItem = CreateNewItemInDb();
            newItem.Freight = GetUniqueNumberValue();
            UpdateItemInDb(newItem);
            
            GridPage.Grid.Filter(GridColumns.Freight, FilterOperator.EqualTo, newItem.Freight.ToString());
            WaitForGridToLoadAtLeast(1, GridPage.Grid);
            var results = GridPage.Grid.GetItems<AdvancedReuseTacticsGridControlsAutomatedTests.Order>();
            
            Assert.IsTrue(results.Count() == 1);
            Assert.AreEqual(newItem.Freight.ToString(), results[0].Freight);
        }
        
        public void FreightGreaterThanOrEqualToFilter()
        {
            GridPage.NavigateTo();
            
            var allItems = GetAllItemsFromDb().OrderBy(x => x.Freight);
            var biggestFreight = allItems.Last().Freight;
            
            var newItem = CreateNewItemInDb();
            newItem.Freight = biggestFreight + GetUniqueNumberValue();
            UpdateItemInDb(newItem);
            
            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = newItem.Freight + 1;
            UpdateItemInDb(secondNewItem);
            
            GridPage.Grid.Filter(GridColumns.Freight, FilterOperator.GreaterThanOrEqualTo, newItem.Freight.ToString());
            WaitForGridToLoadAtLeast(2, GridPage.Grid);
            var results = GridPage.Grid.GetItems<AdvancedReuseTacticsGridControlsAutomatedTests.Order>();

            Assert.IsTrue(results.Count() == 2);
            
            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
            Assert.AreEqual(1, results.Count(x => x.Freight == newItem.Freight));
        }
        
        public void FreightGreaterThanFilter()
        {
            GridPage.NavigateTo();
            
            var allItems = GetAllItemsFromDb().OrderBy(x => x.Freight);
            var biggestFreight = allItems.Last().Freight;
            
            var newItem = CreateNewItemInDb();
            newItem.Freight = biggestFreight + GetUniqueNumberValue();
            UpdateItemInDb(newItem);
            
            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = newItem.Freight + 1;
            UpdateItemInDb(secondNewItem);
            
            GridPage.Grid.Filter(GridColumns.Freight, FilterOperator.GreaterThan, newItem.Freight.ToString());
            WaitForGridToLoadAtLeast(1, GridPage.Grid);
            var results = GridPage.Grid.GetItems<AdvancedReuseTacticsGridControlsAutomatedTests.Order>();

            Assert.IsTrue(results.Count() == 1);
            
            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
        }
        
        public void FreightLessThanOrEqualToFilter()
        {
            GridPage.NavigateTo();
            
            var allItems = GetAllItemsFromDb().OrderBy(x => x.Freight);
            var smallestFreight = allItems.First().Freight;
            
            var newItem = CreateNewItemInDb();
            newItem.Freight = Math.Round(smallestFreight - GetUniqueNumberValue(), 3, MidpointRounding.AwayFromZero);
            UpdateItemInDb(newItem);
            
            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = Math.Round((newItem.Freight - 0.01), 3, MidpointRounding.AwayFromZero);
            UpdateItemInDb(secondNewItem);
            
            GridPage.Grid.Filter(GridColumns.Freight, FilterOperator.LessThanOrEqualTo, newItem.Freight.ToString());
            WaitForGridToLoadAtLeast(2, GridPage.Grid);
            var results = GridPage.Grid.GetItems<AdvancedReuseTacticsGridControlsAutomatedTests.Order>();

            Assert.IsTrue(results.Count() == 2);
            
            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
            Assert.AreEqual(1, results.Count(x => x.Freight == newItem.Freight));
        }
        
        public void FreightLessThanFilter()
        {
            GridPage.NavigateTo();
            
            var allItems = GetAllItemsFromDb().OrderBy(x => x.Freight);
            var smallestFreight = allItems.First().Freight;
            
            var newItem = CreateNewItemInDb();
            newItem.Freight = Math.Round(smallestFreight - GetUniqueNumberValue(), 3, MidpointRounding.AwayFromZero);
            UpdateItemInDb(newItem);
            
            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = Math.Round((newItem.Freight - 0.01), 3, MidpointRounding.AwayFromZero);
            UpdateItemInDb(secondNewItem);
            
            GridPage.Grid.Filter(GridColumns.Freight, FilterOperator.LessThan, newItem.Freight.ToString());
            WaitForGridToLoadAtLeast(1, GridPage.Grid);
            var results = GridPage.Grid.GetItems<AdvancedReuseTacticsGridControlsAutomatedTests.Order>();

            Assert.IsTrue(results.Count() == 1);
            
            // We assume that there isn't default sorting for this column so we cannot expect that the order will be the same every time.
            Assert.AreEqual(1, results.Count(x => x.Freight == secondNewItem.Freight));
        }
        
        public void FreightNotEqualToFilter()
        {
            GridPage.NavigateTo();
            
            var newItem = CreateNewItemInDb();
            newItem.Freight = GetUniqueNumberValue();
            UpdateItemInDb(newItem);
            
            // After we apply the orderId filter, only 1 item is displayed in the grid. When we apply the NotEqualTo filter this item will disappear.
            GridPage.Grid.Filter(
                new GridFilter(GridColumns.Freight, FilterOperator.NotEqualTo, newItem.Freight.ToString()),
                new GridFilter(GridColumns.OrderId, FilterOperator.EqualTo, newItem.OrderId.ToString()));
            WaitForGridToLoad(0, GridPage.Grid);
            var results = GridPage.Grid.GetItems<AdvancedReuseTacticsGridControlsAutomatedTests.Order>();
        
            Assert.IsTrue(results.Count() == 0);
        }
        
        public void FreightClearFilter()
        {
            GridPage.NavigateTo();
            
            var allItems = GetAllItemsFromDb().OrderBy(x => x.Freight);
            var biggestFreight = allItems.Last().Freight;
            
            var newItem = CreateNewItemInDb();
            newItem.Freight = biggestFreight + GetUniqueNumberValue();
            UpdateItemInDb(newItem);
            
            var secondNewItem = CreateNewItemInDb(newItem.ShipName);
            secondNewItem.Freight = newItem.Freight + 1;
            UpdateItemInDb(secondNewItem);
            
            GridPage.Grid.Filter(GridColumns.Freight, FilterOperator.EqualTo, newItem.Freight.ToString());
            WaitForGridToLoad(1, GridPage.Grid);
            GridPage.Grid.RemoveFilters();
        
            WaitForGridToLoadAtLeast(2, GridPage.Grid);
        }
    }
}