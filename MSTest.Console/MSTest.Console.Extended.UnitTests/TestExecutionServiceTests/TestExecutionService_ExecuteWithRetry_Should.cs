using System.Collections.Generic;
using System.IO;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Console.Extended.Data;
using MSTest.Console.Extended.Interfaces;
using MSTest.Console.Extended.Services;
using Telerik.JustMock;

namespace MSTest.Console.Extended.UnitTests.TestExecutionServiceTests
{
    [TestClass]
    public class TestExecutionService_ExecuteWithRetry_Should
    {
        [TestMethod]
        public void ExecuteOnlyOnce_WhenRetriestCountSetToOneAndNoFailedTestsPresent()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));             
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            Mock.Arrange(() => consoleArgumentsProvider.TestResultPath).Returns("Exceptions.trx");
            Mock.Arrange(() => consoleArgumentsProvider.ConsoleArguments).Returns("ipconfig");
            Mock.Arrange(() => consoleArgumentsProvider.RetriesCount).Returns(1);
            var processExecutionProvider = Mock.Create<IProcessExecutionProvider>();
            Mock.Arrange(() => processExecutionProvider.ExecuteProcessWithAdditionalArguments(string.Empty)).DoNothing();
            Mock.Arrange(() => processExecutionProvider.CurrentProcessWaitForExit()).DoNothing();
            var fileSystemProvider = Mock.Create<IFileSystemProvider>();
            Mock.Arrange(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>())).DoNothing();
            TestRun testRun = new TestRun();
            var testRunUnitTestResult = new TestRunUnitTestResult()
            {
                outcome = "Passed"
            };
            testRun.Results = new TestRunUnitTestResult[]
            {
                testRunUnitTestResult,
                testRunUnitTestResult
            };
            testRun.ResultSummary = new TestRunResultSummary();
            testRun.ResultSummary.outcome = "Passed";
            Mock.Arrange(() => fileSystemProvider.DeserializeTestRun(Arg.AnyString)).Returns(testRun);
            var microsoftTestRunProvider = Mock.Create<IMsTestTestRunProvider>();
          
            Mock.Arrange(() => microsoftTestRunProvider.GetAllPassesTests(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdatePassedTests(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdateResultsSummary(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.GetAllNotPassedTests(Arg.IsAny<List<TestRunUnitTestResult>>())).Returns(new List<TestRunUnitTestResult>() { });
            Mock.Arrange(() => microsoftTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.AnyString)).DoNothing();
          
            var engine = new TestExecutionService(
                microsoftTestRunProvider,
                fileSystemProvider,
                processExecutionProvider,
                consoleArgumentsProvider,
                log);
            int result = engine.ExecuteWithRetry();
            Mock.Assert(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>()), Occurs.Exactly(1));
            Assert.AreEqual<int>(0, result);
        }

        [TestMethod]
        public void ExecuteOnlyOnce_WhenRetriestCountSetToOneAndFailedTestsPresent()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));             
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            Mock.Arrange(() => consoleArgumentsProvider.TestResultPath).Returns("Exceptions.trx");
            Mock.Arrange(() => consoleArgumentsProvider.ConsoleArguments).Returns("ipconfig");
            Mock.Arrange(() => consoleArgumentsProvider.RetriesCount).Returns(1);
            var processExecutionProvider = Mock.Create<IProcessExecutionProvider>();
            Mock.Arrange(() => processExecutionProvider.ExecuteProcessWithAdditionalArguments(string.Empty)).DoNothing();
            Mock.Arrange(() => processExecutionProvider.CurrentProcessWaitForExit()).DoNothing();
            var fileSystemProvider = Mock.Create<IFileSystemProvider>();
            Mock.Arrange(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>())).DoNothing();
            TestRun testRun = new TestRun();
            testRun.Results = new TestRunUnitTestResult[]
            {
                new TestRunUnitTestResult(),
                new TestRunUnitTestResult()
            };
            testRun.ResultSummary = new TestRunResultSummary();
            testRun.ResultSummary.outcome = "Failed";
            Mock.Arrange(() => fileSystemProvider.DeserializeTestRun(Arg.AnyString)).Returns(testRun);
            var microsoftTestRunProvider = Mock.Create<IMsTestTestRunProvider>();
          
            Mock.Arrange(() => microsoftTestRunProvider.GetAllPassesTests(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdatePassedTests(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdateResultsSummary(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.GetAllNotPassedTests(Arg.IsAny<List<TestRunUnitTestResult>>())).Returns(new List<TestRunUnitTestResult>() { new TestRunUnitTestResult(), new TestRunUnitTestResult() });
            Mock.Arrange(() => microsoftTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.AnyString)).DoNothing();
          
            var engine = new TestExecutionService(
                microsoftTestRunProvider,
                fileSystemProvider,
                processExecutionProvider,
                consoleArgumentsProvider,
                log);
            int result = engine.ExecuteWithRetry();
            Mock.Assert(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>()), Occurs.Exactly(1));
            Assert.AreEqual<int>(1, result);
        }

        [TestMethod]
        public void ExecuteOnlyOnce_WhenRetriestCountSetToTwoAndNoFailedTestsPresent()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));             
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            Mock.Arrange(() => consoleArgumentsProvider.TestResultPath).Returns("NoExceptions.trx");
            Mock.Arrange(() => consoleArgumentsProvider.ConsoleArguments).Returns("ipconfig");
            Mock.Arrange(() => consoleArgumentsProvider.RetriesCount).Returns(2);
            var processExecutionProvider = Mock.Create<IProcessExecutionProvider>();
            Mock.Arrange(() => processExecutionProvider.ExecuteProcessWithAdditionalArguments(string.Empty)).DoNothing();
            Mock.Arrange(() => processExecutionProvider.CurrentProcessWaitForExit()).DoNothing();
            var fileSystemProvider = Mock.Create<IFileSystemProvider>();
            Mock.Arrange(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>())).DoNothing();
            TestRun testRun = new TestRun();
            var testRunUnitTestResult = new TestRunUnitTestResult()
            {
                outcome = "Passed"
            };
            testRun.Results = new TestRunUnitTestResult[]
            {
                testRunUnitTestResult,
                testRunUnitTestResult
            };
            testRun.ResultSummary = new TestRunResultSummary();
            testRun.ResultSummary.outcome = "Passed";
            Mock.Arrange(() => fileSystemProvider.DeserializeTestRun(Arg.AnyString)).Returns(testRun);
            var microsoftTestRunProvider = Mock.Create<IMsTestTestRunProvider>();
          
            Mock.Arrange(() => microsoftTestRunProvider.GetAllPassesTests(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdatePassedTests(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdateResultsSummary(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.GetAllNotPassedTests(Arg.IsAny<List<TestRunUnitTestResult>>())).Returns(new List<TestRunUnitTestResult>() { });
            Mock.Arrange(() => microsoftTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.AnyString)).DoNothing();
          
            var engine = new TestExecutionService(
                microsoftTestRunProvider,
                fileSystemProvider,
                processExecutionProvider,
                consoleArgumentsProvider,
                log);
            int result = engine.ExecuteWithRetry();

            Mock.Assert(() => processExecutionProvider.ExecuteProcessWithAdditionalArguments(Arg.AnyString), Occurs.Once());
            Mock.Assert(() => fileSystemProvider.DeserializeTestRun(Arg.AnyString), Occurs.Once());
            Mock.Assert(() => microsoftTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.AnyString), Occurs.Never());
            Mock.Assert(() => microsoftTestRunProvider.GetAllPassesTests(Arg.IsAny<TestRun>()), Occurs.Never());
            Mock.Assert(() => microsoftTestRunProvider.UpdatePassedTests(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>()), Occurs.Never());
            Mock.Assert(() => microsoftTestRunProvider.UpdateResultsSummary(Arg.IsAny<TestRun>()), Occurs.Never());
            Mock.Assert(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>()), Occurs.Exactly(1));
            Assert.AreEqual<int>(0, result);
        }

        [TestMethod]
        public void ExecuteTwice_WhenRetriestCountSetToTwoAndFailedTestsPresent()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));             
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            Mock.Arrange(() => consoleArgumentsProvider.TestResultPath).Returns("Exceptions.trx");
            Mock.Arrange(() => consoleArgumentsProvider.ConsoleArguments).Returns("ipconfig");
            Mock.Arrange(() => consoleArgumentsProvider.RetriesCount).Returns(2);
            Mock.Arrange(() => consoleArgumentsProvider.FailedTestsThreshold).Returns(100);
            var processExecutionProvider = Mock.Create<IProcessExecutionProvider>();
            Mock.Arrange(() => processExecutionProvider.ExecuteProcessWithAdditionalArguments(string.Empty)).DoNothing();
            Mock.Arrange(() => processExecutionProvider.CurrentProcessWaitForExit()).DoNothing();
            var fileSystemProvider = Mock.Create<IFileSystemProvider>();
            Mock.Arrange(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>())).DoNothing();
            TestRun testRun = new TestRun();
            testRun.Results = new TestRunUnitTestResult[]
            {
                new TestRunUnitTestResult(),
                new TestRunUnitTestResult()
            };
            testRun.ResultSummary = new TestRunResultSummary();
            testRun.ResultSummary.outcome = "Failed";
            Mock.Arrange(() => fileSystemProvider.DeserializeTestRun(Arg.AnyString)).Returns(testRun);
            var microsoftTestRunProvider = Mock.Create<IMsTestTestRunProvider>();
          
            Mock.Arrange(() => microsoftTestRunProvider.GetAllPassesTests(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdatePassedTests(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdateResultsSummary(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.GetAllNotPassedTests(Arg.IsAny<List<TestRunUnitTestResult>>())).Returns(new List<TestRunUnitTestResult>() { new TestRunUnitTestResult(), new TestRunUnitTestResult() });
            Mock.Arrange(() => microsoftTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.AnyString)).DoNothing();

            var engine = new TestExecutionService(
                microsoftTestRunProvider,
                fileSystemProvider,
                processExecutionProvider,
                consoleArgumentsProvider,
                log);
            int result = engine.ExecuteWithRetry();

            Mock.Assert(() => processExecutionProvider.ExecuteProcessWithAdditionalArguments(Arg.AnyString), Occurs.Exactly(2));
            Mock.Assert(() => fileSystemProvider.DeserializeTestRun(Arg.AnyString), Occurs.Exactly(2));
            Mock.Assert(() => microsoftTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.AnyString), Occurs.Once());
            Mock.Assert(() => microsoftTestRunProvider.GetAllPassesTests(Arg.IsAny<TestRun>()), Occurs.Once());
            Mock.Assert(() => microsoftTestRunProvider.UpdatePassedTests(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>()), Occurs.Once());
            Mock.Assert(() => microsoftTestRunProvider.UpdateResultsSummary(Arg.IsAny<TestRun>()), Occurs.Once());
            Mock.Assert(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>()), Occurs.Exactly(1));
            Assert.AreEqual<int>(1, result);
        }

         [TestMethod]
        public void ExecuteOnlyOnce_WhenThresholdSmallerThanFailedTestsPercentage()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));             
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            Mock.Arrange(() => consoleArgumentsProvider.TestResultPath).Returns("Exceptions.trx");
            Mock.Arrange(() => consoleArgumentsProvider.ConsoleArguments).Returns("ipconfig");
            Mock.Arrange(() => consoleArgumentsProvider.RetriesCount).Returns(2);
            Mock.Arrange(() => consoleArgumentsProvider.FailedTestsThreshold).Returns(50);
            var processExecutionProvider = Mock.Create<IProcessExecutionProvider>();
            Mock.Arrange(() => processExecutionProvider.ExecuteProcessWithAdditionalArguments(string.Empty)).DoNothing();
            Mock.Arrange(() => processExecutionProvider.CurrentProcessWaitForExit()).DoNothing();
            var fileSystemProvider = Mock.Create<IFileSystemProvider>();
            Mock.Arrange(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>())).DoNothing();
            TestRun testRun = new TestRun();
            testRun.Results = new TestRunUnitTestResult[]
            {
                new TestRunUnitTestResult(),
                new TestRunUnitTestResult()
            };
            testRun.ResultSummary = new TestRunResultSummary();
            testRun.ResultSummary.outcome = "Failed";
            Mock.Arrange(() => fileSystemProvider.DeserializeTestRun(Arg.AnyString)).Returns(testRun);
            var microsoftTestRunProvider = Mock.Create<IMsTestTestRunProvider>();
          
            Mock.Arrange(() => microsoftTestRunProvider.GetAllPassesTests(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdatePassedTests(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdateResultsSummary(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.GetAllNotPassedTests(Arg.IsAny<List<TestRunUnitTestResult>>())).Returns(new List<TestRunUnitTestResult>() { new TestRunUnitTestResult(), new TestRunUnitTestResult() });
            Mock.Arrange(() => microsoftTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.AnyString)).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.CalculatedFailedTestsPercentage(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>())).Returns(100);

            var engine = new TestExecutionService(
                microsoftTestRunProvider,
                fileSystemProvider,
                processExecutionProvider,
                consoleArgumentsProvider,
                log);
            int result = engine.ExecuteWithRetry();

            Mock.Assert(() => processExecutionProvider.ExecuteProcessWithAdditionalArguments(Arg.AnyString), Occurs.Once());
            Mock.Assert(() => fileSystemProvider.DeserializeTestRun(Arg.AnyString), Occurs.Once());
            Mock.Assert(() => microsoftTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.AnyString), Occurs.Never());
            Mock.Assert(() => microsoftTestRunProvider.GetAllPassesTests(Arg.IsAny<TestRun>()), Occurs.Never());
            Mock.Assert(() => microsoftTestRunProvider.UpdatePassedTests(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>()), Occurs.Never());
            Mock.Assert(() => microsoftTestRunProvider.UpdateResultsSummary(Arg.IsAny<TestRun>()), Occurs.Never());
            Mock.Assert(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>()), Occurs.Once());
            Assert.AreEqual<int>(1, result);
        }

         [TestMethod]
        public void ExecuteOnlyOnce_WhenThresholdEqualToFailedTestsPercentage()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));             
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            Mock.Arrange(() => consoleArgumentsProvider.TestResultPath).Returns("Exceptions.trx");
            Mock.Arrange(() => consoleArgumentsProvider.ConsoleArguments).Returns("ipconfig");
            Mock.Arrange(() => consoleArgumentsProvider.RetriesCount).Returns(2);
            Mock.Arrange(() => consoleArgumentsProvider.FailedTestsThreshold).Returns(50);
            var processExecutionProvider = Mock.Create<IProcessExecutionProvider>();
            Mock.Arrange(() => processExecutionProvider.ExecuteProcessWithAdditionalArguments(string.Empty)).DoNothing();
            Mock.Arrange(() => processExecutionProvider.CurrentProcessWaitForExit()).DoNothing();
            var fileSystemProvider = Mock.Create<IFileSystemProvider>();
            Mock.Arrange(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>())).DoNothing();
            TestRun testRun = new TestRun();
            testRun.Results = new TestRunUnitTestResult[]
            {
                new TestRunUnitTestResult(),
                new TestRunUnitTestResult()
            };
            testRun.ResultSummary = new TestRunResultSummary();
            testRun.ResultSummary.outcome = "Failed";
            Mock.Arrange(() => fileSystemProvider.DeserializeTestRun(Arg.AnyString)).Returns(testRun);
            var microsoftTestRunProvider = Mock.Create<IMsTestTestRunProvider>();
          
            Mock.Arrange(() => microsoftTestRunProvider.GetAllPassesTests(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdatePassedTests(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdateResultsSummary(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.GetAllNotPassedTests(Arg.IsAny<List<TestRunUnitTestResult>>())).Returns(new List<TestRunUnitTestResult>() { new TestRunUnitTestResult(), new TestRunUnitTestResult() });
            Mock.Arrange(() => microsoftTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.AnyString)).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.CalculatedFailedTestsPercentage(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>())).Returns(50);

            var engine = new TestExecutionService(
                microsoftTestRunProvider,
                fileSystemProvider,
                processExecutionProvider,
                consoleArgumentsProvider,
                log);
            int result = engine.ExecuteWithRetry();

            Mock.Assert(() => processExecutionProvider.ExecuteProcessWithAdditionalArguments(Arg.AnyString), Occurs.Once());
            Mock.Assert(() => fileSystemProvider.DeserializeTestRun(Arg.AnyString), Occurs.Once());
            Mock.Assert(() => microsoftTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.AnyString), Occurs.Never());
            Mock.Assert(() => microsoftTestRunProvider.GetAllPassesTests(Arg.IsAny<TestRun>()), Occurs.Never());
            Mock.Assert(() => microsoftTestRunProvider.UpdatePassedTests(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>()), Occurs.Never());
            Mock.Assert(() => microsoftTestRunProvider.UpdateResultsSummary(Arg.IsAny<TestRun>()), Occurs.Never());
            Mock.Assert(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>()), Occurs.Once());
            Assert.AreEqual<int>(1, result);
        }

        [TestMethod]
        public void ExecuteTwice_WhenRetriestCountSetToThreeAndFailedTestsPresent()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));             
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            Mock.Arrange(() => consoleArgumentsProvider.TestResultPath).Returns("Exceptions.trx");
            Mock.Arrange(() => consoleArgumentsProvider.ConsoleArguments).Returns("ipconfig");
               Mock.Arrange(() => consoleArgumentsProvider.RetriesCount).Returns(3);
            Mock.Arrange(() => consoleArgumentsProvider.FailedTestsThreshold).Returns(100);
            var processExecutionProvider = Mock.Create<IProcessExecutionProvider>();
            Mock.Arrange(() => processExecutionProvider.ExecuteProcessWithAdditionalArguments(string.Empty)).DoNothing();
            Mock.Arrange(() => processExecutionProvider.CurrentProcessWaitForExit()).DoNothing();
            var fileSystemProvider = Mock.Create<IFileSystemProvider>();
            TestRun testRun = new TestRun();
            testRun.Results = new TestRunUnitTestResult[]
            {
                new TestRunUnitTestResult(),
                new TestRunUnitTestResult()
            };
            testRun.ResultSummary = new TestRunResultSummary();
            testRun.ResultSummary.outcome = "Passed";
            Mock.Arrange(() => fileSystemProvider.DeserializeTestRun(Arg.AnyString)).Returns(testRun);
            Mock.Arrange(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>())).DoNothing();
            var microsoftTestRunProvider = Mock.Create<IMsTestTestRunProvider>();
          
            Mock.Arrange(() => microsoftTestRunProvider.GetAllPassesTests(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdatePassedTests(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdateResultsSummary(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.GetAllNotPassedTests(Arg.IsAny<List<TestRunUnitTestResult>>())).Returns(new List<TestRunUnitTestResult>() { new TestRunUnitTestResult(), new TestRunUnitTestResult() });
            Mock.Arrange(() => microsoftTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.AnyString)).DoNothing();

            var engine = new TestExecutionService(
                microsoftTestRunProvider,
                fileSystemProvider,
                processExecutionProvider,
                consoleArgumentsProvider,
                log);
            int result = engine.ExecuteWithRetry();

            Mock.Assert(() => processExecutionProvider.ExecuteProcessWithAdditionalArguments(Arg.AnyString), Occurs.Exactly(3));
            Mock.Assert(() => fileSystemProvider.DeserializeTestRun(Arg.AnyString), Occurs.Exactly(3));
            Mock.Assert(() => microsoftTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.AnyString), Occurs.Exactly(2));
            Mock.Assert(() => microsoftTestRunProvider.GetAllPassesTests(Arg.IsAny<TestRun>()), Occurs.Exactly(2));
            Mock.Assert(() => microsoftTestRunProvider.UpdatePassedTests(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>()), Occurs.Exactly(2));
            Mock.Assert(() => microsoftTestRunProvider.UpdateResultsSummary(Arg.IsAny<TestRun>()), Occurs.Exactly(2));
            Mock.Assert(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>()), Occurs.Exactly(1));
            Assert.AreEqual<int>(1, result);
        }

        [TestMethod]
        public void ExecuteOnce_WhenRetriestCountSetToThreeAndFailedTestsPresentAndSecondTimeNoFailedTestsPresent()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));             
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            Mock.Arrange(() => consoleArgumentsProvider.TestResultPath).Returns("Exceptions.trx");
            Mock.Arrange(() => consoleArgumentsProvider.ConsoleArguments).Returns("ipconfig");
               Mock.Arrange(() => consoleArgumentsProvider.RetriesCount).Returns(3);
            Mock.Arrange(() => consoleArgumentsProvider.FailedTestsThreshold).Returns(100);
            var processExecutionProvider = Mock.Create<IProcessExecutionProvider>();
            Mock.Arrange(() => processExecutionProvider.ExecuteProcessWithAdditionalArguments(string.Empty)).DoNothing();
            Mock.Arrange(() => processExecutionProvider.CurrentProcessWaitForExit()).DoNothing();
            var fileSystemProvider = Mock.Create<IFileSystemProvider>();
            TestRun testRun = new TestRun();
            testRun.Results = new TestRunUnitTestResult[]
            {
                new TestRunUnitTestResult(),
                new TestRunUnitTestResult()
            };
            testRun.ResultSummary = new TestRunResultSummary();
            testRun.ResultSummary.outcome = "Passed";
            Mock.Arrange(() => fileSystemProvider.DeserializeTestRun(Arg.AnyString)).Returns(testRun);
            Mock.Arrange(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>())).DoNothing();
            var microsoftTestRunProvider = Mock.Create<IMsTestTestRunProvider>();
          
            Mock.Arrange(() => microsoftTestRunProvider.GetAllPassesTests(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdatePassedTests(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.UpdateResultsSummary(Arg.IsAny<TestRun>())).DoNothing();
            Mock.Arrange(() => microsoftTestRunProvider.GetAllNotPassedTests(Arg.IsAny<List<TestRunUnitTestResult>>())).Returns(new List<TestRunUnitTestResult>() { new TestRunUnitTestResult(), new TestRunUnitTestResult() }).InSequence();
            Mock.Arrange(() => microsoftTestRunProvider.GetAllNotPassedTests(Arg.IsAny<List<TestRunUnitTestResult>>())).Returns(new List<TestRunUnitTestResult>()).InSequence();
            Mock.Arrange(() => microsoftTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.AnyString)).DoNothing();

            var engine = new TestExecutionService(
                microsoftTestRunProvider,
                fileSystemProvider,
                processExecutionProvider,
                consoleArgumentsProvider,
                log);
            int result = engine.ExecuteWithRetry();

            Mock.Assert(() => processExecutionProvider.ExecuteProcessWithAdditionalArguments(Arg.AnyString), Occurs.Exactly(2));
            Mock.Assert(() => fileSystemProvider.DeserializeTestRun(Arg.AnyString), Occurs.Exactly(2));
            Mock.Assert(() => microsoftTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.AnyString), Occurs.Once());
            Mock.Assert(() => microsoftTestRunProvider.GetAllPassesTests(Arg.IsAny<TestRun>()), Occurs.Once());
            Mock.Assert(() => microsoftTestRunProvider.UpdatePassedTests(Arg.IsAny<List<TestRunUnitTestResult>>(), Arg.IsAny<List<TestRunUnitTestResult>>()), Occurs.Once());
            Mock.Assert(() => microsoftTestRunProvider.UpdateResultsSummary(Arg.IsAny<TestRun>()), Occurs.Once());
            Mock.Assert(() => fileSystemProvider.SerializeTestRun(Arg.IsAny<TestRun>()), Occurs.Once());
            Assert.AreEqual<int>(0, result);
        }
    }
}