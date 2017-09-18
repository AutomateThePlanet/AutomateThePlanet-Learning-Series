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
using System.Text.RegularExpressions;

namespace MSTest.Console.Extended.Infrastructure
{
    public class MsTestTestRunProvider : IMsTestTestRunProvider
    {
        private readonly string testToRunRegexPattern = @".test:[0-9A-Za-z._-]+";
        private readonly ILog log;
        private readonly IConsoleArgumentsProvider consoleArgumentsProvider;

        public MsTestTestRunProvider(IConsoleArgumentsProvider consoleArgumentsProvider, ILog log)
        {
            this.consoleArgumentsProvider = consoleArgumentsProvider;
            this.log = log;
        }

        public List<TestRunUnitTestResult> GetAllPassesTests(TestRun testRun)
        {
            List<TestRunUnitTestResult> results = new List<TestRunUnitTestResult>();

            results = testRun.Results.ToList().Where(x => x.outcome.Equals("Passed")).ToList();
            return results;
        }

        public void UpdatePassedTests(List<TestRunUnitTestResult> passedTests, List<TestRunUnitTestResult> allTests)
        {
            foreach (var currentTest in allTests)
            {
                if (passedTests.Count(x => x.testId.Equals(currentTest.testId)) > 0)
                {
                    currentTest.outcome = "Passed";
                }
            }
        }

        public List<TestRunUnitTestResult> GetAllNotPassedTests(List<TestRunUnitTestResult> allTests)
        {
            List<TestRunUnitTestResult> results = new List<TestRunUnitTestResult>();
            results = allTests.Where(x => !x.outcome.Equals("Passed")).ToList();
            return results;
        }

        public void UpdateResultsSummary(TestRun testRun)
        {
            testRun.ResultSummary.Counters.failed = (int)testRun.Results.ToList().Count(x => x.outcome.Equals("Failed"));
            testRun.ResultSummary.Counters.passed = (int)testRun.Results.ToList().Count(x => x.outcome.Equals("Passed"));
            if ((int)testRun.ResultSummary.Counters.passed != testRun.Results.Length)
            {
                testRun.ResultSummary.outcome = "Failed";
            }
            else
            {
                testRun.ResultSummary.outcome = "Passed";
            }
        }

        public string GenerateAdditionalArgumentsForFailedTestsRun(List<TestRunUnitTestResult> failedTests, string newTestResultFilePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" ");
            foreach (var currentFailedTest in failedTests)
            {
                sb.AppendFormat("/test:{0} ", currentFailedTest.testName);
                System.Console.WriteLine("##### MSTestRetrier: Execute again {0}", currentFailedTest.testName);
                this.log.InfoFormat("##### MSTestRetrier: Execute again {0}", currentFailedTest.testName);
            }

            string oldAgruments = this.consoleArgumentsProvider.ConsoleArguments;

            // Exclude original test list from command line arguments
            Regex r1 = new Regex(testToRunRegexPattern, RegexOptions.Singleline);
            foreach (Match currentMatch in r1.Matches(oldAgruments))
            {
                if (currentMatch.Success)
                {
                    oldAgruments = oldAgruments.Replace(currentMatch.Groups[0].Value, "");
                }
            }

            string additionalArgumentsForFailedTestsRun = string.Concat(oldAgruments, sb.ToString());

            additionalArgumentsForFailedTestsRun = additionalArgumentsForFailedTestsRun.Replace(this.consoleArgumentsProvider.TestResultPath, newTestResultFilePath);
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