using System;
using System.Collections.Generic;
using System.Threading;
using WebDriver.Series.Tests.Controls;
using WebDriver.Series.Tests.GridTestCases.Pages;

namespace WebDriver.Series.Tests.GridTestCases
{
    public class GridColumnAsserter
    {
        public GridColumnAsserter(IGridPage gridPage)
        {
            this.GridPage = gridPage;
        }

        protected IGridPage GridPage { get; set; }

        protected void WaitForPageToLoad(int expectedPage, KendoGrid grid)
        {
            this.Until(() =>
            {
                int currentPage = grid.GetCurrentPageNumber();
                return currentPage == expectedPage;
            });
        }

        protected void WaitForGridToLoad(int expectedCount, KendoGrid grid)
        {
            this.Until(
                () =>
                {
                    var items = grid.GetItems<GridItem>();
                    return expectedCount == items.Count;
                });
        }
            
        protected void WaitForGridToLoadAtLeast(int expectedCount, KendoGrid grid)
        {
            this.Until(
                () =>
                {
                    var items = grid.GetItems<GridItem>();
                    return items.Count >= expectedCount;
                });
        }
            
        protected void Until(Func<bool> condition, int timeout = 10, string exceptionMessage = "Timeout exceeded.", int retryRateDelay = 50)
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
            
        protected List<Order> GetAllItemsFromDb()
        {
            // Create dummy orders. This logic should be replaced with service oriented call to your DB and get all items that are populated in the grid.
            List<Order> orders = new List<Order>();
            for (int i = 0; i < 10; i++)
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
            int result = currentTime.Year + currentTime.Month + currentTime.Hour + currentTime.Minute + currentTime.Second + currentTime.Millisecond;
            return result;
        }
    }
}