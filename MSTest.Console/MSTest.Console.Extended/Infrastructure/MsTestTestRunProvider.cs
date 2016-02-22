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
            testRun.ResultSummary.Counters.failed = (byte)testRun.Results.ToList().Count(x => x.outcome.Equals("Failed"));
            testRun.ResultSummary.Counters.passed = (byte)testRun.Results.ToList().Count(x => x.outcome.Equals("Passed"));
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
            string additionalArgumentsForFailedTestsRun = string.Concat(this.consoleArgumentsProvider.ConsoleArguments, sb.ToString());
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