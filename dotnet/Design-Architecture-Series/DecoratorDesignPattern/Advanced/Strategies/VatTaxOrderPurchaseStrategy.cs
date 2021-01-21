// <copyright file="VatTaxOrderPurchaseStrategy.cs" company="Automate The Planet Ltd.">
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

using System;
using DecoratorDesignPattern.Enums;
using DecoratorDesignPattern.Pages.PlaceOrderPage;
using DecoratorDesignPattern.Services;

namespace DecoratorDesignPattern.Advanced.Strategies
{
    public class VatTaxOrderPurchaseStrategy : OrderPurchaseStrategyDecorator
    {
        private readonly VatTaxCalculationService _vatTaxCalculationService;
        private decimal _vatTax;     

        public VatTaxOrderPurchaseStrategy(OrderPurchaseStrategy orderPurchaseStrategy, decimal itemsPrice, Data.ClientPurchaseInfo clientPurchaseInfo) : base(orderPurchaseStrategy, itemsPrice, clientPurchaseInfo)
        {
            _vatTaxCalculationService = new VatTaxCalculationService();
        }

        public override decimal CalculateTotalPrice()
        {
            var currentCountry = (Countries)Enum.Parse(typeof(Countries), ClientPurchaseInfo.BillingInfo.Country);
            _vatTax = _vatTaxCalculationService.Calculate(ItemsPrice, currentCountry);
            return OrderPurchaseStrategy.CalculateTotalPrice() + _vatTax;
        }

        public override void ValidateOrderSummary(decimal totalPrice)
        {
            OrderPurchaseStrategy.ValidateOrderSummary(totalPrice);
            PlaceOrderPage.Instance.Validate().EstimatedTaxPrice(_vatTax.ToString());
        }
    }
}