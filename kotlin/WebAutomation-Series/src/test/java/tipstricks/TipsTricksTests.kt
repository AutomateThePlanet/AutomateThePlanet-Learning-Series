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
package tipstricks

import org.apache.tools.ant.util.FileUtils
import org.openqa.selenium.*
import org.openqa.selenium.support.ui.WebDriverWait
import kotlin.Throws
import java.io.IOException
import org.openqa.selenium.chrome.ChromeOptions
import org.openqa.selenium.chrome.ChromeDriver
import org.openqa.selenium.firefox.FirefoxDriver
import org.openqa.selenium.firefox.FirefoxOptions
import java.io.File
import java.util.HashMap
import java.util.concurrent.TimeUnit
import org.openqa.selenium.firefox.FirefoxProfile
import org.openqa.selenium.firefox.ProfilesIni
import org.openqa.selenium.interactions.Actions
import java.lang.InterruptedException
import org.openqa.selenium.support.ui.ExpectedConditions
import org.testng.Assert
import java.nio.file.Paths
import java.lang.System
import org.testng.annotations.AfterClass
import org.testng.annotations.BeforeClass
import org.testng.annotations.Test
import java.awt.image.BufferedImage
import java.lang.Exception
import java.nio.file.Files
import javax.imageio.ImageIO

class TipsTricksTests {
    private lateinit var driver: WebDriver
    private lateinit var wait: WebDriverWait
    
    @BeforeClass
    @Throws(IOException::class)
    fun testSetup() {
        System.setProperty("webdriver.chrome.driver", "resources\\chromedriver.exe")

        // 5. Execute tests in headless Chrome
        val chromeOptions = ChromeOptions()
        chromeOptions.addArguments("--headless", "--disable-gpu", "--window-size=1920,1200", "--ignore-certificate-errors")
        driver = ChromeDriver(chromeOptions)
        // 22. Set HTTP Proxy ChromeDriver
        val proxy = Proxy()
        proxy.proxyType = Proxy.ProxyType.MANUAL
        proxy.isAutodetect = false
        proxy.sslProxy = "127.0.0.1:3239"
        chromeOptions.setProxy(proxy)

        // 23. Set HTTP Proxy with Authentication ChromeDriver
        // chromeOptions.addArguments("--proxy-server=http://user:password@127.0.0.1:3239");

        // 24. tart ChromeDriver with an Packed Extension
        chromeOptions.addArguments("load-extension=/pathTo/extension")

        // 25. Start ChromeDriver with an Unpacked Extension
        chromeOptions.addExtensions(File("local/path/to/extension.crx"))

        // 29. Verify File Downloaded ChromeDriver
        // String downloadFilepath = "c:\\temp";
        val chromePrefs = HashMap<String, Any>()
        chromePrefs["profile.default_content_settings.popups"] = 0
        chromePrefs["download.default_directory"] = "downloadFilepath"
        chromeOptions.setExperimentalOption("prefs", chromePrefs)
        chromeOptions.addArguments("--test-type")
        chromeOptions.addArguments("start-maximized", "disable-popup-blocking")

        // 18.2. Handle SSL Certificate Error ChromeDrive
        chromeOptions.addArguments("--ignore-certificate-errors")

        // 7. Use Specific Profile in Chrome
        chromeOptions.addArguments("user-data-dri=C:\\Users\\Your path to user\\Roaming\\Google\\Chrome\\User Data")
        driver = ChromeDriver()
        driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS)

        // 10. Maximize Window
        driver.manage().window().maximize()

        // 4. Set Page Load Timeout
        // driver.manage().timeouts().pageLoadTimeout(10, TimeUnit.SECONDS);
        wait = WebDriverWait(driver, 30)

        // 7. Use Specific Profile in Firefox
        val profile = ProfilesIni()
        val firefoxProfile = profile.getProfile("xyzProfile")
        val firefoxOptions = FirefoxOptions()

        // 8. Turn Off JavaScript
        firefoxProfile.setPreference("javascript.enabled", false)

        // 16. Change User Agent
        firefoxProfile.setPreference("general.useragent.override", "Mozilla/5.0 (BlackBerry; U; BlackBerry 9900; en) AppleWebKit/534.11+ (KHTML, like Gecko) Version/7.1.0.346 Mobile Safari/534.11+")

        // 17. Set HTTP Proxy for Browser
        firefoxProfile.setPreference("network.proxy.type", 1)
        firefoxProfile.setPreference("network.proxy.http", "myproxy.com")
        firefoxProfile.setPreference("network.proxy.http_port", 3239)

        // 18. Handle SSL Certificate Error FirefoxDriver
        firefoxProfile.setAcceptUntrustedCertificates(true)
        firefoxProfile.setAssumeUntrustedCertificateIssuer(false)

        // 21. Start FirefoxDriver with Plugins
        firefoxProfile.addExtension(File("C:\\extensionsLocation\\extension.xpi"))

        // 30. Verify File Downloaded FirefoxDriver
        val downloadFilepath = "c:\\temp"
        firefoxProfile.setPreference("browser.download.folderList", 2)
        firefoxProfile.setPreference("browser.download.dir", downloadFilepath)
        firefoxProfile.setPreference("browser.download.manager.alertOnEXEOpen", false)
        firefoxProfile.setPreference("browser.helperApps.neverAsk.saveToDisk", "application/msword, application/binary, application/ris, text/csv, image/png, application/pdf, text/html, text/plain, application/zip, application/x-zip, application/x-zip-compressed, application/download, application/octet-stream")
        firefoxOptions.setProfile(firefoxProfile)
        val firefoxDriver: WebDriver = FirefoxDriver(firefoxOptions)
    }

    @AfterClass
    fun afterClass() {
        driver.quit()
    }

    @Test
    fun takeFullScreenshot_test() {
        driver.navigate().to("http://automatetheplanet.com")
        takeFullScreenshot("testImage")
    }

    @Test
    fun takeElementScreenshot_test() {
        driver.navigate().to("http://automatetheplanet.com")
        val element = wait.until(ExpectedConditions.visibilityOfElementLocated(By.xpath("/html/body/div[1]/header/div/div[2]/div/div[2]/nav")))
        takeScreenshotOfElement(element, "testElementImage")
    }

    // 2. Get HTML Source of WebElement
    @get:Test
    val htmlSourceOfWebElement: Unit
        get() {
            driver.navigate().to("http://automatetheplanet.com")
            val element = wait.until(ExpectedConditions.visibilityOfElementLocated(By.xpath("/html/body/div[1]/header/div/div[2]/div/div[2]/nav")))
            val sourceHtml = element.getAttribute("innerHTML")
            println(sourceHtml)
        }

    // 3. Execute JavaScript
    @Test
    fun executeJavaScript() {
        driver.navigate().to("http://automatetheplanet.com")
        val javascriptExecutor = driver as JavascriptExecutor?
        val title = javascriptExecutor!!.executeScript("return document.title") as String

        // 4. Visibility of all elements wait
        wait.until(ExpectedConditions.visibilityOfAllElements(driver.findElements(By.xpath("//*[@id='tve_editor']/div[2]/div[2]/div/div"))))
        println(title)
    }

    // 6. Check If an Element Is Visible
    @Test
    fun checkIfElementIsVisible() {
        driver.navigate().to("http://automatetheplanet.com")
        val element = driver.findElement(By.xpath("/html/body/div[1]/header/div/div[2]/div/div[2]/nav"))
        Assert.assertTrue(element.isDisplayed)
    }

    @Test
    fun manageCookies() {
        driver.navigate().to("http://automatetheplanet.com")

        // get all cookies
        val cookies = driver.manage().cookies
        for (cookie in cookies) {
            println(cookie.name)
        }

        // get a cookie by name
        val fbPixelCookie = driver.manage().getCookieNamed("_fbp")

        // create a new cookie by name
        val newCookie = Cookie("customName", "customValue")
        driver.manage().addCookie(newCookie)

        // delete a cookie
        driver.manage().deleteCookie(fbPixelCookie)

        // delete a cookie by name
        driver.manage().deleteCookieNamed("customName")

        // delete all cookies
        driver.manage().deleteAllCookies()
    }

    // 11. Drag and Drop
    @Test
    fun dragAndDrop() {
        driver.navigate().to("http://loopj.com/jquery-simple-slider/")
        val element = driver.findElement(By.xpath("//*[@id='project']/p[1]/div/div[2]"))
        val action = Actions(driver)
        action.dragAndDropBy(element, 30, 0).build().perform()
    }

    // 12. Upload a File
    @Test
    fun fileUpload() {
        driver.navigate().to("https://demos.telerik.com/aspnet-ajax/ajaxpanel/application-scenarios/file-upload/defaultcs.aspx")
        val element = driver.findElement(By.id("ctl00_ContentPlaceholder1_RadUpload1file0"))
        val filePath = Paths.get(System.getProperty("java.io.tmpdir"), "debugWebDriver.xml").toString()
        val destFile = File(filePath)
        destFile.createNewFile()
        element.sendKeys(filePath)
    }

    // 13. Handle JavaScript Pop-ups
    @Test
    fun handleJavaScripPopUps() {
        driver.navigate().to("http://www.w3schools.com/js/tryit.asp?filename=tryjs_confirm")
        driver.switchTo().frame("iframeResult")
        val button = driver.findElement(By.xpath("/html/body/button"))
        button.click()
        val alert = driver.switchTo().alert()
        if (alert.text == "Press a button!") {
            alert.accept()
        } else {
            alert.dismiss()
        }
    }

    // 14. Switch Between Browser Windows or Tabs
    @Test
    fun movingBetweenTabs() {
        driver.navigate().to("https://www.automatetheplanet.com/")
        val firstLink = driver.findElement(By.xpath("//*[@id='menu-item-11362']/a"))
        val secondLink = driver.findElement(By.xpath("//*[@id='menu-item-6']/a"))
        val selectLinkOpenninNewTab = Keys.chord(Keys.CONTROL, Keys.RETURN)
        firstLink.sendKeys(selectLinkOpenninNewTab)
        secondLink.sendKeys(selectLinkOpenninNewTab)
        val windows = driver.windowHandles
        val firstTab = windows.toTypedArray()[1] as String
        val lastTab = windows.toTypedArray()[2] as String
        driver.switchTo().window(lastTab)
        Assert.assertEquals("Resources - Automate The Planet", driver.title)
        driver.switchTo().window(firstTab)
        Assert.assertEquals("Blog - Automate The Planet", driver.title)
    }

    // 15. Navigation History
    @Test
    fun navigationHistory() {
        driver.navigate().to("https://www.codeproject.com/Articles/1078541/Advanced-WebDriver-Tips-and-Tricks-Part")
        driver.navigate().to("http://www.codeproject.com/Articles/1017816/Speed-up-Selenium-Tests-through-RAM-Facts-and-Myth")
        driver.navigate().back()
        Assert.assertEquals("10 Advanced WebDriver Tips and Tricks - Part 1 - CodeProject", driver.title)
        driver.navigate().refresh()
        Assert.assertEquals("10 Advanced WebDriver Tips and Tricks - Part 1 - CodeProject", driver.title)
        driver.navigate().forward()
        Assert.assertEquals("Speed up Selenium Tests through RAM Facts and Myths - CodeProject", driver.title)
    }

    // 19. Scroll Focus to Control
    @Test
    fun scrollFocusToControl() {
        driver.navigate().to("http://automatetheplanet.com/")
        val ourMissionLink = driver.findElement(By.xpath("//*[@id=\"panel-6435-0-0-4\"]/div"))
        val jsToBeExecuted = String.format("window.scroll(0, {0});", ourMissionLink.location.getY())
        val javascriptExecutor = driver as JavascriptExecutor?
        javascriptExecutor!!.executeScript(jsToBeExecuted)
    }

    // 20. Focus on a Control
    @Test
    fun focusOnControl() {
        driver.navigate().to("http://automatetheplanet.com/")
        waitUntilLoaded()
        val ourMissionLink = driver.findElement(By.xpath("//*[@id=\"panel-6435-0-0-4\"]/div"))
        val action = Actions(driver)
        action.moveToElement(ourMissionLink).build().perform()
    }

    // 26. Assert a Button Enabled or Disabled
    @Test
    fun assertButtonEnabledDisabled() {
        driver.navigate().to("http://www.w3schools.com/tags/tryit.asp?filename=tryhtml_button_disabled")
        driver.switchTo().frame("iframeResult")
        val button = driver.findElement(By.xpath("/html/body/button"))
        Assert.assertFalse(button.isEnabled)
    }

    // 27. Set and Assert the Value of a Hidden Field
    @Test
    fun setHiddenField() {
        //<input type="hidden" name="country" value="Bulgaria"/>
        val theHiddenElem = driver.findElement(By.name("country"))
        val javascriptExecutor = driver as JavascriptExecutor?
        javascriptExecutor!!.executeScript("arguments[0].value='Germany';", theHiddenElem)
        val hiddenFieldValue = theHiddenElem.getAttribute("value")
        Assert.assertEquals("Germany", hiddenFieldValue)
    }

    // 29. Verify File Downloaded ChromeDriver
    @Test
    fun VerifyFileDownloadChrome() {
        val expectedFilePath = Paths.get("c:\\temp\\Testing_Framework_2015_3_1314_2_Free.exe")
        try {
            driver.navigate().to("https://www.telerik.com/download-trial-file/v2/telerik-testing-framework")
            wait.until { x: WebDriver? -> Files.exists(expectedFilePath) }
            val bytes = Files.size(expectedFilePath)
            Assert.assertEquals(4326192, bytes)
        } finally {
            if (Files.exists(expectedFilePath)) {
                Files.delete(expectedFilePath)
            }
        }
    }

    // 1. Taking a Screenshot
    fun takeFullScreenshot(fileName: String) {
        val srcFile = (driver as TakesScreenshot).getScreenshotAs(OutputType.FILE)
        val tempDir = System.getProperty("java.io.tmpdir")
        val destFile = File(Paths.get(tempDir, "$fileName.png").toString())
        FileUtils.getFileUtils().copyFile(srcFile, destFile)
    }

    fun takeScreenshotOfElement(element: WebElement, fileName: String) {
        val screenshotFile = (driver as TakesScreenshot).getScreenshotAs(OutputType.FILE)
        val fullImg = ImageIO.read(screenshotFile)
        val point = element.location
        val elementWidth = element.size.getWidth()
        val elementHeight = element.size.getHeight()
        val eleScreenshot = fullImg.getSubimage(point.getX(), point.getY(), elementWidth, elementHeight)
        ImageIO.write(eleScreenshot, "png", screenshotFile)
        val tempDir = System.getProperty("java.io.tmpdir")
        val destFile = File(Paths.get(tempDir, "$fileName.png").toString())
        FileUtils.getFileUtils().copyFile(screenshotFile, destFile)
    }

    // 4. Set Page Load Timeout
    private fun waitUntilLoaded() {
        wait.until { x: WebDriver? ->
            val javascriptExecutor = driver as JavascriptExecutor?
            val isReady = javascriptExecutor!!.executeScript("return document.readyState") as String
            isReady == "complete"
        }
    }

    // 28. Wait AJAX Call to Complete Using JQuery
    private fun waitForAjaxComplete() {
        wait.until { x: WebDriver? ->
            val javascriptExecutor = driver as JavascriptExecutor?
            val isAjaxCallComplete = javascriptExecutor!!.executeScript("return window.jQuery != undefined && jQuery.active == 0") as Boolean
            isAjaxCallComplete
        }
    }
}