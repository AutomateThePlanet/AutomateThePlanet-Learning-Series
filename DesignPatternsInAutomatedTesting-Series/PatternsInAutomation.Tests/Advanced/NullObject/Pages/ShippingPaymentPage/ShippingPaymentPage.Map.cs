using OpenQA.Selenium;

namespace PatternsInAutomatedTests.Advanced.NullObject.Pages.ShippingPaymentPage
{
    public partial class ShippingPaymentPage
    {
        public IWebElement BottomContinueButton
        {
            get
            {
                return this.driver.FindElement(By.XPath("//*[@id='shippingOptionFormId']/div[3]/div/div/span[1]/span/input"));
            }
        }

        public IWebElement TopContinueButton
        {
            get
            {
                return this.driver.FindElement(By.Id("continue-top"));
            }
        }
    }
}
