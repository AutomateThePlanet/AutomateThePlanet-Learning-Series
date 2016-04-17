using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.NullObject.Base;

namespace PatternsInAutomatedTests.Advanced.NullObject.Pages.ShippingPaymentPage
{
    public partial class ShippingPaymentPage : BasePage
    {
        public ShippingPaymentPage(IWebDriver driver)
            : base(driver)
        {
        }

        public void ClickBottomContinueButton()
        {
            this.BottomContinueButton.Click();
        }

        public void ClickTopContinueButton()
        {
            this.TopContinueButton.Click();
        }
    }
}
