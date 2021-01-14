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
package strategyadvanced.strategies;

import strategyadvanced.base.OrderPurchaseStrategy;
import strategyadvanced.core.Driver;
import strategyadvanced.data.PurchaseInfo;
import strategyadvanced.pages.checkoutpage.CheckoutPage;
import strategyadvanced.services.CouponCodeCalculationService;

public class CouponCodeOrderPurchaseStrategy implements OrderPurchaseStrategy {
    private final CouponCodeCalculationService couponCodeCalculationService;

    public CouponCodeOrderPurchaseStrategy() {
        couponCodeCalculationService = new CouponCodeCalculationService();
    }

    @Override
    public void assertOrderSummary(double itemPrice, PurchaseInfo purchaseInfo) {
        var discount = couponCodeCalculationService.calculate(itemPrice, purchaseInfo.getCouponCode());

        var checkoutPage = new CheckoutPage();
        Driver.waitForAjax();
        Driver.waitUntilPageLoadsCompletely();
        checkoutPage.assertions().assertOrderDiscountPrice(discount);
    }

    @Override
    public void validatePurchaseInfo(PurchaseInfo purchaseInfo) {
        if (purchaseInfo.getCouponCode() == null) {
            throw new IllegalArgumentException("A coupon code should be set if the CouponCodeOrderPurchaseStrategy is executed");
        }
    }
}
