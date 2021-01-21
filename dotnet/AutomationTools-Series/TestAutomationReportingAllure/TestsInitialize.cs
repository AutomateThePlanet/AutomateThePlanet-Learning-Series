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
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string assemblyPath = Path.GetDirectoryName(path);
            return Path.Combine(assemblyPath, "allureConfig.json");
        }
    }
}
