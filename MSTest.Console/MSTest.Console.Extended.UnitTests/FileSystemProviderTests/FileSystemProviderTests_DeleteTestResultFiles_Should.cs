// <copyright file="FileSystemProviderTests_DeleteTestResultFiles_Should.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Console.Extended.Infrastructure;
using MSTest.Console.Extended.Interfaces;
using Telerik.JustMock;

namespace MSTest.Console.Extended.UnitTests.FileSystemProviderTests
{
    [TestClass]
    public class FileSystemProviderTests_DeleteTestResultFiles_Should
    {
        [TestMethod]
        public void NoDeletedFiles_WhenShouldDeleteOldTestResultFilesTrue()
        {
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            var file = File.CreateText(newFileName);
            file.Close();

            Mock.Arrange(() => consoleArgumentsProvider.ShouldDeleteOldTestResultFiles).Returns(false);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            fileSystemProvider.DeleteTestResultFiles();
         
            Assert.IsTrue(File.Exists(newFileName));
        }

        [TestMethod]
        public void DeletedFiles_WhenShouldDeleteOldTestResultFilesFilesAndTwoFilesExist()
        {
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            var file = File.CreateText(newFileName);
            file.Close();
            string newFileName1 = Path.GetTempFileName();
            file = File.CreateText(newFileName);
            file.Close();
            Mock.Arrange(() => consoleArgumentsProvider.ShouldDeleteOldTestResultFiles).Returns(true);
            Mock.Arrange(() => consoleArgumentsProvider.TestResultPath).Returns(newFileName);
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName1);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            fileSystemProvider.DeleteTestResultFiles();
         
            Assert.IsFalse(File.Exists(newFileName));
            Assert.IsFalse(File.Exists(newFileName1));
        }
        
        [TestMethod]
        public void DeletedFirstFile_WhenShouldDeleteOldTestResultFilesFilesAndSecondFileNotExist()
        {
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            var file = File.CreateText(newFileName);
            file.Close();
            Mock.Arrange(() => consoleArgumentsProvider.ShouldDeleteOldTestResultFiles).Returns(true);
            Mock.Arrange(() => consoleArgumentsProvider.TestResultPath).Returns(newFileName);
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            fileSystemProvider.DeleteTestResultFiles();
         
            Assert.IsFalse(File.Exists(newFileName));
        }
    }
}