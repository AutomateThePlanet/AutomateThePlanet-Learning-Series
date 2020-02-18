// <copyright file="SalesTaxOrderPurchaseStrategy.cs" company="Automate The Planet Ltd.">
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
using DecoratorDesignPattern.Pages.PlaceOrderPage;
using DecoratorDesignPattern.Services;

namespace DecoratorDesignPattern.Advanced.Strategies
{
    public class SalesTaxOrderPurchaseStrategy : OrderPurchaseStrategyDecorator
    {
        private readonly SalesTaxCalculationService _salesTaxCalculationService;
        private decimal _salesTax;        

        public SalesTaxOrderPurchaseStrategy(OrderPurchaseStrategy orderPurchaseStrategy, decimal itemsPrice, Data.ClientPurchaseInfo clientPurchaseInfo) : base(orderPurchaseStrategy, itemsPrice, clientPurchaseInfo)
        {
            _salesTaxCalculationService = new SalesTaxCalculationService();
        }

        public SalesTaxCalculationService SalesTaxCalculationService { get; set; }

        public override decimal CalculateTotalPrice()
        {
            var currentState = (Enums.States)Enum.Parse(typeof(Enums.States), ClientPurchaseInfo.ShippingInfo.State);
            _salesTax = _salesTaxCalculationService.Calculate(ItemsPrice, currentState, ClientPurchaseInfo.ShippingInfo.Zip);
            return OrderPurchaseStrategy.CalculateTotalPrice() + _salesTax;
        }

        public override void ValidateOrderSummary(decimal totalPrice)
        {
            OrderPurchaseStrategy.ValidateOrderSummary(totalPrice);
            PlaceOrderPage.Instance.Validate().EstimatedTaxPrice(_salesTax.ToString());
        }
    }
}