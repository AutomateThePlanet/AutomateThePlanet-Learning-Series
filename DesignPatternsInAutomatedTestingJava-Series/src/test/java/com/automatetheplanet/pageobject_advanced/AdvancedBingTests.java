package com.automatetheplanet.pageobject_advanced;

import com.automatetheplanet.pageobject_advanced.Pages.BingMainPage.BingMainPage;
import io.github.bonigarcia.wdm.WebDriverManager;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

public class AdvancedBingTests {
    @BeforeClass
    public static void classInit() {
        WebDriverManager.firefoxdriver().setup();
    }

    @BeforeMethod
    public void testInit() {
        Driver.startBrowser();
    }

    @AfterMethod
    public void testCleanup() {
        Driver.stopBrowser();
    }

    @Test
    public void searchTextInBing_Advanced_PageObjectPattern() {
        // Arrange
        var bingMainPage = new BingMainPage();
        bingMainPage.navigate();

        // Act
        bingMainPage.search("Automate The Planet");

        // Assert
        bingMainPage.asserts().resultsCount(",000 Results");
    }
}
