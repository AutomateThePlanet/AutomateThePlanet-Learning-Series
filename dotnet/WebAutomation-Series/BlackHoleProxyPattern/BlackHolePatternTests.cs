// <copyright file="CaptureHttpTrafficTests.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;
using Proxy = Titanium.Web.Proxy.Http;

namespace BlackHoleProxyPattern
{
    [TestClass]
    public class BlackHolePatternTests
    {
        private static IWebDriver _driver;
        private static ProxyServer _proxyServer;
        private static IDictionary<int, Proxy.Request> _requestsHistory;
        private static IDictionary<int, Proxy.Response> _responsesHistory;
        private static ConcurrentBag<string> _blockUrls;

        [ClassInitialize]
        public static void OnClassInitialize(TestContext context)
        {
            _proxyServer = new ProxyServer();
            _blockUrls = new ConcurrentBag<string>();
            _responsesHistory = new ConcurrentDictionary<int, Proxy.Response>();
            _requestsHistory = new ConcurrentDictionary<int, Proxy.Request>();
            var explicitEndPoint = new ExplicitProxyEndPoint(System.Net.IPAddress.Any, 18882, true);
            _proxyServer.AddEndPoint(explicitEndPoint);
            _proxyServer.Start();
            _proxyServer.SetAsSystemHttpProxy(explicitEndPoint);
            _proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);
            _proxyServer.BeforeRequest += OnRequestBlockResourceEventHandler;
            _proxyServer.BeforeRequest += OnRequestCaptureTrafficEventHandler;
            _proxyServer.BeforeResponse += OnResponseCaptureTrafficEventHandler;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _proxyServer.Stop();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var proxy = new OpenQA.Selenium.Proxy
            {
                HttpProxy = "http://localhost:18882",
                SslProxy = "http://localhost:18882",
                FtpProxy = "http://localhost:18882"
            };
            var options = new ChromeOptions
            {
                Proxy = proxy
            };
            _driver = new ChromeDriver(Environment.CurrentDirectory, options);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Dispose();
            _requestsHistory.Clear();
            _responsesHistory.Clear();
        }

        [TestMethod]
        public void FontRequestsNotMade_When_FontRequestSetToBeBlocked()
        {
            _blockUrls.Add("fontawesome-webfont.woff");

            _driver.Navigate().GoToUrl("https://automatetheplanet.com/");
        }

        private static async Task OnRequestBlockResourceEventHandler(object sender, SessionEventArgs e) => await Task.Run(
            () =>
            {
                if (_blockUrls.Count > 0)
                {
                    foreach (var urlToBeBlocked in _blockUrls)
                    {
                        if (e.HttpClient.Request.RequestUri.ToString().Contains(urlToBeBlocked))
                        {
                            string customBody = string.Empty;
                            e.Ok(Encoding.UTF8.GetBytes(customBody));
                        }
                    }
                }
            });

        private static async Task OnRequestCaptureTrafficEventHandler(object sender, SessionEventArgs e) => await Task.Run(
            () =>
            {
                if (!_requestsHistory.ContainsKey(e.HttpClient.Request.GetHashCode()) && e.HttpClient.Request != null)
                {
                    _requestsHistory.Add(e.HttpClient.Request.GetHashCode(), e.HttpClient.Request);
                }
            });

        private static async Task OnResponseCaptureTrafficEventHandler(object sender, SessionEventArgs e) => await Task.Run(
            () =>
            {
                if (!_responsesHistory.ContainsKey(e.HttpClient.Response.GetHashCode()) && e.HttpClient.Response != null)
                {
                    _responsesHistory.Add(e.HttpClient.Response.GetHashCode(), e.HttpClient.Response);
                }
            });
    }
}