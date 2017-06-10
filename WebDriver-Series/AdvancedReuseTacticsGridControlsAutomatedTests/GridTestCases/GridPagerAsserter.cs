// <copyright file="GridPagerAsserter.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ui.Automation.Core.Controls.Enums;

namespace AdvancedReuseTacticsGridControlsAutomatedTests.GridTestCases
{
    public class GridPagerAsserter : GridColumnAsserter
    {
        private string uniqueShippingName;
        private List<Order> testPagingItems;
        
        public GridPagerAsserter(Pages.IGridPage gridPage) : base(gridPage)
        {
        }

        public void NavigateToFirstPage_GoToFirstPageButton()
        {
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(11);
            int targetPage = 1;
            GridPage.GoToFirstPageButton.Click();
            WaitForPageToLoad(targetPage, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();
        
            Assert.AreEqual(testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(targetPage, targetPage, testPagingItems.Count());
        }

        public void NavigateToLastPage_GoToLastPageButton()
        {
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(1);
            int targetPage = 11;
            GridPage.GoToLastPage.Click();
            WaitForPageToLoad(targetPage, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.AreEqual(testPagingItems.Last().OrderId, results.First().OrderId);
            AssertPagerInfoLabel(targetPage, targetPage, testPagingItems.Count());
        }

        public void NavigateToPageNine_GoToPreviousPageButton()
        {
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(11);
            int targetPage = 10;
            GridPage.GoToPreviousPage.Click();
            WaitForPageToLoad(targetPage, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.AreEqual(testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(targetPage, targetPage, testPagingItems.Count());
        }

        public void NavigateToPageTwo_GoToNextPageButton()
        {
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(1);
            int targetPage = 2;
            GridPage.GoToNextPage.Click();
            WaitForPageToLoad(targetPage, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.AreEqual(testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(targetPage, targetPage, testPagingItems.Count());
        }

        public void NavigateToPageTwo_SecondPageButton()
        {
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(1);
            int targetPage = 2;
            GridPage.PageOnSecondPositionButton.Click();
            WaitForPageToLoad(targetPage, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.AreEqual(testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(targetPage, targetPage, testPagingItems.Count());
        }

        public void NavigateToLastPage_MorePagesNextButton()
        {
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(1);
            int targetPage = 11;
            GridPage.NextMorePages.Click();
            WaitForPageToLoad(targetPage, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.AreEqual(testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(targetPage, targetPage, testPagingItems.Count());
        }

        public void NavigateToPageOne_MorePagesPreviousButton()
        {
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(11);
            int targetPage = 1;
            GridPage.PreviousMorePages.Click();
            WaitForPageToLoad(targetPage, GridPage.Grid);
            var results = GridPage.Grid.GetItems<Order>();

            Assert.AreEqual(testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(targetPage, targetPage, testPagingItems.Count());
        }

        public void GoToFirstPageButtonDisabled_WhenFirstPageIsLoaded()
        {
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(11);
            int targetPage = 1;
            GridPage.GoToFirstPageButton.Click();
            WaitForPageToLoad(targetPage, GridPage.Grid);
        
            Assert.IsFalse(GridPage.GoToFirstPageButton.Enabled);
        }

        public void GoToPreviousPageButtonDisabled_WhenFirstPageIsLoaded()
        {
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(11);
            int targetPage = 1;
            GridPage.GoToFirstPageButton.Click();
            WaitForPageToLoad(targetPage, GridPage.Grid);
        
            Assert.IsFalse(GridPage.GoToPreviousPage.Enabled);
        }

        public void PreviousMorePagesButtonDisabled_WhenFirstPageIsLoaded()
        {
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(11);
            int targetPage = 1;
            GridPage.GoToFirstPageButton.Click();
            WaitForPageToLoad(targetPage, GridPage.Grid);
        
            Assert.IsFalse(GridPage.PreviousMorePages.Displayed);
        }

        public void GoToLastPageButtonDisabled_WhenLastPageIsLoaded()
        {
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(1);
            int targetPage = 11;
            GridPage.GoToLastPage.Click();
            WaitForPageToLoad(targetPage, GridPage.Grid);

            Assert.IsFalse(GridPage.GoToLastPage.Enabled);
        }

        public void GoToNextPageButtonDisabled_WhenLastPageIsLoaded()
        {
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(1);
            int targetPage = 11;
            GridPage.GoToLastPage.Click();
            WaitForPageToLoad(targetPage, GridPage.Grid);

            Assert.IsFalse(GridPage.GoToNextPage.Enabled);
        }

        public void NextMorePageButtonDisabled_WhenLastPageIsLoaded()
        {
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(1);
            int targetPage = 11;
            GridPage.GoToLastPage.Click();
            WaitForPageToLoad(targetPage, GridPage.Grid);

            Assert.IsFalse(GridPage.PreviousMorePages.Enabled);
        }

        private void NavigateToGridInitialPage(int initialPageNumber)
        {
            GridPage.NavigateTo();
            GridPage.Grid.Filter(GridColumns.ShipName, FilterOperator.EqualTo, uniqueShippingName);
            GridPage.Grid.ChangePageSize(1);
            WaitForGridToLoad(1, GridPage.Grid);
            GridPage.Grid.NavigateToPage(initialPageNumber);
            WaitForPageToLoad(initialPageNumber, GridPage.Grid);
            AssertPagerInfoLabel(initialPageNumber, initialPageNumber, testPagingItems.Count);
        }
            
        private void AssertPagerInfoLabel(int startItems, int endItems, int totalItems)
        {
            string expectedLabel = string.Format("{0} - {1} of {2} items", startItems, endItems, totalItems);
            Assert.AreEqual(expectedLabel, GridPage.PagerInfoLabel.Text);
        }

        private void InitializeInvoicesForPaging()
        {
            int totalOrders = 11;
            if (!string.IsNullOrEmpty(uniqueShippingName))
            {
                uniqueShippingName = Guid.NewGuid().ToString();
            }
            testPagingItems = new List<Order>();
            for (int i = 0; i < totalOrders; i++)
            {
                var newOrder = CreateNewItemInDb(uniqueShippingName);
                testPagingItems.Add(newOrder);
            }
        }
    }
}