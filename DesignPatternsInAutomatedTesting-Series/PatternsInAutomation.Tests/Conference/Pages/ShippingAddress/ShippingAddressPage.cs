using System;
using OpenQA.Selenium;
using PatternsInAutomation.Tests.Conference.Base;
using PatternsInAutomation.Tests.Conference.Data;

namespace PatternsInAutomation.Tests.Conference.Pages.ShippingAddress
{
    public class ShippingAddressPage : BasePage<ShippingAddressPageMap>, IShippingAddressPage
    {
        public ShippingAddressPage(IWebDriver driver)
            : base(driver, new ShippingAddressPageMap(driver))
        {
        }

        public override string Url
        {
            get
            {
                return string.Empty;
            }
        }

        public void ClickContinueButton()
        {
            this.Map.ContinueButton.Click();
        }

        public void FillShippingInfo(ClientInfo clientInfo)
        {
            this.Map.SwitchToShippingFrame();
            this.Map.CountryDropDown.SelectByText(clientInfo.Country);
            this.Map.FirstName.SendKeys(clientInfo.FirstName);
            this.Map.LastName.SendKeys(clientInfo.LastName);
            this.Map.Address1.SendKeys(clientInfo.Address1);
            this.Map.City.SendKeys(clientInfo.City);
            this.Map.Zip.SendKeys(clientInfo.Zip);
            this.Map.Phone.SendKeys(clientInfo.Phone);
            this.Map.Email.SendKeys(clientInfo.Email);
            this.Map.SwitchToDefault();
        }

        public double GetSubtotalAmount()
        {
            throw new NotImplementedException();
        }
    }
}
