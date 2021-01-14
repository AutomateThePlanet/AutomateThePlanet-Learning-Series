package facade.pages.bellatrixdemosshoppingcartpage;

import facade.core.BaseElements;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;

public class BellatrixDemoShoppingCartElements extends BaseElements {
    public WebElement proceedToCheckoutButton() {
        return browser.findElement(By.xpath("//a[contains(@class,'checkout-button')]"));
    }

    public WebElement shoppingCartSubtotalPrice() {
        return browser.findElement(By.xpath("//tr[@class='cart-subtotal']//bdi"));
    }
}
