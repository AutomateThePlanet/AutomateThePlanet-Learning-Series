// <copyright file="UiPurchasePromotionalCodeStrategy.cs" company="Automate The Planet Ltd.">
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
using System;
using AdvancedNullObjectDesignPattern.Pages.PlaceOrderPage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvancedNullObjectDesignPattern.ImmutableStrategies
{
    public class UiPurchasePromotionalCodeStrategy : BasePromotionalCodeStrategy
    {
        private readonly PlaceOrderPage placeOrderPage;
        private readonly double couponDiscountAmount;

        public UiPurchasePromotionalCodeStrategy(PlaceOrderPage placeOrderPage, double couponDiscountAmount)
        {
            this.placeOrderPage = placeOrderPage;
            this.couponDiscountAmount = couponDiscountAmount;
        }

        public override void AssertPromotionalCodeDiscount()
        {
            Assert.AreEqual(this.couponDiscountAmount.ToString(), this.placeOrderPage.PromotionalDiscountPrice.Text);
        }

        public override double GetPromotionalCodeDiscountAmount()
        {
            return this.couponDiscountAmount;
        }

        public override void ApplyPromotionalCode(string couponCode)
        {
            this.placeOrderPage.PromotionalCode.SendKeys(couponCode);
        }
    }
}