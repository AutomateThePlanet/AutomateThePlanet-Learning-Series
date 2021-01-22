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

package decorator.base;

import decorator.data.PurchaseInfo;
import decorator.pages.checkoutpage.CheckoutPage;
import decorator.pages.itempage.ItemPage;
import decorator.pages.shoppingcartpage.ShoppingCartPage;
import decorator.strategies.OrderPurchaseStrategy;

public class PurchaseContext {
    private final OrderPurchaseStrategy orderPurchaseStrategy;
    private final ItemPage itemPage;
    private final ShoppingCartPage shoppingCartPage;
    private final CheckoutPage checkoutPage;

    public PurchaseContext(OrderPurchaseStrategy orderPurchaseStrategy) {
        this.orderPurchaseStrategy = orderPurchaseStrategy;
        itemPage = new ItemPage();
        shoppingCartPage = new ShoppingCartPage();
        checkoutPage = new CheckoutPage();
    }

    public void purchaseItem(String itemUrl, PurchaseInfo clientPurchaseInfo) {
        itemPage.navigate(itemUrl);
        itemPage.clickBuyNowButton();
        itemPage.clickViewShoppingCartButton();
        shoppingCartPage.clickProceedToCheckoutButton();
        checkoutPage.fillBillingInfo(clientPurchaseInfo);
        var expectedTotalPrice = orderPurchaseStrategy.calculateTotalPrice();
        orderPurchaseStrategy.assertOrderSummary(expectedTotalPrice);
    }
}
