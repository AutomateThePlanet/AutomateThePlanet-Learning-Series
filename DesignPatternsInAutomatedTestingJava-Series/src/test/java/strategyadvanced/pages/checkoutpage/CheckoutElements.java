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
package strategyadvanced.pages.checkoutpage;

import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.ExpectedConditions;
import strategyadvanced.core.BaseElements;

public class CheckoutElements extends BaseElements {

    public WebElement billingFirstName() {
        return browser.findElement(By.id("billing_first_name"));
    }

    public WebElement billingLastName() {
        return browser.findElement(By.id("billing_last_name"));
    }

    public WebElement billingCompany() {
        return browser.findElement(By.id("billing_company"));
    }

    public WebElement billingCountryWrapper() {
        return browser.findElement(By.id("select2-billing_country-container"));
    }

    public WebElement billingCountryFilter() {
        return browser.findElement(By.className("select2-search__field"));
    }

    public WebElement billingAddress1() {
        return browser.findElement(By.id("billing_address_1"));
    }

    public WebElement billingAddress2() {
        return browser.findElement(By.id("billing_address_2"));
    }

    public WebElement billingCity() {
        return browser.findElement(By.id("billing_city"));
    }

    public WebElement billingZip() {
        return browser.findElement(By.id("billing_postcode"));
    }

    public WebElement billingPhone() {
        return browser.findElement(By.id("billing_phone"));
    }

    public WebElement billingEmail() {
        return browser.findElement(By.id("billing_email"));
    }

    public WebElement couponCodeShowInputButton() {
        return browser.findElement(By.className("showcoupon"));
    }

    public WebElement couponCodeInput() {
        return browser.findElement(By.id("coupon_code"));
    }

    public WebElement couponCodeApplyButton() {
        return browser.findElement(By.name("apply_coupon"));
    }

    public WebElement createAccountCheckBox() {
        return browser.findElement(By.id("createaccount"));
    }

    public WebElement checkPaymentsRadioButton() {
        return browser.findElement(By.cssSelector("[for*='payment_method_cheque']"));
    }

    public WebElement placeOrderButton() {
        return browser.findElement(By.id("place_order"));
    }

    public WebElement receivedMessage() {
        browserWait.until(ExpectedConditions.presenceOfElementLocated(By.tagName("h1")));
        return browser.findElement(By.tagName("h1"));
    }

    public WebElement orderDetailsSubtotal() {
        String locator = "//th[text()='Subtotal:']/following-sibling::td/span";
        browserWait.until(ExpectedConditions.presenceOfElementLocated(By.xpath(locator)));
        return browser.findElement(By.xpath(locator));
    }

    public WebElement orderDetailsDiscount() {
        String locator = "//th[text()='Discount:']/following-sibling::td/span";
        browserWait.until(ExpectedConditions.presenceOfElementLocated(By.xpath(locator)));
        return browser.findElement(By.xpath(locator));
    }

    public WebElement orderDetailsVatTax() {
        String locator = "//th[text()='VAT:']/following-sibling::td/span";
        browserWait.until(ExpectedConditions.presenceOfElementLocated(By.xpath(locator)));
        return browser.findElement(By.xpath(locator));
    }

    public WebElement orderDetailsPaymentMethod() {
        String locator = "//th[text()='Payment method:']/following-sibling::td/span";
        browserWait.until(ExpectedConditions.presenceOfElementLocated(By.xpath(locator)));
        return browser.findElement(By.xpath(locator));
    }

    public WebElement orderDetailsNote() {
        String locator = "//th[text()='Note:']/following-sibling::td/span";
        browserWait.until(ExpectedConditions.presenceOfElementLocated(By.xpath(locator)));
        return browser.findElement(By.xpath(locator));
    }

    public WebElement orderDetailsTotal() {
        String locator = "//th[text()='Total:']/following-sibling::td/span";
        browserWait.until(ExpectedConditions.presenceOfElementLocated(By.xpath(locator)));
        return browser.findElement(By.xpath(locator));
    }

    public WebElement getCountryOptionByName(String countryName) {
        return browser.findElement(By.xpath(String.format("//li[contains(text(),'%s')]", countryName)));
    }
}
