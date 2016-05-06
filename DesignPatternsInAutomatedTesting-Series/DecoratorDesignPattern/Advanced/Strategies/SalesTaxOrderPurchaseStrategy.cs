// <copyright file="SalesTaxOrderPurchaseStrategy.cs" company="Automate The Planet Ltd.">
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
using DecoratorDesignPattern.Pages.PlaceOrderPage;
using DecoratorDesignPattern.Services;

namespace DecoratorDesignPattern.Advanced.Strategies
{
    public class SalesTaxOrderPurchaseStrategy : DecoratorDesignPattern.Advanced.Strategies.OrderPurchaseStrategyDecorator
    {
        private readonly SalesTaxCalculationService salesTaxCalculationService;
        private decimal salesTax;        

        public SalesTaxOrderPurchaseStrategy(DecoratorDesignPattern.Advanced.Strategies.OrderPurchaseStrategy orderPurchaseStrategy, decimal itemsPrice, DecoratorDesignPattern.Data.ClientPurchaseInfo clientPurchaseInfo) : base(orderPurchaseStrategy, itemsPrice, clientPurchaseInfo)
        {
            this.salesTaxCalculationService = new SalesTaxCalculationService();
        }

        public SalesTaxCalculationService SalesTaxCalculationService { get; set; }

        public override decimal CalculateTotalPrice()
        {
            var currentState = (DecoratorDesignPattern.Enums.States)Enum.Parse(typeof(DecoratorDesignPattern.Enums.States), this.clientPurchaseInfo.ShippingInfo.State);
            this.salesTax = this.salesTaxCalculationService.Calculate(this.itemsPrice, currentState, this.clientPurchaseInfo.ShippingInfo.Zip);
            return this.orderPurchaseStrategy.CalculateTotalPrice() + this.salesTax;
        }

        public override void ValidateOrderSummary(decimal totalPrice)
        {
            base.orderPurchaseStrategy.ValidateOrderSummary(totalPrice);
            PlaceOrderPage.Instance.Validate().EstimatedTaxPrice(this.salesTax.ToString());
        }
    }
}