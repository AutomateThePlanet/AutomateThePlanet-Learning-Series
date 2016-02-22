using System.Collections.Generic;
using MSTest.Console.Extended.Data;

namespace MSTest.Console.Extended.Interfaces
{
    public interface IMsTestTestRunProvider
    {
        void UpdatePassedTests(List<TestRunUnitTestResult> passedTests, List<TestRunUnitTestResult> allTests);

        List<TestRunUnitTestResult> GetAllPassesTests(TestRun testRun);

        List<TestRunUnitTestResult> GetAllNotPassedTests(List<TestRunUnitTestResult> allTests);

        void UpdateResultsSummary(TestRun testRun);

        string GenerateAdditionalArgumentsForFailedTestsRun(List<TestRunUnitTestResult> failedTests, string newTestResultFilePath);

        int CalculatedFailedTestsPercentage(List<TestRunUnitTestResult> failedTests, List<TestRunUnitTestResult> allTests);
    }
}