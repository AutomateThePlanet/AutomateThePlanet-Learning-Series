using System;
using OpenQA.Selenium;
using PatternsInAutomation.Tests.Conference.Base;

namespace PatternsInAutomation.Tests.Advanced.PageObjectv21
{
    public class BingMainPage : BasePage<BingMainPageMap>
    {
        public BingMainPage(IWebDriver driver) : base(driver, new BingMainPageMap(driver))
        {
        }

        public override string Url
        {
            get
            {
                return @"http://www.bing.com/";
            }
        }

        public void Search(string textToType)
        {
            this.Map.SearchBox.Clear();
            this.Map.SearchBox.SendKeys(textToType);
            this.Map.GoButton.Click();
        }
    }
}