// <copyright file="MsTestTestRunProvider_GetFailedTestsPercent_Should.cs" company="Automate The Planet Ltd.">
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
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Console.Extended.Data;
using MSTest.Console.Extended.Infrastructure;
using MSTest.Console.Extended.Interfaces;
using Telerik.JustMock;

namespace MSTest.Console.Extended.UnitTests.MsTestTestRunProviderTests
{
    [TestClass]
    public class MsTestTestRunProviderGetFailedTestsPercentShould
    {
        [TestMethod]
        public void ReturnZero_WhenNoFailedTestsPresent()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            
            var microsoftTestTestRunProvider = new MsTestTestRunProvider(consoleArgumentsProvider, log);
            var failedTests = new List<TestRunUnitTestResult>();
            var allTests = new List<TestRunUnitTestResult>()
            {
                new TestRunUnitTestResult()
            };
            var failedTestsPercentage = microsoftTestTestRunProvider.CalculatedFailedTestsPercentage(failedTests, allTests);

            Assert.AreEqual<int>(0, failedTestsPercentage);
        }

        [TestMethod]
        public void Return50Percent_WhenOneFailedTestPresentOfTwo()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            
            var microsoftTestTestRunProvider = new MsTestTestRunProvider(consoleArgumentsProvider, log);
            var failedTests = new List<TestRunUnitTestResult>()
            {
                new TestRunUnitTestResult()
            };
            var allTests = new List<TestRunUnitTestResult>()
            {
                new TestRunUnitTestResult(),
                new TestRunUnitTestResult()
            };
            var failedTestsPercentage = microsoftTestTestRunProvider.CalculatedFailedTestsPercentage(failedTests, allTests);

            Assert.AreEqual<int>(50, failedTestsPercentage);
        }

        [TestMethod]
        public void ReturnZeroPercent_WhenNoTestsPresent()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            
            var microsoftTestTestRunProvider = new MsTestTestRunProvider(consoleArgumentsProvider, log);
            var failedTests = new List<TestRunUnitTestResult>();
            var allTests = new List<TestRunUnitTestResult>();
            var failedTestsPercentage = microsoftTestTestRunProvider.CalculatedFailedTestsPercentage(failedTests, allTests);

            Assert.AreEqual<int>(0, failedTestsPercentage);
        }
    }
}