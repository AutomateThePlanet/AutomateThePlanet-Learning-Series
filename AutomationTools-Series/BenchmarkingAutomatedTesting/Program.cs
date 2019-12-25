// <copyright file="Program.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
// Licensed under the Royalty-free End-user License Agreement, Version 1.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://bellatrix.solutions/licensing-royalty-free/
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>

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
