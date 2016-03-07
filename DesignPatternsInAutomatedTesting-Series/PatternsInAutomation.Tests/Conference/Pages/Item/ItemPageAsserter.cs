using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomatedTests.Conference.Pages.Item
{
    public static class ItemPageAsserter
    {
        public static void AssertPrice(this IItemPage itemPage, double expectedPrice)
        {
            //AU $168.00
            Assert.AreEqual(expectedPrice, itemPage.GetPrice());
        }
    }
}
