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
package decorator;

import decorator.base.PurchaseContext;
import decorator.core.Driver;
import decorator.data.PurchaseInfo;
import decorator.strategies.CouponCodeOrderPurchaseStrategy;
import decorator.strategies.OrderPurchaseStrategy;
import decorator.strategies.TotalPriceOrderPurchaseStrategy;
import decorator.strategies.VatTaxOrderPurchaseStrategy;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

public class StorePurchaseDecoratedStrategiesTests {
    @BeforeMethod
    public void testInit() {
        Driver.startBrowser();
    }

    @AfterMethod
    public void testCleanup() {
        Driver.stopBrowser();
    }

    @Test
    public void totalPriceCalculatedCorrect_when_AtCheckoutAndDecoratedStrategyPatternUsed() {
        var itemUrl = "falcon-9";
        var itemPrice = 50.00;
        var purchaseInfo = new PurchaseInfo();
        purchaseInfo.setEmail("info@berlinspaceflowers.com");
        purchaseInfo.setFirstName("Anton");
        purchaseInfo.setLastName("Angelov");
        purchaseInfo.setCompany("Space Flowers");
        purchaseInfo.setCountry("Germany");
        purchaseInfo.setAddress1("1 Willi Brandt Avenue Tiergarten");
        purchaseInfo.setAddress2("Lützowplatz 17");
        purchaseInfo.setCity("Berlin");
        purchaseInfo.setZip("10115");
        purchaseInfo.setPhone("+491888999281");
        purchaseInfo.setCouponCode("happybirthday");

        OrderPurchaseStrategy orderPurchaseStrategy = new TotalPriceOrderPurchaseStrategy(itemPrice);
        orderPurchaseStrategy = new VatTaxOrderPurchaseStrategy(orderPurchaseStrategy, itemPrice, purchaseInfo);
        orderPurchaseStrategy = new CouponCodeOrderPurchaseStrategy(orderPurchaseStrategy, itemPrice, purchaseInfo);

        new PurchaseContext(orderPurchaseStrategy).purchaseItem(itemUrl, purchaseInfo);
    }
}
