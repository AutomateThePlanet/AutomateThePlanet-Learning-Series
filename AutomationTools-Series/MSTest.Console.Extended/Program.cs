// <copyright file="Program.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>
using System;
using System.Configuration;
using log4net;
using MSTest.Console.Extended.Infrastructure;
using MSTest.Console.Extended.Services;

namespace MSTest.Console.Extended
{
    public class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            string microsoftTestConsoleExePath = ConfigurationManager.AppSettings["MSTestConsoleRunnerPath"]; 
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            var engine = new TestExecutionService(
                new MsTestTestRunProvider(consoleArgumentsProvider, LogManager.GetLogger(typeof(MsTestTestRunProvider))),
                new FileSystemProvider(consoleArgumentsProvider),
                new ProcessExecutionProvider(microsoftTestConsoleExePath, consoleArgumentsProvider, LogManager.GetLogger(typeof(ProcessExecutionProvider))),
                consoleArgumentsProvider,
                LogManager.GetLogger(typeof(TestExecutionService)));
            try
            {
                int result = engine.ExecuteWithRetry();
                Environment.Exit(result);
            }
            catch (Exception ex)
            {
                log.Error(string.Concat(ex.Message, ex.StackTrace));
            }
        }
    }
}