/*
 * Copyright 2021 Automate The Planet Ltd.
 * Author: Anton Angelov
 * Licensed under the Apache License, Version 2.0 (the "License");
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
package pageobject.selenium.bing.pages;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;
import org.testng.Assert;

public class BingMainPage {
    private final WebDriver driver;
    private final String url = "http://www.bing.com/";

    @FindBy(id = "sb_form_q")
    private WebElement searchBox;

    @FindBy(xpath = "//label[@for='sb_form_go']")
    private WebElement goButton;

    @FindBy(id = "b_tween")
    private WebElement resultsCountDiv;

    public BingMainPage(WebDriver browser) {
        driver = browser;
        PageFactory.initElements(browser, this);
    }

    public void navigate() {
        driver.navigate().to(url);
    }

    public void search(String textToType) {
        searchBox.clear();
        searchBox.sendKeys(textToType);
        goButton.click();
    }

    public void assertResultsCount(String expectedCount) {
        Assert.assertTrue(resultsCountDiv.getText().contains(expectedCount), "The results DIV doesn't contain the specified text.");
    }
}
