using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Ebay.Pages.ItemPage
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
