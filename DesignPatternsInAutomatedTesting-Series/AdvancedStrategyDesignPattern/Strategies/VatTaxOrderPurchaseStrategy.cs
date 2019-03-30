// <copyright file="VatTaxOrderPurchaseStrategy.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
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
using PatternsInAutomatedTests.Advanced.Strategy;

namespace AdvancedStrategyDesignPattern.Strategies
{
    public class VatTaxOrderPurchaseStrategy : IOrderPurchaseStrategy
    {
        public VatTaxOrderPurchaseStrategy()
        {
            VatTaxCalculationService = new VatTaxCalculationService();
        }

        public VatTaxCalculationService VatTaxCalculationService { get; set; }

        public void AssertOrderSummary(string itemsPrice, ClientPurchaseInfo clientPurchaseInfo)
        {
            var currentCountry = (Countries)Enum.Parse(typeof(Countries), clientPurchaseInfo.BillingInfo.Country);
            var currentItemPrice = decimal.Parse(itemsPrice);
            var vatTax = VatTaxCalculationService.Calculate(currentItemPrice, currentCountry);

            PlaceOrderPage.Instance.Validate().EstimatedTaxPrice(vatTax.ToString());
        }

        public void ValidateClientPurchaseInfo(ClientPurchaseInfo clientPurchaseInfo)
        {
            // Throw a new Argument exection if the country is not part of the EU Union.
        }
    }
}