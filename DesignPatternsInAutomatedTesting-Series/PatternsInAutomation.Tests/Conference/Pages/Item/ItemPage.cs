using System;
using OpenQA.Selenium;
using PatternsInAutomatedTests.Conference.Base;

namespace PatternsInAutomatedTests.Conference.Pages.Item
{
    public class ItemPage : BasePage<ItemPageMap>, IItemPage
    {
        public ItemPage(IWebDriver driver)
            : base(driver, new ItemPageMap(driver))
        {
        }

        public override string Url
        {
            get
            {
                return "http://www.ebay.com/itm/";
            }
        }

        public void ClickBuyNowButton()
        {
            this.Map.BuyNowButton.Click();
        }

        public double GetPrice()
        {
            throw new NotImplementedException();
        }

        public override void Open(string part)
        {
            //Casio-G-Shock-Standard-GA-100-1A2-Mens-Watch-Brand-New-/161209550414?pt=LH_DefaultDomain_15&hash=item2588d6864e
            base.Open(part);
        }
    }
}
