// <copyright file="KendoGridTestCasesAutomationTests.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using System.Threading;
using DesignGridControlAutomatedTestsPartThree.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using Ui.Automation.Core.Controls.Controls;
using Ui.Automation.Core.Controls.Enums;

namespace DesignGridControlAutomatedTestsPartThree
{
    [TestClass]
    public class KendoGridTestCasesAutomationTests
    {
        private IWebDriver _driver;
        private const string ShipNameColumnName = @"ShipName";
        private string _uniqueShippingName;
        private List<Order> _testPagingItems;

        [TestInitialize]
        public void SetupTest()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }
        
        // ** Paging Test Cases **
        
        #region Paging Test Cases
        
        [TestMethod]
        public void NavigateToFirstPage_GoToFirstPageButton()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 11);
            var targetPage = 1;
            var gridFilterPage = new GridFilterPage(_driver);
            gridFilterPage.GoToFirstPageButton.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();
        
            Assert.AreEqual(_testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, _testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToLastPage_GoToLastPageButton()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 1);
            var targetPage = 11;
            var gridFilterPage = new GridFilterPage(_driver);
            gridFilterPage.GoToLastPage.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(_testPagingItems.Last().OrderId, results.First().OrderId);
            AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, _testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToPageNine_GoToPreviousPageButton()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 11);
            var targetPage = 10;
            var gridFilterPage = new GridFilterPage(_driver);
            gridFilterPage.GoToPreviousPage.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(_testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, _testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToPageTwo_GoToNextPageButton()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 1);
            var targetPage = 2;
            var gridFilterPage = new GridFilterPage(_driver);
            gridFilterPage.GoToNextPage.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(_testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, _testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToPageTwo_SecondPageButton()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 1);
            var targetPage = 2;
            var gridFilterPage = new GridFilterPage(_driver);
            gridFilterPage.PageOnSecondPositionButton.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(_testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, _testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToLastPage_MorePagesNextButton()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 1);
            var targetPage = 11;
            var gridFilterPage = new GridFilterPage(_driver);
            gridFilterPage.NextMorePages.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(_testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, _testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToPageOne_MorePagesPreviousButton()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 11);
            var targetPage = 1;
            var gridFilterPage = new GridFilterPage(_driver);
            gridFilterPage.PreviousMorePages.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(_testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, _testPagingItems.Count());
        }

        [TestMethod]
        public void GoToFirstPageButtonDisabled_WhenFirstPageIsLoaded()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 11);
            var targetPage = 1;
            var gridFilterPage = new GridFilterPage(_driver);
            gridFilterPage.GoToFirstPageButton.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
        
            Assert.IsFalse(gridFilterPage.GoToFirstPageButton.Enabled);
        }

        [TestMethod]
        public void GoToPreviousPageButtonDisabled_WhenFirstPageIsLoaded()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 11);
            var targetPage = 1;
            var gridFilterPage = new GridFilterPage(_driver);
            gridFilterPage.GoToFirstPageButton.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
        
            Assert.IsFalse(gridFilterPage.GoToPreviousPage.Enabled);
        }

        [TestMethod]
        public void PreviousMorePagesButtonDisabled_WhenFirstPageIsLoaded()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 11);
            var targetPage = 1;
            var gridFilterPage = new GridFilterPage(_driver);
            gridFilterPage.GoToFirstPageButton.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
        
            Assert.IsFalse(gridFilterPage.PreviousMorePages.Displayed);
        }

        [TestMethod]
        public void GoToLastPageButtonDisabled_WhenLastPageIsLoaded()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 1);
            var targetPage = 11;
            var gridFilterPage = new GridFilterPage(_driver);
            gridFilterPage.GoToLastPage.Click();
            WaitForPageToLoad(targetPage, kendoGrid);

            Assert.IsFalse(gridFilterPage.GoToLastPage.Enabled);
        }

        [TestMethod]
        public void GoToNextPageButtonDisabled_WhenLastPageIsLoaded()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 1);
            var targetPage = 11;
            var gridFilterPage = new GridFilterPage(_driver);
            gridFilterPage.GoToLastPage.Click();
            WaitForPageToLoad(targetPage, kendoGrid);

            Assert.IsFalse(gridFilterPage.GoToNextPage.Enabled);
        }

        [TestMethod]
        public void NextMorePageButtonDisabled_WhenLastPageIsLoaded()
        {
            var kendoGrid = new KendoGrid(_driver, _driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 1);
            var targetPage = 11;
            var gridFilterPage = new GridFilterPage(_driver);
            gridFilterPage.GoToLastPage.Click();
            WaitForPageToLoad(targetPage, kendoGrid);

            Assert.IsFalse(gridFilterPage.PreviousMorePages.Enabled);
        }
        
        #endregion
            
        private void NavigateToGridInitialPage(KendoGrid kendoGrid, int initialPageNumber)
        {
            var gridFilterPage = new GridFilterPage(_driver);
            gridFilterPage.NavigateTo();
            kendoGrid.Filter(ShipNameColumnName, FilterOperator.EqualTo, _uniqueShippingName);
            kendoGrid.ChangePageSize(1);
            WaitForGridToLoad(1, kendoGrid);
            kendoGrid.NavigateToPage(initialPageNumber);
            WaitForPageToLoad(initialPageNumber, kendoGrid);
            AssertPagerInfoLabel(gridFilterPage, initialPageNumber, initialPageNumber, _testPagingItems.Count);
        }
            
        private void AssertPagerInfoLabel(GridFilterPage page, int startItems, int endItems, int totalItems)
        {
            var expectedLabel = string.Format("{0} - {1} of {2} items", startItems, endItems, totalItems);
            Assert.AreEqual(expectedLabel, page.PagerInfoLabel.Text);
        }
            
        public void WaitForPageToLoad(int expectedPage, KendoGrid grid)
        {
            Until(() =>
            {
                var currentPage = grid.GetCurrentPageNumber();
                return currentPage == expectedPage;
            });
        }

        private void WaitForGridToLoad(int expectedCount, KendoGrid grid)
        {
            Until(
                () =>
                {
                    var items = grid.GetItems<GridItem>();
                    return expectedCount == items.Count;
                });
        }
            
        private void Until(Func<bool> condition, int timeout = 10, string exceptionMessage = "Timeout exceeded.", int retryRateDelay = 50)
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
            
        private void InitializeInvoicesForPaging()
        {
            var totalOrders = 11;
            if (!string.IsNullOrEmpty(_uniqueShippingName))
            {
                _uniqueShippingName = Guid.NewGuid().ToString();
            }
            _testPagingItems = new List<Order>();
            for (var i = 0; i < totalOrders; i++)
            {
                var newOrder = CreateNewItemInDb(_uniqueShippingName);
                _testPagingItems.Add(newOrder);
            }
        }
            
        private Order CreateNewItemInDb(string shipName = null)
        {
            // Replace it with service oriented call to your DB. Create real enity in DB.
            return new Order(shipName);
        }
    }
}