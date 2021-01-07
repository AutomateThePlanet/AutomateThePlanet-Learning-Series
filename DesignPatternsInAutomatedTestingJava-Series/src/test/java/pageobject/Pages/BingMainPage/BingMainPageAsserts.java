package pageobject.pages.bingmainpage;

import org.openqa.selenium.WebDriver;
import org.testng.Assert;

public class BingMainPageAsserts {
    private final WebDriver browser;

    public BingMainPageAsserts(WebDriver browser) {
        this.browser = browser;
    }

    protected BingMainPageElements elements() {
        return new BingMainPageElements(browser);
    }

    public void resultsCount(String expectedCount) {
        Assert.assertTrue(elements().resultsCountDiv().getText().contains(expectedCount), "The results DIV doesn't contain the specified text.");
    }
}
