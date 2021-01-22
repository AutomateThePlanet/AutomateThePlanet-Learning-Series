/*
 * Copyright 2021 Automate The Planet Ltd.
 * Author: Teodor Nikolov
 * Licensed under the Apache License, Version 2.0 (the "License");
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package pageobjectfluent.pages.bingmainpage;

import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.ExpectedConditions;
import pageobjectfluent.core.BasePage;
import pageobjectfluent.core.Driver;
import pageobjectfluent.enums.*;

public class BingMainPage extends BasePage<BingMainPageElements, BingMainPageAssertions> {
    @Override
    protected String getUrl() {
        return "http://www.bing.com/";
    }

    @Override
    public BingMainPage navigate() {
        super.navigate();
        return this;
    }

    @Override
    public BingMainPage navigate(String part) {
        super.navigate(part);
        return this;
    }

    public BingMainPage search(String textToType) {
        elements().searchBox().clear();
        elements().searchBox().sendKeys(textToType);
        elements().goButton().click();
        return this;
    }

    public BingMainPage clickImages() {
        elements().imagesLink().click();
        return this;
    }

    public BingMainPage clickImagesFilter() {
        elements().filterMenu().click();
        return this;
    }

    public BingMainPage setSize(Size size) {
        waitForAsyncRefresh(elements().sizes());
        elements().sizes().click();
        elements().sizesOption().get(size.ordinal()).click();
        return this;
    }

    public BingMainPage setColor(Color color) {
        waitForAsyncRefresh(elements().color());
        elements().color().click();
        elements().colorOption().get(color.ordinal()).click();
        return this;
    }

    public BingMainPage setType(Type type) {
        waitForAsyncRefresh(elements().type());
        elements().type().click();
        elements().typeOption().get(type.ordinal()).click();
        return this;
    }

    public BingMainPage setLayout(Layout layout) {
        waitForAsyncRefresh(elements().layout());
        elements().layout().click();
        elements().layoutOption().get(layout.ordinal()).click();
        return this;
    }

    public BingMainPage setPeople(People people) {
        waitForAsyncRefresh(elements().people());
        elements().people().click();
        elements().peopleOption().get(people.ordinal()).click();
        return this;
    }

    public BingMainPage setDate(Date date) {
        waitForAsyncRefresh(elements().date());
        elements().date().click();
        elements().dateOption().get(date.ordinal()).click();
        return this;
    }

    public BingMainPage setLicense(License license) {
        waitForAsyncRefresh(elements().license());
        elements().license().click();
        elements().licenseOption().get(license.ordinal()).click();
        return this;
    }

    private void waitForAsyncRefresh(WebElement element) {
        Driver.getBrowserWait().until(ExpectedConditions.elementToBeClickable(element));
        Driver.getBrowserWait().until(ExpectedConditions.invisibilityOfElementLocated(By.id("ajaxMaskLayer")));
    }
}
