// <copyright file="FileSystemProviderTests_SerializeTestRun_Should.cs" company="Automate The Planet Ltd.">
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
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Console.Extended.Infrastructure;
using MSTest.Console.Extended.Interfaces;
using Telerik.JustMock;

namespace MSTest.Console.Extended.UnitTests.FileSystemProviderTests
{
    [TestClass]
    public class FileSystemProviderTestsSerializeTestRunShould
    {
        [TestMethod]
        public void SerializeTestResultsFile_WhenNoFailedTestsPresent()
        {
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            var newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var testRun = fileSystemProvider.DeserializeTestRun("NoExceptions.trx");

            fileSystemProvider.SerializeTestRun(testRun);

            var originalFileContent = File.ReadAllText("NoExceptions.trx");
            var newFileContent = File.ReadAllText(newFileName);
            var originalDoc = new XmlDocument();
            originalDoc.LoadXml(originalFileContent);
            var newDoc = new XmlDocument();
            newDoc.LoadXml(newFileContent);
            var originalXElement = XElement.Parse(originalFileContent);
            var newXElement = XElement.Parse(newFileContent);
            XNode.DeepEquals(originalXElement, newXElement);
        }

        [TestMethod]
        public void SerializeTestResultsFile_WhenFailedTestsPresent()
        {
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            var newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var testRun = fileSystemProvider.DeserializeTestRun("Exceptions.trx");

            fileSystemProvider.SerializeTestRun(testRun);

            var originalFileContent = File.ReadAllText("Exceptions.trx");
            var newFileContent = File.ReadAllText(newFileName);
            var originalDoc = new XmlDocument();
            originalDoc.LoadXml(originalFileContent);
            var newDoc = new XmlDocument();
            newDoc.LoadXml(newFileContent);
            var originalXElement = XElement.Parse(originalFileContent);
            var newXElement = XElement.Parse(newFileContent);
            XNode.DeepEquals(originalXElement, newXElement);
        }
    }
}