using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SoftwareManagementAutomationWindows
{
    [TestClass]
    public class SoftwareManagementAutomationTests
    {
        private static readonly string AssemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static IWebDriver _driver;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            SoftwareAutomationService.InstallRequiredSoftware();
        }

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _driver = new ChromeDriver(AssemblyFolder);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _driver?.Dispose();
        }

        [TestMethod]
        public void CheckCurrentIpAddressEqualToSetProxyIp()
        {
            _driver.Navigate().GoToUrl("https://whatismyipaddress.com/");
            var element = _driver.FindElement(By.XPath("//*[@id=\"ipv4\"]/a"));

            Console.WriteLine(element.Text);
        }
    }
}