using System;
using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.NullObject.Base;

namespace PatternsInAutomatedTests.Advanced.NullObject.Pages.ItemPage
{
    public partial class ItemPage : BasePage
    {
        public ItemPage(IWebDriver driver)
            : base(driver)
        {
        }

        public override string Url
        {
            get
            {
                return "http://www.amazon.com/";
            }
        }

        public void ClickBuyNowButton()
        {
            this.AddToCartButton.Click();
        }

        public void Navigate(string part)
        {
            ///Selenium-Testing-Cookbook-Gundecha-Unmesh/dp/1849515743
            base.Open(part);
        }
    }
}
