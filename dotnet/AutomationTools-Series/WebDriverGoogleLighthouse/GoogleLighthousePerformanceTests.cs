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
    public class GoogleLighthousePerformanceTests
    {
        private static ChromeDriver _driver;
        private static int freePort;
        private static IDevToolsSession _devTools;
        private static DevTools.DevToolsSessionDomains _devToolsSession;

        [SetUp]
        public void Setup()
        {
            freePort = GetFreeTcpPort();
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            ChromeOptions options = new ChromeOptions();
            options.AddAdditionalCapability(CapabilityType.EnableProfiling, true, true);
            options.SetLoggingPreference("performance", LogLevel.All);
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--remote-debugging-address=0.0.0.0");
            options.AddArgument("--remote-debugging-port=5333");
            options.AddArguments("--disable-storage-reset");
            options.AddArguments("--disable-storage-reset");
            options.DebuggerAddress = $"127.0.0.1:{freePort}";
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

            IWebElement imageTitle = _driver.FindElement(By.XPath("//h2[text()='Falcon 9']"));
            IWebElement falconSalesButton = _driver.FindElement(RelativeBy.WithTagName("span").Below(By.XPath("")).LeftOf(By.Id("")));

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

            Console.WriteLine();
            Console.WriteLine("Chrome Performance Metrics");
            var logs = _driver.Manage().Logs.GetLog("performance");
            for (int i = 0; i < logs.Count; i++)
            {
                Debug.WriteLine(logs[i].Message);
            }

            File.WriteAllLines("chromeMetrics.txt", logs.Select(m => m.Message));

            Console.WriteLine();
            Console.WriteLine("DevTools Performance Metrics");
            var metrics = _devToolsSession.Performance.GetMetrics();
            foreach (var metric in metrics.Result.Metrics)
            {
                Console.WriteLine($"{metric.Name} = {metric.Value}");
            }

            File.WriteAllLines("devToolsMetrics.txt", _devToolsSession.Performance.GetMetrics().Result.Metrics.Select(m => $"{m.Name} = {m.Value}"));

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