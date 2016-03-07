using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace PatternsInAutomatedTests.Beginners.Pages.BingMainPage
{
    public class BingMainPageValidator
    {
        private readonly IWebDriver browser;

        public BingMainPageValidator(IWebDriver browser)
        {
            this.browser = browser;
        }

        protected BingMainPageElementMap Map
        {
            get
            {
                return new BingMainPageElementMap(this.browser);
            }
        }

        public void ResultsCount(string expectedCount)
        {
            Assert.IsTrue(this.Map.ResultsCountDiv.Text.Contains(expectedCount), "The results DIV doesn't contains the specified text.");
        }
    }
}