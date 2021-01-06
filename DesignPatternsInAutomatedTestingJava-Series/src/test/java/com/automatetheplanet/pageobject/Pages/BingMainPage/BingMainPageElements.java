package com.automatetheplanet.pageobject.Pages.BingMainPage;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class BingMainPageElements {

    private final WebDriver browser;

    public BingMainPageElements(WebDriver browser) {
        this.browser = browser;
    }

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
