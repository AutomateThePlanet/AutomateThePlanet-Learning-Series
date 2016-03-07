using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.BingMainPage;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.BingMainPageSingletonBuiltIn
{
    public class BingMainPageValidator : BasePageValidator<PatternsInAutomatedTests.Advanced.BingMainPageSingletonBuiltIn.BingMainPageElementMap>
    {
        public void ResultsCount(string expectedCount)
        {
            Assert.IsTrue(this.Map.ResultsCountDiv.Text.Contains(expectedCount), "The results DIV doesn't contains the specified text.");
        }
    }
}