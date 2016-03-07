using OpenQA.Selenium;

namespace PatternsInAutomatedTests.Conference.Base
{
    public abstract class BaseElementMap
    {
        protected IWebDriver driver;

        public BaseElementMap(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void SwitchToDefault()
        {
            this.driver.SwitchTo().DefaultContent();
        }
    }
}
