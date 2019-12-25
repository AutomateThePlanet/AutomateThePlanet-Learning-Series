using BenchmarkDotNet.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace BenchmarkingAutomatedTesting
{
    public class BenchmarkSelectProvider
    {
        private const string TestPage = "http://htmlpreview.github.io/?https://github.com/angelovstanton/AutomateThePlanet/blob/master/WebDriver-Series/TestPage.html";
        private static IWebDriver _driver;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _driver = new ChromeDriver(DriverExecutablePathResolver.GetDriverExecutablePath());
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _driver?.Dispose();
        }

        [Benchmark(Baseline = true)]
        public void BenchmarkSelectElementSelect()
        {
            _driver.Navigate().GoToUrl(TestPage);
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
            _driver.Navigate().GoToUrl(TestPage);
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
