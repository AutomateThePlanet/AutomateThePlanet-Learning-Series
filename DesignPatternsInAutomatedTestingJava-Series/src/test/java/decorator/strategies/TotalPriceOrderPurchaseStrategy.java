/*
 * Copyright 2021 Automate The Planet Ltd.
 * Author: Teodor Nikolov
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

import decorator.core.Driver;
import decorator.pages.checkoutpage.CheckoutPage;

public class TotalPriceOrderPurchaseStrategy extends OrderPurchaseStrategy {
    private final double itemsPrice;

    public TotalPriceOrderPurchaseStrategy(double itemsPrice) {
        this.itemsPrice = itemsPrice;
    }

    @Override
    public double calculateTotalPrice() {
        return itemsPrice;
    }

    @Override
    public void assertOrderSummary(double totalPrice) {
        var checkoutPage = new CheckoutPage();
        Driver.waitForAjax();
        Driver.waitUntilPageLoadsCompletely();
        checkoutPage.assertions().assertOrderTotalPrice(totalPrice);
    }
}
