﻿// <copyright file="MsTestTestRunProvider_GetAllFailedTests_Should.cs" company="Automate The Planet Ltd.">
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
    public class MsTestTestRunProviderGetAllFailedTestsShould
    {
        [TestMethod]
        public void GetAllFailedTests_WhenAllArePassed()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            var newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var testRun = fileSystemProvider.DeserializeTestRun("NoExceptions.trx");
            
            var microsoftTestTestRunProvider = new MsTestTestRunProvider(consoleArgumentsProvider, log);
            var failedTests = microsoftTestTestRunProvider.GetAllNotPassedTests(testRun.Results.ToList());
            Assert.AreEqual<int>(0, failedTests.Count);
        }

        [TestMethod]
        public void GetAllFailedTests_WhenNotAllArePassed()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            var newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var testRun = fileSystemProvider.DeserializeTestRun("Exceptions.trx");
            
            var microsoftTestTestRunProvider = new MsTestTestRunProvider(consoleArgumentsProvider, log);
            var failedTests = microsoftTestTestRunProvider.GetAllNotPassedTests(testRun.Results.ToList());
            Assert.AreEqual<int>(1, failedTests.Count);
        }
    }
}