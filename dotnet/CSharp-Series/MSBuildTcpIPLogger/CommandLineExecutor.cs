// <copyright file="CommandLineExecutor.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;

namespace MSBuildTcpIPLogger
{
    class CommandLineExecutor
    {
        public const string MSBUILD_PATH = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe";

        public Process ExecuteMsbuildProject(string msbuildProjPath, IpAddressSettings ipAddressSettings, string additionalArgs = "")
        {
            var currentAssemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var currentAssemblyFullpath = String.Format(currentAssemblyLocation, "\\AutomationTestAssistantCore.dll");
            var additionalArguments = BuildMsBuildAdditionalArguments(msbuildProjPath, ipAddressSettings, additionalArgs, currentAssemblyFullpath);
            var procStartInfo = new ProcessStartInfo(MSBUILD_PATH, additionalArguments);
            procStartInfo.RedirectStandardOutput = false;
            //procStartInfo.UseShellExecute = true;
            //procStartInfo.CreateNoWindow = false;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            var proc = new Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            return proc;
        }

        public Process ExecuteMsTest(MessageArgsMsTest messageArgs)
        {
            messageArgs.CreateTestList();
            var additionalArgs = String.Concat("/p:TestListPath=\"", messageArgs.TestListPath, "/p:ResultsFilePath=", "\"", messageArgs.ResultsFilePath, "\"");
            var currentProcess = ExecuteMsbuildProject(messageArgs.ProjectPath, messageArgs.IpAddressSettings, additionalArgs);

            return currentProcess;
        }

        public Process ExecuteMsTestSpecificList(MessageArgsMsTest messageArgs)
        {
            messageArgs.CreateTestList();
            var uniqueTestResultName = TimeStampGenerator.GenerateTrxFilePath(messageArgs.WorkingDir);
            var additionalArgs = String.Concat("/p:TestListPath=\"", messageArgs.TestListPath, "\" /p:ResultsFilePath=", "\"", messageArgs.ResultsFilePath, "\"", " /p:TestListName=", "\"", messageArgs.ListName, "\"");
            var currentProcess = ExecuteMsbuildProject(messageArgs.ProjectPath, messageArgs.IpAddressSettings, additionalArgs);

            return currentProcess;
        }
      
        public void WaitForProcessToFinish(int procId)
        {
            var proc = Process.GetProcessById(procId);
            proc.WaitForExit();
        }

        private string BuildMsBuildAdditionalArguments(string msbuildProjPath, IpAddressSettings ipAddressSettings, string additionalArgs, string currentAssemblyFullpath)
        {
            var additionalArguments = String.Concat(msbuildProjPath, " ", additionalArgs, " ", "/fileLoggerParameters:LogFile=MsBuildLog.txt;append=true;Verbosity=normal;Encoding=UTF-8 /l:AutomationTestAssistantCore.MsBuildLogger.TcpIpLogger,",
                currentAssemblyFullpath, ";Ip=", ipAddressSettings.GetIPAddress(), ";Port=", ipAddressSettings.Port, ";");

            return additionalArguments;
        }
    }
}
