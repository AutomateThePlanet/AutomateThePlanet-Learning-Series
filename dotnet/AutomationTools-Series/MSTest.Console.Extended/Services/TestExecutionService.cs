// <copyright file="TestExecutionService.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Linq;
using log4net;
using MSTest.Console.Extended.Data;
using MSTest.Console.Extended.Interfaces;

namespace MSTest.Console.Extended.Services
{
    public class TestExecutionService
    {
        private readonly ILog _log;

        private readonly IMsTestTestRunProvider _microsoftTestTestRunProvider;

        private readonly IFileSystemProvider _fileSystemProvider;

        private readonly IProcessExecutionProvider _processExecutionProvider;

        private readonly IConsoleArgumentsProvider _consoleArgumentsProvider;

        public TestExecutionService(
            IMsTestTestRunProvider microsoftTestTestRunProvider,
            IFileSystemProvider fileSystemProvider,
            IProcessExecutionProvider processExecutionProvider,
            IConsoleArgumentsProvider consoleArgumentsProvider,
            ILog log)
        {
            this._microsoftTestTestRunProvider = microsoftTestTestRunProvider;
            this._fileSystemProvider = fileSystemProvider;
            this._processExecutionProvider = processExecutionProvider;
            this._consoleArgumentsProvider = consoleArgumentsProvider;
            this._log = log;
        }
        
        public int ExecuteWithRetry()
        {
            _fileSystemProvider.DeleteTestResultFiles();
            _processExecutionProvider.ExecuteProcessWithAdditionalArguments();
            _processExecutionProvider.CurrentProcessWaitForExit();
            var testRun = _fileSystemProvider.DeserializeTestRun();
            var areAllTestsGreen = 0;
            var failedTests = new List<TestRunUnitTestResult>();
            failedTests = _microsoftTestTestRunProvider.GetAllNotPassedTests(testRun.Results.ToList());
            var failedTestsPercentage = _microsoftTestTestRunProvider.CalculatedFailedTestsPercentage(failedTests, testRun.Results.ToList());
            if (failedTestsPercentage < _consoleArgumentsProvider.FailedTestsThreshold)
            {
                for (var i = 0; i < _consoleArgumentsProvider.RetriesCount - 1; i++)
                {
                    _log.InfoFormat("Start to execute again {0} failed tests.", failedTests.Count);
                    if (failedTests.Count > 0)
                    {
                        var currentTestResultPath = _fileSystemProvider.GetTempTrxFile();
                        var retryRunArguments = _microsoftTestTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(failedTests, currentTestResultPath);
                   
                        _log.InfoFormat("Run {0} time with arguments {1}", i + 2, retryRunArguments);
                        _processExecutionProvider.ExecuteProcessWithAdditionalArguments(retryRunArguments);
                        _processExecutionProvider.CurrentProcessWaitForExit();
                        var currentTestRun = _fileSystemProvider.DeserializeTestRun(currentTestResultPath);
                        var passedTests = _microsoftTestTestRunProvider.GetAllPassesTests(currentTestRun);
                        _microsoftTestTestRunProvider.UpdatePassedTests(passedTests, testRun.Results.ToList());
                        _microsoftTestTestRunProvider.UpdateResultsSummary(testRun);
                    }
                    else
                    {
                        break;
                    }
                    failedTests = _microsoftTestTestRunProvider.GetAllNotPassedTests(testRun.Results.ToList());
                }
            }
            if (failedTests.Count > 0)
            {
                areAllTestsGreen = 1;
            }
            _fileSystemProvider.SerializeTestRun(testRun);

            return areAllTestsGreen;
        }
    }
}