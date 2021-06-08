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

public class CheckoutPageAssertions {
    private final WebDriver browser;

    public CheckoutPageAssertions(WebDriver browser) {
        this.browser = browser;
    }

    protected CheckoutPageElements elements() {
        return new CheckoutPageElements(browser);
    }

    public void formSent() {
        Assertions.assertTrue(browser.getCurrentUrl().contains("paymentMethod=on"), "Form not sent");
    }

    public void validatedFirstName() {
        Assertions.assertTrue(elements().firstNameValidation().isDisplayed());
    }

    public void validatedLastName() {
        Assertions.assertTrue(elements().lastNameValidation().isDisplayed());
    }

    public void validatedUsername() {
        Assertions.assertTrue(elements().usernameValidation().isDisplayed());
    }

    public void validatedEmail() {
        Assertions.assertTrue(elements().emailValidation().isDisplayed());
    }

    public void validatedAddress1() {
        Assertions.assertTrue(elements().address1Validation().isDisplayed());
    }

    public void validatedCountry() {
        Assertions.assertTrue(elements().countryValidation().isDisplayed());
    }

    public void validatedState() {
        Assertions.assertTrue(elements().stateValidation().isDisplayed());
    }

    public void validatedZip() {
        Assertions.assertTrue(elements().zipValidation().isDisplayed());
    }

    public void validatedCardName() {
        Assertions.assertTrue(elements().cardNameValidation().isDisplayed());
    }

    public void validatedCardNumber() {
        Assertions.assertTrue(elements().cardNumberValidation().isDisplayed());
    }

    public void validatedCardExpiration() {
        Assertions.assertTrue(elements().cardExpirationValidation().isDisplayed());
    }

    public void validatedCardCVV() {
        Assertions.assertTrue(elements().cardCVVValidation().isDisplayed());
    }
}
