using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Console.Extended.Infrastructure;

namespace MSTest.Console.Extended.UnitTests.ConsoleArgumentsProviderTests
{
    [TestClass]
    public class ConsoleArgumentsProvider_Constructor_Should
    {
        [TestMethod]
        public void SetRetriesCount_When_RetriesCountArgumentPresent()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\ResultsNew.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.AreEqual<int>(3, consoleArgumentsProvider.RetriesCount);
        }

        [TestMethod]
        public void SetRetriesCount_When_RetriesCountArgumentNotPresent()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\ResultsNew.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.AreEqual<int>(0, consoleArgumentsProvider.RetriesCount);
        }

        [TestMethod]
        public void SetTestResultsPath()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\ResultsNew.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.AreEqual<string>(@"C:\Results.trx", consoleArgumentsProvider.TestResultPath);
        }

        [TestMethod]
        public void SetTestResultsPath_WhenTestResultsPathContainsUnderscore()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results_FF.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\ResultsNew.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.AreEqual<string>(@"C:\Results_FF.trx", consoleArgumentsProvider.TestResultPath);
        }

        [TestMethod]
        public void SetTestResultsPath_WhenTestResultsPathContainsDigit()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results1.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\ResultsNew.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.AreEqual<string>(@"C:\Results1.trx", consoleArgumentsProvider.TestResultPath);
        }

        [TestMethod]
        [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "You need to specify path to test results.")]
        public void ThrowArgumentException_WhenTestResultsArgumentMissing()
        {
            string[] args = 
            {
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\ResultsNew.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.AreEqual<string>(@"C:\Results1.trx", consoleArgumentsProvider.TestResultPath);
        }

        [TestMethod]
        public void SetNewTestResultsPath_WhenNoNewResultsFileArgumentPresent()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.AreEqual<string>(@"C:\Results.trx", consoleArgumentsProvider.TestResultPath);
        }

        [TestMethod]
        public void SetNewTestResultsPath_WhenNewResultsFileArgumentPresent()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\ResultsNew.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.AreEqual<string>(@"C:\ResultsNew.trx", consoleArgumentsProvider.NewTestResultPath);
        }

        [TestMethod]
        public void SetNewTestResultsPath_WhenNewTestResultsPathContainsUnderscore()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results_FF.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\Results_New.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.AreEqual<string>(@"C:\Results_New.trx", consoleArgumentsProvider.NewTestResultPath);
        }

        [TestMethod]
        public void SetNewTestResultsPath_WhenNewTestResultsPathContainsDigit()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results1.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\ResultsNew1.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.AreEqual<string>(@"C:\ResultsNew1.trx", consoleArgumentsProvider.NewTestResultPath);
        }

        [TestMethod]
        public void SetDeleteOlsResultsFiles_WhenDeleteOlsResultsFilesArgumentPresent()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results1.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\ResultsNew1.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.AreEqual<bool>(true, consoleArgumentsProvider.ShouldDeleteOldTestResultFiles);
        }

        [TestMethod]
        public void SetDeleteOlsResultsFiles_WhenDeleteOlsResultsFilesArgumentNotPresent()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results1.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                @"/newResultsfile:C:\ResultsNew1.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.AreEqual<bool>(false, consoleArgumentsProvider.ShouldDeleteOldTestResultFiles);
        }

        [TestMethod]
        public void SetConsoleArgumentsWithoutDeleteOldResultsFiles_WhenDeleteOlsResultsFilesArgumentPresent()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results1.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\ResultsNew1.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.IsFalse(consoleArgumentsProvider.ConsoleArguments.Contains("/deleteOldResultsFiles:true"));
        }

        [TestMethod]
        public void SetConsoleArgumentsWithoutRetriesCount_WhenRetriesCountPresent()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results1.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\ResultsNew1.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.IsFalse(consoleArgumentsProvider.ConsoleArguments.Contains("/retriesCount:3"));
        }

        [TestMethod]
        public void SetConsoleArgumentsWithoutNewResultsFile_WhenNewResultsFile()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results1.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\ResultsNew1.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.IsFalse(consoleArgumentsProvider.ConsoleArguments.Contains(@"/newResultsfile:C:\ResultsNew1.trx"));
        }

        [TestMethod]
        public void SetQuotes_WhenArgumentValueContainsSpaces()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results1.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\Results New1.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.IsTrue(consoleArgumentsProvider.ConsoleArguments.Contains(@"/newResultsfile:""C:\Results New1.trx"""));
        }

        [TestMethod]
        public void SetThreshold_When_ThresholdArgumentPresent()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/retriesCount:3",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\ResultsNew.trx",
                "/threshold:5"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.AreEqual<int>(5, consoleArgumentsProvider.FailedTestsThreshold);
        }

        [TestMethod]
        public void SetThreshold_When_ThresholdArgumentNotPresent()
        {
            string[] args = 
            {
                @"/resultsfile:C:\Results.trx",
                @"/testcontainer:C:\Frontend\Tests.dll",
                "/nologo",
                "/category:MSTestConsoleExtendedTEST",
                "/deleteOldResultsFiles:true",
                @"/newResultsfile:C:\ResultsNew.trx"
            };
            var consoleArgumentsProvider = new ConsoleArgumentsProvider(args);
            Assert.AreEqual<int>(10, consoleArgumentsProvider.FailedTestsThreshold);
        }
    }
}