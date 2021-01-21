// <copyright file="ShippingAddressPageFillDifferentBillingBehaviour.cs" company="Automate The Planet Ltd.">
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

using AdvancedBehavioursDesignPattern.Base;
using AdvancedBehavioursDesignPattern.Behaviours.Core;
using AdvancedBehavioursDesignPattern.Data;
using AdvancedBehavioursDesignPattern.Pages.ShippingAddressPage;
using AdvancedBehavioursDesignPattern.Pages.ShippingPaymentPage;
using Unity;

namespace AdvancedBehavioursDesignPattern.Behaviours
{
    public class ShippingAddressPageFillDifferentBillingBehaviour : ActionBehaviour
    {
        private readonly ShippingAddressPage _shippingAddressPage;
        private readonly ShippingPaymentPage _shippingPaymentPage;
        private readonly ClientPurchaseInfo _clientPurchaseInfo;

        public ShippingAddressPageFillDifferentBillingBehaviour(ClientPurchaseInfo clientPurchaseInfo)
        {
            _shippingAddressPage = UnityContainerFactory.GetContainer().Resolve<ShippingAddressPage>();
            _shippingPaymentPage = UnityContainerFactory.GetContainer().Resolve<ShippingPaymentPage>();
            _clientPurchaseInfo = clientPurchaseInfo;
        }

        protected override void PerformAct()
        {
            _shippingAddressPage.ClickDifferentBillingCheckBox(_clientPurchaseInfo);
            _shippingAddressPage.ClickContinueButton();
            _shippingPaymentPage.ClickBottomContinueButton();
            _shippingAddressPage.FillBillingInfo(_clientPurchaseInfo);
        }
    }
}