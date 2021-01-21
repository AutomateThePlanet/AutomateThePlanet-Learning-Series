// <copyright file="ProcessExecutionProviderTests_ExecuteProcessWithAdditionalArguments_Should.cs" company="Automate The Planet Ltd.">
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
using MSTest.Console.Extended.Infrastructure;
using MSTest.Console.Extended.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using log4net;

namespace MSTest.Console.Extended.UnitTests.ProcessExecutionProviderTests
{
    [TestClass]
    public class ProcessExecutionProviderTestsExecuteProcessWithAdditionalArgumentsShould
    {
        [TestMethod]
        public void StartProcessWithArguments()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
            var processExecutionProvider = new ProcessExecutionProvider("cmd.exe", null, log);

            processExecutionProvider.ExecuteProcessWithAdditionalArguments("ipconfig");

            Assert.IsNotNull(processExecutionProvider.CurrentProcess);
            Assert.IsNotNull(processExecutionProvider.CurrentProcess.StartInfo);
            Assert.AreEqual("cmd.exe", processExecutionProvider.CurrentProcess.StartInfo.FileName);
            Assert.AreEqual("ipconfig", processExecutionProvider.CurrentProcess.StartInfo.Arguments);
            processExecutionProvider.CurrentProcess.Kill();
        }

        [TestMethod]
        public void StartProcessWithDefaultConsoleArguments_WhenArgumentsNotSet()
        {
            var log = Mock.Create<ILog>();
            Mock.Arrange(() => log.Info(Arg.AnyString));
             
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            Mock.Arrange(() => consoleArgumentsProvider.ConsoleArguments).Returns("ipconfig");
            var processExecutionProvider = new ProcessExecutionProvider("cmd.exe", consoleArgumentsProvider, log);

            processExecutionProvider.ExecuteProcessWithAdditionalArguments();

            Assert.IsNotNull(processExecutionProvider.CurrentProcess);
            Assert.IsNotNull(processExecutionProvider.CurrentProcess.StartInfo);
            Assert.AreEqual("cmd.exe", processExecutionProvider.CurrentProcess.StartInfo.FileName);
            Assert.AreEqual("ipconfig", processExecutionProvider.CurrentProcess.StartInfo.Arguments);
            processExecutionProvider.CurrentProcess.Kill();
        }
    }
}