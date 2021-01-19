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

package strategyadvanced.base;

import strategyadvanced.data.PurchaseInfo;
import strategyadvanced.pages.checkoutpage.CheckoutPage;
import strategyadvanced.pages.itempage.ItemPage;
import strategyadvanced.pages.shoppingcartpage.ShoppingCartPage;

public class PurchaseContext {
    private final OrderPurchaseStrategy[] orderPurchaseStrategies;
    private final ItemPage itemPage;
    private final ShoppingCartPage shoppingCartPage;
    private final CheckoutPage checkoutPage;

    public PurchaseContext(OrderPurchaseStrategy... orderPurchaseStrategies) {
        this.orderPurchaseStrategies = orderPurchaseStrategies;
        itemPage = new ItemPage();
        shoppingCartPage = new ShoppingCartPage();
        checkoutPage = new CheckoutPage();
    }

    public void purchaseItem(String itemUrl, double itemPrice, PurchaseInfo purchaseInfo) {
        validatePurchaseInfo(purchaseInfo);
        itemPage.navigate(itemUrl);
        itemPage.clickBuyNowButton();
        itemPage.clickViewShoppingCartButton();
        shoppingCartPage.clickProceedToCheckoutButton();
        checkoutPage.fillBillingInfo(purchaseInfo);

        validateOrderSummary(itemPrice, purchaseInfo);
    }

    public void validatePurchaseInfo(PurchaseInfo purchaseInfo) {
        for (var currentStrategy : orderPurchaseStrategies) {
            currentStrategy.validatePurchaseInfo(purchaseInfo);
        }
    }

    public void validateOrderSummary(double itemPrice, PurchaseInfo purchaseInfo) {
        for (var currentStrategy : orderPurchaseStrategies) {
            currentStrategy.assertOrderSummary(itemPrice, purchaseInfo);
        }
    }
}
