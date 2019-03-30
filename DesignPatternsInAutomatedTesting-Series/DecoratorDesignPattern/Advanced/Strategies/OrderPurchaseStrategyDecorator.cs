// <copyright file="OrderPurchaseStrategyDecorator.cs" company="Automate The Planet Ltd.">
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

namespace DecoratorDesignPattern.Advanced.Strategies
{
    public abstract class OrderPurchaseStrategyDecorator : OrderPurchaseStrategy
    {
        protected readonly OrderPurchaseStrategy OrderPurchaseStrategy;
        protected readonly Data.ClientPurchaseInfo ClientPurchaseInfo;
        protected readonly decimal ItemsPrice;

        public OrderPurchaseStrategyDecorator(OrderPurchaseStrategy orderPurchaseStrategy, decimal itemsPrice, Data.ClientPurchaseInfo clientPurchaseInfo)
        {
            OrderPurchaseStrategy = orderPurchaseStrategy;
            ItemsPrice = itemsPrice;
            ClientPurchaseInfo = clientPurchaseInfo;
        }

        public override decimal CalculateTotalPrice()
        {
            ValidateOrderStrategy();

            return OrderPurchaseStrategy.CalculateTotalPrice();
        }

        public override void ValidateOrderSummary(decimal totalPrice)
        {
            ValidateOrderStrategy();
            OrderPurchaseStrategy.ValidateOrderSummary(totalPrice);
        }

        private void ValidateOrderStrategy()
        {
            if (OrderPurchaseStrategy == null)
            {
                throw new Exception("The OrderPurchaseStrategy should be first initialized.");
            }
        }
    }
}