using System;
using System.IO;
using System.Linq;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Console.Extended.Infrastructure;
using MSTest.Console.Extended.Interfaces;
using Telerik.JustMock;

namespace MSTest.Console.Extended.UnitTests.MsTestTestRunProviderTests
{
    [TestClass]
    public class MsTestTestRunProvider_UpdatePassedTests_Should
    {
        [TestMethod]
        public void UpdatePassedTests_WhenNoMatchingTestsExist()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var failedTestsRun = fileSystemProvider.DeserializeTestRun("Exceptions.trx");
            var passedTestRun = fileSystemProvider.DeserializeTestRun("NoExceptions.trx");
            var microsoftTestTestRunProvider = new MsTestTestRunProvider(consoleArgumentsProvider, log);

            var passedTests = microsoftTestTestRunProvider.GetAllPassesTests(passedTestRun);
            var failedTests = microsoftTestTestRunProvider.GetAllNotPassedTests(failedTestsRun.Results.ToList());
            passedTests.ForEach(x => x.testId = Guid.NewGuid().ToString());

            microsoftTestTestRunProvider.UpdatePassedTests(passedTests, failedTestsRun.Results.ToList());

            var updatedFailedTests = microsoftTestTestRunProvider.GetAllNotPassedTests(failedTests);
            Assert.AreEqual<int>(1, updatedFailedTests.Count);
        }

        [TestMethod]
        public void UpdatePassedTests_WhenMatchingTestsExist()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var failedTestsRun = fileSystemProvider.DeserializeTestRun("Exceptions.trx");
            var microsoftTestTestRunProvider = new MsTestTestRunProvider(consoleArgumentsProvider, log);

            var failedTests = microsoftTestTestRunProvider.GetAllNotPassedTests(failedTestsRun.Results.ToList());
            
            microsoftTestTestRunProvider.UpdatePassedTests(failedTests, failedTestsRun.Results.ToList());

            var updatedFailedTests = microsoftTestTestRunProvider.GetAllNotPassedTests(failedTests);
            Assert.AreEqual<int>(0, updatedFailedTests.Count);
        }
    }
}