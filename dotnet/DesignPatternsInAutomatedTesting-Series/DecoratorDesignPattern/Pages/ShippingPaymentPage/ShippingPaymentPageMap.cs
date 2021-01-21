// <copyright file="ShippingPaymentPageMap.cs" company="Automate The Planet Ltd.">
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

using DecoratorDesignPattern.Core;
using OpenQA.Selenium;

namespace DecoratorDesignPattern.Pages.ShippingPaymentPage
{
    public class ShippingPaymentPageMap : BasePageElementMap
    {
        public IWebElement BottomContinueButton
        {
            get
            {
                return Browser.FindElement(By.XPath("//*[@id='shippingOptionFormId']/div[3]/div/div/span[1]/span/input"));
            }
        }

        public IWebElement TopContinueButton
        {
            get
            {
                return Browser.FindElement(By.Id("continue-top"));
            }
        }
    }
}