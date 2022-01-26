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

package appiumandroid;

import io.appium.java_client.TouchAction;
import io.appium.java_client.android.Activity;
import io.appium.java_client.android.AndroidDriver;
import io.appium.java_client.android.AndroidElement;
import io.appium.java_client.remote.AndroidMobileCapabilityType;
import io.appium.java_client.remote.MobileCapabilityType;
import io.appium.java_client.touch.TapOptions;
import io.appium.java_client.touch.WaitOptions;
import io.appium.java_client.touch.offset.PointOption;
import org.openqa.selenium.Dimension;
import org.openqa.selenium.Point;
import org.openqa.selenium.remote.DesiredCapabilities;
import org.testng.annotations.*;

import java.io.File;
import java.net.MalformedURLException;
import java.net.URISyntaxException;
import java.net.URL;
import java.nio.file.Paths;
import java.time.Duration;
import java.util.Objects;

public class GestureTests {
    private static AndroidDriver<AndroidElement> driver;

    @BeforeClass
    public void classInit() throws URISyntaxException, MalformedURLException {
        URL testAppUrl = getClass().getClassLoader().getResource("ApiDemos.apk");
        File testAppFile = Paths.get(Objects.requireNonNull(testAppUrl).toURI()).toFile();
        String testAppPath = testAppFile.getAbsolutePath();

        var desiredCaps = new DesiredCapabilities();
        desiredCaps.setCapability(MobileCapabilityType.DEVICE_NAME, "android25-test");
        desiredCaps.setCapability(AndroidMobileCapabilityType.APP_PACKAGE, "com.example.android.apis");
        desiredCaps.setCapability(MobileCapabilityType.PLATFORM_NAME, "Android");
        desiredCaps.setCapability(MobileCapabilityType.PLATFORM_VERSION, "7.1");
        desiredCaps.setCapability(AndroidMobileCapabilityType.APP_ACTIVITY, ".view.Controls1");
        desiredCaps.setCapability(MobileCapabilityType.APP, testAppPath);

        driver = new AndroidDriver<AndroidElement>(new URL("http://127.0.0.1:4723/wd/hub"), desiredCaps);
        driver.closeApp();
    }

    @BeforeMethod
    public void testInit() {
        if (driver != null) {
            driver.launchApp();
            driver.startActivity(new Activity("com.example.android.apis", ".view.Controls1"));
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
    }

    @Test
    public void swipeTest() {
        driver.startActivity(new Activity("com.example.android.apis", ".graphics.FingerPaint"));
        TouchAction touchAction = new TouchAction(driver);
        AndroidElement element = driver.findElementById("android:id/content");
        Point point = element.getLocation();
        Dimension size = element.getSize();

        touchAction.press(PointOption.point(point.getX() + 5, point.getY() + 5))
                .waitAction(WaitOptions.waitOptions(Duration.ofMillis(200)))
                .moveTo(PointOption.point(point.getX() + size.getWidth() - 5, point.getY() + size.getHeight() - 5))
                .release()
                .perform();
    }

    @Test
    public void moveToTest() {
        TouchAction touchAction = new TouchAction(driver);
        AndroidElement element = driver.findElementById("android:id/content");
        Point point = element.getLocation();

        touchAction.moveTo(PointOption.point(point)).perform();
    }

    @Test
    public void tapTest() {
        var touchAction = new TouchAction(driver);
        var element = driver.findElementById("android:id/content");
        Point point = element.getLocation();

        touchAction.tap(TapOptions.tapOptions()
                .withPosition(PointOption.point(point))
                .withTapsCount(2))
                .perform();
    }
}
