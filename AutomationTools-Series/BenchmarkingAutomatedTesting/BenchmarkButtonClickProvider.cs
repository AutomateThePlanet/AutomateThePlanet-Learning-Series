using BenchmarkDotNet.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;

namespace BenchmarkingAutomatedTesting
{
    public class BenchmarkButtonClickProvider
    {
        private static IWebDriver _driver;
        private static IJavaScriptExecutor _javaScriptExecutor;
        private static readonly string AssemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        [GlobalSetup]
        public void GlobalSetup()
        {
            ////string currentDir = Environment.CurrentDirectory;
            ////string[] files = Directory.GetFiles(currentDir, "chromedriver.exe", SearchOption.AllDirectories);
            _driver = new ChromeDriver(@"D:\SourceCode\AutomateThePlanet-Blog\AutomationTools-Series\BenchmarkingAutomatedTesting\bin\Release\netcoreapp3.1");
            _javaScriptExecutor = (IJavaScriptExecutor)_driver;
            string testPagePath = "http://htmlpreview.github.io/?https://github.com/angelovstanton/AutomateThePlanet/blob/master/WebDriver-Series/TestPage.html";
            _driver.Navigate().GoToUrl(testPagePath);
        }

        [IterationSetup(Target = nameof(BenchmarkWebDriverClick))]
        public void IterationSetupA()
        {
        }

        [IterationSetup(Target = nameof(BenchmarkJavaScriptClick))]
        public void IterationSetupB()
        {
        }

        [IterationCleanup(Target = nameof(BenchmarkWebDriverClick))]
        public void IterationCleanupA()
        {
        }

        [IterationCleanup(Target = nameof(BenchmarkJavaScriptClick))]
        public void IterationCleanupB()
        {
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _driver?.Dispose();
        }

        [Benchmark(Baseline = true)]
        public void BenchmarkWebDriverClick()
        {
            var buttons = _driver.FindElements(By.XPath("//input[@value='Submit']"));
            foreach (var button in buttons)
            {
                button.Click();
            }
        }

        [Benchmark]
        public void BenchmarkJavaScriptClick()
        {
            var buttons = _driver.FindElements(By.XPath("//input[@value='Submit']"));
            foreach (var button in buttons)
            {
                _javaScriptExecutor.ExecuteScript("arguments[0].click();", button);
            }
        }
    }
}
