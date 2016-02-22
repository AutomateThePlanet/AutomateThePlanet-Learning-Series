using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomation.Tests.Advanced.PageObjectv21
{
    public static class BingMainPageAsserter
    {
        public static void AssertResultsCountIsAsExpected(this BingMainPage page, int expectedCount)
        {
            Assert.AreEqual(page.Map.ResultsCountDiv.Text, expectedCount, "The results count is not as expected.");
        }
    }
}