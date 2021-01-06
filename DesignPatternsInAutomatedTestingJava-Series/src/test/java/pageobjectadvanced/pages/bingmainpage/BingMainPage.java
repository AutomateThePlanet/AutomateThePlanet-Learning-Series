package pageobjectadvanced.pages.bingmainpage;

import pageobjectadvanced.BasePage;

public class BingMainPage extends BasePage<BingMainPageElements, BingMainPageAsserts> {
    public BingMainPage() {
        super("http://www.bing.com/");
    }

    @Override
    protected BingMainPageElements elements() {
        return new BingMainPageElements();
    }

    @Override
    public BingMainPageAsserts asserts() {
        return new BingMainPageAsserts();
    }

    public void search(String textToType) {
        elements().searchBox().clear();
        elements().searchBox().sendKeys(textToType);
        elements().goButton().click();
    }

}
