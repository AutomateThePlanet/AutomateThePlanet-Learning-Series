﻿// <copyright file="OrderSummaryCalculator.cs" company="Automate The Planet Ltd.">
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

namespace DecoratorDesignPattern.Services
{
    public class OrderSummaryCalculator
    {
        public OrderSummaryCalculator()
        {
            ShippingCostsCalculationService = new ShippingCostsCalculationService();
        }

        public ShippingCostsCalculationService ShippingCostsCalculationService { get; set; }

        public decimal CalculateTotalPrice(decimal itemsPrice, decimal estimatedTax, Data.ClientPurchaseInfo clientPurchaseInfo)
        {
            var totalPrice = default(decimal);
            var shippingCosts = CalculateShippingPrice(clientPurchaseInfo);
            totalPrice = itemsPrice + estimatedTax + shippingCosts;

            return totalPrice;
        }

        public decimal CalculateBeforeTaxPrice(decimal itemsPrice, Data.ClientPurchaseInfo clientPurchaseInfo)
        {
            var beforeTaxPrice = default(decimal);
            var shippingCosts = CalculateShippingPrice(clientPurchaseInfo);
            beforeTaxPrice = itemsPrice + shippingCosts;

            return beforeTaxPrice;
        }

        public decimal CalculateShippingPrice(Data.ClientPurchaseInfo clientPurchaseInfo)
        {
            var shippingCosts = ShippingCostsCalculationService.Calculate(clientPurchaseInfo.ShippingInfo.Country, clientPurchaseInfo.ShippingInfo.State, clientPurchaseInfo.ShippingInfo.Address1, clientPurchaseInfo.ShippingInfo.Zip);
            return shippingCosts;
        }
    }
}