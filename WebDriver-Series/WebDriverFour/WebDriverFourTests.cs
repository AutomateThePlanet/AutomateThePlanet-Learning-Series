// <copyright file="RunTestsInSelenoid.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
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

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.Network;
using OpenQA.Selenium.DevTools.Security;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.Console;
using System.Diagnostics;
using OpenQA.Selenium.DevTools.Page;
using OpenQA.Selenium.Firefox;

namespace WebDriverFour
{
    [TestFixture]
    public class WebDriverFourTests
    {
        private ChromeDriver _driver;

        [SetUp]
        public void SetupTest()
        {
            _driver = new ChromeDriver();
            ////_driver.SwitchTo().NewWindow(WindowType.Tab);
            ////_driver.SwitchTo().NewWindow(WindowType.Window);
            ////_driver.SwitchTo().ParentFrame();
        }

        [TearDown]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [Test]
        public void ExperimentWithDevTools()
        {
            _driver.Navigate().GoToUrl("http://demos.bellatrix.solutions/");

            var devToolssession = _driver.CreateDevToolsSession();

            var setCacheDisabledCommandSettings = new SetCacheDisabledCommandSettings();
            setCacheDisabledCommandSettings.CacheDisabled = true;
            devToolssession.Network.SetCacheDisabled(setCacheDisabledCommandSettings);
            devToolssession.Network.ClearBrowserCache();

            var setExtraHTTPHeadersCommandSettings = new SetExtraHTTPHeadersCommandSettings();
            setExtraHTTPHeadersCommandSettings.Headers.Add("Accept-Encoding", "gzip, deflate");
            devToolssession.Network.SetExtraHTTPHeaders(setExtraHTTPHeadersCommandSettings);

            var captureScreenshotCommandSettings = new CaptureScreenshotCommandSettings();
            devToolssession.Page.CaptureScreenshot(captureScreenshotCommandSettings);

            EventHandler<LoadingFailedEventArgs> loadingFailed = (sender, e) =>
            {
                Assert.AreEqual(BlockedReason.Inspector, e.BlockedReason);
            };

            EventHandler<RequestInterceptedEventArgs> requestIntercepted = (sender, e) =>
            {
                Assert.IsTrue(e.Request.Url.EndsWith("jpg"));
            };
            
            RequestPattern requestPattern = new RequestPattern();
            requestPattern.InterceptionStage = InterceptionStage.HeadersReceived;
            requestPattern.ResourceType = ResourceType.Image;
            requestPattern.UrlPattern = "*.jpg";
            var setRequestInterceptionCommandSettings = new SetRequestInterceptionCommandSettings();
            setRequestInterceptionCommandSettings.Patterns = new RequestPattern[] { requestPattern };
            devToolssession.Network.SetRequestInterception(setRequestInterceptionCommandSettings);
            devToolssession.Network.RequestIntercepted += requestIntercepted;

            var setUserAgentOverrideCommandSettings = new SetUserAgentOverrideCommandSettings();
            setUserAgentOverrideCommandSettings.UserAgent = "Mozilla/5.0 CK={} (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
            devToolssession.Network.SetUserAgentOverride(setUserAgentOverrideCommandSettings);

            var blockedUrlSettings = new SetBlockedURLsCommandSettings();
            blockedUrlSettings.Urls = new string[] { "http://demos.bellatrix.solutions/wp-content/uploads/2018/04/440px-Launch_Vehicle__Verticalization__Proton-M-324x324.jpg" };
            devToolssession.Network.SetBlockedURLs(blockedUrlSettings);

            devToolssession.Performance.Enable();
            

            IWebElement imageTitle = _driver.FindElement(By.XPath("//h2[text()='Falcon 9']"));
            IWebElement falconSalesButton = _driver.FindElement(RelativeBy.WithTagName("span").Below(imageTitle));
            falconSalesButton.Click();

            var setIgnoreCertificateErrorsCommandSettings = new SetIgnoreCertificateErrorsCommandSettings();
            setIgnoreCertificateErrorsCommandSettings.Ignore = true;
            devToolssession.Security.SetIgnoreCertificateErrors(setIgnoreCertificateErrorsCommandSettings);

            EventHandler<MessageAddedEventArgs> messageAdded = (sender, e) =>
            {
                Assert.AreEqual("BELLATRIX is cool", e.Message);
            };
            devToolssession.Console.Enable();
            devToolssession.Console.ClearMessages();
            devToolssession.Console.MessageAdded += messageAdded;

            _driver.ExecuteScript("console.log('BELLATRIX is cool');");

            var emulationSettings = new EmulateNetworkConditionsCommandSettings();
            emulationSettings.ConnectionType = ConnectionType.Cellular3g;
            emulationSettings.DownloadThroughput = 20;
            emulationSettings.Latency = 1.2;
            emulationSettings.UploadThroughput = 50;
            devToolssession.Network.EmulateNetworkConditions(emulationSettings);

            var metrics = devToolssession.Performance.GetMetrics();

            foreach (var metric in metrics.Result.Metrics)
            {
                Console.WriteLine($"{metric.Name} = {metric.Value}");
            }
        }
    }
}