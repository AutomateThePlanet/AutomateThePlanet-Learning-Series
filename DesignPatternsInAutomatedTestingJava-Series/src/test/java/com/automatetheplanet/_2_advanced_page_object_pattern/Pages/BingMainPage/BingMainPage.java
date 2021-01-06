package com.automatetheplanet._2_advanced_page_object_pattern.Pages.BingMainPage;

import com.automatetheplanet._2_advanced_page_object_pattern.BasePage;

public class BingMainPage extends BasePage<BingMainPageElements, BingMainPageAsserts> {
    public BingMainPage() {
        super("http://www.bing.com/");
    }

    @Override
    protected BingMainPageElements elements() {
        return new BingMainPageElements();
    }

    @Override
    public BingMainPageAsserts validate() {
        return new BingMainPageAsserts();
    }

    public void search(String textToType) {
        elements().searchBox().clear();
        elements().searchBox().sendKeys(textToType);
        elements().goButton().click();
    }

}
