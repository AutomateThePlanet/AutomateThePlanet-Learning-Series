// <copyright file="WebDriverFactory.cs" company="Automate The Planet Ltd.">
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
// <site>http://automatetheplanet.com/</site>
using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace SimpleFactoryDesignPatternProxies
{
    public static class WebDriverFactory
    {
        private static readonly string AssemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static IWebDriver CreateDriver(Browser browser)
        {

            string proxyUrl = ProxyService.GetProxyIp();
            var proxy = new Proxy
            {
                HttpProxy = proxyUrl,
                SslProxy = proxyUrl,
                FtpProxy = proxyUrl,
            };
         
            IWebDriver webDriver;
            switch (browser)
            {
                case Browser.Chrome:

                    var chromeOptions = new ChromeOptions
                    {
                        Proxy = proxy
                    };
                    var chromeDriverService = ChromeDriverService.CreateDefaultService(AssemblyFolder);
                    webDriver = new ChromeDriver(chromeDriverService, chromeOptions);
                    webDriver.Manage().Timeouts().PageLoad = 
                        TimeSpan.FromSeconds(ConfigurationService.Instance.GetWebSettings().Chrome.PageLoadTimeout);
                    webDriver.Manage().Timeouts().AsynchronousJavaScript = 
                        TimeSpan.FromSeconds(ConfigurationService.Instance.GetWebSettings().Chrome.ScriptTimeout);
                    break;
                case Browser.Firefox:
                    var firefoxOptions = new FirefoxOptions()
                    {
                        Proxy = proxy
                    };
                    webDriver = new FirefoxDriver(Environment.CurrentDirectory, firefoxOptions);
                    webDriver.Manage().Timeouts().PageLoad = 
                        TimeSpan.FromSeconds(ConfigurationService.Instance.GetWebSettings().Firefox.PageLoadTimeout);
                    webDriver.Manage().Timeouts().AsynchronousJavaScript = 
                        TimeSpan.FromSeconds(ConfigurationService.Instance.GetWebSettings().Firefox.ScriptTimeout);
                    break;
                case Browser.Edge:
                    var edgeOptions = new EdgeOptions()
                    {
                        Proxy = proxy
                    };
                    webDriver = new EdgeDriver(Environment.CurrentDirectory, edgeOptions);
                    webDriver.Manage().Timeouts().PageLoad = 
                        TimeSpan.FromSeconds(ConfigurationService.Instance.GetWebSettings().Edge.PageLoadTimeout);
                    webDriver.Manage().Timeouts().AsynchronousJavaScript = 
                        TimeSpan.FromSeconds(ConfigurationService.Instance.GetWebSettings().Edge.ScriptTimeout);
                    break;
                case Browser.InternetExplorer:
                    var ieOptions = new InternetExplorerOptions()
                    {
                        Proxy = proxy
                    };
                    webDriver = new InternetExplorerDriver(Environment.CurrentDirectory, ieOptions);
                    webDriver.Manage().Timeouts().PageLoad = 
                        TimeSpan.FromSeconds(ConfigurationService.Instance.GetWebSettings().InternetExplorer.PageLoadTimeout);
                    webDriver.Manage().Timeouts().AsynchronousJavaScript = 
                        TimeSpan.FromSeconds(ConfigurationService.Instance.GetWebSettings().InternetExplorer.ScriptTimeout);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
            }

            return webDriver;
        }
    }
}
