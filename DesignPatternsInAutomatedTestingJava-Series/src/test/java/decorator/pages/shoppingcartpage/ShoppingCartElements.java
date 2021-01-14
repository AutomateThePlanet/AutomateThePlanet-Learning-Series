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
package decorator.pages.shoppingcartpage;

import decorator.core.BaseElements;
import decorator.core.Driver;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.ExpectedConditions;

public class ShoppingCartElements extends BaseElements {
    public WebElement proceedToCheckoutButton() {
        String locator = "//a[contains(@class,'checkout-button')]";
        Driver.getBrowserWait().until(ExpectedConditions.presenceOfElementLocated(By.xpath(locator)));
        return browser.findElement(By.xpath(locator));
    }

    public WebElement shoppingCartSubtotalPrice() {
        String locator = "//tr[@class='cart-subtotal']//bdi";
        Driver.getBrowserWait().until(ExpectedConditions.presenceOfElementLocated(By.xpath(locator)));
        return browser.findElement(By.xpath(locator));
    }
}
