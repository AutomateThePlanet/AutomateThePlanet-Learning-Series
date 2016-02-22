using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatternsInAutomation.Tests.Conference.Pages.ShippingAddress
{
    public static class ShippingAddressPageAsserter 
    {
        public static void AssertSubtotalAmount(this IShippingAddressPage shippingAddressPage, double expectedSubtotal)
        {
            //AU $168.00
            Assert.AreEqual(expectedSubtotal, shippingAddressPage.GetSubtotalAmount());
        }
    }
}
