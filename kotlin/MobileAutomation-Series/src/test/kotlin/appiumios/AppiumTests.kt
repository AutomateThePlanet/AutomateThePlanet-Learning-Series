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

package appiumios

import io.appium.java_client.ios.IOSDriver
import io.appium.java_client.ios.IOSElement
import io.appium.java_client.remote.MobileCapabilityType
import org.openqa.selenium.remote.DesiredCapabilities
import org.testng.Assert
import org.testng.annotations.*
import java.net.URL
import java.nio.file.Paths
import java.util.*

class AppiumTests {
    private lateinit var driver: IOSDriver<IOSElement>
    ////private lateinit var appiumLocalService: AppiumDriverLocalService

    @BeforeClass
    fun classInit() {
        ////appiumLocalService = AppiumServiceBuilder().usingAnyFreePort().build()
        ////appiumLocalService.start()
        val testAppUrl = javaClass.classLoader.getResource("TestApp.app.zip")
        val testAppFile = Paths.get(Objects.requireNonNull(testAppUrl).toURI()).toFile()
        val testAppPath = testAppFile.absolutePath

        val desiredCaps = DesiredCapabilities()
        desiredCaps.setCapability(MobileCapabilityType.DEVICE_NAME, "iPhone 8")
        desiredCaps.setCapability(MobileCapabilityType.PLATFORM_NAME, "iOS")
        desiredCaps.setCapability(MobileCapabilityType.PLATFORM_VERSION, "14.4")
        desiredCaps.setCapability(MobileCapabilityType.APP, testAppPath)

        ////driver = IOSDriver(appiumLocalService, desiredCaps)
        driver = IOSDriver(URL("http://127.0.0.1:4723/wd/hub"), desiredCaps)
        driver.closeApp()
    }

    @BeforeMethod
    fun testInit() {
        driver.launchApp()
    }

    @AfterMethod
    fun testCleanup() {
        driver.closeApp()
    }

    @AfterClass
    fun classCleanup() {
        ////appiumLocalService.stop()
    }

    @Test
    fun addTwoNumbersTest() {
        val numberOne = driver.findElementByName("IntegerA")
        val numberTwo = driver.findElementByName("IntegerB")
        val compute = driver.findElementByName("ComputeSumButton")
        val answer = driver.findElementByName("Answer")

        numberOne.clear()
        numberOne.setValue("5")
        numberTwo.clear()
        numberTwo.setValue("6")
        compute.click()

        Assert.assertEquals("11", answer.getAttribute("value"))
    }

    @Test
    fun locatingElementsInsideAnotherElementTest() {
        val mainElement = driver.findElementByIosNsPredicate("type == \"XCUIElementTypeApplication\" AND name == \"TestApp\"")
        val numberOne = mainElement.findElementById("IntegerA")
        val numberTwo = mainElement.findElementById("IntegerB")
        val compute = mainElement.findElementByName("ComputeSumButton")
        val answer = mainElement.findElementByName("Answer")

        numberOne.clear()
        numberOne.setValue("5")
        numberTwo.clear()
        numberTwo.setValue("6")
        compute.click()

        Assert.assertEquals("11", answer.getAttribute("value"))
    }
}