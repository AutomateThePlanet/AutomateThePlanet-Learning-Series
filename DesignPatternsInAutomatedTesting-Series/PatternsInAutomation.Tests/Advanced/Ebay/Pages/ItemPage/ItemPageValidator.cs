using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.Ebay.Pages.ItemPage
{
    public class ItemPageValidator : BasePageValidator<ItemPageMap>
    {
        public void Price(string expectedPrice)
        {
            //AU $168.00
            Assert.AreEqual<string>(expectedPrice, this.Map.Price.Text);
        }
    }
}
