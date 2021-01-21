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

using StrategyDesignPattern.Data;

namespace StrategyDesignPattern.Pages.ShippingAddressPage
{
    public class ShippingAddressPage : Core.BasePageSingleton<ShippingAddressPage, ShippingAddressPageMap>
    {
        public void ClickContinueButton()
        {
            Map.ContinueButton.Click();
        }

        public void FillShippingInfo(ClientPurchaseInfo clientInfo)
        {
            FillAddressInfoInternal(clientInfo);
        }

        public void ClickDifferentBillingCheckBox(ClientPurchaseInfo clientInfo)
        {
            if (clientInfo.BillingInfo != null)
            {
                Map.DifferemtFromBillingCheckbox.Click();
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
            Map.CountryDropDown.SelectByText(clientInfo.ShippingInfo.Country);
            Map.FullNameInput.SendKeys(clientInfo.ShippingInfo.FullName);
            Map.Address1Input.SendKeys(clientInfo.ShippingInfo.Address1);
            Map.CityInput.SendKeys(clientInfo.ShippingInfo.City);
            Map.ZipInput.SendKeys(clientInfo.ShippingInfo.Zip);
            Map.PhoneInput.SendKeys(clientInfo.ShippingInfo.Phone);
            Map.ShipToThisAddress.Click();
        }
    }
}