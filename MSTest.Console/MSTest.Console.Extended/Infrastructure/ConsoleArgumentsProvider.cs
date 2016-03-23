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
        private readonly string testResultFilePathRegexPattern = @".*resultsfile:(?<ResultsFilePath>[0-9A-Za-z\\:._-]{1,})";
        private readonly string testNewResultFilePathRegexPattern = @".*(?<NewResultsFilePathArgument>/newResultsfile:(?<NewResultsFilePath>[0-9A-Za-z\\:._-]{1,}))";
        private readonly string retriesRegexPattern = @".*(?<RetriesArgument>/retriesCount:(?<RetriesCount>[0-9]{1})).*";
        private readonly string failedTestsThresholdRegexPattern = @".*(?<ThresholdArgument>/threshold:(?<ThresholdCount>[0-9]{1,})).*";
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