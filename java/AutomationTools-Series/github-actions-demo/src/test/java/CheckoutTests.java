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

import io.github.bonigarcia.wdm.WebDriverManager;
import localpage.ClientInfo;
import localpage.CheckoutPage;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;

public class CheckoutTests {
    public WebDriver driver;
    public CheckoutPage page;

    @BeforeAll
    public static void classInit() {
        WebDriverManager.chromedriver().setup();
    }

    @BeforeEach
    public void testInit() {
        ChromeOptions options = new ChromeOptions();

        options.addArguments("--headless");

        driver = new ChromeDriver(options);
        page = new CheckoutPage(driver);

        page.navigate();
    }

    @AfterEach
    public void testCleanup() {
        driver.quit();
    }


    @Test
    public void formSent_When_InfoValid() {
        var clientInfo = new ClientInfo();
        clientInfo.setFirstName("Anton");
        clientInfo.setLastName("Angelov");
        clientInfo.setUsername("aangelov");
        clientInfo.setEmail("info@berlinspaceflowers.com");
        clientInfo.setAddress1("1 Willi Brandt Avenue Tiergarten");
        clientInfo.setAddress2("Lützowplatz 17");
        clientInfo.setCountry(1);
        clientInfo.setState(1);
        clientInfo.setZip("10115");
        clientInfo.setCardName("Anton Angelov");
        clientInfo.setCardNumber("1234567890123456");
        clientInfo.setCardExpiration("12/23");
        clientInfo.setCardCVV("123");

        page.fillInfo(clientInfo);

        page.assertions().formSent();
    }

    @Test
    public void validatedFirstName_When_FirstNameNotSet() {
        var clientInfo = new ClientInfo();
        clientInfo.setFirstName("");
        clientInfo.setLastName("Angelov");
        clientInfo.setUsername("aangelov");
        clientInfo.setEmail("info@berlinspaceflowers.com");
        clientInfo.setAddress1("1 Willi Brandt Avenue Tiergarten");
        clientInfo.setAddress2("Lützowplatz 17");
        clientInfo.setCountry(1);
        clientInfo.setState(1);
        clientInfo.setZip("10115");
        clientInfo.setCardName("Anton Angelov");
        clientInfo.setCardNumber("1234567890123456");
        clientInfo.setCardExpiration("12/23");
        clientInfo.setCardCVV("123");

        page.fillInfo(clientInfo);

        page.assertions().validatedFirstName();
    }

    @Test
    public void validatedLastName_When_LastNameNotSet() {
        var clientInfo = new ClientInfo();
        clientInfo.setFirstName("Anton");
        clientInfo.setLastName("");
        clientInfo.setUsername("aangelov");
        clientInfo.setEmail("info@berlinspaceflowers.com");
        clientInfo.setAddress1("1 Willi Brandt Avenue Tiergarten");
        clientInfo.setAddress2("Lützowplatz 17");
        clientInfo.setCountry(1);
        clientInfo.setState(1);
        clientInfo.setZip("10115");
        clientInfo.setCardName("Anton Angelov");
        clientInfo.setCardNumber("1234567890123456");
        clientInfo.setCardExpiration("12/23");
        clientInfo.setCardCVV("123");

        page.fillInfo(clientInfo);

        page.assertions().validatedLastName();
    }

    @Test
    public void validatedUsername_When_UsernameNotSet() {
        var clientInfo = new ClientInfo();
        clientInfo.setFirstName("Anton");
        clientInfo.setLastName("Angelov");
        clientInfo.setUsername("");
        clientInfo.setEmail("info@berlinspaceflowers.com");
        clientInfo.setAddress1("1 Willi Brandt Avenue Tiergarten");
        clientInfo.setAddress2("Lützowplatz 17");
        clientInfo.setCountry(1);
        clientInfo.setState(1);
        clientInfo.setZip("10115");
        clientInfo.setCardName("Anton Angelov");
        clientInfo.setCardNumber("1234567890123456");
        clientInfo.setCardExpiration("12/23");
        clientInfo.setCardCVV("123");

        page.fillInfo(clientInfo);

        page.assertions().validatedUsername();
    }

    @Test
    public void validatedEmail_When_EmailNotValid() {
        var clientInfo = new ClientInfo();
        clientInfo.setFirstName("Anton");
        clientInfo.setLastName("Angelov");
        clientInfo.setUsername("aangelov");
        clientInfo.setEmail("infoberlinspaceflowers.com");
        clientInfo.setAddress1("1 Willi Brandt Avenue Tiergarten");
        clientInfo.setAddress2("Lützowplatz 17");
        clientInfo.setCountry(1);
        clientInfo.setState(1);
        clientInfo.setZip("10115");
        clientInfo.setCardName("Anton Angelov");
        clientInfo.setCardNumber("1234567890123456");
        clientInfo.setCardExpiration("12/23");
        clientInfo.setCardCVV("123");

        page.fillInfo(clientInfo);

        page.assertions().validatedEmail();
    }

    @Test
    public void validatedAddress1_When_Address1NotSet() {
        var clientInfo = new ClientInfo();
        clientInfo.setFirstName("Anton");
        clientInfo.setLastName("Angelov");
        clientInfo.setUsername("aangelov");
        clientInfo.setEmail("info@berlinspaceflowers.com");
        clientInfo.setAddress1("");
        clientInfo.setAddress2("Lützowplatz 17");
        clientInfo.setCountry(1);
        clientInfo.setState(1);
        clientInfo.setZip("10115");
        clientInfo.setCardName("Anton Angelov");
        clientInfo.setCardNumber("1234567890123456");
        clientInfo.setCardExpiration("12/23");
        clientInfo.setCardCVV("123");

        page.fillInfo(clientInfo);

        page.assertions().validatedAddress1();
    }

    @Test
    public void validatedCountry_When_CountryNotSet() {
        var clientInfo = new ClientInfo();
        clientInfo.setFirstName("Anton");
        clientInfo.setLastName("Angelov");
        clientInfo.setUsername("aangelov");
        clientInfo.setEmail("infoberlinspaceflowers.com");
        clientInfo.setAddress1("1 Willi Brandt Avenue Tiergarten");
        clientInfo.setAddress2("Lützowplatz 17");
        clientInfo.setCountry(0);
        clientInfo.setState(1);
        clientInfo.setZip("10115");
        clientInfo.setCardName("Anton Angelov");
        clientInfo.setCardNumber("1234567890123456");
        clientInfo.setCardExpiration("12/23");
        clientInfo.setCardCVV("123");

        page.fillInfo(clientInfo);

        page.assertions().validatedCountry();
    }

    @Test
    public void validatedState_When_StateNotSet() {
        var clientInfo = new ClientInfo();
        clientInfo.setFirstName("Anton");
        clientInfo.setLastName("Angelov");
        clientInfo.setUsername("aangelov");
        clientInfo.setEmail("infoberlinspaceflowers.com");
        clientInfo.setAddress1("1 Willi Brandt Avenue Tiergarten");
        clientInfo.setAddress2("Lützowplatz 17");
        clientInfo.setCountry(1);
        clientInfo.setState(0);
        clientInfo.setZip("10115");
        clientInfo.setCardName("Anton Angelov");
        clientInfo.setCardNumber("1234567890123456");
        clientInfo.setCardExpiration("12/23");
        clientInfo.setCardCVV("123");

        page.fillInfo(clientInfo);

        page.assertions().validatedState();
    }

    @Test
    public void validatedZip_When_ZipNotSet() {
        var clientInfo = new ClientInfo();
        clientInfo.setFirstName("Anton");
        clientInfo.setLastName("Angelov");
        clientInfo.setUsername("aangelov");
        clientInfo.setEmail("infoberlinspaceflowers.com");
        clientInfo.setAddress1("1 Willi Brandt Avenue Tiergarten");
        clientInfo.setAddress2("Lützowplatz 17");
        clientInfo.setCountry(1);
        clientInfo.setState(1);
        clientInfo.setZip("");
        clientInfo.setCardName("Anton Angelov");
        clientInfo.setCardNumber("1234567890123456");
        clientInfo.setCardExpiration("12/23");
        clientInfo.setCardCVV("123");

        page.fillInfo(clientInfo);

        page.assertions().validatedZip();
    }

    @Test
    public void validatedCardName_When_CardNameNotSet() {
        var clientInfo = new ClientInfo();
        clientInfo.setFirstName("Anton");
        clientInfo.setLastName("Angelov");
        clientInfo.setUsername("aangelov");
        clientInfo.setEmail("infoberlinspaceflowers.com");
        clientInfo.setAddress1("1 Willi Brandt Avenue Tiergarten");
        clientInfo.setAddress2("Lützowplatz 17");
        clientInfo.setCountry(1);
        clientInfo.setState(1);
        clientInfo.setZip("10115");
        clientInfo.setCardName("");
        clientInfo.setCardNumber("1234567890123456");
        clientInfo.setCardExpiration("12/23");
        clientInfo.setCardCVV("123");

        page.fillInfo(clientInfo);

        page.assertions().validatedCardName();
    }

    @Test
    public void validatedCardNumber_When_CardNumberNotSet() {
        var clientInfo = new ClientInfo();
        clientInfo.setFirstName("Anton");
        clientInfo.setLastName("Angelov");
        clientInfo.setUsername("aangelov");
        clientInfo.setEmail("infoberlinspaceflowers.com");
        clientInfo.setAddress1("1 Willi Brandt Avenue Tiergarten");
        clientInfo.setAddress2("Lützowplatz 17");
        clientInfo.setCountry(1);
        clientInfo.setState(1);
        clientInfo.setZip("10115");
        clientInfo.setCardName("Anton Angelov");
        clientInfo.setCardNumber("");
        clientInfo.setCardExpiration("12/23");
        clientInfo.setCardCVV("123");

        page.fillInfo(clientInfo);

        page.assertions().validatedCardNumber();
    }

    @Test
    public void validatedCardExpiration_When_CardExpirationNotSet() {
        var clientInfo = new ClientInfo();
        clientInfo.setFirstName("Anton");
        clientInfo.setLastName("Angelov");
        clientInfo.setUsername("aangelov");
        clientInfo.setEmail("infoberlinspaceflowers.com");
        clientInfo.setAddress1("1 Willi Brandt Avenue Tiergarten");
        clientInfo.setAddress2("Lützowplatz 17");
        clientInfo.setCountry(1);
        clientInfo.setState(1);
        clientInfo.setZip("10115");
        clientInfo.setCardName("Anton Angelov");
        clientInfo.setCardNumber("1234567890123456");
        clientInfo.setCardExpiration("");
        clientInfo.setCardCVV("123");

        page.fillInfo(clientInfo);

        page.assertions().validatedCardExpiration();
    }

    @Test
    public void validatedCardCVV_When_CardCVVNotSet() {
        var clientInfo = new ClientInfo();
        clientInfo.setFirstName("Anton");
        clientInfo.setLastName("Angelov");
        clientInfo.setUsername("aangelov");
        clientInfo.setEmail("infoberlinspaceflowers.com");
        clientInfo.setAddress1("1 Willi Brandt Avenue Tiergarten");
        clientInfo.setAddress2("Lützowplatz 17");
        clientInfo.setCountry(1);
        clientInfo.setState(1);
        clientInfo.setZip("10115");
        clientInfo.setCardName("Anton Angelov");
        clientInfo.setCardNumber("1234567890123456");
        clientInfo.setCardExpiration("12/23");
        clientInfo.setCardCVV("");

        page.fillInfo(clientInfo);

        page.assertions().validatedCardCVV();
    }
}
