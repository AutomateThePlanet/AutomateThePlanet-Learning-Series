package com.automatetheplanet._1_page_object_pattern.Pages.BingMainPage;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class BingMainPageElementMap {

    private final WebDriver _browser;

    public BingMainPageElementMap(WebDriver browser) {
        _browser = browser;
    }

    public WebElement searchBox() {
        return _browser.findElement(By.id("sb_form_q"));
    }

    public WebElement goButton() {
        return _browser.findElement(By.xpath("//label[@for='sb_form_go']"));
    }

    public WebElement resultsCountDiv() {
        return _browser.findElement(By.id("b_tween"));
    }

}
