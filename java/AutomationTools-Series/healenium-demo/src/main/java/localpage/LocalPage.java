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

import org.openqa.selenium.WebDriver;

import java.net.URL;

public class LocalPage {
    private final WebDriver driver;
    private final URL url = getClass().getClassLoader().getResource("checkout/index.html");

    public LocalPage(WebDriver driver) {
        this.driver = driver;
    }

    protected LocalPageElements elements() {
        return new LocalPageElements(driver);
    }

    public LocalPageAssertions assertions() {
        return new LocalPageAssertions(driver);
    }

    public void navigate() {
        driver.navigate().to(url);
    }

    public void fillInfo(ClientInfo clientInfo) {
        elements().firstName().sendKeys(clientInfo.getFirstName());
        elements().lastName().sendKeys(clientInfo.getLastName());
        elements().username().sendKeys(clientInfo.getUsername());
        elements().email().sendKeys(clientInfo.getEmail());
        elements().address1().sendKeys(clientInfo.getAddress1());
        elements().address2().sendKeys(clientInfo.getAddress2());
        elements().country().selectByIndex(clientInfo.getCountry());
        elements().state().selectByIndex(clientInfo.getState());
        elements().zip().sendKeys(clientInfo.getZip());
        elements().cardName().sendKeys(clientInfo.getCardName());
        elements().cardNumber().sendKeys(clientInfo.getCardNumber());
        elements().cardExpiration().sendKeys(clientInfo.getCardExpiration());
        elements().cardCVV().sendKeys(clientInfo.getCardCVV());

        elements().submitButton().click();
    }
}
