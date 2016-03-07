using OpenQA.Selenium;

namespace PatternsInAutomatedTests.Beginners.Pages.BingMainPage
{
    public class BingMainPage
    {
        private readonly IWebDriver browser;
        private readonly string url = @"http://www.bing.com/";

        public BingMainPage(IWebDriver browser)
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

        public BingMainPageValidator Validate()
        {
            return new BingMainPageValidator(this.browser);
        }

        public void Navigate()
        {
            this.browser.Navigate().GoToUrl(this.url);
        }

        public void Search(string textToType)
        {
            this.Map.SearchBox.Clear();
            this.Map.SearchBox.SendKeys(textToType);
            this.Map.GoButton.Click();
        }
    }
}