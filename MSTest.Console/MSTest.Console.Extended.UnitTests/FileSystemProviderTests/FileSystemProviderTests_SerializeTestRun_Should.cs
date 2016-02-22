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
    public class FileSystemProviderTests_SerializeTestRun_Should
    {
        [TestMethod]
        public void SerializeTestResultsFile_WhenNoFailedTestsPresent()
        {
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var testRun = fileSystemProvider.DeserializeTestRun("NoExceptions.trx");

            fileSystemProvider.SerializeTestRun(testRun);

            string originalFileContent = File.ReadAllText("NoExceptions.trx");
            string newFileContent = File.ReadAllText(newFileName);
            var originalDoc = new XmlDocument();
            originalDoc.LoadXml(originalFileContent);
            var newDoc = new XmlDocument();
            newDoc.LoadXml(newFileContent);
            var originalXElement = XElement.Parse(originalFileContent);
            var newXElement = XElement.Parse(newFileContent);
            XElement.DeepEquals(originalXElement, newXElement);
        }

        [TestMethod]
        public void SerializeTestResultsFile_WhenFailedTestsPresent()
        {
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();
            string newFileName = Path.GetTempFileName();
            Mock.Arrange(() => consoleArgumentsProvider.NewTestResultPath).Returns(newFileName);
            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var testRun = fileSystemProvider.DeserializeTestRun("Exceptions.trx");

            fileSystemProvider.SerializeTestRun(testRun);

            string originalFileContent = File.ReadAllText("Exceptions.trx");
            string newFileContent = File.ReadAllText(newFileName);
            var originalDoc = new XmlDocument();
            originalDoc.LoadXml(originalFileContent);
            var newDoc = new XmlDocument();
            newDoc.LoadXml(newFileContent);
            var originalXElement = XElement.Parse(originalFileContent);
            var newXElement = XElement.Parse(newFileContent);
            XElement.DeepEquals(originalXElement, newXElement);
        }
    }
}