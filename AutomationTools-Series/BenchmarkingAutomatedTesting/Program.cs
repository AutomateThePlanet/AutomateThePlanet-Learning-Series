using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Running;
using StackExchange.Profiling;
using System;

namespace BenchmarkingAutomatedTesting
{
    //[InProcess]

    [CsvExporter]
    [HtmlExporter]
    [DisassemblyDiagnoser(printAsm: true, printSource: true)]
    [RyuJitX64Job]
    [MemoryDiagnoser]
    ////[EtwProfiler]
    public class Program
    {
        static void Main(string[] args)
        {
            ////_driver = new ChromeDriver(AssemblyFolder);
            ////var config = DefaultConfig.Instance.With(ConfigOptions.DisableOptimizationsValidator);
            var summary = BenchmarkRunner.Run<BenchmarkButtonClickProvider>();
            Console.WriteLine(summary);
            ////var summary = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, DefaultConfig.Instance.With(new EtwProfiler()));
            ////Console.WriteLine(summary);
        }
    }
}
