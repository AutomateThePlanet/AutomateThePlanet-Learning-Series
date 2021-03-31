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

package appiumandroid

import io.appium.java_client.android.Activity
import io.appium.java_client.android.AndroidDriver
import io.appium.java_client.android.AndroidElement
import io.appium.java_client.remote.AndroidMobileCapabilityType
import io.appium.java_client.remote.MobileCapabilityType
import org.openqa.selenium.remote.DesiredCapabilities
import org.testng.annotations.*
import java.net.URL
import java.nio.file.Paths

class AppiumTests {
    private lateinit var driver: AndroidDriver<AndroidElement>
    ////private lateinit var appiumLocalService: AppiumDriverLocalService

    @BeforeClass
    fun classInit() {
        ////appiumLocalService = AppiumServiceBuilder().usingAnyFreePort().build()
        ////appiumLocalService.start()
        val testAppUrl = javaClass.classLoader.getResource("ApiDemos.apk")
        val testAppFile = Paths.get((testAppUrl!!).toURI()).toFile()
        val testAppPath = testAppFile.absolutePath

        val desiredCaps = DesiredCapabilities()
        desiredCaps.setCapability(MobileCapabilityType.DEVICE_NAME, "android25-test")
        desiredCaps.setCapability(AndroidMobileCapabilityType.APP_PACKAGE, "com.example.android.apis")
        desiredCaps.setCapability(MobileCapabilityType.PLATFORM_NAME, "Android")
        desiredCaps.setCapability(MobileCapabilityType.PLATFORM_VERSION, "7.1")
        desiredCaps.setCapability(AndroidMobileCapabilityType.APP_ACTIVITY, ".view.Controls1")
        desiredCaps.setCapability(MobileCapabilityType.APP, testAppPath)

        ////driver = AndroidDriver<AndroidElement>(appiumLocalService, desiredCaps)
        driver = AndroidDriver<AndroidElement>(URL("http://127.0.0.1:4723/wd/hub"), desiredCaps)
        driver.closeApp()
    }

    @BeforeMethod
    fun testInit() {
        driver.launchApp()
        driver.startActivity(Activity("com.example.android.apis", ".view.Controls1"))
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
    fun locatingElementsTest() {
        val button = driver.findElementById("com.example.android.apis:id/button")
        button.click()

        val checkBox = driver.findElementByClassName("android.widget.CheckBox")
        checkBox.click()

        val secondButton = driver.findElementByXPath("//*[@resource-id='com.example.android.apis:id/button']")
        secondButton.click()

        val thirdButton = driver.findElementByAndroidUIAutomator("new UiSelector().textContains(\"BUTTO\");")
        thirdButton.click()
    }

    @Test
    fun locatingElementsInsideAnotherElementTest() {
        val mainElement = driver.findElementById("android:id/content")

        val button = mainElement.findElementById("com.example.android.apis:id/button")
        button.click()

        val checkBox = mainElement.findElementByClassName("android.widget.CheckBox")
        checkBox.click()

        val secondButton = mainElement.findElementByXPath("//*[@resource-id='com.example.android.apis:id/button']")
        secondButton.click()

        val thirdButton = mainElement.findElementByAndroidUIAutomator("new UiSelector().textContains(\"BUTTO\");")
        thirdButton.click()
    }
}