/*
 * Copyright 2020 Automate The Planet Ltd.
 * Author: Anton Angelov
 * Licensed under the Apache License, Version 2.0 (the "License");
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package kendogrid;

import io.github.bonigarcia.wdm.WebDriverManager;
import net.lightbody.bmp.BrowserMobProxyServer;
import org.apache.tools.ant.util.FileUtils;
import org.openqa.selenium.*;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.firefox.FirefoxProfile;
import org.openqa.selenium.firefox.ProfilesIni;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Test;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Set;
import java.util.concurrent.TimeUnit;

import static java.lang.System.getProperty;

public class KendoGridTests {
    private WebDriver driver;
    private WebDriverWait wait;

    @BeforeClass
    private void classInit() {
        WebDriverManager.chromedriver().setup();
    }

    @BeforeClass
    public void testSetup() throws IOException {
        driver = new ChromeDriver();
        driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
        driver.manage().window().maximize();
        wait = new WebDriverWait(driver, 30);
    }

    @AfterClass
    public void afterClass() throws InterruptedException {
        driver.quit();
    }

    @Test
    public void takeFullScreenshot_test() throws Exception {
        driver.navigate().to("http://automatetheplanet.com");
    }

    private void waitUntilLoaded() {
        wait.until(x ->
        {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) driver;
            String isReady = (String) javascriptExecutor.executeScript("return document.readyState");
            return isReady.equals("complete");
        });
    }

    private void waitForAjaxComplete() {
        wait.until(x ->
        {
            JavascriptExecutor javascriptExecutor = (JavascriptExecutor) driver;
            Boolean isAjaxCallComplete  = (Boolean) javascriptExecutor.executeScript("return window.jQuery != undefined && jQuery.active == 0");
            return isAjaxCallComplete ;
        });
    }
}