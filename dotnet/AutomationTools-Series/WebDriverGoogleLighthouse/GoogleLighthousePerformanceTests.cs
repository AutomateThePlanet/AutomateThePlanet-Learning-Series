using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace WebDriverGoogleLighthouse
{
    public class GoogleLighthousePerformanceTests
    {
        private ChromeDriver _driver;
        private static int freePort;

        [SetUp]
        public void Setup()
        {
            freePort = GetFreeTcpPort();
            ExecuteCommand($"chrome-debug --port={freePort}");
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--no-sandbox");
            options.AddArguments("--disable-storage-reset");
            ////options.AddArguments("--headless");
            options.DebuggerAddress = $"127.0.0.1:{freePort}";
            _driver = new ChromeDriver(chromeDriverService, options);
            _driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
            _driver.Quit();
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

            ExecuteCommand($"lighthouse http://demos.bellatrix.solutions/my-account/orders/ --output=json --output-path=./googleLightHouseResults.json --port={freePort}  --preset=desktop --screenEmulation.disabled --throttling-method=provided --no-emulatedUserAgent --only-categories=performance", true);
            var performanceReport = ReadPerformanceReport();

            Console.WriteLine($"{performanceReport.Audits.FirstMeaningfulPaint.Title} = {performanceReport.Audits.FirstMeaningfulPaint.DisplayValue}");
            Console.WriteLine($"{performanceReport.Audits.FirstContentfulPaint.Title} = {performanceReport.Audits.FirstContentfulPaint.DisplayValue}");
            Console.WriteLine($"{performanceReport.Audits.SpeedIndex.Title} = {performanceReport.Audits.SpeedIndex.DisplayValue}");
            Console.WriteLine($"{performanceReport.Audits.LargestContentfulPaint.Title} = {performanceReport.Audits.LargestContentfulPaint.DisplayValue}");
            Console.WriteLine($"{performanceReport.Audits.Interactive.Title} = {performanceReport.Audits.Interactive.DisplayValue}");
            Console.WriteLine($"{performanceReport.Audits.TotalBlockingTime.Title} = {performanceReport.Audits.TotalBlockingTime.DisplayValue}");
            Console.WriteLine($"{performanceReport.Audits.CumulativeLayoutShift.Title} = {performanceReport.Audits.CumulativeLayoutShift.DisplayValue}");
        }

        private Root ReadPerformanceReport()
        {
            string fileContent = File.ReadAllText("googleLightHouseResults.json");
            Root root =
                JsonConvert.DeserializeObject<Root>(fileContent);
            return root;
        }

        private IWebElement WaitAndFindElement(By by)
        {
            var webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            return webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
        }

        private void ExecuteCommand(string command, bool shouldWaitToExit = false)
        {
            var p = new Process();
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/c " + command;
            p.StartInfo = startInfo;
            p.Start();
            if (shouldWaitToExit)
            {
                p.WaitForExit();
            }
        }

        private static int GetFreeTcpPort()
        {
            Thread.Sleep(100);
            var tcpListener = new TcpListener(IPAddress.Loopback, 0);
            tcpListener.Start();
            int port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
            tcpListener.Stop();
            return port;
        }
    }
}