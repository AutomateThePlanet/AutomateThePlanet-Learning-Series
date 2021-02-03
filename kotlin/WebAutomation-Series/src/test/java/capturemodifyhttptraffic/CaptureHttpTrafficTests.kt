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
package capturemodifyhttptraffic

import org.openqa.selenium.WebDriver
import net.lightbody.bmp.BrowserMobProxyServer
import io.github.bonigarcia.wdm.WebDriverManager
import org.openqa.selenium.chrome.ChromeOptions
import org.openqa.selenium.chrome.ChromeDriver
import net.lightbody.bmp.core.har.HarEntry
import org.openqa.selenium.Proxy
import org.testng.Assert
import org.testng.annotations.*

class CaptureHttpTrafficTests {
    private lateinit var driver: WebDriver
    private lateinit var proxyServer: BrowserMobProxyServer
    
    @BeforeClass
    private fun classInit() {
        proxyServer = BrowserMobProxyServer()
        proxyServer.start(18882)
        WebDriverManager.chromedriver().setup()
    }

    @AfterClass
    fun classCleanup() {
        proxyServer.stop()
    }

    @BeforeMethod
    fun testInit() {
        val proxyConfig = Proxy()
                .setHttpProxy("127.0.0.1:18882")
                .setSslProxy("127.0.0.1:18882")
                .setFtpProxy("127.0.0.1:18882")
        val options = ChromeOptions()
                .setProxy(proxyConfig)
                .setAcceptInsecureCerts(true)
        driver = ChromeDriver(options)
        proxyServer.newHar()
    }

    @AfterMethod
    fun testCleanup() {
        driver.quit()
    }

    @Test
    fun analyticsRequestMade_when_NavigateToHomePage() {
        driver.navigate().to("https://www.automatetheplanet.com/")
        assertRequestMade("analytics")
    }

    @Test
    fun fontRequestMade_when_NavigateToHomePage() {
        driver.navigate().to("https://www.automatetheplanet.com/")
        assertRequestMade("fontawesome-webfont.woff2?v=4.7.0")
    }

    @Test
    fun testNoLargeImages_when_NavigateToHomePage() {
        driver.navigate().to("https://www.automatetheplanet.com/")
        assertNoLargeImagesRequested()
    }

    @Test
    fun noErrorCodes_when_NavigateToHomePage() {
        driver.navigate().to("https://www.automatetheplanet.com/")
        assertNoErrorCodes()
    }

    private fun assertNoErrorCodes() {
        val harEntries = proxyServer.har.log.entries
        val areThereErrorCodes = harEntries.any { r: HarEntry -> (r.response.status > 400 && r.response.status < 599) }
        Assert.assertFalse(areThereErrorCodes)
    }

    private fun assertRequestMade(url: String) {
        val harEntries = proxyServer.har.log.entries
        val areRequestsMade = harEntries.any { r: HarEntry -> r.request.url.contains(url) }
        Assert.assertTrue(areRequestsMade)
    }

    private fun assertNoLargeImagesRequested() {
        val harEntries = proxyServer.har.log.entries
        val areThereLargeImages = harEntries.any { r: HarEntry -> (r.response.content.mimeType.startsWith("image") && r.response.content.size > 40000)
        }
        Assert.assertFalse(areThereLargeImages)
    }
}