// <copyright file="ShippingAddressPage.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
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

using HandlingTestEnvironmentsData.Base.Second;
using HandlingTestEnvironmentsData.Data.Second;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HandlingTestEnvironmentsData.Pages.ShippingAddress.Second
{
    public partial class ShippingAddressPage : WebPage
    {
        private readonly WebDriverWait _driverWait;

        public ShippingAddressPage(IWebDriver driver)
            : base(driver) 
            => _driverWait = new WebDriverWait(Driver, new System.TimeSpan(0, 0, 30));

        protected override string Url => string.Empty;

        public void ClickContinueButton()
        {
            ContinueButton.Click();
        }

        public void FillShippingInfo(ClientInfo clientInfo)
        {
            SwitchToShippingFrame();
            CountryDropDown.SelectByText(clientInfo.Country);
            FirstName.SendKeys(clientInfo.FirstName);
            LastName.SendKeys(clientInfo.LastName);
            Address1.SendKeys(clientInfo.Address1);
            City.SendKeys(clientInfo.City);
            Zip.SendKeys(clientInfo.Zip);
            Phone.SendKeys(clientInfo.Phone);
            Email.SendKeys(clientInfo.Email);
            Driver.SwitchTo().DefaultContent();
        }

        public double GetSubtotalAmount() => double.Parse(Subtotal.Text);
    }
}