package pageobjectadvanced.pages.bingmainpage;

import pageobjectadvanced.BasePageElements;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;

public class BingMainPageElements extends BasePageElements {
    public WebElement searchBox() {
        return browser.findElement(By.id("sb_form_q"));
    }

    public WebElement goButton() {
        return browser.findElement(By.xpath("//label[@for='sb_form_go']"));
    }

    public WebElement resultsCountDiv() {
        return browser.findElement(By.id("b_tween"));
    }
}
