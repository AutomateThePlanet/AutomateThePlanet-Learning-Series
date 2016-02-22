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
    public class MsTestTestRunProvider_GenerateAdditionalArgumentsForFailedTestsRun_Should
    {
        [TestMethod]
        public void AddOneTestArgument_WhenOneFailedTestPresent()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newTestResultsPath = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.ConsoleArguments).Returns(@"/resultsfile:""C:\Results.trx""");
            Mock.Arrange(() => consoleArgumentsProvider.TestResultPath).Returns(@"C:\Results.trx");
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var testRun = fileSystemProvider.DeserializeTestRun("Exceptions.trx");
            
            var microsoftTestTestRunProvider = new MsTestTestRunProvider(consoleArgumentsProvider, log);
            var failedTests = microsoftTestTestRunProvider.GetAllNotPassedTests(testRun.Results.ToList());
            string additionalArguments = microsoftTestTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(failedTests, newTestResultsPath);
            Assert.AreEqual<string>(string.Format(@"/resultsfile:""{0}"" /test:TestConsoleExtended", newTestResultsPath), additionalArguments);
        }

        [TestMethod]
        public void AddTwoTestArgument_WhenTwoFailedTestsPresent()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newTestResultsPath = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.ConsoleArguments).Returns(@"/resultsfile:""C:\Results.trx""");
            Mock.Arrange(() => consoleArgumentsProvider.TestResultPath).Returns(@"C:\Results.trx");
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var testRun = fileSystemProvider.DeserializeTestRun("Exceptions.trx");
            
            var microsoftTestTestRunProvider = new MsTestTestRunProvider(consoleArgumentsProvider, log);
            string additionalArguments = microsoftTestTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(testRun.Results.ToList(), newTestResultsPath);
            Assert.AreEqual<string>(string.Format(@"/resultsfile:""{0}"" /test:TestConsoleExtended /test:TestConsoleExtended_Second", newTestResultsPath), additionalArguments);
        }
    }
}