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
        private readonly string _testResultFilePathRegexPattern = @".*resultsfile:(?<ResultsFilePath>[1-9A-Za-z\\:._]{1,})";
        private readonly string _testNewResultFilePathRegexPattern = @".*(?<NewResultsFilePathArgument>/newResultsfile:(?<NewResultsFilePath>[1-9A-Za-z\\:._]{1,}))";
        private readonly string _retriesRegexPattern = @".*(?<RetriesArgument>/retriesCount:(?<RetriesCount>[0-9]{1})).*";
        private readonly string _failedTestsThresholdRegexPattern = @".*(?<ThresholdArgument>/threshold:(?<ThresholdCount>[0-9]{1,3})).*";
        private readonly string _deleteOldFilesRegexPattern = @".*(?<DeleteOldFilesArgument>/deleteOldResultsFiles:(?<DeleteOldFilesValue>[a-zA-Z]{4,5})).*";
        private readonly string _argumentRegexPattern = @".*/(?<ArgumentName>[a-zA-Z]{1,}):(?<ArgumentValue>.*)";

        public ConsoleArgumentsProvider(string[] arguments)
        {
            ConsoleArguments = InitializeInitialConsoleArguments(arguments);
            InitializeTestResultsPath();
            InitializeNewTestResultsPath();
            InitializeRetriesCount();
            InitializeFailedTestsThreshold();
            InitializeDeleteOldResultFiles();
        }

        public string ConsoleArguments { get; set; }

        public string TestResultPath { get; set; }

        public string NewTestResultPath { get; set; }

        public int RetriesCount { get; set; }

        public int FailedTestsThreshold { get; set; }

        public bool ShouldDeleteOldTestResultFiles { get; set; }

        private void InitializeTestResultsPath()
        {
            var r1 = new Regex(_testResultFilePathRegexPattern, RegexOptions.Singleline);
            var currentMatch = r1.Match(ConsoleArguments);
            if (!currentMatch.Success)
            {
                throw new ArgumentException("You need to specify path to test results.");
            }
            TestResultPath = currentMatch.Groups["ResultsFilePath"].Value;
        }

        private void InitializeNewTestResultsPath()
        {
            var r1 = new Regex(_testNewResultFilePathRegexPattern, RegexOptions.Singleline);
            var currentMatch = r1.Match(ConsoleArguments);
            if (!currentMatch.Success)
            {
                NewTestResultPath = TestResultPath;
            }
            else
            {
                NewTestResultPath = currentMatch.Groups["NewResultsFilePath"].Value;
                ConsoleArguments = ConsoleArguments.Replace(currentMatch.Groups["NewResultsFilePathArgument"].Value, string.Empty);
            }
        }

        private void InitializeRetriesCount()
        {
            var r1 = new Regex(_retriesRegexPattern, RegexOptions.Singleline);
            var currentMatch = r1.Match(ConsoleArguments);
            if (!currentMatch.Success)
            {
                RetriesCount = 0;
            }
            else
            {
                RetriesCount = int.Parse(currentMatch.Groups["RetriesCount"].Value);
                ConsoleArguments = ConsoleArguments.Replace(currentMatch.Groups["RetriesArgument"].Value, string.Empty);
            }
        }

        private void InitializeFailedTestsThreshold()
        {
            var r1 = new Regex(_failedTestsThresholdRegexPattern, RegexOptions.Singleline);
            var currentMatch = r1.Match(ConsoleArguments);
            if (!currentMatch.Success)
            {
                FailedTestsThreshold = int.Parse(ConfigurationManager.AppSettings["ThresholdDefaultPercentage"]);
            }
            else
            {
                FailedTestsThreshold = int.Parse(currentMatch.Groups["ThresholdCount"].Value);
                ConsoleArguments = ConsoleArguments.Replace(currentMatch.Groups["ThresholdArgument"].Value, string.Empty);
            }
        }

        private void InitializeDeleteOldResultFiles()
        {
            var r1 = new Regex(_deleteOldFilesRegexPattern, RegexOptions.Singleline);
            var currentMatch = r1.Match(ConsoleArguments);
            if (!currentMatch.Success)
            {
                ShouldDeleteOldTestResultFiles = false;
            }
            else
            {
                ShouldDeleteOldTestResultFiles = bool.Parse(currentMatch.Groups["DeleteOldFilesValue"].Value);
                ConsoleArguments = ConsoleArguments.Replace(currentMatch.Groups["DeleteOldFilesArgument"].Value, string.Empty);
            }
        }

        private string InitializeInitialConsoleArguments(string[] arguments)
        {
            var sb = new StringBuilder();
            foreach (var currentArgument in arguments)
            {
                var currentValueToBeAppended = currentArgument;
                var currentArgumentPair = SplitArgumentNameAndValue(currentArgument);
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
            var argumentPair = new KeyValuePair<string, string>();
            var regexPattern = new Regex(_argumentRegexPattern, RegexOptions.Singleline);
            var currentMatch = regexPattern.Match(argument);
            if (currentMatch.Success)
            {
                argumentPair = new KeyValuePair<string, string>(currentMatch.Groups["ArgumentName"].Value, currentMatch.Groups["ArgumentValue"].Value);
            }

            return argumentPair;
        }
    }
}