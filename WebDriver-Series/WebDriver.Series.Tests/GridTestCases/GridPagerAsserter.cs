using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebDriver.Series.Tests.GridTestCases.Pages;

namespace WebDriver.Series.Tests.GridTestCases
{
    public class GridPagerAsserter : GridColumnAsserter
    {
        private string uniqueShippingName;
        private List<Order> testPagingItems;
        
        public GridPagerAsserter(IGridPage gridPage) : base(gridPage)
        {
        }

        public void NavigateToFirstPage_GoToFirstPageButton()
        {
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(11);
            int targetPage = 1;
            this.GridPage.GoToFirstPageButton.Click();
            this.WaitForPageToLoad(targetPage, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();
        
            Assert.AreEqual(this.testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            this.AssertPagerInfoLabel(targetPage, targetPage, this.testPagingItems.Count());
        }

        public void NavigateToLastPage_GoToLastPageButton()
        {
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(1);
            int targetPage = 11;
            this.GridPage.GoToLastPage.Click();
            this.WaitForPageToLoad(targetPage, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.AreEqual(this.testPagingItems.Last().OrderId, results.First().OrderId);
            this.AssertPagerInfoLabel(targetPage, targetPage, this.testPagingItems.Count());
        }

        public void NavigateToPageNine_GoToPreviousPageButton()
        {
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(11);
            int targetPage = 10;
            this.GridPage.GoToPreviousPage.Click();
            this.WaitForPageToLoad(targetPage, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.AreEqual(this.testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            this.AssertPagerInfoLabel(targetPage, targetPage, this.testPagingItems.Count());
        }

        public void NavigateToPageTwo_GoToNextPageButton()
        {
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(1);
            int targetPage = 2;
            this.GridPage.GoToNextPage.Click();
            this.WaitForPageToLoad(targetPage, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.AreEqual(this.testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            this.AssertPagerInfoLabel(targetPage, targetPage, this.testPagingItems.Count());
        }

        public void NavigateToPageTwo_SecondPageButton()
        {
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(1);
            int targetPage = 2;
            this.GridPage.PageOnSecondPositionButton.Click();
            this.WaitForPageToLoad(targetPage, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.AreEqual(this.testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            this.AssertPagerInfoLabel(targetPage, targetPage, this.testPagingItems.Count());
        }

        public void NavigateToLastPage_MorePagesNextButton()
        {
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(1);
            int targetPage = 11;
            this.GridPage.NextMorePages.Click();
            this.WaitForPageToLoad(targetPage, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.AreEqual(this.testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            this.AssertPagerInfoLabel(targetPage, targetPage, this.testPagingItems.Count());
        }

        public void NavigateToPageOne_MorePagesPreviousButton()
        {
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(11);
            int targetPage = 1;
            this.GridPage.PreviousMorePages.Click();
            this.WaitForPageToLoad(targetPage, this.GridPage.Grid);
            var results = this.GridPage.Grid.GetItems<Order>();

            Assert.AreEqual(this.testPagingItems[targetPage - 1].OrderId, results.First().OrderId);
            this.AssertPagerInfoLabel(targetPage, targetPage, this.testPagingItems.Count());
        }

        public void GoToFirstPageButtonDisabled_WhenFirstPageIsLoaded()
        {
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(11);
            int targetPage = 1;
            this.GridPage.GoToFirstPageButton.Click();
            this.WaitForPageToLoad(targetPage, this.GridPage.Grid);
        
            Assert.IsFalse(this.GridPage.GoToFirstPageButton.Enabled);
        }

        public void GoToPreviousPageButtonDisabled_WhenFirstPageIsLoaded()
        {
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(11);
            int targetPage = 1;
            this.GridPage.GoToFirstPageButton.Click();
            this.WaitForPageToLoad(targetPage, this.GridPage.Grid);
        
            Assert.IsFalse(this.GridPage.GoToPreviousPage.Enabled);
        }

        public void PreviousMorePagesButtonDisabled_WhenFirstPageIsLoaded()
        {
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(11);
            int targetPage = 1;
            this.GridPage.GoToFirstPageButton.Click();
            this.WaitForPageToLoad(targetPage, this.GridPage.Grid);
        
            Assert.IsFalse(this.GridPage.PreviousMorePages.Displayed);
        }

        public void GoToLastPageButtonDisabled_WhenLastPageIsLoaded()
        {
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(1);
            int targetPage = 11;
            this.GridPage.GoToLastPage.Click();
            this.WaitForPageToLoad(targetPage, this.GridPage.Grid);

            Assert.IsFalse(this.GridPage.GoToLastPage.Enabled);
        }

        public void GoToNextPageButtonDisabled_WhenLastPageIsLoaded()
        {
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(1);
            int targetPage = 11;
            this.GridPage.GoToLastPage.Click();
            this.WaitForPageToLoad(targetPage, this.GridPage.Grid);

            Assert.IsFalse(this.GridPage.GoToNextPage.Enabled);
        }

        public void NextMorePageButtonDisabled_WhenLastPageIsLoaded()
        {
            this.InitializeInvoicesForPaging();
            this.NavigateToGridInitialPage(1);
            int targetPage = 11;
            this.GridPage.GoToLastPage.Click();
            this.WaitForPageToLoad(targetPage, this.GridPage.Grid);

            Assert.IsFalse(this.GridPage.PreviousMorePages.Enabled);
        }

        private void NavigateToGridInitialPage(int initialPageNumber)
        {
            this.GridPage.NavigateTo();
            this.GridPage.Grid.Filter(GridColumns.ShipName, Enums.FilterOperator.EqualTo, this.uniqueShippingName);
            this.GridPage.Grid.ChangePageSize(1);
            this.WaitForGridToLoad(1, this.GridPage.Grid);
            this.GridPage.Grid.NavigateToPage(initialPageNumber);
            WaitForPageToLoad(initialPageNumber, this.GridPage.Grid);
            this.AssertPagerInfoLabel(initialPageNumber, initialPageNumber, this.testPagingItems.Count);
        }
            
        private void AssertPagerInfoLabel(int startItems, int endItems, int totalItems)
        {
            string expectedLabel = string.Format("{0} - {1} of {2} items", startItems, endItems, totalItems);
            Assert.AreEqual(expectedLabel, this.GridPage.PagerInfoLabel.Text);
        }

        private void InitializeInvoicesForPaging()
        {
            int totalOrders = 11;
            if (!string.IsNullOrEmpty(this.uniqueShippingName))
            {
                uniqueShippingName = Guid.NewGuid().ToString();
            }
            this.testPagingItems = new List<Order>();
            for (int i = 0; i < totalOrders; i++)
            {
                var newOrder = this.CreateNewItemInDb(this.uniqueShippingName);
                testPagingItems.Add(newOrder);
            }
        }
    }
}