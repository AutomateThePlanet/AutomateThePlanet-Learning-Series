// <copyright file="AdvancedWebDriverUsageTests.cs" company="Automate The Planet Ltd.">
// Copyright 2017 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace AdvancedWebDriverTipsTricksPartOne
{
    [TestClass]
    public class AdvancedWebDriverUsageTests
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void SetupTest()
        {
            // 7. Use specific Firefox Profile
            ////FirefoxProfileManager profileManager = new FirefoxProfileManager();
            ////FirefoxProfile profile = profileManager.GetProfile("HARDDISKUSER");
            ////this.driver = new FirefoxDriver(profile);
            // 7.1. Set Chrome Options.
            ////ChromeOptions options = new ChromeOptions();
            ////// set some options
            ////DesiredCapabilities dc = DesiredCapabilities.Chrome();
            ////dc.SetCapability(ChromeOptions.Capability, options);
            ////IWebDriver driver = new RemoteWebDriver(dc);
            // 8. Turn off Java Script
            ////FirefoxProfileManager profileManager = new FirefoxProfileManager();
            ////FirefoxProfile profile = profileManager.GetProfile("HARDDISKUSER");
            ////profile.SetPreference("javascript.enabled", false);
            ////this.driver = new FirefoxDriver(profile);
            ////this.driver = new FirefoxDriver();
            ////var options = new InternetExplorerOptions();
            ////options.EnsureCleanSession = true;
            ////options.IgnoreZoomLevel = true;
            ////options.EnableNativeEvents = true;
            ////options.PageLoadStrategy = InternetExplorerPageLoadStrategy.Eager;
            ////this.driver = new InternetExplorerDriver(@"D:\Projects\PatternsInAutomation.Tests\WebDriver.Series.Tests\Drivers", options);
            _driver = new FirefoxDriver();
            _driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));         
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        // 1.1. Taking Screenshot. Full Screen.
        [TestMethod]
        public void WebDriverAdvancedUsage_TakingFullScrenenScreenshot()
        {
            _driver.Navigate().GoToUrl(@"http://automatetheplanet.com");
            WaitUntilLoaded();
            var tempFilePath = Path.GetTempFileName().Replace(".tmp", ".png");
            TakeFullScreenshot(_driver, tempFilePath);
        }

        // 1.2. Taking Screenshot. Full Screen.
        [TestMethod]
        public void WebDriverAdvancedUsage_TakingElementScreenshot()
        {
            _driver.Navigate().GoToUrl(@"http://automatetheplanet.com");
            WaitUntilLoaded();
            var tempFilePath = Path.GetTempFileName().Replace(".tmp", ".png");
            TakeScreenshotOfElement(_driver, By.XPath("//*[@id='tve_editor']/div[2]/div[2]/div/div"), tempFilePath);
        }

        // 2. How to set Page Load Timeout
        [TestMethod]
        public void SetPageLoadTimeout()
        {
            // 2.1. Set Default Page Load Timeout
            _driver.Manage().Timeouts().SetPageLoadTimeout(new TimeSpan(0, 0, 10));

            // 2.2. Wait Until Page is Fully Loaded via JS
            WaitUntilLoaded();

            // 2.3. Wait For Visibility of element
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='tve_editor']/div[2]/div[2]/div/div")));
        }

        
                                [TestMethod]
                                public void GetHtmlSourceOfWebElement()
                                {
                                    _driver.Navigate().GoToUrl(@"http://automatetheplanet.com");
                                    WaitUntilLoaded();
                                    var element = _driver.FindElement(By.XPath("//*[@id='tve_editor']/div[2]/div[3]/div/div"));
                                    var sourceHtml = element.GetAttribute("innerHTML");
                                    Debug.WriteLine(sourceHtml);
                                }

        [TestMethod]
        public void ExecuteJavaScript()
        {
            _driver.Navigate().GoToUrl(@"http://automatetheplanet.com");
            WaitUntilLoaded();
            var js = _driver as IJavaScriptExecutor;
            var title = (string)js.ExecuteScript("return document.title");
            Debug.WriteLine(title);
        }

        // 5. Execute in headless browser
        // Download binaries- http://phantomjs.org/download.html
        [TestMethod]
        public void ExecuteInHeadlessBrowser()
        {
            _driver = new PhantomJSDriver(@"D:\Projects\PatternsInAutomation.Tests\WebDriver.Series.Tests\Drivers");
            _driver.Navigate().GoToUrl(@"http://automatetheplanet.com");
            WaitUntilLoaded();
            var js = _driver as IJavaScriptExecutor;
            var title = (string)js.ExecuteScript("return document.title");
            Debug.WriteLine(title);
        }

        // 6. How to check if an element is visible
        [TestMethod]
        public void CheckIfElementIsVisible()
        {
            _driver.Navigate().GoToUrl(@"http://automatetheplanet.com");
            Assert.IsTrue(_driver.FindElement(By.XPath("//*[@id='tve_editor']/div[2]/div[2]/div/div")).Displayed);
        }

        // 9. Manage Cookies
        [TestMethod]
        public void ManageCookies()
        {
            _driver.Navigate().GoToUrl(@"http://automatetheplanet.com");

            // 9.1. Add new cookie
            var cookie = new OpenQA.Selenium.Cookie("key", "value");
            _driver.Manage().Cookies.AddCookie(cookie);

            // 9.2. Get All Cookies
            var cookies = _driver.Manage().Cookies.AllCookies;
            foreach (var currentCookie in cookies)
            {
                Debug.WriteLine(currentCookie.Value);
            }

            // 9.3. Delete Cookie by name
            _driver.Manage().Cookies.DeleteCookieNamed("CookieName");

            // 9.4. Delete All Cookies
            _driver.Manage().Cookies.DeleteAllCookies();

            // 9.5. Get Cookie by name
            var myCookie = _driver.Manage().Cookies.GetCookieNamed("CookieName");
            Debug.WriteLine(myCookie.Value);
        }

        // 10. Maximize Window
        [TestMethod]
        public void MaximizeWindow()
        {
            _driver.Navigate().GoToUrl(@"http://automatetheplanet.com");
            _driver.Manage().Window.Maximize();
        }

      
        // 8. Wait AJAX call to complete using JQuery
        public void WaitForAjaxComplete(int maxSeconds)
        {
            var isAjaxCallComplete = false;
            for (var i = 1; i <= maxSeconds; i++)
            {
                isAjaxCallComplete = (bool)((IJavaScriptExecutor)_driver).
                ExecuteScript("return window.jQuery != undefined && jQuery.active == 0");

                if (isAjaxCallComplete)
                {
                    return;
                }
                Thread.Sleep(1000);
            }
            throw new Exception(string.Format("Timed out after {0} seconds", maxSeconds));
        }

        private void WaitUntilLoaded()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until((x) =>
            {
                return ((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState").Equals("complete");
            });
        }

        public void TakeFullScreenshot(IWebDriver driver, String filename)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(filename, ImageFormat.Png);
        }

        public void TakeScreenshotOfElement(IWebDriver driver, By by, string fileName)
        {
            // 1. Make screenshot of all screen
            var screenshotDriver = driver as ITakesScreenshot;
            var screenshot = screenshotDriver.GetScreenshot();
            var bmpScreen = new Bitmap(new MemoryStream(screenshot.AsByteArray));

            // 2. Get screenshot of specific element
            var element = driver.FindElement(by);
            var cropArea = new Rectangle(element.Location, element.Size);
            var bitmap = bmpScreen.Clone(cropArea, bmpScreen.PixelFormat);
            bitmap.Save(fileName);
        }
    }
}