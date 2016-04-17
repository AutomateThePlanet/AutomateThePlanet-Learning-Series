using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomatedTests.Advanced.NullObject.Pages.ItemPage
{
    public static class ItemPageAsserter
    {
        public static void ProductTitle(this ItemPage page, string expectedTitle)
        {
            //Selenium Testing Tools Cookbook
            Assert.AreEqual<string>(expectedTitle, page.ProductTitle.Text);
        }
    }
}
