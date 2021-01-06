package com.automatetheplanet._2_advanced_page_object_pattern;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.WebDriverWait;

public abstract class BasePageElements {
    protected WebDriver browser;
    protected WebDriverWait browserWait;

    public BasePageElements() {
        browser = Driver.getBrowser();
        browserWait = Driver.getBrowserWait();
    }

    public void switchToDefault() {
        browser.switchTo().defaultContent();
    }
}
