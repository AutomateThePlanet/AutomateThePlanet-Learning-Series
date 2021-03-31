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

import io.appium.java_client.PerformsTouchActions
import io.appium.java_client.TouchAction
import io.appium.java_client.ios.IOSDriver
import io.appium.java_client.ios.IOSElement
import io.appium.java_client.remote.MobileCapabilityType
import io.appium.java_client.touch.TapOptions
import io.appium.java_client.touch.WaitOptions
import io.appium.java_client.touch.offset.PointOption
import org.openqa.selenium.remote.DesiredCapabilities
import org.testng.annotations.*
import java.net.URL
import java.nio.file.Paths
import java.time.Duration

class GestureTests {
    private lateinit var driver: IOSDriver<IOSElement>

    @BeforeClass
    fun classInit() {
        val testAppUrl = javaClass.classLoader.getResource("TestApp.app.zip")
        val testAppFile = Paths.get((testAppUrl!!).toURI()).toFile()
        val testAppPath = testAppFile.absolutePath

        val desiredCaps = DesiredCapabilities()
        desiredCaps.setCapability(MobileCapabilityType.DEVICE_NAME, "iPhone 8")
        desiredCaps.setCapability(MobileCapabilityType.PLATFORM_NAME, "iOS")
        desiredCaps.setCapability(MobileCapabilityType.PLATFORM_VERSION, "14.4")
        desiredCaps.setCapability(MobileCapabilityType.APP, testAppPath)

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

    @Test
    fun swipeTest() {
        class PlatformTouchAction(performsTouchActions: PerformsTouchActions) : TouchAction<PlatformTouchAction>(performsTouchActions)
        val touchAction = PlatformTouchAction(driver)
        val element = driver.findElementById("IntegerA")
        val point = element.location
        val size = element.size

        touchAction.press(PointOption.point(point.x + 5, point.y + 5))
            .waitAction(WaitOptions.waitOptions(Duration.ofMillis(200)))
            .moveTo(PointOption.point(point.x + size.width - 5, point.y + size.height - 5))
            .release()
            .perform()
    }

    @Test
    fun moveToTest() {
        class PlatformTouchAction(performsTouchActions: PerformsTouchActions) : TouchAction<PlatformTouchAction>(performsTouchActions)
        val touchAction = PlatformTouchAction(driver)
        val element = driver.findElementById("IntegerA")
        val point = element.location

        touchAction.moveTo(PointOption.point(point)).perform()
    }

    @Test
    fun tapTest() {
        class PlatformTouchAction(performsTouchActions: PerformsTouchActions) : TouchAction<PlatformTouchAction>(performsTouchActions)
        val touchAction = PlatformTouchAction(driver)
        val element = driver.findElementById("IntegerA")
        val point = element.location

        touchAction.tap(TapOptions.tapOptions().withPosition(PointOption.point(point)).withTapsCount(2)).perform()
    }
}