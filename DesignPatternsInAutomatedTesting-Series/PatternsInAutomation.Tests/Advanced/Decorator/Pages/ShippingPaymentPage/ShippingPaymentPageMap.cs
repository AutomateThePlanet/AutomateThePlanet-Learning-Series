using OpenQA.Selenium;
using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Decorator.Pages.ShippingPaymentPage
{
    public class ShippingPaymentPageMap : BasePageElementMap
    {
        public IWebElement BottomContinueButton
        {
            get
            {
                return this.browser.FindElement(By.XPath("//*[@id='shippingOptionFormId']/div[3]/div/div/span[1]/span/input"));
            }
        }

        public IWebElement TopContinueButton
        {
            get
            {
                return this.browser.FindElement(By.Id("continue-top"));
            }
        }
    }
}
