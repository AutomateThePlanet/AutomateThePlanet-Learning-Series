package strategy.pages.itempage;

import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;
import strategy.core.BaseElements;

public class ItemElements extends BaseElements {
    public WebElement addToCartButton() {
        return browser.findElement(By.name("add-to-cart"));
    }

    public WebElement productTitle() {
        return browser.findElement(By.tagName("h1"));
    }

    public WebElement viewShoppingCartButton() {
        return browser.findElement(By.className("cart-contents"));
    }
}
