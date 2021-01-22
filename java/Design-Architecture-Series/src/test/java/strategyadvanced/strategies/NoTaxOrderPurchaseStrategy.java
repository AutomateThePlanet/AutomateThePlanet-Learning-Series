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

package strategyadvanced.strategies;

import strategyadvanced.base.OrderPurchaseStrategy;
import strategyadvanced.core.Driver;
import strategyadvanced.data.PurchaseInfo;
import strategyadvanced.pages.checkoutpage.CheckoutPage;

public class NoTaxOrderPurchaseStrategy implements OrderPurchaseStrategy {
    @Override
    public void assertOrderSummary(double itemPrice, PurchaseInfo purchaseInfo) {
        var checkoutPage = new CheckoutPage();
        Driver.waitForAjax();
        Driver.waitUntilPageLoadsCompletely();
        checkoutPage.assertions().assertOrderVatTaxPrice(0);
    }

    @Override
    public void validatePurchaseInfo(PurchaseInfo purchaseInfo) {
        // Throw a new IllegalArgumentException if the country is part of the EU Union.
    }
}
