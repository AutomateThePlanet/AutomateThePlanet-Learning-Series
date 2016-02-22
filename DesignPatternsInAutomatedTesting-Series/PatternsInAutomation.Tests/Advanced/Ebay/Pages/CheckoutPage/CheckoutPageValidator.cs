using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Ebay.Pages.CheckoutPage
{
    public class CheckoutPageValidator : BasePageValidator<CheckoutPageMap>
    {
        public void Subtotal(string expectedSubtotal)
        {
            //AU $168.00
            Assert.AreEqual<string>(expectedSubtotal, this.Map.TotalPrice.Text);
        }
    }
}
