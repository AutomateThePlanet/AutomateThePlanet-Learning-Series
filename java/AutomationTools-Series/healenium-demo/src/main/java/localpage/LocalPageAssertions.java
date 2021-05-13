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

package localpage;

import org.junit.jupiter.api.Assertions;
import org.openqa.selenium.WebDriver;

public class LocalPageAssertions {
    private final WebDriver browser;

    public LocalPageAssertions(WebDriver browser) {
        this.browser = browser;
    }

    protected LocalPageElements elements() {
        return new LocalPageElements(browser);
    }

    public void formSent() {
        Assertions.assertTrue(browser.getCurrentUrl().contains("paymentMethod=on"), "Form not sent");
    }
}
