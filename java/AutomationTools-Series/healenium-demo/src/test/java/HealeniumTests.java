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

import com.epam.healenium.SelfHealingDriver;
import io.github.bonigarcia.wdm.WebDriverManager;
import localpage.ClientInfo;
import localpage.LocalPage;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.firefox.FirefoxDriver;

public class HealeniumTests {
    public WebDriver driver;

    @BeforeAll
    public static void classInit() {
        WebDriverManager.firefoxdriver().setup();
    }

    @BeforeEach
    public void testInit() {
        WebDriver delegate = new FirefoxDriver(); // declare delegate
        driver = SelfHealingDriver.create(delegate); // create Self-healing driver
    }

    @AfterEach
    public void testCleanup() {
        driver.quit();
    }

    @Test
    public void assertFormSent_When_ValidInfoInput() throws InterruptedException {
        var localPage = new LocalPage(driver);
        localPage.navigate();

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

        localPage.fillInfo(clientInfo);

        localPage.assertions().formSent();
    }
}
