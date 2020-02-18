// <copyright file="GiftOrderPurchaseStrategy.cs" company="Automate The Planet Ltd.">
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
using AdvancedStrategyDesignPattern.Base;
using AdvancedStrategyDesignPattern.Data;
using AdvancedStrategyDesignPattern.Enums;
using AdvancedStrategyDesignPattern.Pages.PlaceOrderPage;
using AdvancedStrategyDesignPattern.Services;

namespace AdvancedStrategyDesignPattern.Strategies
{
    public class GiftOrderPurchaseStrategy : IOrderPurchaseStrategy
    {
        public GiftOrderPurchaseStrategy()
        {
            GiftWrappingPriceCalculationService = new GiftWrappingPriceCalculationService();
        }

        public GiftWrappingPriceCalculationService GiftWrappingPriceCalculationService { get; set; }

        public void AssertOrderSummary(string itemsPrice, ClientPurchaseInfo clientPurchaseInfo)
        {
            var giftWrapPrice = GiftWrappingPriceCalculationService.Calculate(clientPurchaseInfo.GiftWrapping);

            PlaceOrderPage.Instance.Validate().GiftWrapPrice(giftWrapPrice.ToString());
        }

        public void ValidateClientPurchaseInfo(ClientPurchaseInfo clientPurchaseInfo)
        {
            if (clientPurchaseInfo.GiftWrapping.Equals(GiftWrappingStyles.None))
            {
                throw new ArgumentException("The gift wrapping style cannot be set to None if the GiftOrderPurchaseStrategy should be executed.");
            }
        }
    }
}