// <copyright file="ProcessExecutionProvider.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using log4net;
using MSTest.Console.Extended.Interfaces;

namespace MSTest.Console.Extended.Infrastructure
{
    public class ProcessExecutionProvider : IProcessExecutionProvider
    {
        private readonly ILog _log;
        private readonly IConsoleArgumentsProvider _consoleArgumentsProvider;

        public ProcessExecutionProvider(string microsoftTestConsoleExePath, IConsoleArgumentsProvider consoleArgumentsProvider, ILog log)
        {
            MicrosoftTestConsoleExePath = microsoftTestConsoleExePath;
            this._consoleArgumentsProvider = consoleArgumentsProvider;
            this._log = log;
        }

        public string MicrosoftTestConsoleExePath { get; set; }

        public Process CurrentProcess { get; set; }

        public void ExecuteProcessWithAdditionalArguments(string arguments = "")
        {
            if (string.IsNullOrEmpty(arguments))
            {
                arguments = _consoleArgumentsProvider.ConsoleArguments;
            }
            CurrentProcess = new Process();
            var processStartInfo = new ProcessStartInfo(MicrosoftTestConsoleExePath, arguments);
            processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.UseShellExecute = false;
            CurrentProcess.StartInfo = processStartInfo;
            CurrentProcess.OutputDataReceived += (sender, args) =>
            {
                System.Console.WriteLine(args.Data);
                _log.Info(args.Data);
            };
            CurrentProcess.Start();
            if (CurrentProcess != null)
            {
                CurrentProcess.BeginErrorReadLine();
            }
            if (CurrentProcess != null)
            {
                CurrentProcess.BeginOutputReadLine();
            }
        }

        public void CurrentProcessWaitForExit()
        {
            CurrentProcess.WaitForExit();
        }
    }
}