using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace HeadlessTestsEdge
{
    [TestFixture]
    public class HeadlessTestsExecutionTests
    {
        [Test]
        public void TestChromeExecutionTime()
        {
            Profile
            (
                "TestChromeExecutionTime",
                10,
                () =>
                {
                    using (IWebDriver driver = new ChromeDriver())
                    {
                        PerformTestOperations(driver);
                    }
                }
            );
        }

        [Test]
        public void TestFirefoxExecutionTime()
        {
            Profile
            (
                "TestFirefoxExecutionTime",
                1,
                () =>
                {
                    using (IWebDriver driver = new FirefoxDriver())
                    {
                        PerformTestOperations(driver);
                    }
                }
            );
        }

        [Test]
        public void TestEdgeHeadlessExecutionTime()
        {
            Profile
            (
                "TestEdgeHeadlessExecutionTime",
                10,
                () =>
                {
                    var edgeDriverService = Microsoft.Edge.SeleniumTools.EdgeDriverService.CreateChromiumService();
                    var edgeOptions = new Microsoft.Edge.SeleniumTools.EdgeOptions();
                    edgeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                    edgeOptions.UseChromium = true;
                    edgeOptions.AddArguments("--headless");
                    using (IWebDriver driver = new Microsoft.Edge.SeleniumTools.EdgeDriver(edgeDriverService, edgeOptions))
                    {
                        PerformTestOperations(driver);
                    }
                }
            );
        }

        [Test]
        public void TestEdgeExecutionTime()
        {
            Profile
            (
                "TestEdgeExecutionTime",
                10,
                () =>
                {
                    var edgeDriverService = Microsoft.Edge.SeleniumTools.EdgeDriverService.CreateChromiumService();
                    var edgeOptions = new Microsoft.Edge.SeleniumTools.EdgeOptions();
                    edgeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                    edgeOptions.UseChromium = true;
                    using (IWebDriver driver = new Microsoft.Edge.SeleniumTools.EdgeDriver(edgeDriverService, edgeOptions))
                    {
                        PerformTestOperations(driver);
                    }
                }
            );
        }

        [Test]
        public void TestChromeHeadlessExecutionTime()
        {
            Profile
            (
                "TestChromeHeadlessExecutionTime",
                10,
                () =>
                {
                    var options = new ChromeOptions();
                    options.AddArguments("headless");
                    using (IWebDriver driver = new ChromeDriver(options))
                    {
                        PerformTestOperations(driver);
                    }
                }
            );
        }

        [Test]
        public void TestFirefoxHeadlessExecutionTime()
        {
            Profile
            (
                "TestFirefoxHeadlessExecutionTime",
                1,
                () =>
                {
                    var options = new FirefoxOptions();
                    options.AddArguments("--headless");
                    using (IWebDriver driver = new FirefoxDriver(options))
                    {
                        PerformTestOperations(driver);
                    }
                }
            );
        }

        private void PerformTestOperations(IWebDriver driver)
        {
            string testPagePath = "http://htmlpreview.github.io/?https://github.com/angelovstanton/AutomateThePlanet/blob/master/WebDriver-Series/TestPage.html";
            driver.Navigate().GoToUrl(testPagePath);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var textBoxes = driver.FindElements(By.Name("fname"));
            foreach (var textBox in textBoxes)
            {
                textBox.SendKeys(Guid.NewGuid().ToString());
            }

            var selects = driver.FindElements(By.TagName("select"));
            foreach (var select in selects)
            {
                var selectElement = new SelectElement(select);
                selectElement.SelectByText("Mercedes");
            }

            var submits = driver.FindElements(By.XPath("//input[@type='submit']"));
            foreach (var submit in submits)
            {
                submit.Click();
            }

            var colors = driver.FindElements(By.XPath("//input[@type='color']"));
            foreach (var color in colors)
            {
                SetValueAttribute(driver, color, "#000000");
            }

            var dates = driver.FindElements(By.XPath("//input[@type='date']"));
            foreach (var date in dates)
            {
                SetValueAttribute(driver, date, "2020-06-01");
            }

            var radioButtons = driver.FindElements(By.XPath("//input[@type='radio']"));
            foreach (var radio in radioButtons)
            {
                radio.Click();
            }
        }

        private void SetValueAttribute(IWebDriver driver, IWebElement element, string value)
        {
            SetAttribute(driver, element, "value", value);
        }

        private void SetAttribute(IWebDriver driver, IWebElement element, string attributeName, string attributeValue)
        {
            driver.ExecuteJavaScript
            (
                "arguments[0].setAttribute(arguments[1], arguments[2]);",
                element,
                attributeName,
                attributeValue);
        }

        private void Profile(string testResultsFileName, int iterations, Action actionToProfile)
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string resultsDirPath = Path.Combine(desktopPath, "BenchmarkTestResults");
            Directory.CreateDirectory(resultsDirPath);
            var resultsPath = Path.Combine(resultsDirPath, string.Concat(testResultsFileName, "_", Guid.NewGuid().ToString(), ".txt"));
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
