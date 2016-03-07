using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Unity.Base;

namespace PatternsInAutomatedTests.Advanced.Unity.BingMainPage
{
    public class BingMainPageValidator : BasePageValidator<BingMainPageElementMap>
    {
        public void ResultsCount(string expectedCount)
        {
            Assert.IsTrue(this.Map.ResultsCountDiv.Text.Contains(expectedCount), "The results DIV doesn't contains the specified text.");
        }
    }
}