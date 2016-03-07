using OpenQA.Selenium;
using PatternsInAutomatedTests.Conference.Base;

namespace PatternsInAutomatedTests.Conference.Pages.Item
{
    public class ItemPageMap : BaseElementMap
    {
        public ItemPageMap(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement BuyNowButton
        {
            get
            {
                return this.driver.FindElement(By.Id("binBtn_btn"));
            }
        }

        public IWebElement Price
        {
            get
            {
                return this.driver.FindElement(By.Id("prcIsum"));
            }
        }
    }
}
