using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomation.Tests.Conference.Pages.BingMain
{
    public static class BingMainPageAsserter
    {
        public static void AssertResultsCountIsAsExpected(this IBingMainPage page, int expectedCount)
        {
            Assert.AreEqual(page.GetResultsCount(), expectedCount, "The results count is not as expected.");
        }
    }
}