using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using MSTest.Console.Extended.Data;
using MSTest.Console.Extended.Interfaces;

namespace MSTest.Console.Extended.Infrastructure
{
    public class FileSystemProvider : IFileSystemProvider
    {
        private readonly IConsoleArgumentsProvider consoleArgumentsProvider;

        public FileSystemProvider(IConsoleArgumentsProvider consoleArgumentsProvider)
        {
            this.consoleArgumentsProvider = consoleArgumentsProvider;
        }
    
        public void SerializeTestRun(TestRun updatedTestRun)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TestRun));
            TextWriter writer = new StreamWriter(this.consoleArgumentsProvider.NewTestResultPath);
            using (writer)
            {
                serializer.Serialize(writer, updatedTestRun); 
            }
        }

        public TestRun DeserializeTestRun(string resultsPath = "")
        {
            TestRun testRun = null;
            if (string.IsNullOrEmpty(resultsPath))
            {
                resultsPath = this.consoleArgumentsProvider.TestResultPath;
            }
            if (File.Exists(resultsPath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TestRun));
                StreamReader reader = new StreamReader(resultsPath);
                testRun = (TestRun)serializer.Deserialize(reader);
                reader.Close();
            }
            return testRun;
        }

        public void DeleteTestResultFiles()
        {
            if (this.consoleArgumentsProvider.ShouldDeleteOldTestResultFiles)
            {
                var filesToBeDeleted = new List<string>()
                {
                    this.consoleArgumentsProvider.TestResultPath,
                    this.consoleArgumentsProvider.NewTestResultPath
                };
                foreach (var currentFilePath in filesToBeDeleted)
                {
                    if (File.Exists(currentFilePath))
                    {
                        File.Delete(currentFilePath);
                    }
                }
            }
        }

        public string GetTempTrxFile()
        {
            return Path.GetTempFileName().Replace(".tmp", ".trx");
        }
    }
}