using OpenQA.Selenium;

namespace PatternsInAutomatedTests.Beginners.Pages.BingMainPage
{
    public class BingMainPageElementMap
    {
        private readonly IWebDriver browser;

        public BingMainPageElementMap(IWebDriver browser)
        {
            this.browser = browser;
        }

        public IWebElement SearchBox 
        {
            get
            {
                return this.browser.FindElement(By.Id("sb_form_q"));
            }
        }

        public IWebElement GoButton 
        {
            get
            {
                return this.browser.FindElement(By.Id("sb_form_go"));
            }
        }
       
        public IWebElement ResultsCountDiv
        {
            get
            {
                return this.browser.FindElement(By.Id("b_tween"));
            }
        }
    }
}