// <copyright file="FileSystemProvider.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using System.Xml.Serialization;
using MSTest.Console.Extended.Data;
using MSTest.Console.Extended.Interfaces;

namespace MSTest.Console.Extended.Infrastructure
{
    public class FileSystemProvider : IFileSystemProvider
    {
        private readonly IConsoleArgumentsProvider _consoleArgumentsProvider;

        public FileSystemProvider(IConsoleArgumentsProvider consoleArgumentsProvider)
        {
            this._consoleArgumentsProvider = consoleArgumentsProvider;
        }
    
        public void SerializeTestRun(TestRun updatedTestRun)
        {
            var serializer = new XmlSerializer(typeof(TestRun));
            TextWriter writer = new StreamWriter(_consoleArgumentsProvider.NewTestResultPath);
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
                resultsPath = _consoleArgumentsProvider.TestResultPath;
            }
            if (File.Exists(resultsPath))
            {
                var serializer = new XmlSerializer(typeof(TestRun));
                var reader = new StreamReader(resultsPath);
                testRun = (TestRun)serializer.Deserialize(reader);
                reader.Close();
            }
            return testRun;
        }

        public void DeleteTestResultFiles()
        {
            if (_consoleArgumentsProvider.ShouldDeleteOldTestResultFiles)
            {
                var filesToBeDeleted = new List<string>()
                {
                    _consoleArgumentsProvider.TestResultPath,
                    _consoleArgumentsProvider.NewTestResultPath
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