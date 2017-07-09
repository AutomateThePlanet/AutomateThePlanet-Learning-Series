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
// <site>https://automatetheplanet.com/</site>
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
using System.Reflection;
using System.Threading;

namespace AdvancedWebDriverTipsTricksPartTwo
{
    [TestClass]
    public class AdvancedWebDriverUsageTests
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void SetupTest()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            // 10 Advanced WebDriver Tips and Tricks Part 2
            // 6. Change Firefox user agent
            ////FirefoxProfileManager profileManager = new FirefoxProfileManager();
            ////FirefoxProfile profile = new FirefoxProfile();
            ////profile.SetPreference(
            ////"general.useragent.override",
            ////"Mozilla/5.0 (BlackBerry; U; BlackBerry 9900; en) AppleWebKit/534.11+ (KHTML, like Gecko) Version/7.1.0.346 Mobile Safari/534.11+");
            ////this.driver = new FirefoxDriver(profile);
            // 7. Set HTTP proxy for browser
            ////FirefoxProfile firefoxProfile = new FirefoxProfile();
            ////firefoxProfile.SetPreference("network.proxy.type", 1);
            ////firefoxProfile.SetPreference("network.proxy.http", "myproxy.com");
            ////firefoxProfile.SetPreference("network.proxy.http_port", 3239);
            ////driver = new FirefoxDriver(firefoxProfile);
            // 8.1. How to handle SSL certificate error Firefox Driver
            ////FirefoxProfile firefoxProfile = new FirefoxProfile();
            ////firefoxProfile.AcceptUntrustedCertificates = true;
            ////firefoxProfile.AssumeUntrustedCertificateIssuer = false;
            ////driver = new FirefoxDriver(firefoxProfile);
            // 8.2. Accept all certificates Chrome Driver
            ////DesiredCapabilities capability = DesiredCapabilities.Chrome();
            ////Environment.SetEnvironmentVariable("webdriver.chrome.driver", "C:\\Path\\To\\ChromeDriver.exe");
            ////capability.SetCapability(CapabilityType.AcceptSslCertificates, true);
            ////driver = new RemoteWebDriver(capability);
            // 8.3. Accept all certificates IE Driver
            ////DesiredCapabilities capability = DesiredCapabilities.InternetExplorer();
            ////Environment.SetEnvironmentVariable("webdriver.ie.driver", "C:\\Path\\To\\IEDriver.exe");
            ////capability.SetCapability(CapabilityType.AcceptSslCertificates, true);
            ////driver = new RemoteWebDriver(capability);         
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        // 1. Drag and Drop
        [TestMethod]
        public void DragAndDrop()
        {
            _driver.Navigate().GoToUrl(@"http://loopj.com/jquery-simple-slider/");
            var element = _driver.FindElement(By.XPath("//*[@id='project']/p[1]/div/div[2]"));
            var move = new Actions(_driver);
            move.DragAndDropToOffset(element, 30, 0).Perform();
        }

        // 2. File Upload
        [TestMethod]
        public void FileUpload()
        {
            _driver.Navigate().GoToUrl(@"https://demos.telerik.com/aspnet-ajax/ajaxpanel/application-scenarios/file-upload/defaultcs.aspx");
            var element = _driver.FindElement(By.Id("ctl00_ContentPlaceholder1_RadUpload1file0"));
            var filePath =  Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "WebDriver.xml");
            element.SendKeys(filePath);
        }

        // 3. JavaScript pop-ups
        [TestMethod]
        public void JavaScripPopUps()
        {
            _driver.Navigate().GoToUrl(@"http://www.w3schools.com/js/tryit.asp?filename=tryjs_confirm");
            _driver.SwitchTo().Frame("iframeResult");
            var button = _driver.FindElement(By.XPath("/html/body/button"));
            button.Click();
            var a = _driver.SwitchTo().Alert();
            if (a.Text.Equals("Press a button!"))
            {
                a.Accept();
            }
            else
            {
                a.Dismiss();
            }
        }

        // 4. Switch between browser windows or tabs
        [TestMethod]
        public void MovingBetweenTabs()
        {
            _driver.Navigate().GoToUrl(@"https://automatetheplanet.com/compelling-sunday-14022016/");
            _driver.FindElement(By.LinkText("10 Advanced WebDriver Tips and Tricks Part 1")).Click();
            var windowHandles = _driver.WindowHandles;
            var firstTab = windowHandles.First();
            var lastTab = windowHandles.Last();
            _driver.SwitchTo().Window(lastTab);
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("1. Taking a Screenshot")));
            Assert.AreEqual("10 Advanced WebDriver Tips and Tricks Part 1", _driver.Title);
            _driver.SwitchTo().Window(firstTab);
            Assert.AreEqual("Compelling Sunday – 19 Posts on Programming and QA", _driver.Title);
        }

        // 5. Navigation History
        [TestMethod]
        public void NavigationHistory()
        {
            _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/1078541/Advanced-WebDriver-Tips-and-Tricks-Part");
            _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/1017816/Speed-up-Selenium-Tests-through-RAM-Facts-and-Myth");
            _driver.Navigate().Back();
            Assert.AreEqual("10 Advanced WebDriver Tips and Tricks - Part 1 - CodeProject", _driver.Title);
            _driver.Navigate().Refresh();
            Assert.AreEqual("10 Advanced WebDriver Tips and Tricks - Part 1 - CodeProject", _driver.Title);
            _driver.Navigate().Forward();
            Assert.AreEqual("Speed up Selenium Tests through RAM Facts and Myths - CodeProject", _driver.Title);
        }

        // 9. Scroll focus to control
        [TestMethod]
        public void ScrollFocusToControl()
        {
            _driver.Navigate().GoToUrl(@"https://automatetheplanet.com/compelling-sunday-14022016/");
            var link = _driver.FindElement(By.LinkText("10 Advanced WebDriver Tips and Tricks Part 1"));
            var jsToBeExecuted = string.Format("window.scroll(0, {0});", link.Location.Y);
            ((IJavaScriptExecutor)_driver).ExecuteScript(jsToBeExecuted);
            link = _driver.FindElement(By.LinkText("10 Advanced WebDriver Tips and Tricks Part 1"));
            link.Click();
            Assert.AreEqual("10 Advanced WebDriver Tips and Tricks Part 1", _driver.Title);
        }

        // 10. Focus on a Control
        [TestMethod]
        public void FocusOnControl()
        {
            _driver.Navigate().GoToUrl(@"https://automatetheplanet.com/compelling-sunday-14022016/");
            var link = _driver.FindElement(By.PartialLinkText("Subscribe"));

            // 9.1. Option 1.
            link.SendKeys(string.Empty);

            // 9.1. Option 2.
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].focus();", link);
        }
    }
}