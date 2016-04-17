using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomatedTests.Advanced.NullObject.Pages.PlaceOrderPage
{
    public static class PlaceOrderPageAsserter
    {
        public static void AssertOrderTotalPrice(this PlaceOrderPage page, double totalPrice, double discountPrice)
        {
            double expectedTotalPrice = totalPrice - discountPrice;
            Assert.AreEqual<string>(expectedTotalPrice.ToString(), page.TotalPrice.Text);
        }
    }
}
