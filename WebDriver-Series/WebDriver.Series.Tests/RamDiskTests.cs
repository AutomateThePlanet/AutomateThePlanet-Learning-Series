using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace WebDriver.Series.Tests
{
    [TestClass]
    public class RamDiskTests
    {
        private IWebDriver driver;

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

            this.driver = new ChromeDriver(@"D:\Projects\PatternsInAutomation.Tests\WebDriver.Series.Tests\Drivers");
            this.driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));
        }

        [TestCleanup]
        public void TeardownTest()
        {
            this.driver.Quit();
        }

        [TestMethod]
        public void NavigateToUrlsTest()
        {
            this.Profile("Firefox-CacheProfile-RAMDrive", 10,
                () =>
                {
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878773/Implement-Copy-Paste-Csharp-Code");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878205/Read-Write-Windows-Registry-Csharp-VB-NET-Reusable");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/997954/SSRS-SQL-Server-Reporting-Services-Subscriptions-f");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/877859/Generic-Properties-Validator-Csharp-VB-NET-Code");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/886894/Reduced-AutoMapper-Auto-Map-Objects-Faster");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878910/Change-config-File-at-Runtime-Csharp-VB-NET-Code");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/877860/Assert-DateTime-the-Right-Way-MSTest-NUnit-Csharp");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878584/Manage-TFS-Test-Cases-in-MS-Test-Manager-Csharp-VB");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878774/Manage-TFS-Test-Suites-in-MS-Test-Manager-Csharp-V");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878583/Associate-Automated-Test-with-TFS-Test-Case-Csharp");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878912/Connect-to-TFS-Team-Project-Csharp-VB-NET-Code");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"https://dzone.com/articles/design-techniques-for-enhancing-unit-tests");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"https://dzone.com/articles/observer-design-pattern-design");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"https://dzone.com/articles/types-code-coverage-examples-c");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"https://dzone.com/articles/reduced-automapper-auto-map");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"https://dzone.com/articles/advanced-page-object-pattern-0");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"https://dzone.com/articles/design-patterns-in-automation-testing");
                    this.WaitUntilLoaded();
                    this.driver.Navigate().GoToUrl(@"https://dzone.com/articles/using-selenium-webdriver-tor-c");
                    this.WaitUntilLoaded();
                });
        }

        [TestMethod]
        public void NavigateToUrlsTestIE()
        {
            this.Profile("IE-SSD-Drive", 10,
                () =>
                {
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878773/Implement-Copy-Paste-Csharp-Code");
                    this.driver.Url = @"http://www.codeproject.com/Articles/878205/Read-Write-Windows-Registry-Csharp-VB-NET-Reusable";
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/997954/SSRS-SQL-Server-Reporting-Services-Subscriptions-f");
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/877859/Generic-Properties-Validator-Csharp-VB-NET-Code");
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/886894/Reduced-AutoMapper-Auto-Map-Objects-Faster");
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878910/Change-config-File-at-Runtime-Csharp-VB-NET-Code");
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/877860/Assert-DateTime-the-Right-Way-MSTest-NUnit-Csharp");
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878584/Manage-TFS-Test-Cases-in-MS-Test-Manager-Csharp-VB");
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878774/Manage-TFS-Test-Suites-in-MS-Test-Manager-Csharp-V");
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878583/Associate-Automated-Test-with-TFS-Test-Case-Csharp");
                    this.driver.Navigate().GoToUrl(@"http://www.codeproject.com/Articles/878912/Connect-to-TFS-Team-Project-Csharp-VB-NET-Code");
                    this.driver.Navigate().GoToUrl(@"https://dzone.com/articles/design-techniques-for-enhancing-unit-tests");
                    this.driver.Navigate().GoToUrl(@"https://dzone.com/articles/observer-design-pattern-design");
                    this.driver.Navigate().GoToUrl(@"https://dzone.com/articles/types-code-coverage-examples-c");
                    this.driver.Navigate().GoToUrl(@"https://dzone.com/articles/reduced-automapper-auto-map");
                    this.driver.Navigate().GoToUrl(@"https://dzone.com/articles/advanced-page-object-pattern-0");
                    this.driver.Navigate().GoToUrl(@"https://dzone.com/articles/design-patterns-in-automation-testing");
                    this.driver.Navigate().GoToUrl(@"https://dzone.com/articles/using-selenium-webdriver-tor-c");
                });
        }

        private void WaitUntilLoaded()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until((x) =>
            {
                return ((IJavaScriptExecutor)this.driver).ExecuteScript("return document.readyState").Equals("complete");
            });
        }

        private void Profile(string testResultsFileName, int iterations, Action actionToProfile)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string resultsPath = Path.Combine(desktopPath, "BenchmarkTestResults", string.Concat(testResultsFileName, "_", Guid.NewGuid().ToString(), ".txt"));
            StreamWriter writer = new StreamWriter(resultsPath);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < iterations; i++)
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