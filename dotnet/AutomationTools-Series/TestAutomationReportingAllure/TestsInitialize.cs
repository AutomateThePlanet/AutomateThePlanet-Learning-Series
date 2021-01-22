using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace TestAutomationReportingAllure
{
    [SetUpFixture]
    public class TestsInitialize
    {
        [OneTimeSetUp]
        public void AssemblyInitialize()
        {
            string allureConfigPath = GetAllureConfigPath();
            Environment.SetEnvironmentVariable("ALLURE_CONFIG", allureConfigPath);
        }

        public string GetAllureConfigPath()
        {
            var codeBase = Assembly.GetExecutingAssembly().Location;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.Combine(path, "allureConfig.json");
        }
    }
}
