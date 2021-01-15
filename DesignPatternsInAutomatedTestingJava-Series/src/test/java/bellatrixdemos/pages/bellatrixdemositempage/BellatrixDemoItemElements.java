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
package bellatrixdemos.pages.bellatrixdemositempage;

import bellatrixdemos.core.BaseElements;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;

public class BellatrixDemoItemElements extends BaseElements {
    public WebElement addToCartButton() {
        return browser.findElement(By.name("add-to-cart"));
    }

    public WebElement productTitle() {
        return browser.findElement(By.tagName("h1"));
    }

    public WebElement productPrice() {
        return browser.findElement(By.xpath("//div[@class='summary entry-summary']//ins//bdi"));
    }

    public WebElement viewShoppingCartButton() {
        return browser.findElement(By.className("cart-contents"));
    }
}
