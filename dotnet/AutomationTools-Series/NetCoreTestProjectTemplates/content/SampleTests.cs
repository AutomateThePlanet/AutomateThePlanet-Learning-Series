using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebDriverTestsProject
{
    [TestClass]
    public class SampleTests
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void TestInit()
        {
            _driver = new ChromeDriver();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Quit();
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void BellatrixSuccessfullyOpened_When_ClickTestFrameworkLink()
        {
            _driver.Navigate().GoToUrl("https://www.automatetheplanet.com/");

            var frameworkLink = _driver.FindElement(By.XPath("//*[@id=\"menu-item-9565\"]/a"));
            frameworkLink.Click();

            string currentUrl = _driver.Url;

            Assert.AreEqual("https://bellatrix.solutions/", currentUrl);
        }
    }
}
