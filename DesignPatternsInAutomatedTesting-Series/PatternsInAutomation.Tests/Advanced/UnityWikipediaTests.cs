using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomation.Tests.Advanced.Core;
using PatternsInAutomation.Tests.Advanced.Unity.Base;
using PatternsInAutomation.Tests.Advanced.Unity.WikipediaMainPage;

namespace PatternsInAutomation.Tests.Advanced
{
    [TestClass]
    public class UnityWikipediaTests
    {
        private static IUnityContainer pageFactory = new UnityContainer();

        [AssemblyInitialize()]
        public static void MyTestInitialize(TestContext testContext)
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = "unity.config" };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var unitySection = (UnityConfigurationSection)configuration.GetSection("unity");
            pageFactory.LoadConfiguration(unitySection);

            pageFactory.RegisterType<IWikipediaMainPage, WikipediaMainPage>(new ContainerControlledLifetimeManager());
        }

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
        public void TestWikiContentsToggle()
        {
            WikipediaMainPage wikiPage = new WikipediaMainPage();
            wikiPage.Navigate();
            wikiPage.Search("Quality assurance");
            wikiPage.Validate().ToogleLinkTextHide();
            wikiPage.Validate().ContentsListVisible();
            wikiPage.ToggleContents();
            wikiPage.Validate().ToogleLinkTextShow();
            wikiPage.Validate().ContentsListHidden();
        }

        [TestMethod]
        public void TestWikiContentsToggle_Unity()
        {
            ////var wikiPage = PageFactory.Get<IWikipediaMainPage>();
            var wikiPage = pageFactory.Resolve<IWikipediaMainPage>();
            wikiPage.Navigate();
            wikiPage.Search("Quality assurance");
            wikiPage.Validate().ToogleLinkTextHide();
            wikiPage.Validate().ContentsListVisible();
            wikiPage.ToggleContents();
            wikiPage.Validate().ToogleLinkTextShow();
            wikiPage.Validate().ContentsListHidden();
        }
    }
}