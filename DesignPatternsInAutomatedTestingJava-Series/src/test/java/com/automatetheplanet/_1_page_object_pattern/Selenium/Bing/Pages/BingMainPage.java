package com.automatetheplanet._1_page_object_pattern.Selenium.Bing.Pages;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;
import org.testng.Assert;

public class BingMainPage {
    private final WebDriver driver;
    private final String url = "http://www.bing.com/";

    public BingMainPage(WebDriver browser) {
        driver = browser;
        PageFactory.initElements(browser, this);
    }

    @FindBy(id = "sb_form_q")
    public WebElement searchBox;

    @FindBy(xpath = "//label[@for='sb_form_go']")
    public WebElement goButton;

    @FindBy(id = "b_tween")
    public WebElement resultsCountDiv;

    public void navigate() {
        driver.navigate().to(url);
    }

    public void search(String textToType) {
        searchBox.clear();
        searchBox.sendKeys(textToType);
        goButton.click();
    }

    public void assertsResultsCount(String expectedCount) {
        Assert.assertTrue(resultsCountDiv.getText().contains(expectedCount), "The results DIV doesn't contain the specified text.");
    }
}
