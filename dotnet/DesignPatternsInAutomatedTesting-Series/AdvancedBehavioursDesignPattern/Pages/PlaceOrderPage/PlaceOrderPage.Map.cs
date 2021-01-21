﻿// <copyright file="PlaceOrderPage.Map.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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

namespace AdvancedBehavioursDesignPattern.Pages.PlaceOrderPage
{
    public partial class PlaceOrderPage
    {
        public IWebElement TotalPrice
        {
            get
            {
                return Driver.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[7]/td[2]/strong"));
            }
        }
     
        public IWebElement PromotionalDiscountPrice
        {
            get
            {
                return Driver.FindElement(By.XPath("//*[@id='subtotals-marketplace-table']/table/tbody/tr[5]/td[2]"));
            }
        }

        public IWebElement PromotionalCode
        {
            get
            {
                return Driver.FindElement(By.Id("xocpnety_cnt"));
            }
        }
    }
}