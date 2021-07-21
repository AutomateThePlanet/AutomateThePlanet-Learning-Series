using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using RestSharp.Extensions;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using DevTools = OpenQA.Selenium.DevTools.V91;

namespace WebDriverGoogleLighthouse
{
    public class DevToolsPerformanceTests
    {
        private static ChromeDriver _driver;
        private static IDevToolsSession _devTools;
        private static DevTools.DevToolsSessionDomains _devToolsSession;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            ChromeOptions options = new ChromeOptions();
            _driver = new ChromeDriver(chromeDriverService, options);
            _driver.Manage().Window.Maximize();

            _devTools = _driver.GetDevToolsSession();
            _devToolsSession = _devTools.GetVersionSpecificDomains<DevTools.DevToolsSessionDomains>();
            _devToolsSession.Performance.Enable(new DevTools.Performance.EnableCommandSettings());
            _devToolsSession.Network.Enable(new DevTools.Network.EnableCommandSettings());
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Close();
            _driver?.Quit();
        }

        [Test]
        public void LighthouseMetricsCheck()
        {
            _driver.Navigate().GoToUrl("http://demos.bellatrix.solutions/");

            var myAccountLink = _driver.FindElement(By.LinkText("My account"));
            myAccountLink.Click();
            var userName = _driver.FindElement(By.Id("username"));
            userName.SendKeys("info@berlinspaceflowers.com");
            var password = _driver.FindElement(By.Id("password"));
            password.SendKeys("@purISQzt%%DYBnLCIhaoG6$");
            var loginButton = _driver.FindElement(By.XPath("//button[@name='login']"));
            loginButton.Click();

            WaitAndFindElement(By.LinkText("Orders"));

            Console.WriteLine();
            Console.WriteLine("DevTools Performance Metrics");
            var metrics = _devToolsSession.Performance.GetMetrics();
            foreach (var metric in metrics.Result.Metrics)
            {
                Console.WriteLine($"{metric.Name} = {metric.Value}");
            }

            File.WriteAllLines("devToolsMetrics.txt", _devToolsSession.Performance.GetMetrics().Result.Metrics.Select(m => $"{m.Name} = {m.Value}"));
        }

        private IWebElement WaitAndFindElement(By by)
        {
            var webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            return webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
        }
    }
}