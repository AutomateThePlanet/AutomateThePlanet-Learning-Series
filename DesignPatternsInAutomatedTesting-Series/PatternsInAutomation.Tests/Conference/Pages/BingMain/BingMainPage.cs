using System;
using OpenQA.Selenium;
using PatternsInAutomatedTests.Conference.Base;

namespace PatternsInAutomatedTests.Conference.Pages.BingMain
{
public class BingMainPage : BasePage<BingMainPageMap>, IBingMainPage
{
    public BingMainPage(IWebDriver driver)
        : base(driver, new BingMainPageMap(driver))
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
    public int GetResultsCount()
    {
        int resultsCount = default(int);
        resultsCount = int.Parse(this.Map.ResultsCountDiv.Text);
        return resultsCount;
    }
}
}