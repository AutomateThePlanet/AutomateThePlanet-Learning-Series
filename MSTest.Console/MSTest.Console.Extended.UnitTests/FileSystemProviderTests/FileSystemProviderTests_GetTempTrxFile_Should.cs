using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Console.Extended.Infrastructure;
using MSTest.Console.Extended.Interfaces;
using Telerik.JustMock;

namespace MSTest.Console.Extended.UnitTests.FileSystemProviderTests
{
    [TestClass]
    public class FileSystemProviderTests_GetTempTrxFile_Should
    {
        [TestMethod]
        public void GetTempTrxFile()
        {
            var consoleArgumentsProvider = Mock.Create<IConsoleArgumentsProvider>();

            var fileSystemProvider = new FileSystemProvider(consoleArgumentsProvider);
            var tempTestResultsFilePath = fileSystemProvider.GetTempTrxFile();

            Assert.AreEqual<string>(".trx", new FileInfo(tempTestResultsFilePath).Extension);
        }
    }
}