package com.automatetheplanet._1_page_object_pattern.Pages.BingMainPage;

import org.openqa.selenium.WebDriver;

public class BingMainPage {
    private final WebDriver _browser;
    private final String _url = "http://www.bing.com/";

    public BingMainPage(WebDriver browser) {
        _browser = browser;
    }
    protected BingMainPageElementMap map() {
        return new BingMainPageElementMap(_browser);
    }

    public BingMainPageValidator validate() {
        return new BingMainPageValidator(_browser);
    }

    public void navigate() {
        _browser.navigate().to(_url);
    }

    public void search(String textToType) {
        map().searchBox().clear();
        map().searchBox().sendKeys(textToType);
        map().goButton().click();
    }

}
