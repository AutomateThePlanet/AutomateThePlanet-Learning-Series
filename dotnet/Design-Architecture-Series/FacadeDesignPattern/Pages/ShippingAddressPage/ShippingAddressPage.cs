// <copyright file="ShippingAddressPage.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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

using FacadeDesignPattern.Core;
using FacadeDesignPattern.Data;

namespace FacadeDesignPattern.Pages.ShippingAddressPage;

public class ShippingAddressPage : BasePage<ShippingAddressPageMap, ShippingAddressPageValidator>
{
    public void ClickContinueButton()
    {
        Map.ContinueButton.Click();
    }

    public void FillShippingInfo(ClientInfo clientInfo)
    {
        Map.SwitchToShippingFrame();
        Map.CountryDropDown.SelectByText(clientInfo.Country);
        Map.FirstName.SendKeys(clientInfo.FirstName);
        Map.LastName.SendKeys(clientInfo.LastName);
        Map.Address1.SendKeys(clientInfo.Address1);
        Map.City.SendKeys(clientInfo.City);
        Map.Zip.SendKeys(clientInfo.Zip);
        Map.Phone.SendKeys(clientInfo.Phone);
        Map.Email.SendKeys(clientInfo.Email);
        Map.SwitchToDefault();
    }
}