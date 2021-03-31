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

package appiumiosmac;

import io.appium.java_client.ios.IOSDriver;
import io.appium.java_client.ios.IOSElement;
import io.appium.java_client.remote.MobileCapabilityType;
import io.appium.java_client.service.local.AppiumDriverLocalService;
import io.appium.java_client.service.local.AppiumServiceBuilder;
import org.openqa.selenium.remote.DesiredCapabilities;
import org.testng.Assert;
import org.testng.annotations.*;

import java.io.File;
import java.net.MalformedURLException;
import java.net.URISyntaxException;
import java.net.URL;
import java.nio.file.Paths;
import java.util.Objects;

public class AppiumTests {
    private static IOSDriver<IOSElement> driver;
    ////private static AppiumDriverLocalService appiumLocalService;

    @BeforeClass
    public void classInit() throws URISyntaxException, MalformedURLException {
        ////appiumLocalService = new AppiumServiceBuilder().usingAnyFreePort().build();
        ////appiumLocalService.start();
        URL testAppUrl = getClass().getClassLoader().getResource("TestApp.app.zip");
        File testAppFile = Paths.get(Objects.requireNonNull(testAppUrl).toURI()).toFile();
        String testAppPath = testAppFile.getAbsolutePath();

        var desiredCaps = new DesiredCapabilities();
        desiredCaps.setCapability(MobileCapabilityType.DEVICE_NAME, "iPhone 8");
        desiredCaps.setCapability(MobileCapabilityType.PLATFORM_NAME, "iOS");
        desiredCaps.setCapability(MobileCapabilityType.PLATFORM_VERSION, "14.4");
        desiredCaps.setCapability(MobileCapabilityType.APP, testAppPath);

        ////driver = new IOSDriver<IOSElement>(appiumLocalService, desiredCaps);
        driver = new IOSDriver<IOSElement>(new URL("http://127.0.0.1:4723/wd/hub"), desiredCaps);
        driver.closeApp();
    }

    @BeforeMethod
    public void testInit() {
        if (driver != null) {
            driver.launchApp();
        }
    }

    @AfterMethod
    public void testCleanup() {
        if (driver != null) {
            driver.closeApp();
        }
    }

    @AfterClass
    public void classCleanup() {
        ////appiumLocalService.stop();
    }

    @Test
    public void addTwoNumbersTest() {
        var numberOne = driver.findElementByName("IntegerA");
        var numberTwo = driver.findElementByName("IntegerB");
        var compute = driver.findElementByName("ComputeSumButton");
        var answer = driver.findElementByName("Answer");

        numberOne.clear();
        numberOne.setValue("5");
        numberTwo.clear();
        numberTwo.setValue("6");
        compute.click();

        Assert.assertEquals("11", answer.getAttribute("value"));
    }

    @Test
    public void locatingElementsInsideAnotherElementTest() {
        var mainElement = driver.findElementByIosNsPredicate("type == \"XCUIElementTypeApplication\" AND name == \"TestApp\"");

        var numberOne = mainElement.findElementById("IntegerA");
        var numberTwo = mainElement.findElementById("IntegerB");
        var compute = mainElement.findElementByName("ComputeSumButton");
        var answer = mainElement.findElementByName("Answer");

        numberOne.clear();
        numberOne.setValue("5");
        numberTwo.clear();
        numberTwo.setValue("6");
        compute.click();

        Assert.assertEquals("11", answer.getAttribute("value"));
    }
}
