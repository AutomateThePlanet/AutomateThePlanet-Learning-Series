package pageobject;

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
    public void searchTextInBing_UsingSeleniumPageFactory() {
        var bingMainPage = new pageobject.selenium.bing.pages.BingMainPage(driver);
        bingMainPage.navigate();

        bingMainPage.search("Automate The Planet");

        bingMainPage.assertsResultsCount(",000 Results");
    }

    @Test
    public void searchTextInBing_WithoutSeleniumPageFactory() {
        var bingMainPage = new pageobject.pages.bingmainpage.BingMainPage(driver);
        bingMainPage.navigate();

        bingMainPage.search("Automate The Planet");

        bingMainPage.assertions().resultsCount(",000 Results");
    }
}
