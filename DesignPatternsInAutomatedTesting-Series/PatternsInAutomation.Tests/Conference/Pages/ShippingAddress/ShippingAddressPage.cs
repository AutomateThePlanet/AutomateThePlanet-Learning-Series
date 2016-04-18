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
using System;
using OpenQA.Selenium;
using PatternsInAutomatedTests.Conference.Base;
using PatternsInAutomatedTests.Conference.Data;

namespace PatternsInAutomatedTests.Conference.Pages.ShippingAddress
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
