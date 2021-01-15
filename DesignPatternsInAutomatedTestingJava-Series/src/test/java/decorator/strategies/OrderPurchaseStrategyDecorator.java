/*
 * Copyright 2021 Automate The Planet Ltd.
 * Author: Anton Angelov
 * Licensed under the Apache License, Version 2.0 (the "License");
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
package decorator.strategies;

import decorator.data.PurchaseInfo;

public class OrderPurchaseStrategyDecorator extends OrderPurchaseStrategy {
    protected final OrderPurchaseStrategy orderPurchaseStrategy;
    protected final PurchaseInfo purchaseInfo;
    protected final double itemPrice;

    public OrderPurchaseStrategyDecorator(OrderPurchaseStrategy orderPurchaseStrategy, double itemPrice, PurchaseInfo purchaseInfo) {
        this.orderPurchaseStrategy = orderPurchaseStrategy;
        this.itemPrice = itemPrice;
        this.purchaseInfo = purchaseInfo;
    }

    @Override
    public double calculateTotalPrice() {
        validateOrderStrategy();
        return orderPurchaseStrategy.calculateTotalPrice();
    }

    @Override
    public void assertOrderSummary(double totalPrice) {
        validateOrderStrategy();
        orderPurchaseStrategy.assertOrderSummary(totalPrice);
    }

    private void validateOrderStrategy() {
        if (orderPurchaseStrategy == null) {
            throw new NullPointerException("The OrderPurchaseStrategy should be first initialized.");
        }
    }
}
