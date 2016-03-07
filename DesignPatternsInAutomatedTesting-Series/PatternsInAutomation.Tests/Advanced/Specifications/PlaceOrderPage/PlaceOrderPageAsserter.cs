using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomatedTests.Advanced.Specifications
{
    public static class PlaceOrderPageAsserter
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
