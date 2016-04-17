using OpenQA.Selenium;
using PatternsInAutomatedTests.Advanced.NullObject.Base;
using PatternsInAutomatedTests.Advanced.NullObject.Data;

namespace PatternsInAutomatedTests.Advanced.NullObject.Pages.ShippingAddressPage
{
    public partial class ShippingAddressPage : BasePage
    {
        public ShippingAddressPage(IWebDriver driver) : base(driver)
        {
        }

        public void ClickContinueButton()
        {
            this.ContinueButton.Click();
        }

        public void FillShippingInfo(ClientPurchaseInfo clientInfo)
        {
            this.FillAddressInfoInternal(clientInfo);
        }

        public void ClickDifferentBillingCheckBox(ClientPurchaseInfo clientInfo)
        {
            if (clientInfo.BillingInfo != null)
            {
                this.DifferemtFromBillingCheckbox.Click();
            }
        }

        public void FillBillingInfo(ClientPurchaseInfo clientInfo)
        {
            if (clientInfo.BillingInfo != null)
            {
                this.FillAddressInfoInternal(clientInfo);
            }
        }

        private void FillAddressInfoInternal(ClientPurchaseInfo clientInfo)
        {
            this.CountryDropDown.SelectByText(clientInfo.ShippingInfo.Country);
            this.FullNameInput.SendKeys(clientInfo.ShippingInfo.FullName);
            this.Address1Input.SendKeys(clientInfo.ShippingInfo.Address1);
            this.CityInput.SendKeys(clientInfo.ShippingInfo.City);
            this.ZipInput.SendKeys(clientInfo.ShippingInfo.Zip);
            this.PhoneInput.SendKeys(clientInfo.ShippingInfo.Phone);
            this.ShipToThisAddress.Click();
        }
    }
}