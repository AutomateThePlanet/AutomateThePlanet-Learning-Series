using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomatedTests.Advanced.Specifications
{
    public static class LinqPlaceOrderPageAsserter
    {
        public static void AssertPromoCodeLabel(this PlaceOrderPage page, string promoCode)
        {
            if (!string.IsNullOrEmpty(promoCode) && page.IsPromoCodePurchase)
            {
                Assert.AreEqual<string>(page.PromotionalCode.Text, promoCode);
            }            
        }
    }
}
