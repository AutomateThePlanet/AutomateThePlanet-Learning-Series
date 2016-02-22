using OpenQA.Selenium;
using PatternsInAutomation.Tests.Conference.Base;

namespace PatternsInAutomation.Tests.Conference.Pages.Item
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
