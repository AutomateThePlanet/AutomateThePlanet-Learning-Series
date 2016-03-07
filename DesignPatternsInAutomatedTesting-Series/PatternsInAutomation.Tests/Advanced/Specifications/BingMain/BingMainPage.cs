using System;
using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.Specifications.Base;

namespace PatternsInAutomatedTests.Advanced.Specifications
{
    public partial class BingMainPage : BasePage
    {
        public BingMainPage(IWebDriver driver) : base(driver)
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
            this.SearchBox.Clear();
            this.SearchBox.SendKeys(textToType);
            this.GoButton.Click();
        }
    
        public int GetResultsCount()
        {
            int resultsCount = default(int);
            resultsCount = int.Parse(this.ResultsCountDiv.Text);
            return resultsCount;
        }
    }
}