// <copyright file="ShippingAddressPageMap.cs" company="Automate The Planet Ltd.">
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

using FacadeDesignPattern.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FacadeDesignPattern.Pages.ShippingAddressPage
{
    public class ShippingAddressPageMap : BasePageElementMap
    {
        public SelectElement CountryDropDown
        {
            get
            {
                BrowserWait.Until<IWebElement>((d) => { return d.FindElement(By.Name("country")); });
                return new SelectElement(Browser.FindElement(By.Name("country")));
            }
        }

        public IWebElement FirstName
        {
            get
            {
                return Browser.FindElement(By.Id("firstName"));
            }
        }

        public IWebElement LastName
        {
            get
            {
                return Browser.FindElement(By.Id("lastName"));
            }
        }

        public IWebElement Address1
        {
            get
            {
                return Browser.FindElement(By.Id("address1"));
            }
        }

        public IWebElement City
        {
            get
            {
                return Browser.FindElement(By.Id("city"));
            }
        }

        public IWebElement Zip
        {
            get
            {
                return Browser.FindElement(By.Id("zip"));
            }
        }

        public IWebElement Phone
        {
            get
            {
                return Browser.FindElement(By.Id("dayphone1"));
            }
        }

        public IWebElement Email
        {
            get
            {
                return Browser.FindElement(By.Id("email"));
            }
        }

        public IWebElement Subtotal
        {
            get
            {
                return Browser.FindElement(By.Id("xo_tot_amt"));
            }
        }

        public IWebElement ContinueButton
        {
            get
            {
                return Browser.FindElement(By.Id("but_address_continue"));
            }
        }

        public void SwitchToShippingFrame()
        {
            WaitForLogo();
            Browser.SwitchTo().Frame("shpFrame");
        }

        private void WaitForLogo()
        {
            BrowserWait.Until<IWebElement>((d) => { return d.FindElement(By.Id("gh-logo")); });
        }
    }
}