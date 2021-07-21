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
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using RestSharp.Extensions;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace WebDriverGoogleLighthouse
{
    public class GoogleLighthousePerformanceSeleniumGridTests
    {
        private static IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--remote-debugging-address=0.0.0.0");
            options.AddArgument("--remote-debugging-port=5333");
            options.PlatformName = "Windows 10";

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _driver = new RemoteWebDriver(new Uri("http://127.0.0.1:4444/wd/hub"), options);
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Close();
            _driver?.Quit();
        }

        [Test]
        public void CallServlet()
        {
            var client = new RestClient("http://127.0.0.1:4444");
            var request = new RestRequest($"/grid/admin/HubRemoteHostRetrieverServlet/session/{((RemoteWebDriver)_driver).SessionId}/", Method.GET);
            var remoteDriver = (RemoteWebDriver)_driver;
            Console.WriteLine(remoteDriver);
            var queryResult = client.Execute(request);
            client = new RestClient($"http://{queryResult.Content}");
            request = new RestRequest($"/extra/LighthouseServlet", Method.GET);
            request.AddHeader("lighthouse", "http://demos.bellatrix.solutions/my-account/ --output=json,html,csv --screenEmulation.disabled --port=5333");
            var queryResult2 = client.Execute<Root>(request);
            Console.WriteLine(queryResult2.Content);
        }
    }
}