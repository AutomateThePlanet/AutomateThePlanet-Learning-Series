// <copyright file="MsTestTestRunProvider.cs" company="Automate The Planet Ltd.">
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
using System.Text;
using log4net;
using MSTest.Console.Extended.Data;
using MSTest.Console.Extended.Interfaces;

namespace MSTest.Console.Extended.Infrastructure
{
    public class MsTestTestRunProvider : IMsTestTestRunProvider
    {
        private readonly ILog _log;
        private readonly IConsoleArgumentsProvider _consoleArgumentsProvider;

        public MsTestTestRunProvider(IConsoleArgumentsProvider consoleArgumentsProvider, ILog log)
        {
            this._consoleArgumentsProvider = consoleArgumentsProvider;
            this._log = log;
        }

        public List<TestRunUnitTestResult> GetAllPassesTests(TestRun testRun)
        {
            var results = new List<TestRunUnitTestResult>();

            results = testRun.Results.ToList().Where(x => x.Outcome.Equals("Passed")).ToList();
            return results;
        }

        public void UpdatePassedTests(List<TestRunUnitTestResult> passedTests, List<TestRunUnitTestResult> allTests)
        {
            foreach (var currentTest in allTests)
            {
                if (passedTests.Count(x => x.TestId.Equals(currentTest.TestId)) > 0)
                {
                    currentTest.Outcome = "Passed";
                }
            }
        }

        public List<TestRunUnitTestResult> GetAllNotPassedTests(List<TestRunUnitTestResult> allTests)
        {
            var results = new List<TestRunUnitTestResult>();
            results = allTests.Where(x => !x.Outcome.Equals("Passed")).ToList();
            return results;
        }

        public void UpdateResultsSummary(TestRun testRun)
        {
            testRun.ResultSummary.Counters.Failed = (int)testRun.Results.ToList().Count(x => x.Outcome.Equals("Failed"));
            testRun.ResultSummary.Counters.Passed = (int)testRun.Results.ToList().Count(x => x.Outcome.Equals("Passed"));
            if ((int)testRun.ResultSummary.Counters.Passed != testRun.Results.Length)
            {
                testRun.ResultSummary.Outcome = "Failed";
            }
            else
            {
                testRun.ResultSummary.Outcome = "Passed";
            }
        }

        public string GenerateAdditionalArgumentsForFailedTestsRun(List<TestRunUnitTestResult> failedTests, string newTestResultFilePath)
        {
            var sb = new StringBuilder();
            sb.Append(" ");
            foreach (var currentFailedTest in failedTests)
            {
                sb.AppendFormat("/test:{0} ", currentFailedTest.TestName);
                System.Console.WriteLine("##### MSTestRetrier: Execute again {0}", currentFailedTest.TestName);
                _log.InfoFormat("##### MSTestRetrier: Execute again {0}", currentFailedTest.TestName);
            }
            var additionalArgumentsForFailedTestsRun = string.Concat(_consoleArgumentsProvider.ConsoleArguments, sb.ToString());
            additionalArgumentsForFailedTestsRun = additionalArgumentsForFailedTestsRun.Replace(_consoleArgumentsProvider.TestResultPath, newTestResultFilePath);
            additionalArgumentsForFailedTestsRun = additionalArgumentsForFailedTestsRun.TrimEnd();
            return additionalArgumentsForFailedTestsRun;
        }

        public int CalculatedFailedTestsPercentage(List<TestRunUnitTestResult> failedTests, List<TestRunUnitTestResult> allTests)
        {
            double result = 0;
            if (allTests.Count > 0)
            {
                result = ((double)failedTests.Count / (double)allTests.Count) * 100;
            }

            return (int)result;
        }
    }
}