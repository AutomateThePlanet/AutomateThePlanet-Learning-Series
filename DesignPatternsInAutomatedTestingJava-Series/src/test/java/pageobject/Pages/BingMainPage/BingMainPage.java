package pageobject.pages.bingmainpage;

import org.openqa.selenium.WebDriver;

public class BingMainPage {
    private final WebDriver browser;
    private final String url = "http://www.bing.com/";

    public BingMainPage(WebDriver browser) {
        this.browser = browser;
    }
    protected BingMainPageElements elements() {
        return new BingMainPageElements(browser);
    }

    public BingMainPageAssertions assertions() {
        return new BingMainPageAssertions(browser);
    }

    public void navigate() {
        browser.navigate().to(url);
    }

    public void search(String textToType) {
        elements().searchBox().clear();
        elements().searchBox().sendKeys(textToType);
        elements().goButton().click();
    }
}
