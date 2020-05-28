// <copyright file="UiPurchasePromotionalCodeStrategy.cs" company="Automate The Planet Ltd.">
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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NullObjectDesignPattern.Base;
using NullObjectDesignPattern.Pages.PlaceOrderPage;

namespace NullObjectDesignPattern.Strategies
{
    public class UiPurchasePromotionalCodeStrategy : IPurchasePromotionalCodeStrategy
    {
        private readonly PlaceOrderPage _placeOrderPage;
        private readonly double _couponDiscountAmount;

        public UiPurchasePromotionalCodeStrategy(PlaceOrderPage placeOrderPage, double couponDiscountAmount)
        {
            _placeOrderPage = placeOrderPage;
            _couponDiscountAmount = couponDiscountAmount;
        }

        public void AssertPromotionalCodeDiscount()
        {
            Assert.AreEqual(_couponDiscountAmount.ToString(), _placeOrderPage.PromotionalDiscountPrice.Text);
        }

        public double GetPromotionalCodeDiscountAmount()
        {
            return _couponDiscountAmount;
        }

        public void ApplyPromotionalCode(string couponCode)
        {
            _placeOrderPage.PromotionalCode.SendKeys(couponCode);
        }
    }
}