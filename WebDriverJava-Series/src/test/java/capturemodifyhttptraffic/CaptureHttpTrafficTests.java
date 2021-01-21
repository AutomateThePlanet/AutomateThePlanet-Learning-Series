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

package capturemodifyhttptraffic;

import io.github.bonigarcia.wdm.WebDriverManager;
import net.lightbody.bmp.BrowserMobProxyServer;
import net.lightbody.bmp.proxy.CaptureType;
import org.openqa.selenium.Proxy;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.testng.Assert;
import org.testng.annotations.*;

import java.io.File;
import java.io.IOException;

public class CaptureHttpTrafficTests {
    private WebDriver driver;
    private BrowserMobProxyServer proxyServer;

    @BeforeClass
    private void classInit() {
        proxyServer = new BrowserMobProxyServer();
        proxyServer.start(18882);
        proxyServer.newHar();

        WebDriverManager.chromedriver().setup();
    }

    @AfterClass
    public void classCleanup() {
        proxyServer.abort();
    }

    @BeforeMethod
    public void testInit() {
        final var proxyConfig = new Proxy()
                .setHttpProxy("127.0.0.1:18882")
                .setSslProxy("127.0.0.1:18882")
                .setFtpProxy("127.0.0.1:18882");
        final var options = new ChromeOptions()
                .setProxy(proxyConfig)
                .setAcceptInsecureCerts(true);
        driver = new ChromeDriver(options);
    }

    @AfterMethod
    public void testCleanup() {
        driver.quit();
    }

    @Test
    public void analyticsRequestMade_when_NavigateToHomePage() {
        driver.navigate().to("https://www.automatetheplanet.com/");

        assertRequestMade("analytics");
    }

    @Test
    public void fontRequestMade_when_NavigateToHomePage() {
        driver.navigate().to("https://www.automatetheplanet.com/");

        assertRequestMade("fontawesome-webfont.woff2?v=4.7.0");
    }

    @Test
    public void testNoLargeImages_when_NavigateToHomePage() {
        driver.navigate().to("https://www.automatetheplanet.com/");

        assertNoLargeImagesRequested();
    }

    @Test
    public void noErrorCodes_when_NavigateToHomePage() {
        driver.navigate().to("https://www.automatetheplanet.com/");

        assertNoErrorCodes();
    }

    private void assertNoErrorCodes() {
        var harEntries = proxyServer.getHar().getLog().getEntries();
        boolean areThereErrorCodes = harEntries.stream().anyMatch(r
                -> r.getResponse().getStatus() > 400
                && r.getResponse().getStatus() < 599);

        Assert.assertFalse(areThereErrorCodes);
    }

    private void assertRequestMade(String url) {
        var harEntries = proxyServer.getHar().getLog().getEntries();
        boolean areRequestsMade = harEntries.stream().anyMatch(r -> r.getRequest().getUrl().contains(url));

        Assert.assertTrue(areRequestsMade);
    }

    private void assertNoLargeImagesRequested() {
        var harEntries = proxyServer.getHar().getLog().getEntries();
        boolean areThereLargeImages = harEntries.stream().anyMatch(r
                -> r.getResponse().getContent().getMimeType().startsWith("image")
                && r.getResponse().getContent().getSize() > 40000);

        Assert.assertFalse(areThereLargeImages);
    }
}
