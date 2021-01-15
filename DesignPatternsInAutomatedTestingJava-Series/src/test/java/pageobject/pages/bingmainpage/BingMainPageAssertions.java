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
package pageobject.pages.bingmainpage;

import org.openqa.selenium.WebDriver;
import org.testng.Assert;

public class BingMainPageAssertions {
    private final WebDriver browser;

    public BingMainPageAssertions(WebDriver browser) {
        this.browser = browser;
    }

    protected BingMainPageElements elements() {
        return new BingMainPageElements(browser);
    }

    public void resultsCount(String expectedCount) {
        Assert.assertTrue(elements().resultsCountDiv().getText().contains(expectedCount), "The results DIV doesn't contain the specified text.");
    }
}
