// <copyright file="ShippingAddressPage.Map.cs" company="Automate The Planet Ltd.">
// Copyright 2017 Automate The Planet Ltd.
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

namespace WebDriverTestsCSharpSix.CSharpSix.NullConditionalOperator
{
    public partial class ShippingAddressPage
    {
        public SelectElement Country
        {
            get
            {
                return new SelectElement(driver.FindElement(By.Name("enterAddressCountryCode")));
            }
        }

        public IWebElement FullName
        {
            get
            {
                return driver.FindElement(By.Id("enterAddressFullName"));
            }
        }

        public IWebElement Address
        {
            get
            {
                return driver.FindElement(By.Id("enterAddressAddressLine1"));
            }
        }

        public IWebElement City
        {
            get
            {
                return driver.FindElement(By.Id("enterAddressCity"));
            }
        }

        public IWebElement Zip
        {
            get
            {
                return driver.FindElement(By.Id("enterAddressPostalCode"));
            }
        }

        public IWebElement Phone
        {
            get
            {
                return driver.FindElement(By.Id("enterAddressPhoneNumber"));
            }
        }

        public IWebElement Vat
        {
            get
            {
                return driver.FindElement(By.Id("enterVat"));
            }
        }
    }
}
