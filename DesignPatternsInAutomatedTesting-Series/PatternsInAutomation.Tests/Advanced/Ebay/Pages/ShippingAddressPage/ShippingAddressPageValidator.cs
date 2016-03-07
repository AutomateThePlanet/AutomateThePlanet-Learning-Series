using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.Ebay.Pages.ShippingAddressPage
{
    public class ShippingAddressPageValidator : BasePageValidator<ShippingAddressPageMap>
    {
        public void Subtotal(string expectedSubtotal)
        {
            //AU $168.00
            Assert.AreEqual<string>(expectedSubtotal, this.Map.Subtotal.Text);
        }
    }
}
