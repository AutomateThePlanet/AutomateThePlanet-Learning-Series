using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.BingMainPage;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.BingMainPageSingletonDerived
{
    public class BingMainPageValidator : BasePageValidator<BingMainPageElementMap>
    {
        public void ResultsCount(string expectedCount)
        {
            Assert.IsTrue(this.Map.ResultsCountDiv.Text.Contains(expectedCount), "The results DIV doesn't contains the specified text.");
        }
    }
}