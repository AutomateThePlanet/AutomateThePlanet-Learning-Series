using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace WebDriver.Series.Tests.GridTestCases.Pages
{
    public class GridFilterPage
    {
        public readonly string Url = @"http://demos.telerik.com/kendo-ui/grid/filter-row";
        private readonly IWebDriver driver;

        public GridFilterPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/span")]
        public IWebElement PagerInfoLabel { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/a[3]")]
        public IWebElement GoToNextPage { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/a[1]")]
        public IWebElement GoToFirstPageButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/a[4]/span")]
        public IWebElement GoToLastPage { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/a[2]/span")]
        public IWebElement GoToPreviousPage { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/ul/li[12]/a")]
        public IWebElement NextMorePages { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/ul/li[2]/a")]
        public IWebElement PreviousMorePages { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/ul/li[2]/a")]
        public IWebElement PageOnFirstPositionButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/ul/li[3]/a")]
        public IWebElement PageOnSecondPositionButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='grid']/div[3]/ul/li[11]/a")]
        public IWebElement PageOnTenthPositionButton { get; set; }
        
        public void NavigateTo()
        {
            this.driver.Navigate().GoToUrl(this.Url);
        }
    }
}