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
package facade;

import facade.core.Driver;
import facade.data.PurchaseInfo;
import facade.pages.bellatrixdemoscheckoutpage.BellatrixDemoCheckoutPage;
import facade.pages.bellatrixdemositempage.BellatrixDemoItemPage;
import facade.pages.bellatrixdemosshoppingcartpage.BellatrixDemoShoppingCartPage;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

public class StorePurchaseWithoutFacadeTests {
    @BeforeMethod
    public void testInit() {
        Driver.startBrowser();
    }

    @AfterMethod
    public void testCleanup() {
        Driver.stopBrowser();
    }

    @Test
    public void subtotalPriceOfFalcon9CalculatedCorrect_when_NoFacadePatternUsed() {
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
        var itemPage = new BellatrixDemoItemPage();
        var shoppingCartPage = new BellatrixDemoShoppingCartPage();
        var checkoutPage = new BellatrixDemoCheckoutPage();

        itemPage.navigate(itemUrl);
        itemPage.assertions().assertProductPrice(itemPrice);
        itemPage.clickBuyNowButton();
        itemPage.clickViewShoppingCartButton();
        shoppingCartPage.clickProceedToCheckoutButton();
        shoppingCartPage.assertions().assertShoppingCartSubtotalPrice(itemPrice);
        checkoutPage.fillBillingInfo(purchaseInfo);
        checkoutPage.assertions().assertOrderSubtotalPrice(itemPrice);
    }

    @Test
    public void subtotalPriceOfSaturnVCalculatedCorrect_when_NoFacadePatternUsed() {
        var itemUrl = "saturn-v";
        var itemPrice = 120.00;
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
        var itemPage = new BellatrixDemoItemPage();
        var shoppingCartPage = new BellatrixDemoShoppingCartPage();
        var checkoutPage = new BellatrixDemoCheckoutPage();

        itemPage.navigate(itemUrl);
        itemPage.assertions().assertProductPrice(itemPrice);
        itemPage.clickBuyNowButton();
        itemPage.clickViewShoppingCartButton();
        shoppingCartPage.clickProceedToCheckoutButton();
        shoppingCartPage.assertions().assertShoppingCartSubtotalPrice(itemPrice);
        checkoutPage.fillBillingInfo(purchaseInfo);
        checkoutPage.assertions().assertOrderSubtotalPrice(itemPrice);
    }
}
