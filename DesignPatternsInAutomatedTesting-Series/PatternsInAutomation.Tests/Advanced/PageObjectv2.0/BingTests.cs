using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace PatternsInAutomatedTests.Advanced.PageObjectv20
{
    [TestClass]
    public class BingTests 
    {
        private IBingMainPage bingMainPage;
        private IWebDriver driver;

        [TestInitialize]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            bingMainPage = new BingMainPage(driver);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            driver.Quit();
        }

        [TestMethod]
        public void SearchForAutomateThePlanet()
        {
            this.bingMainPage.Open();
            this.bingMainPage.Search("Automate The Planet");
            this.bingMainPage.AssertResultsCountIsAsExpected(264);
        }
    }
}