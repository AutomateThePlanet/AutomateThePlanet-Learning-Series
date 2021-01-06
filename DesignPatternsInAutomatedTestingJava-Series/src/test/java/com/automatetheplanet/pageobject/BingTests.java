package com.automatetheplanet.pageobject;

import io.github.bonigarcia.wdm.WebDriverManager;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

public class BingTests {
    public WebDriver driver;
    public WebDriverWait wait;

    @BeforeClass
    public static void classInit() {
        WebDriverManager.firefoxdriver().setup();
    }

    @BeforeMethod
    public void testInit() {
        driver = new FirefoxDriver();
        wait = new WebDriverWait(driver, 30);
    }

    @AfterMethod
    public void testCleanup() {
            driver.quit();
    }

    @Test
    public void searchTextInBing_SeleniumPageFactory() {
        // Arrange
        var bingMainPage = new com.automatetheplanet.pageobject.Selenium.Bing.Pages.BingMainPage(driver);
        bingMainPage.navigate();

        // Act
        bingMainPage.search("Automate The Planet");

        // Assert
        bingMainPage.assertsResultsCount(",000 Results");
    }

    @Test
    public void searchTextInBing_PageObjectPattern() {
        // Arrange
        var bingMainPage = new com.automatetheplanet.pageobject.Pages.BingMainPage.BingMainPage(driver);
        bingMainPage.navigate();

        // Act
        bingMainPage.search("Automate The Planet");

        // Assert
        bingMainPage.asserts().resultsCount(",000 Results");
    }
}
