using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreateHybridTestFrameworkInterfaceContracts.NonHybridVersion.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace CreateHybridTestFrameworkInterfaceContracts.NonHybridVersion
{
    [TestClass]
    public class BingTests
    {
        private BingMainPage _bingMainPage;
        private IWebDriver _driver;

        [TestInitialize]
        public void SetupTest()
        {
            _driver = new FirefoxDriver();
            _bingMainPage = new BingMainPage(_driver);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void SearchForAutomateThePlanet()
        {
            _bingMainPage.Open();
            _bingMainPage.Search("Automate The Planet");
            _bingMainPage.AssertResultsCountIsAsExpected(264);
        }
    }
}
