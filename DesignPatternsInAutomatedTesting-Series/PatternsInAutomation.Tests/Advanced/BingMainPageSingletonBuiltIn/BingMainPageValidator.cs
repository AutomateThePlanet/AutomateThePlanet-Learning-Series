using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomation.Tests.Advanced.BingMainPage;
using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.BingMainPageSingletonBuiltIn
{
    public class BingMainPageValidator : BasePageValidator<PatternsInAutomation.Tests.Advanced.BingMainPageSingletonBuiltIn.BingMainPageElementMap>
    {
        public void ResultsCount(string expectedCount)
        {
            Assert.IsTrue(this.Map.ResultsCountDiv.Text.Contains(expectedCount), "The results DIV doesn't contains the specified text.");
        }
    }
}