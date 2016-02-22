using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using WebDriver.Series.Tests.Controls;

namespace WebDriver.Series.Tests
{
    [TestClass]
    public class KendoGridTests
    {
        private IWebDriver driver;

        [TestInitialize]
        public void SetupTest()
        {
            this.driver = new FirefoxDriver();
            this.driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(5));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.driver.Quit();
        }

        [TestMethod]
        public void FilterContactName()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            kendoGrid.Filter("ContactName", Enums.FilterOperator.Contains, "Thomas");
            var items = kendoGrid.GetItems<GridItem>();
            Assert.AreEqual(1, items.Count);
        }

        [TestMethod]
        public void SortContactTitleDesc()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            kendoGrid.Sort("ContactTitle", SortType.Desc);
            var items = kendoGrid.GetItems<GridItem>();
            Assert.AreEqual("Sales Representative", items[0]);
            Assert.AreEqual("Sales Representative", items[1]);
        }

        [TestMethod]
        public void TestCurrentPage()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            var pageNumber = kendoGrid.GetCurrentPageNumber();
            Assert.AreEqual(1, pageNumber);
        }

        [TestMethod]
        public void GetPageSize()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));
            var pageNumber = kendoGrid.GetPageSize();
            Assert.AreEqual(20, pageNumber);
        }

        [TestMethod]
        public void GetAllItems()
        {
            this.driver.Navigate().GoToUrl(@"http://demos.telerik.com/kendo-ui/grid/index");
            var kendoGrid = new KendoGrid(this.driver, this.driver.FindElement(By.Id("grid")));

            var items = kendoGrid.GetItems<GridItem>();
            Assert.AreEqual(91, items.Count);
        }
    }
}