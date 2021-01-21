﻿// <copyright file="ShippingAddressPage.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
// <site>https://automatetheplanet.com/</site>
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace WebDriverTestsCSharpSix.CSharpFive.NullConditionalOperator
{
    public partial class ShippingAddressPage
    {
        private readonly IWebDriver _driver;

        public ShippingAddressPage(IWebDriver browser)
        {
            _driver = browser;
            PageFactory.InitElements(browser, this);
        }

        // some other actions
        private void FillAddressInfoInternal(ClientPurchaseInfo clientInfo)
        {
            Country.SelectByText(clientInfo.Country);
            FullName.SendKeys(clientInfo.FullName);
            Address.SendKeys(clientInfo.Address);
            City.SendKeys(clientInfo.City);
            Zip.SendKeys(clientInfo.Zip == null ? string.Empty : clientInfo.Zip);
            Phone.SendKeys(clientInfo.Phone == null ? string.Empty : clientInfo.Phone);
            Vat.SendKeys(clientInfo.Vat == null ? string.Empty : clientInfo.Vat);
        }
    }
}
