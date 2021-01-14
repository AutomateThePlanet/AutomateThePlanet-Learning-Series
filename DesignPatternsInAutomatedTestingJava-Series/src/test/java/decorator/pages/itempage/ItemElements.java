package decorator.pages.itempage;

import decorator.core.BaseElements;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;

public class ItemElements extends BaseElements {
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
