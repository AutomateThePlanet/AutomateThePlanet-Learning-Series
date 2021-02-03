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
import strategyadvanced.enums.Country;
import strategyadvanced.pages.checkoutpage.CheckoutPage;
import strategyadvanced.services.VatTaxCalculationService;

import java.util.Arrays;

public class VatTaxOrderPurchaseStrategy implements OrderPurchaseStrategy {
    private final VatTaxCalculationService vatTaxCalculationService;

    public VatTaxOrderPurchaseStrategy() {
        vatTaxCalculationService = new VatTaxCalculationService();
    }

    @Override
    public void assertOrderSummary(double itemPrice, PurchaseInfo purchaseInfo) {
        var currentCountry = Arrays.stream(Country.values())
                .filter(country -> country.toString().equals(purchaseInfo.getCountry()))
                .toArray(Country[]::new)[0];
        var vatTax = vatTaxCalculationService.calculate(itemPrice, currentCountry, purchaseInfo);

        var checkoutPage = new CheckoutPage();
        Driver.waitForAjax();
        Driver.waitUntilPageLoadsCompletely();
        checkoutPage.assertions().assertOrderVatTaxPrice(vatTax);
    }

    @Override
    public void validatePurchaseInfo(PurchaseInfo purchaseInfo) {
        // Throw a new IllegalArgumentException if the country is not part of the EU Union.
    }
}
