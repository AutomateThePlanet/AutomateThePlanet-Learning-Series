using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace PatternsInAutomation.Tests.Advanced.PageObjectv21
{
    [TestClass]
    public class BingTests 
    {
        private BingMainPage bingMainPage;
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
            this.bingMainPage.Map.FeelingLuckyButton.Click();
            this.driver.Navigate().Back();
            this.bingMainPage.Search("Automate The Planet");            
            this.bingMainPage.AssertResultsCountIsAsExpected(264);           
        }
    }
}