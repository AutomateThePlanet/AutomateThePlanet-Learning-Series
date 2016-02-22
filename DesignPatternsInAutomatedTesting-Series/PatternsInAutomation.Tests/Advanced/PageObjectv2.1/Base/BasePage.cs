using System;
using OpenQA.Selenium;

namespace PatternsInAutomation.Tests.Advanced.PageObjectv21.Base
{
    public abstract class BasePage<TMap>
        where TMap : BaseElementMap
    {
        private readonly TMap map;
        protected IWebDriver driver;

        public BasePage(IWebDriver driver, TMap map)
        {
            this.driver = driver;
            this.map = map;
        }

        public TMap Map 
        {
            get
            {
                return this.map;
            }
        }

        public abstract string Url { get; }

        public virtual void Open(string part = "")
        {
            this.driver.Navigate().GoToUrl(string.Concat(this.Url, part));
        }
    }
}