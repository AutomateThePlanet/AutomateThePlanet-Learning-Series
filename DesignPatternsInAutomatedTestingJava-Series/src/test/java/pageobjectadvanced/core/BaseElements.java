package pageobjectadvanced.core;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.WebDriverWait;

public class BaseElements {
    protected WebDriver browser;
    protected WebDriverWait browserWait;

    public BaseElements() {
        browser = Driver.getBrowser();
        browserWait = Driver.getBrowserWait();
    }

    public void switchToDefault() {
        browser.switchTo().defaultContent();
    }
}
