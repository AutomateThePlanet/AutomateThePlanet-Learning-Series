package decorator.pages.shoppingcartpage;

import decorator.core.BaseElements;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;

public class ShoppingCartElements extends BaseElements {
    public WebElement proceedToCheckoutButton() {
        return browser.findElement(By.xpath("//a[contains(@class,'checkout-button')]"));
    }
}
