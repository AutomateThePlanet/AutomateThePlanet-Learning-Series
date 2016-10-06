// <copyright file="PlaceOrderPageAssertFinalAmountsBehaviour.cs" company="Automate The Planet Ltd.">
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
using PerfectSystemTestsDesign.Pages.PlaceOrderPage;
using PerfectSystemTestsDesign.SpecflowBehaviours.Core;
using TechTalk.SpecFlow;

namespace PerfectSystemTestsDesign.SpecflowBehaviours
{
    [Binding]
    public class PlaceOrderPageAssertFinalAmountsBehaviour : AssertBehaviour
    {
        private readonly PlaceOrderPage placeOrderPage;
        private string itemPrice;

        public PlaceOrderPageAssertFinalAmountsBehaviour()
        {
            this.placeOrderPage = UnityContainerFactory.GetContainer().Resolve<PlaceOrderPage>();
        }

        [Then(@"assert that order total price = ""([^""]*)""")]
        public void AssertOrderTotalPrice(string itemPrice)
        {
            this.itemPrice = itemPrice;
            this.Execute();
        }

        protected override void Assert()
        {
            double totalPrice = double.Parse(this.itemPrice);
            this.placeOrderPage.AssertOrderTotalPrice(totalPrice);
        }
    }
}