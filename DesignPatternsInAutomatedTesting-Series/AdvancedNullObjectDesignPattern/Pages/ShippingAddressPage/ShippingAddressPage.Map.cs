// <copyright file="ShippingAddressPage.Map.cs" company="Automate The Planet Ltd.">
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

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AdvancedNullObjectDesignPattern.Pages.ShippingAddressPage
{
    public partial class ShippingAddressPage
    {
        public SelectElement CountryDropDown
        {
            get
            {
                this.driverWait.Until<IWebElement>((d) => { return d.FindElement(By.Name("enterAddressCountryCode")); });
                return new SelectElement(this.driver.FindElement(By.Name("enterAddressCountryCode")));
            }
        }

        public IWebElement FullNameInput
        {
            get
            {
                return this.driver.FindElement(By.Id("enterAddressFullName"));
            }
        }

        public IWebElement Address1Input
        {
            get
            {
                return this.driver.FindElement(By.Id("enterAddressAddressLine1"));
            }
        }

        public IWebElement CityInput
        {
            get
            {
                return this.driver.FindElement(By.Id("enterAddressCity"));
            }
        }

        public IWebElement ZipInput
        {
            get
            {
                return this.driver.FindElement(By.Id("enterAddressPostalCode"));
            }
        }

        public IWebElement PhoneInput
        {
            get
            {
                return this.driver.FindElement(By.Id("enterAddressPhoneNumber"));
            }
        }

        public SelectElement DeliveryPreferenceDropDown
        {
            get
            {
                return new SelectElement(this.driver.FindElement(By.Name("AddressType")));
            }
        }

        public IWebElement ContinueButton
        {
            get
            {
                return this.driver.FindElement(By.XPath("//input[@value='Continue']"));
            }
        }

        public IWebElement ShipToThisAddress
        {
            get
            {
                return this.driver.FindElement(By.CssSelector("input.a-button-text"));
            }
        }

        public IWebElement DifferemtFromBillingCheckbox
        {
            get
            {
                return this.driver.FindElement(By.Id("isBillingAddress"));
            }
        }
    }
}