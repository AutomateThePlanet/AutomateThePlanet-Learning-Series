// <copyright file="KendoGridTestCasesAutomationTests.cs" company="Automate The Planet Ltd.">
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
        private IWebDriver driver;
        private const string ShipNameColumnName = @"ShipName";
        private string uniqueShippingName;
        private List<Order> testPagingItems;

        [TestInitialize]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(5));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            driver.Quit();
        }
        
        // ** Paging Test Cases **
        
        #region Paging Test Cases
        
        [TestMethod]
        public void NavigateToFirstPage_GoToFirstPageButton()
        {
            var kendoGrid = new KendoGrid(driver, driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 11);
            int targetPage = 1;
            GridFilterPage gridFilterPage = new GridFilterPage(driver);
            gridFilterPage.GoToFirstPageButton.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();
        
            Assert.AreEqual(testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToLastPage_GoToLastPageButton()
        {
            var kendoGrid = new KendoGrid(driver, driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 1);
            int targetPage = 11;
            GridFilterPage gridFilterPage = new GridFilterPage(driver);
            gridFilterPage.GoToLastPage.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(testPagingItems.Last().OrderId, results.First().OrderId);
            AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToPageNine_GoToPreviousPageButton()
        {
            var kendoGrid = new KendoGrid(driver, driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 11);
            int targetPage = 10;
            GridFilterPage gridFilterPage = new GridFilterPage(driver);
            gridFilterPage.GoToPreviousPage.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToPageTwo_GoToNextPageButton()
        {
            var kendoGrid = new KendoGrid(driver, driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 1);
            int targetPage = 2;
            GridFilterPage gridFilterPage = new GridFilterPage(driver);
            gridFilterPage.GoToNextPage.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToPageTwo_SecondPageButton()
        {
            var kendoGrid = new KendoGrid(driver, driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 1);
            int targetPage = 2;
            GridFilterPage gridFilterPage = new GridFilterPage(driver);
            gridFilterPage.PageOnSecondPositionButton.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToLastPage_MorePagesNextButton()
        {
            var kendoGrid = new KendoGrid(driver, driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 1);
            int targetPage = 11;
            GridFilterPage gridFilterPage = new GridFilterPage(driver);
            gridFilterPage.NextMorePages.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, testPagingItems.Count());
        }

        [TestMethod]
        public void NavigateToPageOne_MorePagesPreviousButton()
        {
            var kendoGrid = new KendoGrid(driver, driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 11);
            int targetPage = 1;
            GridFilterPage gridFilterPage = new GridFilterPage(driver);
            gridFilterPage.PreviousMorePages.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
            var results = kendoGrid.GetItems<Order>();

            Assert.AreEqual(testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            AssertPagerInfoLabel(gridFilterPage, targetPage, targetPage, testPagingItems.Count());
        }

        [TestMethod]
        public void GoToFirstPageButtonDisabled_WhenFirstPageIsLoaded()
        {
            var kendoGrid = new KendoGrid(driver, driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 11);
            int targetPage = 1;
            GridFilterPage gridFilterPage = new GridFilterPage(driver);
            gridFilterPage.GoToFirstPageButton.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
        
            Assert.IsFalse(gridFilterPage.GoToFirstPageButton.Enabled);
        }

        [TestMethod]
        public void GoToPreviousPageButtonDisabled_WhenFirstPageIsLoaded()
        {
            var kendoGrid = new KendoGrid(driver, driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 11);
            int targetPage = 1;
            GridFilterPage gridFilterPage = new GridFilterPage(driver);
            gridFilterPage.GoToFirstPageButton.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
        
            Assert.IsFalse(gridFilterPage.GoToPreviousPage.Enabled);
        }

        [TestMethod]
        public void PreviousMorePagesButtonDisabled_WhenFirstPageIsLoaded()
        {
            var kendoGrid = new KendoGrid(driver, driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 11);
            int targetPage = 1;
            GridFilterPage gridFilterPage = new GridFilterPage(driver);
            gridFilterPage.GoToFirstPageButton.Click();
            WaitForPageToLoad(targetPage, kendoGrid);
        
            Assert.IsFalse(gridFilterPage.PreviousMorePages.Displayed);
        }

        [TestMethod]
        public void GoToLastPageButtonDisabled_WhenLastPageIsLoaded()
        {
            var kendoGrid = new KendoGrid(driver, driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 1);
            int targetPage = 11;
            GridFilterPage gridFilterPage = new GridFilterPage(driver);
            gridFilterPage.GoToLastPage.Click();
            WaitForPageToLoad(targetPage, kendoGrid);

            Assert.IsFalse(gridFilterPage.GoToLastPage.Enabled);
        }

        [TestMethod]
        public void GoToNextPageButtonDisabled_WhenLastPageIsLoaded()
        {
            var kendoGrid = new KendoGrid(driver, driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 1);
            int targetPage = 11;
            GridFilterPage gridFilterPage = new GridFilterPage(driver);
            gridFilterPage.GoToLastPage.Click();
            WaitForPageToLoad(targetPage, kendoGrid);

            Assert.IsFalse(gridFilterPage.GoToNextPage.Enabled);
        }

        [TestMethod]
        public void NextMorePageButtonDisabled_WhenLastPageIsLoaded()
        {
            var kendoGrid = new KendoGrid(driver, driver.FindElement(By.Id("grid")));
            InitializeInvoicesForPaging();
            NavigateToGridInitialPage(kendoGrid, 1);
            int targetPage = 11;
            GridFilterPage gridFilterPage = new GridFilterPage(driver);
            gridFilterPage.GoToLastPage.Click();
            WaitForPageToLoad(targetPage, kendoGrid);

            Assert.IsFalse(gridFilterPage.PreviousMorePages.Enabled);
        }
        
        #endregion
            
        private void NavigateToGridInitialPage(KendoGrid kendoGrid, int initialPageNumber)
        {
            GridFilterPage gridFilterPage = new GridFilterPage(driver);
            gridFilterPage.NavigateTo();
            kendoGrid.Filter(ShipNameColumnName, FilterOperator.EqualTo, uniqueShippingName);
            kendoGrid.ChangePageSize(1);
            WaitForGridToLoad(1, kendoGrid);
            kendoGrid.NavigateToPage(initialPageNumber);
            WaitForPageToLoad(initialPageNumber, kendoGrid);
            AssertPagerInfoLabel(gridFilterPage, initialPageNumber, initialPageNumber, testPagingItems.Count);
        }
            
        private void AssertPagerInfoLabel(GridFilterPage page, int startItems, int endItems, int totalItems)
        {
            string expectedLabel = string.Format("{0} - {1} of {2} items", startItems, endItems, totalItems);
            Assert.AreEqual(expectedLabel, page.PagerInfoLabel.Text);
        }
            
        public void WaitForPageToLoad(int expectedPage, KendoGrid grid)
        {
            Until(() =>
            {
                int currentPage = grid.GetCurrentPageNumber();
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
            DateTime start = DateTime.Now;
            while (!condition())
            {
                DateTime now = DateTime.Now;
                double totalSeconds = (now - start).TotalSeconds;
                if (totalSeconds >= timeout)
                {
                    throw new TimeoutException(exceptionMessage);
                }
                Thread.Sleep(retryRateDelay);
            }
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
            
        private Order CreateNewItemInDb(string shipName = null)
        {
            // Replace it with service oriented call to your DB. Create real enity in DB.
            return new Order(shipName);
        }
    }
}