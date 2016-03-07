using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.Unity.Base
{
    public class BasePageElementMap
    {
        protected IWebDriver browser;
        protected WebDriverWait browserWait;
        public BasePageElementMap()
        {
            this.browser = Driver.Browser;
            this.browserWait = Driver.BrowserWait;
        }

        public void SwitchToDefault()
        {
            this.browser.SwitchTo().DefaultContent();
        }
    }
}
