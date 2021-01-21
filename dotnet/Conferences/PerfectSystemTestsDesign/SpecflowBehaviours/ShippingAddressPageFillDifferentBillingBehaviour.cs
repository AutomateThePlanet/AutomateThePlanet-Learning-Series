﻿// <copyright file="ShippingAddressPageFillDifferentBillingBehaviour.cs" company="Automate The Planet Ltd.">
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

using Microsoft.Practices.Unity;
using PerfectSystemTestsDesign.Base;
using PerfectSystemTestsDesign.Data;
using PerfectSystemTestsDesign.Pages.ShippingAddressPage;
using PerfectSystemTestsDesign.Pages.ShippingPaymentPage;
using PerfectSystemTestsDesign.SpecflowBehaviours.Core;
using TechTalk.SpecFlow;

namespace PerfectSystemTestsDesign.SpecflowBehaviours
{
    [Binding]
    public class ShippingAddressPageFillDifferentBillingBehaviour : ActionBehaviour
    {
        private readonly ShippingAddressPage shippingAddressPage;
        private readonly ShippingPaymentPage shippingPaymentPage;
        private ClientPurchaseInfo clientPurchaseInfo;

        public ShippingAddressPageFillDifferentBillingBehaviour()
        {
            this.shippingAddressPage = UnityContainerFactory.GetContainer().Resolve<ShippingAddressPage>();
            this.shippingPaymentPage = UnityContainerFactory.GetContainer().Resolve<ShippingPaymentPage>();
        }

        [When(@"I choose to fill different billing, full name = ""([^""]*)"", country = ""([^""]*)"", Adress = ""([^""]*)"", city = ""([^""]*)"", state = ""([^""]*)"", zip = ""([^""]*)"" and phone = ""([^""]*)""")]
        public void FillDifferentBillingInfo(string fullName, string country, string address, string state, string city, string zip, string phone)
        {
            this.clientPurchaseInfo = new ClientPurchaseInfo(
                new ClientAddressInfo()
                {
                    FullName = fullName,
                    Country = country,
                    Address1 = address,
                    State = state,
                    City = city,
                    Zip = zip,
                    Phone = phone
                });
            base.Execute();
        }

        protected override void PerformAct()
        {
            this.shippingAddressPage.ClickDifferentBillingCheckBox(this.clientPurchaseInfo);
            this.shippingAddressPage.ClickContinueButton();
            this.shippingPaymentPage.ClickBottomContinueButton();
            this.shippingAddressPage.FillBillingInfo(this.clientPurchaseInfo);
        }
    }
}