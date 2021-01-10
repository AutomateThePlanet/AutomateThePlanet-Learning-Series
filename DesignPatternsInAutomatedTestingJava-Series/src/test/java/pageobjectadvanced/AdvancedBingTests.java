package pageobjectadvanced;

import pageobjectadvanced.core.Driver;
import pageobjectadvanced.pages.bingmainpage.BingMainPage;
import io.github.bonigarcia.wdm.WebDriverManager;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

import java.lang.reflect.InvocationTargetException;

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
        var bingMainPage = new BingMainPage();
        bingMainPage.navigate();

        bingMainPage.search("Automate The Planet");

        bingMainPage.assertions().resultsCount(",000 Results");
    }
}
