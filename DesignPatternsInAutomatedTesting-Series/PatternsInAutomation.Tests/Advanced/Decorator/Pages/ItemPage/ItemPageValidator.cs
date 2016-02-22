using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Decorator.Pages.ItemPage
{
    public class ItemPageValidator : BasePageValidator<PatternsInAutomation.Tests.Advanced.Decorator.Pages.ItemPage.ItemPageMap>
    {
        public void ProductTitle(string expectedTitle)
        {
            //Selenium Testing Tools Cookbook
            Assert.AreEqual<string>(expectedTitle, this.Map.ProductTitle.Text);
        }
    }
}
