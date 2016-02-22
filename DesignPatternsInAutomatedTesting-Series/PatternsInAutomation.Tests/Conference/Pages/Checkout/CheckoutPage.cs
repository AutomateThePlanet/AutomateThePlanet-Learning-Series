using PPatternsInAutomation.Tests.Conference.Pages.Checkout;
using PatternsInAutomation.Tests.Conference.Base;
using System;
using OpenQA.Selenium;

namespace PatternsInAutomation.Tests.Conference.Pages.Checkout
{
    public class CheckoutPage : BasePage<CheckoutPageMap>, ICheckoutPage
    {
        public CheckoutPage(IWebDriver driver)
            : base(driver, new CheckoutPageMap(driver))
        {
        }

        public override string Url
        {
            get
            {
                return string.Empty;
            }
        }

        public double GetTotalPrice()
        {
            double result = default(double);
            string totalPriceText = this.Map.TotalPrice.Text;
            result = double.Parse(totalPriceText);

            return result;
        }
    }
}
