// <copyright file="ShippingAddressPage.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>

using AdvancedNullObjectDesignPattern.Base;
using AdvancedNullObjectDesignPattern.Data;
using OpenQA.Selenium;

namespace AdvancedNullObjectDesignPattern.Pages.ShippingAddressPage
{
    public partial class ShippingAddressPage : BasePage
    {
        public ShippingAddressPage(IWebDriver driver) : base(driver)
        {
        }

        public void ClickContinueButton()
        {
            ContinueButton.Click();
        }

        public void FillShippingInfo(ClientPurchaseInfo clientInfo)
        {
            FillAddressInfoInternal(clientInfo);
        }

        public void ClickDifferentBillingCheckBox(ClientPurchaseInfo clientInfo)
        {
            if (clientInfo.BillingInfo != null)
            {
                DifferemtFromBillingCheckbox.Click();
            }
        }

        public void FillBillingInfo(ClientPurchaseInfo clientInfo)
        {
            if (clientInfo.BillingInfo != null)
            {
                FillAddressInfoInternal(clientInfo);
            }
        }

        private void FillAddressInfoInternal(ClientPurchaseInfo clientInfo)
        {
            CountryDropDown.SelectByText(clientInfo.ShippingInfo.Country);
            FullNameInput.SendKeys(clientInfo.ShippingInfo.FullName);
            Address1Input.SendKeys(clientInfo.ShippingInfo.Address1);
            CityInput.SendKeys(clientInfo.ShippingInfo.City);
            ZipInput.SendKeys(clientInfo.ShippingInfo.Zip);
            PhoneInput.SendKeys(clientInfo.ShippingInfo.Phone);
            ShipToThisAddress.Click();
        }
    }
}