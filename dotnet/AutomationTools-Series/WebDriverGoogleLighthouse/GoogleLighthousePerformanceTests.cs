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

namespace WebDriverGoogleLighthouse
{
    public class GoogleLighthousePerformanceTests
    {
        private static ChromeDriver _driver;
        private static int freePort;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            ChromeOptions options = new ChromeOptions();
            freePort = GetFreeTcpPort();
            options.AddArgument("--remote-debugging-address=0.0.0.0");
            options.AddArgument($"--remote-debugging-port={freePort}");
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

            ExecuteCommand($"lighthouse {_driver.Url} --output=json,html,csv --port={freePort}", true);
            var performanceReport = ReadPerformanceReport();

            Console.WriteLine("Display values");
            Console.WriteLine($"{performanceReport.Audits.FirstMeaningfulPaint.Title} = {performanceReport.Audits.FirstMeaningfulPaint.DisplayValue}");
            Console.WriteLine($"{performanceReport.Audits.FirstContentfulPaint.Title} = {performanceReport.Audits.FirstContentfulPaint.DisplayValue}");
            Console.WriteLine($"{performanceReport.Audits.SpeedIndex.Title} = {performanceReport.Audits.SpeedIndex.DisplayValue}");
            Console.WriteLine($"{performanceReport.Audits.LargestContentfulPaint.Title} = {performanceReport.Audits.LargestContentfulPaint.DisplayValue}");
            Console.WriteLine($"{performanceReport.Audits.Interactive.Title} = {performanceReport.Audits.Interactive.DisplayValue}");
            Console.WriteLine($"{performanceReport.Audits.TotalBlockingTime.Title} = {performanceReport.Audits.TotalBlockingTime.DisplayValue}");
            Console.WriteLine($"{performanceReport.Audits.CumulativeLayoutShift.Title} = {performanceReport.Audits.CumulativeLayoutShift.DisplayValue}");

            VerifyNoJavaScriptErrorsAppeared();
        }

        private Root ReadPerformanceReport()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var directoryInfo = new DirectoryInfo(assemblyFolder);
            string pattern = "*.report.json";
            var file = directoryInfo.GetFiles(pattern).OrderByDescending(f => f.LastWriteTime).First();
            string fileContent = File.ReadAllText(file.FullName);
            Root root = JsonConvert.DeserializeObject<Root>(fileContent);
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

        private static void VerifyNoJavaScriptErrorsAppeared()
        {
            var errorStrings = new List<string>
            {
                "SyntaxError",
                "EvalError",
                "ReferenceError",
                "RangeError",
                "TypeError",
                "URIError",
            };

            var jsErrors = _driver.Manage().Logs.GetLog(LogType.Browser).Where(x => errorStrings.Any(e => x.Message.Contains(e)));

            if (jsErrors.Any())
            {
                Assert.Fail($"JavaScript error(s): {System.Environment.NewLine} {jsErrors.Aggregate("", (s, entry) => s + entry.Message)} {System.Environment.NewLine}");
            }
        }
    }
}