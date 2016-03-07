using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomatedTests.Conference.Pages.Checkout
{
    public static class CheckoutPageAsserter
    {
        public static void AssertSubtotal(this ICheckoutPage checkoutPage, double expectedSubtotal)
        {
            Assert.AreEqual(expectedSubtotal, checkoutPage.GetTotalPrice());
        }
    }
}
