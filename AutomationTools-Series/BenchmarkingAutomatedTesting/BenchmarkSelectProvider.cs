using BenchmarkDotNet.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Reflection;

namespace BenchmarkingAutomatedTesting
{
    public class BenchmarkSelectProvider
    {
        private static IWebDriver _driver;
        private static readonly string AssemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        [GlobalSetup]
        public void GlobalSetup()
        {
            ////string currentDir = Environment.CurrentDirectory;
            ////string[] files = Directory.GetFiles(currentDir, "chromedriver.exe", SearchOption.AllDirectories);
            _driver = new ChromeDriver(@"D:\SourceCode\AutomateThePlanet-Blog\AutomationTools-Series\BenchmarkingAutomatedTesting\bin\Release\netcoreapp3.1");
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _driver?.Dispose();
        }

        [Benchmark(Baseline = true)]
        public void BenchmarkSelectElementSelect()
        {
            string testPagePath = "http://htmlpreview.github.io/?https://github.com/angelovstanton/AutomateThePlanet/blob/master/WebDriver-Series/TestPage.html";
            _driver.Navigate().GoToUrl(testPagePath);
            var selects = _driver.FindElements(By.TagName("select"));
            foreach (var select in selects)
            {
                var selectElement = new SelectElement(select);
                selectElement.SelectByText("Mercedes");
            }
        }

        [Benchmark]
        public void BenchmarkCustomSelect()
        {
            string testPagePath = "http://htmlpreview.github.io/?https://github.com/angelovstanton/AutomateThePlanet/blob/master/WebDriver-Series/TestPage.html";
            _driver.Navigate().GoToUrl(testPagePath);
            var selects = _driver.FindElements(By.TagName("select"));
            foreach (var select in selects)
            {
                select.Click();
                var mercedesOption = select.FindElement(By.XPath("./option[@value='mercedes']"));
                mercedesOption.Click();
            }
        }
    }
}
