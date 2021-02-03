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
package torintegration

import org.openqa.selenium.WebDriver
import java.lang.Process
import kotlin.Throws
import java.io.IOException
import java.lang.Runtime
import org.openqa.selenium.firefox.FirefoxProfile
import org.openqa.selenium.firefox.FirefoxOptions
import org.openqa.selenium.firefox.FirefoxDriver
import java.util.concurrent.TimeUnit
import java.lang.InterruptedException
import java.lang.ProcessHandle
import org.openqa.selenium.support.ui.ExpectedConditions
import org.openqa.selenium.By
import org.openqa.selenium.support.ui.WebDriverWait
import org.testng.Assert
import org.testng.annotations.AfterClass
import org.testng.annotations.BeforeClass
import org.testng.annotations.Test
import java.io.BufferedReader
import java.io.InputStreamReader
import java.net.Socket
import java.net.UnknownHostException

class TorTests {
    private lateinit var driver: WebDriver
    private lateinit var wait: WebDriverWait
    private lateinit var torProcess: Process

    @BeforeClass
    @Throws(IOException::class)
    fun testSetup() {
        System.setProperty("webdriver.gecko.driver", "resources\\geckodriver.exe")
        val torBinaryPath = "C:\\Users\\aangelov\\Desktop\\Tor Browser\\Browser\\firefox.exe"
        val runTime = Runtime.getRuntime()
        torProcess = runTime.exec("$torBinaryPath -n")
        val profile = FirefoxProfile()
        profile.setPreference("network.proxy.type", 1)
        profile.setPreference("network.proxy.socks", "127.0.0.1")
        profile.setPreference("network.proxy.socks_port", 9150)
        val firefoxOptions = FirefoxOptions()
        firefoxOptions.profile = profile
        driver = FirefoxDriver(firefoxOptions)
        driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS)
        driver.manage().window().maximize()
        wait = WebDriverWait(driver, 30)
    }

    @AfterClass
    @Throws(InterruptedException::class)
    fun afterClass() {
        driver.quit()
        torProcess.descendants().forEach { ph: ProcessHandle -> ph.destroy() }
        torProcess.destroyForcibly()
    }

    @Test
    fun open_tor_browser() {
        refreshTorIdentity("johnsmith")
        driver.navigate().to("http://whatismyipaddress.com/")
        val element = wait.until(ExpectedConditions.visibilityOfElementLocated(By.xpath("//*[@id='section_left']/div[2]")))
        Assert.assertNotEquals("151.80.16.169", element.text)
    }

    fun refreshTorIdentity(userName: String) {
        try {
            Socket("127.0.0.1", 9151).use { socket ->
                val output = socket.getOutputStream()
                val authenticationCommand = String.format("AUTHENTICATE \"%s\"\r\n", userName)
                output.write(authenticationCommand.toByteArray())
                output.write("SIGNAL NEWNYM\r\n".toByteArray())
                val input = socket.getInputStream()
                val reader = BufferedReader(InputStreamReader(input))
                val line = reader.readLine()
                if (!line.contains("250")) {
                    println("Unable to signal new user to server.")
                }
            }
        } catch (ex: UnknownHostException) {
            println("Server not found: " + ex.message)
        } catch (ex: IOException) {
            println("I/O error: " + ex.message)
        }
    }
}