// <copyright file="ShippingAddressPage.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
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
using PatternsInAutomatedTests.Advanced.Core;
using PatternsInAutomatedTests.Advanced.Strategy.Data;

namespace PatternsInAutomatedTests.Advanced.Strategy.Pages.ShippingAddressPage
{
    public class ShippingAddressPage : BasePageSingleton<ShippingAddressPage, ShippingAddressPageMap>
    {
        public void ClickContinueButton()
        {
            this.Map.ContinueButton.Click();
        }

        public void FillShippingInfo(ClientPurchaseInfo clientInfo)
        {
            this.FillAddressInfoInternal(clientInfo);
        }

        public void ClickDifferentBillingCheckBox(ClientPurchaseInfo clientInfo)
        {
            if (clientInfo.BillingInfo != null)
            {
                this.Map.DifferemtFromBillingCheckbox.Click();
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
