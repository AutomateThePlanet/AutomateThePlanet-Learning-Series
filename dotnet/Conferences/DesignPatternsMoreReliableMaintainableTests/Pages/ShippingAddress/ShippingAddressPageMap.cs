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

using DesignPatternsMoreReliableMaintainableTests.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DesignPatternsMoreReliableMaintainableTests.Pages.ShippingAddress
{
    public class ShippingAddressPageMap : BaseElementMap
    {
        private readonly WebDriverWait driverWait;

        public ShippingAddressPageMap(IWebDriver driver) : base(driver)
        {
            this.driverWait = new WebDriverWait(driver, new System.TimeSpan(0, 0, 30));
        }

        public SelectElement CountryDropDown
        {
            get
            {
                this.driverWait.Until<IWebElement>((d) => { return d.FindElement(By.Name("country")); });
                return new SelectElement(this.driver.FindElement(By.Name("country")));
            }
        }

        public IWebElement FirstName
        {
            get
            {
                return this.driver.FindElement(By.Id("firstName"));
            }
        }

        public IWebElement LastName
        {
            get
            {
                return this.driver.FindElement(By.Id("lastName"));
            }
        }

        public IWebElement Address1
        {
            get
            {
                return this.driver.FindElement(By.Id("address1"));
            }
        }

        public IWebElement City
        {
            get
            {
                return this.driver.FindElement(By.Id("city"));
            }
        }

        public IWebElement Zip
        {
            get
            {
                return this.driver.FindElement(By.Id("zip"));
            }
        }

        public IWebElement Phone
        {
            get
            {
                return this.driver.FindElement(By.Id("dayphone1"));
            }
        }

        public IWebElement Email
        {
            get
            {
                return this.driver.FindElement(By.Id("email"));
            }
        }

        public IWebElement Subtotal
        {
            get
            {
                return this.driver.FindElement(By.Id("xo_tot_amt"));
            }
        }

        public IWebElement ContinueButton
        {
            get
            {
                return this.driver.FindElement(By.Id("but_address_continue"));
            }
        }

        public void SwitchToShippingFrame()
        {
            this.WaitForLogo();
            this.driver.SwitchTo().Frame("shpFrame");
        }

        private void WaitForLogo()
        {
            this.driverWait.Until<IWebElement>((d) => { return d.FindElement(By.Id("gh-logo")); });
        }
    }
}