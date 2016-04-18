// <copyright file="VatTaxOrderPurchaseStrategy.cs" company="Automate The Planet Ltd.">
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
using PatternsInAutomatedTests.Advanced.Decorator.Data;
using PatternsInAutomatedTests.Advanced.Decorator.Enums;
using PatternsInAutomatedTests.Advanced.Decorator.Pages.PlaceOrderPage;
using PatternsInAutomatedTests.Advanced.Decorator.Services;

namespace PatternsInAutomatedTests.Advanced.Decorator.Advanced.Strategies
{
    public class VatTaxOrderPurchaseStrategy : OrderPurchaseStrategyDecorator
    {
        private readonly VatTaxCalculationService vatTaxCalculationService;
        private decimal vatTax;     

        public VatTaxOrderPurchaseStrategy(OrderPurchaseStrategy orderPurchaseStrategy, decimal itemsPrice, ClientPurchaseInfo clientPurchaseInfo) 
            : base(orderPurchaseStrategy, itemsPrice, clientPurchaseInfo)
        {
            this.vatTaxCalculationService = new VatTaxCalculationService();
        }

        public override decimal CalculateTotalPrice()
        {
            Countries currentCountry = (Countries)Enum.Parse(typeof(Countries), clientPurchaseInfo.BillingInfo.Country);
            this.vatTax = this.vatTaxCalculationService.Calculate(this.itemsPrice, currentCountry);
            return this.orderPurchaseStrategy.CalculateTotalPrice() + this.vatTax;
        }

        public override void ValidateOrderSummary(decimal totalPrice)
        {
            base.orderPurchaseStrategy.ValidateOrderSummary(totalPrice);
            PlaceOrderPage.Instance.Validate().EstimatedTaxPrice(vatTax.ToString());
        }
    }
}