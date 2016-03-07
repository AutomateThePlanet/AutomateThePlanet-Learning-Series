using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace PatternsInAutomatedTests.Beginners.Selenium.Bing.Pages
{
    public class BingMainPage
    {
        private readonly IWebDriver driver;
        private readonly string url = @"http://www.bing.com/";

        public BingMainPage(IWebDriver browser)
        {
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.Id, Using = "sb_form_q")]
        public IWebElement SearchBox { get; set; }

        [FindsBy(How = How.Id, Using = "sb_form_go")]
        public IWebElement GoButton { get; set; }

        [FindsBy(How = How.Id, Using = "b_tween")]
        public IWebElement ResultsCountDiv { get; set; }

        public void Navigate()
        {
            this.driver.Navigate().GoToUrl(this.url);            
        }

        public void Search(string textToType)
        {
            this.SearchBox.Clear();
            this.SearchBox.SendKeys(textToType);
            this.GoButton.Click();
        }

        public void ValidateResultsCount(string expectedCount)
        {
            Assert.IsTrue(this.ResultsCountDiv.Text.Contains(expectedCount), "The results DIV doesn't contains the specified text.");
        }
    }
}