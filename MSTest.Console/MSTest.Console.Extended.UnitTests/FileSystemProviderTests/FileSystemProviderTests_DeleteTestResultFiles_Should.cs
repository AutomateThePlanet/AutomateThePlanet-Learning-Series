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