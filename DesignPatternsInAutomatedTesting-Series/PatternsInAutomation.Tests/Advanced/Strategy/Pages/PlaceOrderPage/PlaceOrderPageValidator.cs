using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.Strategy.Pages.PlaceOrderPage
{
    public class PlaceOrderPageValidator : BasePageValidator<PlaceOrderPageMap>
    {
        public void ItemsPrice(string expectedPrice)
        {
            Assert.AreEqual<string>(expectedPrice, this.Map.ItemsPrice.Text);
        }

        public void BeforeTaxesPrice(string expectedPrice)
        {
            Assert.AreEqual<string>(expectedPrice, this.Map.TotalBeforeTaxPrice.Text);
        }

        public void EstimatedTaxPrice(string expectedPrice)
        {
            Assert.AreEqual<string>(expectedPrice, this.Map.EstimatedTaxPrice.Text);
        }

        public void OrderTotalPrice(string expectedPrice)
        {
            Assert.AreEqual<string>(expectedPrice, this.Map.TotalPrice.Text);
        }

        public void GiftWrapPrice(string expectedPrice)
        {
            Assert.AreEqual<string>(expectedPrice, this.Map.GiftWrapPrice.Text);
        }

        public void ShippingTaxPrice(string expectedPrice)
        {
            Assert.AreEqual<string>(expectedPrice, this.Map.ShippingTax.Text);
        }
    }
}
