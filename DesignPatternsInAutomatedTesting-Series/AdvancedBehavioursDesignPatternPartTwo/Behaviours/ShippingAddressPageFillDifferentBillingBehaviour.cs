// <copyright file="ShippingAddressPageFillDifferentBillingBehaviour.cs" company="Automate The Planet Ltd.">
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

using AdvancedBehavioursDesignPatternPartTwo.Behaviours.Core;
using AdvancedBehavioursDesignPatternPartTwo.Pages.ShippingAddressPage;
using AdvancedBehavioursDesignPatternPartTwo.Pages.ShippingPaymentPage;
using Microsoft.Practices.Unity;

namespace AdvancedBehavioursDesignPatternPartTwo.Behaviours
{
    public class ShippingAddressPageFillDifferentBillingBehaviour : ActionBehaviour
    {
        private readonly ShippingAddressPage shippingAddressPage;
        private readonly ShippingPaymentPage shippingPaymentPage;
        private readonly AdvancedBehavioursDesignPatternPartTwo.Data.ClientPurchaseInfo clientPurchaseInfo;

        public ShippingAddressPageFillDifferentBillingBehaviour(AdvancedBehavioursDesignPatternPartTwo.Data.ClientPurchaseInfo clientPurchaseInfo)
        {
            this.shippingAddressPage = AdvancedBehavioursDesignPatternPartTwo.Base.UnityContainerFactory.GetContainer().Resolve<ShippingAddressPage>();
            this.shippingPaymentPage = AdvancedBehavioursDesignPatternPartTwo.Base.UnityContainerFactory.GetContainer().Resolve<ShippingPaymentPage>();
            this.clientPurchaseInfo = clientPurchaseInfo;
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