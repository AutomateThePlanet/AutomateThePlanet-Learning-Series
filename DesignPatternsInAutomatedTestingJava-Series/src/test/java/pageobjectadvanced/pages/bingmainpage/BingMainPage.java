package pageobjectadvanced.pages.bingmainpage;

import pageobjectadvanced.core.BasePage;

public class BingMainPage extends BasePage<BingMainPageElements, BingMainPageAssertions> {
    public BingMainPage() {
        super("http://www.bing.com/");
    }

    public void search(String textToType) {
        elements().searchBox().clear();
        elements().searchBox().sendKeys(textToType);
        elements().goButton().click();
    }
}
