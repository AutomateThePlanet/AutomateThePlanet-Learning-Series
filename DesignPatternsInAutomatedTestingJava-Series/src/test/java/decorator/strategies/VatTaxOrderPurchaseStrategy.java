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

import decorator.data.PurchaseInfo;
import decorator.enums.Country;
import decorator.services.CouponCodeCalculationService;
import decorator.services.VatTaxCalculationService;

import java.util.Arrays;

public class VatTaxOrderPurchaseStrategy extends OrderPurchaseStrategyDecorator {
    private final VatTaxCalculationService vatTaxCalculationService;
    private final CouponCodeCalculationService couponCodeCalculationService;
    private double vatTax;

    public VatTaxOrderPurchaseStrategy(OrderPurchaseStrategy orderPurchaseStrategy, double itemPrice, PurchaseInfo purchaseInfo) {
        super(orderPurchaseStrategy, itemPrice, purchaseInfo);
        vatTaxCalculationService = new VatTaxCalculationService();
        couponCodeCalculationService = new CouponCodeCalculationService();
    }

    @Override
    public double calculateTotalPrice() {
        var currentCountry = Arrays.stream(Country.values())
                .filter(country -> country.toString().equals(purchaseInfo.getCountry()))
                .toArray(Country[]::new)[0];
        vatTax = vatTaxCalculationService.calculate((itemPrice - couponCodeCalculationService.calculate(itemPrice, purchaseInfo.getCouponCode())), currentCountry);
        return orderPurchaseStrategy.calculateTotalPrice() + vatTax;
    }
}
