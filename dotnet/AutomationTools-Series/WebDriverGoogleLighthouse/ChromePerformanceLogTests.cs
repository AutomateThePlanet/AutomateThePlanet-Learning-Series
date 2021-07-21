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
    public class ChromePerformanceLogTests
    {
        private static ChromeDriver _driver;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            ChromeOptions options = new ChromeOptions();
            options.AddAdditionalCapability(CapabilityType.EnableProfiling, true, true);
            options.SetLoggingPreference("performance", LogLevel.All);
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArguments("--disable-storage-reset");
            _driver = new ChromeDriver(chromeDriverService, options);
            _driver.Manage().Window.Maximize();
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

            Console.WriteLine("Chrome Performance Metrics");
            var logs = _driver.Manage().Logs.GetLog("performance");
            for (int i = 0; i < logs.Count; i++)
            {
                Debug.WriteLine(logs[i].Message);
            }

            File.WriteAllLines("chromeMetrics.txt", logs.Select(m => m.Message));

            Console.WriteLine();
        }

        private IWebElement WaitAndFindElement(By by)
        {
            var webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            return webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
        }
    }
}