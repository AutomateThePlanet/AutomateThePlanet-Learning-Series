package decorator.core;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.Wait;

public abstract class BaseElements {
    protected WebDriver browser;
    protected Wait<WebDriver> browserWait;

    public BaseElements() {
        browser = Driver.getBrowser();
        browserWait = Driver.getBrowserWait();
    }

    public void switchToDefault() {
        browser.switchTo().defaultContent();
    }
}
