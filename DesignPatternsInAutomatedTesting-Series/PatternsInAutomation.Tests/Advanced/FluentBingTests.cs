using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomation.Tests.Advanced.Core;
using PatternsInAutomation.Tests.Advanced.Fluent.Enums;
using P = PatternsInAutomation.Tests.Advanced.Fluent.BingMainPage;

namespace PatternsInAutomation.Tests.Advanced
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