// <copyright file="MsTestTestRunProvider_UpdateResultsSummary_Should.cs" company="Automate The Planet Ltd.">
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
    public class MsTestTestRunProvider_UpdateResultsSummary_Should
    {
        [TestMethod]
        public void SetCorrectPassedCounter_WhenNoFailedTestsPresent()
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
            microsoftTestTestRunProvider.UpdateResultsSummary(failedTestsRun);

            Assert.AreEqual<int>(2, failedTestsRun.ResultSummary.Counters.passed);
        }

        [TestMethod]
        public void SetCorrectPassedCounter_WhenFailedTestsPresent()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var failedTestsRun = fileSystemProvider.DeserializeTestRun("Exceptions.trx");
            var microsoftTestTestRunProvider = new MsTestTestRunProvider(consoleArgumentsProvider, log);

            microsoftTestTestRunProvider.UpdateResultsSummary(failedTestsRun);

            Assert.AreEqual<int>(1, failedTestsRun.ResultSummary.Counters.passed);
        }

        [TestMethod]
        public void SetPassedSummaryOutcome_WhenNoFailedTestsPresentAfterRerun()
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
            microsoftTestTestRunProvider.UpdateResultsSummary(failedTestsRun);

            Assert.AreEqual<string>("Passed", failedTestsRun.ResultSummary.outcome);
        }

        [TestMethod]
        public void SetFailedSummaryOutcome_WhenOneFailedTestPresent()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var failedTestsRun = fileSystemProvider.DeserializeTestRun("Exceptions.trx");
            var microsoftTestTestRunProvider = new MsTestTestRunProvider(consoleArgumentsProvider, log);

            microsoftTestTestRunProvider.UpdateResultsSummary(failedTestsRun);

            Assert.AreEqual<string>("Failed", failedTestsRun.ResultSummary.outcome);
        }

        [TestMethod]
        public void SetCorrectFailedCounter_WhenNoFailedTestsPresent()
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
            microsoftTestTestRunProvider.UpdateResultsSummary(failedTestsRun);

            Assert.AreEqual<int>(0, failedTestsRun.ResultSummary.Counters.failed);
        }

        [TestMethod]
        public void SetCorrectFailedCounter_WhenFailedTestsPresent()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var failedTestsRun = fileSystemProvider.DeserializeTestRun("Exceptions.trx");
            var microsoftTestTestRunProvider = new MsTestTestRunProvider(consoleArgumentsProvider, log);

            microsoftTestTestRunProvider.UpdateResultsSummary(failedTestsRun);

            Assert.AreEqual<int>(1, failedTestsRun.ResultSummary.Counters.failed);
        }
    }
}