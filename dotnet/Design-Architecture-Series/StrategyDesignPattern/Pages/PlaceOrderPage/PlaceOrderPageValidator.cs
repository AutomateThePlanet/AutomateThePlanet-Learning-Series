// <copyright file="PlaceOrderPageValidator.cs" company="Automate The Planet Ltd.">
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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StrategyDesignPattern.Pages.PlaceOrderPage
{
    public class PlaceOrderPageValidator : Core.BasePageValidator<PlaceOrderPageMap>
    {
        public void ItemsPrice(string expectedPrice)
        {
            Assert.AreEqual<string>(expectedPrice, Map.ItemsPrice.Text);
        }

        public void BeforeTaxesPrice(string expectedPrice)
        {
            Assert.AreEqual<string>(expectedPrice, Map.TotalBeforeTaxPrice.Text);
        }

        public void EstimatedTaxPrice(string expectedPrice)
        {
            Assert.AreEqual<string>(expectedPrice, Map.EstimatedTaxPrice.Text);
        }

        public void OrderTotalPrice(string expectedPrice)
        {
            Assert.AreEqual<string>(expectedPrice, Map.TotalPrice.Text);
        }

        public void GiftWrapPrice(string expectedPrice)
        {
            Assert.AreEqual<string>(expectedPrice, Map.GiftWrapPrice.Text);
        }

        public void ShippingTaxPrice(string expectedPrice)
        {
            Assert.AreEqual<string>(expectedPrice, Map.ShippingTax.Text);
        }
    }
}