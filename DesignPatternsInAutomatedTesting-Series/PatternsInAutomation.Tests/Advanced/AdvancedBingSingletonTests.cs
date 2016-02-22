using Microsoft.VisualStudio.TestTools.UnitTesting;
using P = PatternsInAutomation.Tests.Advanced.BingMainPage;
using S = PatternsInAutomation.Tests.Advanced.BingMainPageSingletonDerived;
using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced
{
    [TestClass]
    public class AdvancedBingSingletonTests
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
        public void SearchTextInBing_Advanced_PageObjectPattern_NoSingleton()
        {
            // Usage without Singleton
            P.BingMainPage bingMainPage = new P.BingMainPage();
            bingMainPage.Navigate();
            bingMainPage.Search("Automate The Planet");
            bingMainPage.Validate().ResultsCount(",000 RESULTS");
        }

        [TestMethod]
        public void SearchTextInBing_Advanced_PageObjectPattern_Singleton()
        {
            S.BingMainPage.Instance.Navigate();
            S.BingMainPage.Instance.Search("Automate The Planet");
            S.BingMainPage.Instance.Validate().ResultsCount(",000 RESULTS");
        }
    }
}