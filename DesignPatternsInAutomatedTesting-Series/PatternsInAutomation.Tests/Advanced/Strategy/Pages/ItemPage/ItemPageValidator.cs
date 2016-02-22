using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Strategy.Pages.ItemPage
{
    public class ItemPageValidator : BasePageValidator<ItemPageMap>
    {
        public void ProductTitle(string expectedTitle)
        {
            //Selenium Testing Tools Cookbook
            Assert.AreEqual<string>(expectedTitle, this.Map.ProductTitle.Text);
        }
    }
}
