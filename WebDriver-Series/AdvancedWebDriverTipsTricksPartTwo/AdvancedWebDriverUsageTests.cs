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

namespace AdvancedWebDriverTipsTricksPartTwo
{
    [TestClass]
    public class AdvancedWebDriverUsageTests
    {
        private IWebDriver driver;

        [TestInitialize]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));
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
            driver.Quit();
        }

        // 1. Drag and Drop
        [TestMethod]
        public void DragAndDrop()
        {
            driver.Navigate().GoToUrl(@"http://loopj.com/jquery-simple-slider/");
            IWebElement element = driver.FindElement(By.XPath("//*[@id='project']/p[1]/div/div[2]"));
            Actions move = new Actions(driver);
            move.DragAndDropToOffset(element, 30, 0).Perform();
        }

        // 2. File Upload
        [TestMethod]
        public void FileUpload()
        {
            driver.Navigate().GoToUrl(@"https://demos.telerik.com/aspnet-ajax/ajaxpanel/application-scenarios/file-upload/defaultcs.aspx");
            IWebElement element = driver.FindElement(By.Id("ctl00_ContentPlaceholder1_RadUpload1file0"));
            String filePath = @"D:\Projects\PatternsInAutomation.Tests\WebDriver.Series.Tests\bin\Debug\WebDriver.xml";
            element.SendKeys(filePath);
        }

        // 3. JavaScript pop-ups
        [TestMethod]
        public void JavaScripPopUps()
        {
            driver.Navigate().GoToUrl(@"http://www.w3schools.com/js/tryit.asp?filename=tryjs_confirm");
            driver.SwitchTo().Frame("iframeResult");
            IWebElement button = driver.FindElement(By.XPath("/html/body/button"));
            button.Click();
            IAlert a = driver.SwitchTo().Alert();
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
            driver.Navigate().GoToUrl(@"http://automatetheplanet.com/compelling-sunday-14022016/");
            driver.FindElement(By.LinkText("10 Advanced WebDriver Tips and Tricks Part 1")).Click();
            driver.FindElement(By.LinkText("The Ultimate Guide To Unit Testing in ASP.NET MVC")).Click();
            ReadOnlyCollection<String> windowHandles = driver.WindowHandles;
            String firstTab = windowHandles.First();
            String lastTab = windowHandles.Last();
            driver.SwitchTo().Window(lastTab);
            Assert.AreEqual<string>("The Ultimate Guide To Unit Testing in ASP.NET MVC", driver.Title);
            driver.SwitchTo().Window(firstTab);
            Assert.AreEqual<string>("Compelling Sunday – 19 Posts on Programming and Quality Assurance", driver.Title);
        }

        // 5. Navigation History
        [TestMethod]
        public void NavigationHistory()
        {
            driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/1078541/Advanced-WebDriver-Tips-and-Tricks-Part");
            driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/1017816/Speed-up-Selenium-Tests-through-RAM-Facts-and-Myth");
            driver.Navigate().Back();
            Assert.AreEqual<string>("10 Advanced WebDriver Tips and Tricks - Part 1 - CodeProject", driver.Title);
            driver.Navigate().Refresh();
            Assert.AreEqual<string>("10 Advanced WebDriver Tips and Tricks - Part 1 - CodeProject", driver.Title);
            driver.Navigate().Forward();
            Assert.AreEqual<string>("Speed up Selenium Tests through RAM Facts and Myths - CodeProject", driver.Title);
        }

        // 9. Scroll focus to control
        [TestMethod]
        public void ScrollFocusToControl()
        {
            driver.Navigate().GoToUrl(@"http://automatetheplanet.com/compelling-sunday-14022016/");
            IWebElement link = driver.FindElement(By.PartialLinkText("Previous post"));
            string jsToBeExecuted = string.Format("window.scroll(0, {0});", link.Location.Y);
            ((IJavaScriptExecutor)driver).ExecuteScript(jsToBeExecuted);
            link.Click();
            Assert.AreEqual<string>("10 Advanced WebDriver Tips and Tricks - Part 1", driver.Title);
        }

        // 10. Focus on a Control
        [TestMethod]
        public void FocusOnControl()
        {
            driver.Navigate().GoToUrl(@"http://automatetheplanet.com/compelling-sunday-14022016/");
            IWebElement link = driver.FindElement(By.PartialLinkText("Previous post"));

            // 9.1. Option 1.
            link.SendKeys(string.Empty);

            // 9.1. Option 2.
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].focus();", link);
        }
    }
}