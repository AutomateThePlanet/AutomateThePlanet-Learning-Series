using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Unity.Base;

namespace PatternsInAutomatedTests.Advanced.Unity.WikipediaMainPage
{
    public class WikipediaMainPageValidator : BasePageValidator<WikipediaMainPageMap>
    {
        public void ToogleLinkTextShow()
        {
            Assert.AreEqual<string>("show", this.Map.ContentsToggleLink.Text, "The contents toggle button text was not as expected.");
        }

        public void ToogleLinkTextHide()
        {
            Assert.AreEqual<string>("hide", this.Map.ContentsToggleLink.Text, "The contents toggle button text was not as expected.");
        }

        public void ContentsListHidden()
        {
            string contentsListStyle = this.Map.ContentsList.GetAttribute("style");
            Assert.AreEqual<string>("display: none;", contentsListStyle, "The contents list is still visible.");
        }

        public void ContentsListVisible()
        {
            string contentsListStyle = this.Map.ContentsList.GetAttribute("style");
            Assert.AreEqual<string>("display: block;", contentsListStyle, "The contents list is still invisible.");
        }
    }
}