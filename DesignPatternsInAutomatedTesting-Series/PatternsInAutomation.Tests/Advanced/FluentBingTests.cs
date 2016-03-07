using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Core;
using PatternsInAutomatedTests.Advanced.Fluent.Enums;
using P = PatternsInAutomatedTests.Advanced.Fluent.BingMainPage;

namespace PatternsInAutomatedTests.Advanced
{
    [TestClass]
    public class FluentBingTests
    { 
        [TestInitialize]
        public void SetupTest()
        {
            Driver.StartBrowser();
        }

        [TestCleanup]
        public void TeardownTest()
        {
            Driver.StopBrowser();
        }

        [TestMethod]
        public void SearchForImageFuent()
        {
            P.BingMainPage.Instance
                                .Navigate()
                                .Search("facebook")
                                .ClickImages()
                                .SetSize(Sizes.Large)
                                .SetColor(Colors.BlackWhite)
                                .SetTypes(Types.Clipart)
                                .SetPeople(People.All)
                                .SetDate(Dates.PastYear)
                                .SetLicense(Licenses.All);
        }
    }
}