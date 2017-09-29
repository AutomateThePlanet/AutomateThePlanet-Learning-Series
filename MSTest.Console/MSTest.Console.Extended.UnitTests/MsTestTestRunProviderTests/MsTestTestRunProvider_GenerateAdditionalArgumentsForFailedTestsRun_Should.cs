// <copyright file="MsTestTestRunProvider_GenerateAdditionalArgumentsForFailedTestsRun_Should.cs" company="Automate The Planet Ltd.">
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

        [TestMethod]
        public void ExcludeTestListFromConsoleArguments_WhenTestListPresent()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newTestResultsPath = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.ConsoleArguments).Returns(@"/resultsfile:""C:\Results.trx"" /test:testmask1 /test:testmask2 /retriesCount:3");
            Mock.Arrange(() => consoleArgumentsProvider.TestResultPath).Returns(@"C:\Results.trx");
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var testRun = fileSystemProvider.DeserializeTestRun("Exceptions.trx");

            var microsoftTestTestRunProvider = new MsTestTestRunProvider(consoleArgumentsProvider, log);
            string additionalArguments = microsoftTestTestRunProvider.GenerateAdditionalArgumentsForFailedTestsRun(testRun.Results.ToList(), newTestResultsPath);

            // Check if "/test:testmask1 /test:testmask2" parameters are removed from command line arguments
            Assert.AreEqual<string>(string.Format(@"/resultsfile:""{0}""   /retriesCount:3 /test:TestConsoleExtended /test:TestConsoleExtended_Second", newTestResultsPath), additionalArguments);
        }
    }
}