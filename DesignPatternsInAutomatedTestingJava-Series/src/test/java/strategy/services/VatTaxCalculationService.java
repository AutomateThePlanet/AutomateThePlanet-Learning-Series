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
package strategy.services;

import strategy.data.PurchaseInfo;
import strategy.enums.Country;

public class VatTaxCalculationService {
    private double taxValue;

    public double calculate(double price, Country country, PurchaseInfo purchaseInfo) {
        switch(country) {
            case BULGARIA:
            case UNITED_KINGDOM:
            case GERMANY:
            case AUSTRIA:
            case FRANCE:
                taxValue = calculateVATInternal(price, 20, purchaseInfo);
                break;
            default:
                taxValue = 0;
                break;
        }

        return taxValue;
    }

    private static double calculateVATInternal(double price, double percent, PurchaseInfo purchaseInfo) {
        var couponCodeCalculationService = new CouponCodeCalculationService();
        var taxValue = (price - couponCodeCalculationService.calculate(price, purchaseInfo.getCouponCode())) * percent / 100;
        return taxValue;
    }
}
