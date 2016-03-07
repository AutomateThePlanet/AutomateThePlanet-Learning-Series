using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Core.Fluent;

namespace PatternsInAutomatedTests.Advanced.Fluent.BingMainPage
{
    public class BingMainPageValidator : BasePageValidator<BingMainPage, BingMainPageElementMap, BingMainPageValidator>
    {
        public BingMainPage ResultsCount(string expectedCount)
        {
            Assert.IsTrue(this.Map.ResultsCountDiv.Text.Contains(expectedCount), "The results DIV doesn't contains the specified text.");
            return this.pageInstance;
        }
    }
}