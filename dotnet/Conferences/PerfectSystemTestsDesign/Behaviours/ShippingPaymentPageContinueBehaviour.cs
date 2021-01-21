﻿// <copyright file="ShippingPaymentPageContinueBehaviour.cs" company="Automate The Planet Ltd.">
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
using PerfectSystemTestsDesign.Behaviours.Core;
using PerfectSystemTestsDesign.Pages.ShippingPaymentPage;

namespace PerfectSystemTestsDesign.Behaviours
{
    public class ShippingPaymentPageContinueBehaviour : ActionBehaviour
    {
        private readonly ShippingPaymentPage shippingPaymentPage;

        public ShippingPaymentPageContinueBehaviour()
        {
            this.shippingPaymentPage = PerfectSystemTestsDesign.Base.UnityContainerFactory.GetContainer().Resolve<ShippingPaymentPage>(); 
        }

        protected override void PerformAct()
        {
            this.shippingPaymentPage.ClickTopContinueButton();
        }
    }
}