using PatternsInAutomation.Tests.Advanced.Core;

namespace PatternsInAutomation.Tests.Advanced.Decorator.Pages.ShippingAddressPage
{
    public class ShippingAddressPage : BasePageSingleton<ShippingAddressPage, ShippingAddressPageMap>
    {
        public void ClickContinueButton()
        {
            this.Map.ContinueButton.Click();
        }

        public void FillShippingInfo(PatternsInAutomation.Tests.Advanced.Decorator.Data.ClientPurchaseInfo clientInfo)
        {
            this.FillAddressInfoInternal(clientInfo);
        }

        public void ClickDifferentBillingCheckBox(PatternsInAutomation.Tests.Advanced.Decorator.Data.ClientPurchaseInfo clientInfo)
        {
            if (clientInfo.BillingInfo != null)
            {
                this.Map.DifferemtFromBillingCheckbox.Click();
            }            
        }

        public void FillBillingInfo(PatternsInAutomation.Tests.Advanced.Decorator.Data.ClientPurchaseInfo clientInfo)
        {
            if (clientInfo.BillingInfo != null)
            {
                this.FillAddressInfoInternal(clientInfo);
            }             
        }

        private void FillAddressInfoInternal(PatternsInAutomation.Tests.Advanced.Decorator.Data.ClientPurchaseInfo clientInfo)
        {
            this.Map.CountryDropDown.SelectByText(clientInfo.ShippingInfo.Country);
            this.Map.FullNameInput.SendKeys(clientInfo.ShippingInfo.FullName);
            this.Map.Address1Input.SendKeys(clientInfo.ShippingInfo.Address1);
            this.Map.CityInput.SendKeys(clientInfo.ShippingInfo.City);
            this.Map.ZipInput.SendKeys(clientInfo.ShippingInfo.Zip);
            this.Map.PhoneInput.SendKeys(clientInfo.ShippingInfo.Phone);
            this.Map.ShipToThisAddress.Click();
        }
    }
}
