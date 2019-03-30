// <copyright file="SalesTaxOrderPurchaseStrategy.cs" company="Automate The Planet Ltd.">
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
using AdvancedStrategyDesignPattern.Services;

namespace AdvancedStrategyDesignPattern.Strategies
{
    public class SalesTaxOrderPurchaseStrategy : IOrderPurchaseStrategy
    {
        public SalesTaxOrderPurchaseStrategy()
        {
            SalesTaxCalculationService = new SalesTaxCalculationService();
        }

        public SalesTaxCalculationService SalesTaxCalculationService { get; set; }

        public void AssertOrderSummary(string itemsPrice, ClientPurchaseInfo clientPurchaseInfo)
        {
            var currentState = (States)Enum.Parse(typeof(States), clientPurchaseInfo.ShippingInfo.State);
            var currentItemPrice = decimal.Parse(itemsPrice);
            var salesTax = SalesTaxCalculationService.Calculate(currentItemPrice, currentState, clientPurchaseInfo.ShippingInfo.Zip);

            PlaceOrderPage.Instance.Validate().EstimatedTaxPrice(salesTax.ToString());
        }

        public void ValidateClientPurchaseInfo(ClientPurchaseInfo clientPurchaseInfo)
        {
            if (!clientPurchaseInfo.ShippingInfo.Country.Equals("United States"))
            {
                throw new ArgumentException("If the NoTaxesOrderPurchaseStrategy is used, the country should be set to United States because otherwise no sales tax is going to be applied.");
            }
        }
    }
}