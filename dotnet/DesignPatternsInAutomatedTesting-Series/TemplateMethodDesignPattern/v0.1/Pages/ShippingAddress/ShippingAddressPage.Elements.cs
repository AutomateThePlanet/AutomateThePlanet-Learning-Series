// <copyright file="ShippingAddressPageMap.cs" company="Automate The Planet Ltd.">
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
// <site>http://automatetheplanet.com/</site>

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TemplateMethodDesignPattern.Pages.ShippingAddress.First
{
    public partial class ShippingAddressPage
    {
        public SelectElement CountryDropDown
        {
            get
            {
                _driverWait.Until(d => d.FindElement(By.Name("country")));
                return new SelectElement(Driver.FindElement(By.Name("country")));
            }
        }

        public IWebElement FirstName => Driver.FindElement(By.Id("firstName"));

        public IWebElement LastName => Driver.FindElement(By.Id("lastName"));

        public IWebElement Address1 => Driver.FindElement(By.Id("address1"));

        public IWebElement City => Driver.FindElement(By.Id("city"));

        public IWebElement Zip => Driver.FindElement(By.Id("zip"));

        public IWebElement Phone => Driver.FindElement(By.Id("dayphone1"));

        public IWebElement Email => Driver.FindElement(By.Id("email"));

        public IWebElement Subtotal => Driver.FindElement(By.Id("xo_tot_amt"));

        public IWebElement ContinueButton => Driver.FindElement(By.Id("but_address_continue"));

        public void SwitchToShippingFrame()
        {
            WaitForLogo();
            Driver.SwitchTo().Frame("shpFrame");
        }

        private void WaitForLogo()
        {
            _driverWait.Until(d => d.FindElement(By.Id("gh-logo")));
        }
    }
}