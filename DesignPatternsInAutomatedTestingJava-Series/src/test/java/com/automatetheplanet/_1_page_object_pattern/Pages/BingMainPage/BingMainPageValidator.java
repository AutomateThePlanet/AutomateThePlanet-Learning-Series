package com.automatetheplanet._1_page_object_pattern.Pages.BingMainPage;

import org.openqa.selenium.WebDriver;
import org.testng.Assert;

public class BingMainPageValidator {

    private final WebDriver _browser;

    public BingMainPageValidator(WebDriver browser) {
        _browser = browser;
    }

    protected BingMainPageElementMap map() {
        return new BingMainPageElementMap(_browser);
    }

    public void resultsCount(String expectedCount) {
        Assert.assertTrue(map().resultsCountDiv().getText().contains(expectedCount), "The results DIV doesn't contains the specified text.");
    }
}
