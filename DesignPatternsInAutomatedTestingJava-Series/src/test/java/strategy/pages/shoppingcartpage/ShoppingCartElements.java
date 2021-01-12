package strategy.pages.shoppingcartpage;

import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;
import strategy.core.BaseElements;

public class ShoppingCartElements extends BaseElements {
    public WebElement proceedToCheckoutButton() {
        return browser.findElement(By.xpath("//a[contains(@class,'checkout-button')]"));
    }
}
