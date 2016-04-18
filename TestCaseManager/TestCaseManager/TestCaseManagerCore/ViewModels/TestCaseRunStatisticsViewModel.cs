// <copyright file="TestCaseRunStatisticsViewModel.cs" company="Automate The Planet Ltd.">
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
namespace TestCaseManagerCore.ViewModels
{
    using AAngelov.Utilities.Entities;
    using AAngelov.Utilities.UI.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Documents;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Managers;

    /// <summary>
    /// Contains methods and properties related to the TestCaseRunStatistics View
    /// </summary>
    public class TestCaseRunStatisticsViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The total execution time
        /// </summary>
        private string totalExecutionTime;

        /// <summary>
        /// The selected test cases execution time
        /// </summary>
        private string selectedTestCasesExecutionTime;

        /// <summary>
        /// The execution times sum
        /// </summary>
        private long executionTimesSum;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseRunStatisticsViewModel" /> class.
        /// </summary>
        public TestCaseRunStatisticsViewModel()
        {
            this.ObservableTestCases = new ObservableCollection<TestCase>();
            ExecutionContext.SelectedTestCasesForChange.ForEach(t => this.ObservableTestCases.Add(t));
            this.TestCaseExecutionResultsMappings = new Dictionary<int, List<TestCaseRunResult>>();
            this.InitializeLastExecutionTimesTestCases();
            this.TotalExecutionTime = new TimeSpan(executionTimesSum).ToString(TimeSpanFormats.HourFormat);
            this.SelectedTestCasesExecutionTime = new TimeSpan().ToString(TimeSpanFormats.HourFormat);
        }

        /// <summary>
        /// Gets or sets the observable test cases.
        /// </summary>
        /// <value>
        /// The observable test cases.
        /// </value>
        public ObservableCollection<TestCase> ObservableTestCases { get; set; }

        /// <summary>
        /// Gets or sets the test case execution results mappings.
        /// </summary>
        /// <value>
        /// The test case execution results mappings.
        /// </value>
        public Dictionary<int, List<TestCaseRunResult>> TestCaseExecutionResultsMappings { get; set; }

        /// <summary>
        /// Gets or sets the total execution time.
        /// </summary>
        /// <value>
        /// The total execution time.
        /// </value>
        public string TotalExecutionTime
        {
            get
            {
                return this.totalExecutionTime;
            }

            set
            {
                this.totalExecutionTime = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected test cases execution time.
        /// </summary>
        /// <value>
        /// The selected test cases execution time.
        /// </value>
        public string SelectedTestCasesExecutionTime
        {
            get
            {
                return this.selectedTestCasesExecutionTime;
            }

            set
            {
                this.selectedTestCasesExecutionTime = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes the last execution times test cases.
        /// </summary>
        private void InitializeLastExecutionTimesTestCases()
        {
            foreach (TestCase currentTestCase in this.ObservableTestCases)
            {
                List<TestCaseRunResult> testCaseResultRuns = TestCaseManager.GetLatestExecutionTimes(ExecutionContext.TestManagementTeamProject, ExecutionContext.Preferences.TestPlan, currentTestCase.Id);
                testCaseResultRuns.Sort();
                this.TestCaseExecutionResultsMappings.Add(currentTestCase.Id, testCaseResultRuns);
                TimeSpan currentTestCaseExecutionTime = testCaseResultRuns.Count > 0 ? testCaseResultRuns.Last().Duration : new TimeSpan();
                currentTestCase.LastExecutionTime = currentTestCaseExecutionTime.ToString(TimeSpanFormats.HourFormat); 
                executionTimesSum += currentTestCaseExecutionTime.Ticks;
                currentTestCase.LastExecutionTimesToolTip = this.InitializeTestCaseExecutionTimesToolTip(testCaseResultRuns);
            }
        }

        /// <summary>
        /// Calculates the total execution time selected test case.
        /// </summary>
        /// <param name="selectedTestCases">The selected test cases.</param>
        public void CalculateTotalExecutionTimeSelectedTestCase(ICollection<TestCase> selectedTestCases)
        {
            long sumTotalTime = default(long);
            foreach (TestCase currentTestCase in selectedTestCases)
            {
                sumTotalTime += this.TestCaseExecutionResultsMappings[currentTestCase.Id].Count > 0 ?
                    this.TestCaseExecutionResultsMappings[currentTestCase.Id].Last().Duration.Ticks :
                    new TimeSpan().Ticks;
            }
            this.SelectedTestCasesExecutionTime = new TimeSpan(sumTotalTime).ToString(TimeSpanFormats.HourFormat);
        }

        /// <summary>
        /// Initializes the test case execution times tool tip.
        /// </summary>
        /// <param name="testCaseRunResults">The test case run results.</param>
        /// <returns>test case tooltip</returns>
        private string InitializeTestCaseExecutionTimesToolTip(List<TestCaseRunResult> testCaseRunResults)
        {
            StringBuilder sb = new StringBuilder();
            int count = 1;
            testCaseRunResults.Reverse();
            var lastTenRunResults = testCaseRunResults.Take(10);
            foreach (TestCaseRunResult testCaseRunResult in lastTenRunResults)
            {
                sb.AppendLine(string.Format("{0}. StartDate {1} | EndDate- {2} | Duration- {3} | RunBy- {4}",
                    count++,
                    testCaseRunResult.StartDate.ToShortDateString(),
                    testCaseRunResult.EndDate.ToShortDateString(),
                    testCaseRunResult.Duration.ToString(TimeSpanFormats.HourFormat),
                    testCaseRunResult.RunBy));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Calculates the average.
        /// </summary>
        /// <param name="testCaseRunResults">The test case run results.</param>
        /// <returns>average execution time</returns>
        ////private TimeSpan CalculateAverage(List<TestCaseRunResult> testCaseRunResults)
        ////{
        ////    TimeSpan result = default(TimeSpan);
        ////    long ticks = default(long);
        ////    foreach (TestCaseRunResult currentR in testCaseRunResults)
        ////    {
        ////        ticks += currentR.Duration.Ticks;
        ////    }
        ////    if (testCaseRunResults.Count > 0)
        ////    {
        ////        result = new TimeSpan(ticks / testCaseRunResults.Count);
        ////    }

        ////    return result;
        ////}
    }
}