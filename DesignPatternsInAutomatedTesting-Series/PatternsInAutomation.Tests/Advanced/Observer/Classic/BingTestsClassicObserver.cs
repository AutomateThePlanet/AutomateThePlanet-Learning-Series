using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Core;
using PatternsInAutomatedTests.Advanced.Observer.Attributes;
using B = PatternsInAutomatedTests.Beginners.Selenium.Bing.Pages;

namespace PatternsInAutomatedTests.Advanced.Observer.Classic
{
    [TestClass]
    [ExecutionBrowser(BrowserTypes.Chrome)]
    public class BingTestsClassicObserver : BaseTest
    {
        [TestMethod]
        [ExecutionBrowser(BrowserTypes.Firefox)]
        public void SearchTextInBing_First_Observer()
        {
            B.BingMainPage bingMainPage = new B.BingMainPage(Driver.Browser);
            bingMainPage.Navigate();
            bingMainPage.Search("Automate The Planet");
            bingMainPage.ValidateResultsCount("RESULTS");
        }
    }
}