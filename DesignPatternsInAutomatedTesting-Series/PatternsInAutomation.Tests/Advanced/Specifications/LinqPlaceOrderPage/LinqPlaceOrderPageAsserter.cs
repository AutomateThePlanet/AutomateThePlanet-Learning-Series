using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomation.Tests.Advanced.Specifications
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
