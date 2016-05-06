// <copyright file="AdvancedWebDriverUsageTests.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
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

namespace AdvancedWebDriverTipsTricksPartThree
{
    [TestClass]
    public class AdvancedWebDriverUsageTests
    {
        private IWebDriver driver;

        [TestInitialize]
        public void SetupTest()
        {
            this.driver = new FirefoxDriver();
            this.driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));
            // 10 Advanced WebDriver Tips and Tricks Part 3
            // 1. Start FirefoxDriver with plugins
            ////FirefoxProfile profile = new FirefoxProfile();
            ////profile.AddExtension(@"C:\extensionsLocation\extension.xpi"); 
            ////IWebDriver driver = new FirefoxDriver(profile);
            // 2. Set HTTP proxy ChromeDriver
            ////ChromeOptions options = new ChromeOptions();
            ////var proxy = new Proxy();
            ////proxy.Kind = ProxyKind.Manual;
            ////proxy.IsAutoDetect = false;
            ////proxy.HttpProxy =
            ////proxy.SslProxy = "127.0.0.1:3239";
            ////options.Proxy = proxy;
            ////options.AddArgument("ignore-certificate-errors"); 
            ////IWebDriver driver = new ChromeDriver(options);
            // 3. Set HTTP proxy with authentication ChromeDriver
            ////ChromeOptions options = new ChromeOptions();
            ////var proxy = new Proxy();
            ////proxy.Kind = ProxyKind.Manual;
            ////proxy.IsAutoDetect = false;
            ////proxy.HttpProxy =
            ////proxy.SslProxy = "127.0.0.1:3239";
            ////options.Proxy = proxy;
            ////options.AddArguments("--proxy-server=http://user:password@127.0.0.1:3239");
            ////options.AddArgument("ignore-certificate-errors");
            ////IWebDriver driver = new ChromeDriver(options);
            // 4. Start ChromeDriver with an unpacked extension
            ////ChromeOptions options = new ChromeOptions();
            ////options.AddArguments("load-extension=/pathTo/extension");
            ////DesiredCapabilities capabilities = new DesiredCapabilities();
            ////capabilities.SetCapability(ChromeOptions.Capability, options);
            ////DesiredCapabilities dc = DesiredCapabilities.Chrome();
            ////dc.SetCapability(ChromeOptions.Capability, options);
            ////IWebDriver driver = new RemoteWebDriver(dc);
            // 5. Start ChromeDriver with an packed extension
            ////ChromeOptions options = new ChromeOptions();
            ////options.AddExtension(Path.GetFullPath("local/path/to/extension.crx"));
            ////DesiredCapabilities capabilities = new DesiredCapabilities();
            ////capabilities.SetCapability(ChromeOptions.Capability, options);
            ////DesiredCapabilities dc = DesiredCapabilities.Chrome();
            ////dc.SetCapability(ChromeOptions.Capability, options);
            ////IWebDriver driver = new RemoteWebDriver(dc);
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.driver.Quit();
        }

        // 10 Advanced WebDriver Tips and Tricks Part 3
        // 6. Assert a button enabled or disabled
        [TestMethod]
        public void AssertButtonEnabledDisabled()
        {
            this.driver.Navigate().GoToUrl(@"http://www.w3schools.com/tags/tryit.asp?filename=tryhtml_button_disabled");
            this.driver.SwitchTo().Frame("iframeResult");
            IWebElement button = driver.FindElement(By.XPath("/html/body/button"));
            Assert.IsFalse(button.Enabled);
        }

        // 7. Set and assert the value of a hidden field
        [TestMethod]
        public void SetHiddenField()
        {
            ////<input type="hidden" name="country" value="Bulgaria"/>
            IWebElement theHiddenElem = driver.FindElement(By.Name("country"));
            string hiddenFieldValue = theHiddenElem.GetAttribute("value");
            Assert.AreEqual("Bulgaria", hiddenFieldValue);
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value='Germany';",
                theHiddenElem);
            hiddenFieldValue = theHiddenElem.GetAttribute("value");
            Assert.AreEqual("Germany", hiddenFieldValue);
        }

        // 9. Verify File Download ChromeDriver
        [TestMethod]
        public void VerifyFileDownloadChrome()
        {
            string expectedFilePath = @"c:\temp\Testing_Framework_2015_3_1314_2_Free.exe";
            try
            {
                String downloadFolderPath = @"c:\temp\";
                var options = new ChromeOptions();
                options.AddUserProfilePreference("download.default_directory", downloadFolderPath);
                driver = new ChromeDriver(options);

                driver.Navigate().GoToUrl("https://www.telerik.com/download-trial-file/v2/telerik-testing-framework");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until((x) =>
                {
                    return File.Exists(expectedFilePath);
                });
                FileInfo fileInfo = new FileInfo(expectedFilePath);
                long fileSize = fileInfo.Length;
                Assert.AreEqual(4326192, fileSize);
            }
            finally
            {
                if (File.Exists(expectedFilePath))
                {
                    File.Delete(expectedFilePath);
                }
            }
        }

        // 10. Verify File Download FirefoxDriver
        [TestMethod]
        public void VerifyFileDownloadFirefox()
        {
            string expectedFilePath = @"c:\temp\Testing_Framework_2015_3_1314_2_Free.exe";
            try
            {
                String downloadFolderPath = @"c:\temp\";
                FirefoxProfile profile = new FirefoxProfile();
                profile.SetPreference("browser.download.folderList", 2);
                profile.SetPreference("browser.download.dir", downloadFolderPath);
                profile.SetPreference("browser.download.manager.alertOnEXEOpen", false);
                profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/msword, application/binary, application/ris, text/csv, image/png, application/pdf, text/html, text/plain, application/zip, application/x-zip, application/x-zip-compressed, application/download, application/octet-stream");
                this.driver = new FirefoxDriver(profile);

                driver.Navigate().GoToUrl("https://www.telerik.com/download-trial-file/v2/telerik-testing-framework");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until((x) =>
                {
                    return File.Exists(expectedFilePath);
                });
                FileInfo fileInfo = new FileInfo(expectedFilePath);
                long fileSize = fileInfo.Length;
                Assert.AreEqual(4326192, fileSize);
            }
            finally
            {
                if (File.Exists(expectedFilePath))
                {
                    File.Delete(expectedFilePath);
                }
            }
        }
    }
}