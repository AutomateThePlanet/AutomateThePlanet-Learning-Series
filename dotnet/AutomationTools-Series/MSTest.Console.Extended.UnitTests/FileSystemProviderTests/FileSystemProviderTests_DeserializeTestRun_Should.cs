// <copyright file="FileSystemProviderTests_DeserializeTestRun_Should.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Console.Extended.Infrastructure;
using MSTest.Console.Extended.Interfaces;
using Telerik.JustMock;

namespace MSTest.Console.Extended.UnitTests.FileSystemProviderTests
{
    [TestClass]
    public class FileSystemProviderTestsDeserializeTestRunShould
    {
        [TestMethod]
        public void DeserializeTestResultsFile_WhenNoFailedTestsPresent()
        {
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            var newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var testRun = fileSystemProvider.DeserializeTestRun("NoExceptions.trx");
            Assert.AreEqual<int>(2, testRun.Results.Count());
            Assert.AreEqual<string>("Passed", testRun.Results.First().Outcome);
            Assert.IsNotNull(testRun.ResultSummary);
            Assert.AreEqual<int>(2, testRun.ResultSummary.Counters.Total);
            Assert.AreEqual<int>(2, testRun.ResultSummary.Counters.Passed);
            Assert.AreEqual<int>(0, testRun.ResultSummary.Counters.Failed);
            Assert.AreEqual<string>("Passed", testRun.ResultSummary.Outcome);
        }

        [TestMethod]
        public void DeserializeTestResultsFile_WhenFailedTestsPresent()
        {
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            var newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var testRun = fileSystemProvider.DeserializeTestRun("Exceptions.trx");
            Assert.AreEqual<int>(2, testRun.Results.Count());
            Assert.AreEqual<string>("Failed", testRun.Results.First().Outcome);
            Assert.IsNotNull(testRun.ResultSummary);
            Assert.AreEqual<int>(2, testRun.ResultSummary.Counters.Total);
            Assert.AreEqual<int>(1, testRun.ResultSummary.Counters.Passed);
            Assert.AreEqual<int>(1, testRun.ResultSummary.Counters.Failed);
            Assert.AreEqual<string>("Failed", testRun.ResultSummary.Outcome);
        }

        [TestMethod]
        public void DeserializeTestResultsFile_WhenFailedTestsPresentAndNoTestResultsFilePassed()
        {
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            Mock.Arrange(() => consoleArgumentsProvider.TestResultPath).Returns("Exceptions.trx");
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var testRun = fileSystemProvider.DeserializeTestRun();
            Assert.AreEqual<int>(2, testRun.Results.Count());
            Assert.AreEqual<string>("Failed", testRun.Results.First().Outcome);
            Assert.IsNotNull(testRun.ResultSummary);
            Assert.AreEqual<int>(2, testRun.ResultSummary.Counters.Total);
            Assert.AreEqual<int>(1, testRun.ResultSummary.Counters.Passed);
            Assert.AreEqual<int>(1, testRun.ResultSummary.Counters.Failed);
            Assert.AreEqual<string>("Failed", testRun.ResultSummary.Outcome);
        }
    }
}