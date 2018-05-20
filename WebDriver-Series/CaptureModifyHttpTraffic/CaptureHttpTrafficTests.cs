// <copyright file="CaptureHttpTrafficTests.cs" company="Automate The Planet Ltd.">
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

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;
using Proxy = Titanium.Web.Proxy.Http;

namespace CaptureModifyHttpTraffic
{
    [TestFixture]
    public class CaptureHttpTrafficTests
    {
        private IWebDriver _driver;
        private ProxyServer _proxyServer;
        private readonly IDictionary<int, Proxy.Request> _requestsHistory = 
            new ConcurrentDictionary<int, Proxy.Request>();
        private readonly IDictionary<int, Proxy.Response> _responsesHistory = 
            new ConcurrentDictionary<int, Proxy.Response>();

        [OneTimeSetUp]
        public void ClassInit()
        {
            _proxyServer = new ProxyServer();
            var explicitEndPoint = new ExplicitProxyEndPoint(System.Net.IPAddress.Any, 18882, true);
            _proxyServer.AddEndPoint(explicitEndPoint);
            _proxyServer.Start();
            _proxyServer.SetAsSystemHttpProxy(explicitEndPoint);
            _proxyServer.SetAsSystemHttpsProxy(explicitEndPoint);
            ////_proxyServer.BeforeRequest += OnRequestModifyTrafficEventHandler;
            _proxyServer.BeforeRequest += OnRequestCaptureTrafficEventHandler;
            ////_proxyServer.BeforeResponse += OnResponseModifyTrafficEventHandler;
            _proxyServer.BeforeResponse += OnResponseCaptureTrafficEventHandler;
        }

        [OneTimeTearDown]
        public void ClassCleanup()
        {
            _proxyServer.Stop();
        }

        [SetUp]
        public void TestInit()
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
            _driver = new ChromeDriver(options);
        }

        [TearDown]
        public void TestCleanup()
        {
            _driver.Dispose();
            _requestsHistory.Clear();
            _responsesHistory.Clear();
        }

        [Test]
        public void AnalyticsRequestMade_When_NavigateToHomePage()
        {
            _driver.Navigate().GoToUrl("https://automatetheplanet.com/");

            AssertRequestMade("analytics");
        }

        [Test]
        public void FontRequestMade_When_NavigateToHomePage()
        {
            _driver.Navigate().GoToUrl("https://automatetheplanet.com/");

            AssertRequestMade("fontawesome-webfont.woff2?v=4.7.0");
        }

        [Test]
        public void TestNoLargeImages_When_NavigateToHomePage()
        {
            _driver.Navigate().GoToUrl("https://automatetheplanet.com/");

            AssertNoLargeImagesRequested();
        }

        [Test]
        public void NoErrorCodes_When_NavigateToHomePage()
        {
            _driver.Navigate().GoToUrl("https://automatetheplanet.com/");

            AssertNoErrorCodes();
        }

        private void AssertNoErrorCodes()
        {
            bool areThereErrorCodes = _responsesHistory.Values.Any(r => r.StatusCode > 400 && r.StatusCode < 599);
            Assert.IsFalse(areThereErrorCodes);
        }

        private void AssertRequestMade(string url)
        {
            bool areRequestsMade = _requestsHistory.Values.Any(r => r.RequestUri.ToString().Contains(url));
            Assert.IsTrue(areRequestsMade);
        }

        private void AssertNoLargeImagesRequested()
        {
            bool areThereLargeImages = _requestsHistory.Values.Any(r => r.ContentType != null && r.ContentType.StartsWith("image") && r.ContentLength < 40000);
            Assert.IsFalse(areThereLargeImages);
        }

        private async Task OnRequestCaptureTrafficEventHandler(object sender, SessionEventArgs e) => await Task.Run(
            () =>
            {
                if (!_requestsHistory.ContainsKey(e.WebSession.Request.GetHashCode()) && e.WebSession != null && e.WebSession.Request != null)
                {
                    _requestsHistory.Add(e.WebSession.Request.GetHashCode(), e.WebSession.Request);
                }
            });

        private void OnRequestBlockResourceEventHandler(object sender, SessionEventArgs e)
        {
            if (e.WebSession.Request.RequestUri.ToString().Contains("analytics"))
            {
                string customBody = string.Empty;
                e.Ok(Encoding.UTF8.GetBytes(customBody));
            }
        }

        private void OnRequestRedirectTrafficEventHandler(object sender, SessionEventArgs e)
        {
            if (e.WebSession.Request.RequestUri.AbsoluteUri.Contains("logo.svg"))
            {
                e.Redirect("https://automatetheplanet.com/wp-content/uploads/2016/12/homepage-img-1.svg");
            }
        }

        private void OnRequestModifyTrafficEventHandler(object sender, SessionEventArgs e)
        {
            var method = e.WebSession.Request.Method.ToUpper();

            if ((method == "POST" || method == "PUT" || method == "PATCH" || method == "GET"))
            {
                //Get/Set request body bytes
                byte[] bodyBytes = e.GetRequestBody().Result;
                e.SetRequestBody(bodyBytes);

                //Get/Set request body as string
                string bodyString = e.GetRequestBodyAsString().Result;
                e.SetRequestBodyString(bodyString);
            }
        }

        private async Task OnResponseCaptureTrafficEventHandler(object sender, SessionEventArgs e) => await Task.Run(
            () =>
            {
                if (!_responsesHistory.ContainsKey(e.WebSession.Response.GetHashCode()) && e.WebSession != null && e.WebSession.Response != null)
                {
                    _responsesHistory.Add(e.WebSession.Response.GetHashCode(), e.WebSession.Response);
                }
            });

        private void OnResponseModifyTrafficEventHandler(object sender, SessionEventArgs e)
        {
            if (e.WebSession.Request.Method == "GET" || e.WebSession.Request.Method == "POST")
            {
                if (e.WebSession.Response.StatusCode == 200)
                {
                    if (e.WebSession.Response.ContentType != null && e.WebSession.Response.ContentType.Trim().ToLower().Contains("text/html"))
                    {
                        byte[] bodyBytes = e.GetResponseBody().Result;
                        e.SetResponseBody(bodyBytes);

                        string body = e.GetResponseBodyAsString().Result;
                        e.SetResponseBodyString(body);
                    }
                }
            }
        }
    }
}
