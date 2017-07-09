// <copyright file="RamDiskTests.cs" company="Automate The Planet Ltd.">
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
// <site>http://automatetheplanet.com/</site>
using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace SpeedUpSeleniumTestsRamFactsMyths
{
    [TestClass]
    public class RamDiskTests
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void SetupTest()
        {
            //FirefoxProfileManager profileManager = new FirefoxProfileManager();
            ////FirefoxProfile profile = profileManager.GetProfile("HARDDISKUSER");
            //FirefoxProfile profile = profileManager.GetProfile("RAMUSER");
            //this.driver = new FirefoxDriver(profile);
            ////this.driver = new FirefoxDriver();

            ////var options = new InternetExplorerOptions();
            ////options.EnsureCleanSession = true;
            ////options.IgnoreZoomLevel = true;
            ////options.EnableNativeEvents = true;
            ////options.PageLoadStrategy = InternetExplorerPageLoadStrategy.Eager;
            ////this.driver = new InternetExplorerDriver(@"D:\Projects\PatternsInAutomation.Tests\WebDriver.Series.Tests\Drivers", options);

            _driver = new ChromeDriver(@"D:\Projects\PatternsInAutomation.Tests\WebDriver.Series.Tests\Drivers");
            _driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            _driver.Quit();
        }

        [TestMethod]
        public void NavigateToUrlsTest()
        {
            Profile("Firefox-CacheProfile-RAMDrive", 10,
                () =>
                {
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878773/Implement-Copy-Paste-Csharp-Code");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878205/Read-Write-Windows-Registry-Csharp-VB-NET-Reusable");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/997954/SSRS-SQL-Server-Reporting-Services-Subscriptions-f");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/877859/Generic-Properties-Validator-Csharp-VB-NET-Code");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/886894/Reduced-AutoMapper-Auto-Map-Objects-Faster");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878910/Change-config-File-at-Runtime-Csharp-VB-NET-Code");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/877860/Assert-DateTime-the-Right-Way-MSTest-NUnit-Csharp");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878584/Manage-TFS-Test-Cases-in-MS-Test-Manager-Csharp-VB");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878774/Manage-TFS-Test-Suites-in-MS-Test-Manager-Csharp-V");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878583/Associate-Automated-Test-with-TFS-Test-Case-Csharp");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878912/Connect-to-TFS-Team-Project-Csharp-VB-NET-Code");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"https://dzone.com/articles/design-techniques-for-enhancing-unit-tests");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"https://dzone.com/articles/observer-design-pattern-design");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"https://dzone.com/articles/types-code-coverage-examples-c");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"https://dzone.com/articles/reduced-automapper-auto-map");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"https://dzone.com/articles/advanced-page-object-pattern-0");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"https://dzone.com/articles/design-patterns-in-automation-testing");
                    WaitUntilLoaded();
                    _driver.Navigate().GoToUrl(@"https://dzone.com/articles/using-selenium-webdriver-tor-c");
                    WaitUntilLoaded();
                });
        }

        [TestMethod]
        public void NavigateToUrlsTestIe()
        {
            Profile("IE-SSD-Drive", 10,
                () =>
                {
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878773/Implement-Copy-Paste-Csharp-Code");
                    _driver.Url = @"http://www.codeproject.com/Articles/878205/Read-Write-Windows-Registry-Csharp-VB-NET-Reusable";
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/997954/SSRS-SQL-Server-Reporting-Services-Subscriptions-f");
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/877859/Generic-Properties-Validator-Csharp-VB-NET-Code");
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/886894/Reduced-AutoMapper-Auto-Map-Objects-Faster");
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878910/Change-config-File-at-Runtime-Csharp-VB-NET-Code");
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/877860/Assert-DateTime-the-Right-Way-MSTest-NUnit-Csharp");
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878584/Manage-TFS-Test-Cases-in-MS-Test-Manager-Csharp-VB");
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878774/Manage-TFS-Test-Suites-in-MS-Test-Manager-Csharp-V");
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878583/Associate-Automated-Test-with-TFS-Test-Case-Csharp");
                    _driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878912/Connect-to-TFS-Team-Project-Csharp-VB-NET-Code");
                    _driver.Navigate().GoToUrl(@"https://dzone.com/articles/design-techniques-for-enhancing-unit-tests");
                    _driver.Navigate().GoToUrl(@"https://dzone.com/articles/observer-design-pattern-design");
                    _driver.Navigate().GoToUrl(@"https://dzone.com/articles/types-code-coverage-examples-c");
                    _driver.Navigate().GoToUrl(@"https://dzone.com/articles/reduced-automapper-auto-map");
                    _driver.Navigate().GoToUrl(@"https://dzone.com/articles/advanced-page-object-pattern-0");
                    _driver.Navigate().GoToUrl(@"https://dzone.com/articles/design-patterns-in-automation-testing");
                    _driver.Navigate().GoToUrl(@"https://dzone.com/articles/using-selenium-webdriver-tor-c");
                });
        }

        private void WaitUntilLoaded()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until((x) =>
            {
                return ((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState").Equals("complete");
            });
        }

        private void Profile(string testResultsFileName, int iterations, Action actionToProfile)
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var resultsPath = Path.Combine(desktopPath, "BenchmarkTestResults", string.Concat(testResultsFileName, "_", Guid.NewGuid().ToString(), ".txt"));
            var writer = new StreamWriter(resultsPath);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var watch = new Stopwatch();
            watch.Start();
            for (var i = 0; i < iterations; i++)
            {
                actionToProfile();
            }
            watch.Stop();
            writer.WriteLine("Total: {0:0.00} ms ({1:N0} ticks) (over {2:N0} iterations)",
                watch.ElapsedMilliseconds, watch.ElapsedTicks, iterations);
            var avgElapsedMillisecondsPerRun = watch.ElapsedMilliseconds / iterations;
            var avgElapsedTicksPerRun = watch.ElapsedMilliseconds / iterations;
            writer.WriteLine("AVG: {0:0.00} ms ({1:N0} ticks) (over {2:N0} iterations)",
                avgElapsedMillisecondsPerRun, avgElapsedTicksPerRun, iterations);
            writer.Flush();
            writer.Close();
        }
    }
}