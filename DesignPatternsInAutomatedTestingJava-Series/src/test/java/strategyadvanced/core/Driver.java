/*
 * Copyright 2021 Automate The Planet Ltd.
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
package strategyadvanced.core;

import io.github.bonigarcia.wdm.WebDriverManager;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.support.ui.WebDriverWait;

public class Driver {
    private static WebDriverWait browserWait;
    private static WebDriver browser;

    public static WebDriver getBrowser() {
        if (browser == null) {
            throw new NullPointerException("The WebDriver browser instance was not initialized. You should first call the start() method.");
        }
        return browser;
    }

    public static void setBrowser(WebDriver browser) {
        Driver.browser = browser;
    }

    public static WebDriverWait getBrowserWait() {
        if (browserWait == null || browser == null) {
            throw new NullPointerException("The WebDriver browser wait instance was not initialized. You should first call the start() method.");
        }
        return browserWait;
    }

    public static void setBrowserWait(WebDriverWait browserWait) {
        Driver.browserWait = browserWait;
    }

    public static void startBrowser(BrowserType browserType, int defaultTimeout) {

        switch (browserType) {
            case FIREFOX:
                WebDriverManager.firefoxdriver().setup();
                setBrowser(new FirefoxDriver());
                break;
            case CHROME:
                //setBrowser(new ChromeDriver());
                break;
            case EDGE:
                //setBrowser(new EdgeDriver());
                break;
            default:
                break;
        }

        setBrowserWait(new WebDriverWait(getBrowser(), defaultTimeout));
    }

    public static void startBrowser(BrowserType browserType) {
        startBrowser(browserType, 30);
    }

    public static void startBrowser() {
        startBrowser(BrowserType.FIREFOX);
    }

    public static void stopBrowser() {
        getBrowser().quit();
        setBrowser(null);
        setBrowserWait(null);
    }

    public static void waitForAjax() {
        var javascriptExecutor = (JavascriptExecutor)browser;
        browserWait.until(d -> javascriptExecutor.executeScript("return window.jQuery != undefined && jQuery.active == 0"));
    }

    public static void waitUntilPageLoadsCompletely() {
        var javascriptExecutor = (JavascriptExecutor)browser;
        browserWait.until(d -> javascriptExecutor.executeScript("return document.readyState").toString().equals("complete"));
    }
}
