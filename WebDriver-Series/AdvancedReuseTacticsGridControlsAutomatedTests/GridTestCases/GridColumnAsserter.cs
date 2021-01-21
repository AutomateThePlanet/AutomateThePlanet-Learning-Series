﻿// <copyright file="GridColumnAsserter.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Threading;
using Ui.Automation.Core.Controls.Controls;

namespace AdvancedReuseTacticsGridControlsAutomatedTests.GridTestCases
{
    public class GridColumnAsserter
    {
        public GridColumnAsserter(Pages.IGridPage gridPage)
        {
            GridPage = gridPage;
        }

        protected Pages.IGridPage GridPage { get; set; }

        protected void WaitForPageToLoad(int expectedPage, KendoGrid grid)
        {
            Until(() =>
            {
                var currentPage = grid.GetCurrentPageNumber();
                return currentPage == expectedPage;
            });
        }

        protected void WaitForGridToLoad(int expectedCount, KendoGrid grid)
        {
            Until(
                () =>
                {
                    var items = grid.GetItems<GridItem>();
                    return expectedCount == items.Count;
                });
        }

        protected void WaitForGridToLoadAtLeast(int expectedCount, KendoGrid grid)
        {
            Until(
                () =>
                {
                    var items = grid.GetItems<GridItem>();
                    return items.Count >= expectedCount;
                });
        }

        protected void Until(Func<bool> condition, int timeout = 10, string exceptionMessage = "Timeout exceeded.", int retryRateDelay = 50)
        {
            var start = DateTime.Now;
            while (!condition())
            {
                var now = DateTime.Now;
                var totalSeconds = (now - start).TotalSeconds;
                if (totalSeconds >= timeout)
                {
                    throw new TimeoutException(exceptionMessage);
                }

                Thread.Sleep(retryRateDelay);
            }
        }

        protected List<Order> GetAllItemsFromDb()
        {
            // Create dummy orders. This logic should be replaced with service oriented call to your DB and get all items that are populated in the grid.
            var orders = new List<Order>();
            for (var i = 0; i < 10; i++)
            {
                orders.Add(new Order());
            }

            return orders;
        }

        protected Order CreateNewItemInDb(string shipName = null)
        {
            // Replace it with service oriented call to your DB. Create real enity in DB.
            return new Order(shipName);
        }

        protected void UpdateItemInDb(Order order)
        {
            // Replace it with service oriented call to your DB. Update the enity in the DB.
        }

        protected int GetUniqueNumberValue()
        {
            var currentTime = DateTime.Now;
            var result = currentTime.Year + currentTime.Month + currentTime.Hour + currentTime.Minute + currentTime.Second + currentTime.Millisecond;
            return result;
        }
    }
}