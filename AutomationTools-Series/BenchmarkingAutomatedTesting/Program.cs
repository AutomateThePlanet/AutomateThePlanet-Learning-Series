using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Running;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StackExchange.Profiling;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BenchmarkingAutomatedTesting
{
    //[InProcess]

    [CsvExporter]
    [HtmlExporter]
    [DisassemblyDiagnoser(printAsm: true, printSource: true)]
    [RyuJitX64Job]
    [MemoryDiagnoser]
    ////[EtwProfiler]
    class Program
    {
        static void Main(string[] args)
        {
            ////_driver = new ChromeDriver(AssemblyFolder);
            ////var config = DefaultConfig.Instance.With(ConfigOptions.DisableOptimizationsValidator);
            var summary = BenchmarkRunner.Run<BenchmarkProvider>();
            Console.WriteLine(summary);
            ////var summary = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance.With(new EtwProfiler()));
            ////Console.WriteLine(summary);
        }
    }

    public class BenchmarkProvider
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

        [IterationSetup(Target = nameof(BenchmarkY))]
        public void IterationSetupA()
        {
        }

        [IterationSetup(Target = nameof(BenchmarkD))]
        public void IterationSetupB()
        {
        }

        [IterationCleanup(Target = nameof(BenchmarkY))]
        public void IterationCleanupA()
        {
        }

        [IterationCleanup(Target = nameof(BenchmarkD))]
        public void IterationCleanupB()
        {
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _driver?.Dispose();
        }

        [Benchmark(Baseline = true)]
        public void BenchmarkY()
        {
            _driver.Navigate().GoToUrl("https://yandex.com/");
        }

        [Benchmark]
        public void BenchmarkD()
        {
            _driver.Navigate().GoToUrl("https://duckduckgo.com/");
        }
    }
}
