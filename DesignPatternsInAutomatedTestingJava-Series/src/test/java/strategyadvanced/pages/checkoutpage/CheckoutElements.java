package strategyadvanced.pages.checkoutpage;

import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;
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
        return browser.findElement(By.tagName("h1"));
    }

    public WebElement orderDetailsSubtotal() {
        return browser.findElement(By.xpath("//th[text()='Subtotal:']/following-sibling::td/span"));
    }

    public WebElement orderDetailsDiscount() {
        return browser.findElement(By.xpath("//th[text()='Discount:']/following-sibling::td/span"));
    }

    public WebElement orderDetailsVatTax() {
        return browser.findElement(By.xpath("//th[text()='VAT:']/following-sibling::td/span"));
    }

    public WebElement orderDetailsPaymentMethod() {
        return browser.findElement(By.xpath("//th[text()='Payment method:']/following-sibling::td/span"));
    }

    public WebElement orderDetailsNote() {
        return browser.findElement(By.xpath("//th[text()='Note:']/following-sibling::td/span"));
    }

    public WebElement orderDetailsTotal() {
        return browser.findElement(By.xpath("//th[text()='Total:']/following-sibling::td/span"));
    }

    public WebElement getCountryOptionByName(String countryName) {
        return browser.findElement(By.xpath(String.format("//li[contains(text(),'%s')]", countryName)));
    }
}
