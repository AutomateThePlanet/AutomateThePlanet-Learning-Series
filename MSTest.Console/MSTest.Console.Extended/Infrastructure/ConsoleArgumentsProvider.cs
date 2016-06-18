// <copyright file="ConsoleArgumentsProvider.cs" company="Automate The Planet Ltd.">
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

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using MSTest.Console.Extended.Interfaces;

namespace MSTest.Console.Extended.Infrastructure
{
    public class ConsoleArgumentsProvider : IConsoleArgumentsProvider
    {
        private readonly string testResultFilePathRegexPattern = @".*resultsfile:(?<ResultsFilePath>[1-9A-Za-z\\:._]{1,})";
        private readonly string testNewResultFilePathRegexPattern = @".*(?<NewResultsFilePathArgument>/newResultsfile:(?<NewResultsFilePath>[1-9A-Za-z\\:._]{1,}))";
        private readonly string retriesRegexPattern = @".*(?<RetriesArgument>/retriesCount:(?<RetriesCount>[0-9]{1})).*";
        private readonly string failedTestsThresholdRegexPattern = @".*(?<ThresholdArgument>/threshold:(?<ThresholdCount>[0-9]{1,3})).*";
        private readonly string deleteOldFilesRegexPattern = @".*(?<DeleteOldFilesArgument>/deleteOldResultsFiles:(?<DeleteOldFilesValue>[a-zA-Z]{4,5})).*";
        private readonly string argumentRegexPattern = @".*/(?<ArgumentName>[a-zA-Z]{1,}):(?<ArgumentValue>.*)";

        public ConsoleArgumentsProvider(string[] arguments)
        {
            this.ConsoleArguments = this.InitializeInitialConsoleArguments(arguments);
            this.InitializeTestResultsPath();
            this.InitializeNewTestResultsPath();
            this.InitializeRetriesCount();
            this.InitializeFailedTestsThreshold();
            this.InitializeDeleteOldResultFiles();
        }

        public string ConsoleArguments { get; set; }

        public string TestResultPath { get; set; }

        public string NewTestResultPath { get; set; }

        public int RetriesCount { get; set; }

        public int FailedTestsThreshold { get; set; }

        public bool ShouldDeleteOldTestResultFiles { get; set; }

        private void InitializeTestResultsPath()
        {
            Regex r1 = new Regex(this.testResultFilePathRegexPattern, RegexOptions.Singleline);
            Match currentMatch = r1.Match(this.ConsoleArguments);
            if (!currentMatch.Success)
            {
                throw new ArgumentException("You need to specify path to test results.");
            }
            this.TestResultPath = currentMatch.Groups["ResultsFilePath"].Value;
        }

        private void InitializeNewTestResultsPath()
        {
            Regex r1 = new Regex(this.testNewResultFilePathRegexPattern, RegexOptions.Singleline);
            Match currentMatch = r1.Match(this.ConsoleArguments);
            if (!currentMatch.Success)
            {
                this.NewTestResultPath = this.TestResultPath;
            }
            else
            {
                this.NewTestResultPath = currentMatch.Groups["NewResultsFilePath"].Value;
                this.ConsoleArguments = this.ConsoleArguments.Replace(currentMatch.Groups["NewResultsFilePathArgument"].Value, string.Empty);
            }
        }

        private void InitializeRetriesCount()
        {
            Regex r1 = new Regex(this.retriesRegexPattern, RegexOptions.Singleline);
            Match currentMatch = r1.Match(this.ConsoleArguments);
            if (!currentMatch.Success)
            {
                this.RetriesCount = 0;
            }
            else
            {
                this.RetriesCount = int.Parse(currentMatch.Groups["RetriesCount"].Value);
                this.ConsoleArguments = this.ConsoleArguments.Replace(currentMatch.Groups["RetriesArgument"].Value, string.Empty);
            }
        }

        private void InitializeFailedTestsThreshold()
        {
            Regex r1 = new Regex(this.failedTestsThresholdRegexPattern, RegexOptions.Singleline);
            Match currentMatch = r1.Match(this.ConsoleArguments);
            if (!currentMatch.Success)
            {
                this.FailedTestsThreshold = int.Parse(ConfigurationManager.AppSettings["ThresholdDefaultPercentage"]);
            }
            else
            {
                this.FailedTestsThreshold = int.Parse(currentMatch.Groups["ThresholdCount"].Value);
                this.ConsoleArguments = this.ConsoleArguments.Replace(currentMatch.Groups["ThresholdArgument"].Value, string.Empty);
            }
        }

        private void InitializeDeleteOldResultFiles()
        {
            Regex r1 = new Regex(this.deleteOldFilesRegexPattern, RegexOptions.Singleline);
            Match currentMatch = r1.Match(this.ConsoleArguments);
            if (!currentMatch.Success)
            {
                this.ShouldDeleteOldTestResultFiles = false;
            }
            else
            {
                this.ShouldDeleteOldTestResultFiles = bool.Parse(currentMatch.Groups["DeleteOldFilesValue"].Value);
                this.ConsoleArguments = this.ConsoleArguments.Replace(currentMatch.Groups["DeleteOldFilesArgument"].Value, string.Empty);
            }
        }

        private string InitializeInitialConsoleArguments(string[] arguments)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var currentArgument in arguments)
            {
                string currentValueToBeAppended = currentArgument;
                KeyValuePair<string, string> currentArgumentPair = this.SplitArgumentNameAndValue(currentArgument);
                if (currentArgumentPair.Key != null && currentArgumentPair.Value.Contains(" "))
                {
                    currentValueToBeAppended = string.Concat("/", currentArgumentPair.Key, ":", "\"", currentArgumentPair.Value, "\"");
                }
                sb.AppendFormat("{0} ", currentValueToBeAppended);
            }
            return sb.ToString().TrimEnd();
        }

        private KeyValuePair<string, string> SplitArgumentNameAndValue(string argument)
        {
            KeyValuePair<string, string> argumentPair = new KeyValuePair<string, string>();
            Regex regexPattern = new Regex(this.argumentRegexPattern, RegexOptions.Singleline);
            Match currentMatch = regexPattern.Match(argument);
            if (currentMatch.Success)
            {
                argumentPair = new KeyValuePair<string, string>(currentMatch.Groups["ArgumentName"].Value, currentMatch.Groups["ArgumentValue"].Value);
            }

            return argumentPair;
        }
    }
}