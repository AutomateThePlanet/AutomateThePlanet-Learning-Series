// <copyright file="ProxyService.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TextCopy;

namespace SimpleFactoryDesignPatternProxies
{
    public static class ProxyService
    {
        private static readonly ConcurrentBag<TimeBoundProxy> _activeProxies = new ConcurrentBag<TimeBoundProxy>();
        private static ConcurrentBag<string> _proxiesToCheck = new ConcurrentBag<string>();

        public static string CurrentProxyIp { get; set; }

        public static string GetProxyIp()
        {
            var newProxy = _activeProxies.ToList().OrderByDescending(x => x.LastlyUsed).First();
            newProxy.LastlyUsed = DateTime.Now;
            CurrentProxyIp = newProxy.ProxyIp;

            return newProxy.ProxyIp;
        }

        public static void GetListProxies()
        {
            var options = new ChromeOptions();
            var chromeDriverService = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
            using (IWebDriver driver = new ChromeDriver(chromeDriverService, options))
            {
                driver.Navigate().GoToUrl("https://www.proxy-list.download/HTTPS");
                driver.FindElement(By.Id("btn3")).Click();
                _proxiesToCheck = new ConcurrentBag<string>(Clipboard.GetText().Split(Environment.NewLine).Where(x => !string.IsNullOrEmpty(x)).ToList());
            }
        }

        public static void CheckProxiesStatus()
        {
            Parallel.ForEach(_proxiesToCheck,
                proxyToCheck =>
                {
                    if (PingHost(proxyToCheck))
                    {
                        _activeProxies.Add(new TimeBoundProxy(proxyToCheck));
                    }
                });
        }

        private static bool PingHost(string nameOrAddress)
        {
            var ip = nameOrAddress.Split(':').First();
            var port = int.Parse(nameOrAddress.Split(':').Last());
            Debug.WriteLine($"Ping address {nameOrAddress}");

            bool isProxyActive;
            try
            {
                var client = new TcpClient(ip, port);

                isProxyActive = true;
            }
            catch (Exception)
            {
                isProxyActive = false;
            }

            return isProxyActive;
        }
    }
}
