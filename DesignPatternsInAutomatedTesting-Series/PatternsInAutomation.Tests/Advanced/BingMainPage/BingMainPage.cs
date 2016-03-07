﻿using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.BingMainPage
{
    public class BingMainPage : BasePage<BingMainPageElementMap, BingMainPageValidator>
    {
        public BingMainPage()
           : base(@"http://www.bing.com/")
        {
        }

        public void Search(string textToType)
        {
            this.Map.SearchBox.Clear();
            this.Map.SearchBox.SendKeys(textToType);
            this.Map.GoButton.Click();
        }
    }
}