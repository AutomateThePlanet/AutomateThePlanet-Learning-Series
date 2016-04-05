using OpenQA.Selenium;
using WebDriver.Series.Tests.Controls;

namespace WebDriver.Series.Tests.GridTestCases.Pages
{
    public interface IGridPage
    {
        KendoGrid Grid { get; }

        IWebElement PagerInfoLabel { get; set; }

        IWebElement GoToNextPage { get; set; }

        IWebElement GoToFirstPageButton { get; set; }

        IWebElement GoToLastPage { get; set; }

        IWebElement GoToPreviousPage { get; set; }

        IWebElement NextMorePages { get; set; }

        IWebElement PreviousMorePages { get; set; }

        IWebElement PageOnFirstPositionButton { get; set; }

        IWebElement PageOnSecondPositionButton { get; set; }

        IWebElement PageOnTenthPositionButton { get; set; }

        void NavigateTo();
    }
}