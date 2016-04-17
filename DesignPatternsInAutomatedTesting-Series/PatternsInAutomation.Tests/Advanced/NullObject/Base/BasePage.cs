using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PatternsInAutomatedTests.Advanced.NullObject.Base
{
    public abstract class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait driverWait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            // wait 30 seconds.
            this.driverWait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
        }

        public virtual string Url { get; }

        public virtual void Open(string part = "")
        {
            if (string.IsNullOrEmpty(this.Url))
            {
                throw new ArgumentException("The main URL cannot be null or empty.");
            }
            this.driver.Navigate().GoToUrl(string.Concat(this.Url, part));
        }
    }
}