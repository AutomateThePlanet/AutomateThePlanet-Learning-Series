using PatternsInAutomatedTests.Advanced.Core;

namespace PatternsInAutomatedTests.Advanced.Decorator.Pages.ShippingAddressPage
{
    public class ShippingAddressPage : BasePageSingleton<ShippingAddressPage, ShippingAddressPageMap>
    {
        public void ClickContinueButton()
        {
            this.Map.ContinueButton.Click();
        }

        public void FillShippingInfo(PatternsInAutomatedTests.Advanced.Decorator.Data.ClientPurchaseInfo clientInfo)
        {
            this.FillAddressInfoInternal(clientInfo);
        }

        public void ClickDifferentBillingCheckBox(PatternsInAutomatedTests.Advanced.Decorator.Data.ClientPurchaseInfo clientInfo)
        {
            if (clientInfo.BillingInfo != null)
            {
                this.Map.DifferemtFromBillingCheckbox.Click();
            }            
        }

        public void FillBillingInfo(PatternsInAutomatedTests.Advanced.Decorator.Data.ClientPurchaseInfo clientInfo)
        {
            if (clientInfo.BillingInfo != null)
            {
                this.FillAddressInfoInternal(clientInfo);
            }             
        }

        private void FillAddressInfoInternal(PatternsInAutomatedTests.Advanced.Decorator.Data.ClientPurchaseInfo clientInfo)
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
