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
package pageobjectadvanced;

import org.testng.annotations.AfterMethod;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;
import pageobjectadvanced.core.Driver;
import pageobjectadvanced.pages.bingmainpage.BingMainPage;

public class AdvancedBingTests {
    @BeforeMethod
    public void testInit() {
        Driver.startBrowser();
    }

    @AfterMethod
    public void testCleanup() {
        Driver.stopBrowser();
    }

    @Test
    public void searchTextInBing_when_AdvancedPageObjectPatternUsed() {
        var bingMainPage = new BingMainPage();
        bingMainPage.navigate();

        bingMainPage.search("Automate The Planet");

        bingMainPage.assertions().resultsCount(",000 Results");
    }
}
