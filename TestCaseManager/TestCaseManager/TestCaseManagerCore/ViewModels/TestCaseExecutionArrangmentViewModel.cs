// <copyright file="TestCaseExecutionArrangmentViewModel.cs" company="Automate The Planet Ltd.">
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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using AAngelov.Utilities.UI.Core;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Managers;

    /// <summary>
    /// Contains methods and properties related to the TestCasesInitial View
    /// </summary>
    public class TestCaseExecutionArrangmentViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseExecutionArrangmentViewModel"/> class.
        /// </summary>
        public TestCaseExecutionArrangmentViewModel(int suiteId)
        {
            List<TestCase> suiteTestCases = TestCaseManager.GetAllTestCaseFromSuite(ExecutionContext.Preferences.TestPlan, suiteId, false);
            this.CurrentSuite = TestSuiteManager.GetTestSuiteById(ExecutionContext.TestManagementTeamProject, ExecutionContext.Preferences.TestPlan, suiteId) as IStaticTestSuite;
            this.ObservableTestCases = new ObservableCollection<TestCase>();
            this.InitialTestCaseCollection = new List<TestCase>();
            suiteTestCases.ForEach(t => this.ObservableTestCases.Add(t));
            this.InitializeInitialTestCaseCollection(this.ObservableTestCases);
            this.ViewTitle = string.Format("Arrange Test Cases in Suite: {0}", this.CurrentSuite.Title);
        }

        /// <summary>
        /// Gets or sets the observable test cases.
        /// </summary>
        /// <value>
        /// The observable test cases.
        /// </value>
        public ObservableCollection<TestCase> ObservableTestCases { get; set; }

        /// <summary>
        /// Gets or sets the initial test case collection.
        /// </summary>
        /// <value>
        /// The initial test case collection.
        /// </value>
        public List<TestCase> InitialTestCaseCollection { get; set; }

        /// <summary>
        /// Gets or sets the current suite.
        /// </summary>
        /// <value>
        /// The current suite.
        /// </value>
        public IStaticTestSuite CurrentSuite { get; set; }

        /// <summary>
        /// Gets or sets the view title.
        /// </summary>
        /// <value>
        /// The view title.
        /// </value>
        public string ViewTitle { get; set; }

        /// <summary>
        /// Initializes the initial test case collection.
        /// </summary>
        /// <param name="testCases">The test cases.</param>
        public void InitializeInitialTestCaseCollection(ICollection<TestCase> testCases)
        {
            this.InitialTestCaseCollection.Clear();
            foreach (var currentTestCase in testCases)
            {
                this.InitialTestCaseCollection.Add(currentTestCase);
            }
        }

        /// <summary>
        /// Creates the new test case collection after move up.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="selectedCount">The count of the selected steps.</param>
        public void CreateNewTestCaseCollectionAfterMoveUp(int startIndex, int selectedCount)
        {
            List<TestCase> newCollection = new List<TestCase>();
            for (int i = 0; i < startIndex - 1; i++)
            {
                newCollection.Add(this.ObservableTestCases[i]);
            }

            for (int i = startIndex; i < startIndex + selectedCount; i++)
            {
                newCollection.Add(this.ObservableTestCases[i]);
            }
            for (int i = startIndex - 1; i < startIndex; i++)
            {
                newCollection.Add(this.ObservableTestCases[i]);
            }

            for (int i = startIndex + selectedCount; i < this.ObservableTestCases.Count; i++)
            {
                newCollection.Add(this.ObservableTestCases[i]);
            }

            this.ObservableTestCases.Clear();
            newCollection.ForEach(x => this.ObservableTestCases.Add(x));
            //UndoRedoManager.Instance().Push((si, c) => this.CreateNewTestStepCollectionAfterMoveDown(si, c), startIndex - 1, selectedCount, "Move up selected test steps");
        }

        /// <summary>
        /// Creates the new test case collection after move down.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="selectedCount">The count of the selected test steps.</param>
        public void CreateNewTestCaseCollectionAfterMoveDown(int startIndex, int selectedCount)
        {
            List<TestCase> newCollection = new List<TestCase>();
            for (int i = 0; i < startIndex; i++)
            {
                newCollection.Add(this.ObservableTestCases[i]);
            }
            newCollection.Add(this.ObservableTestCases[startIndex + selectedCount]);
            for (int i = startIndex; i < startIndex + selectedCount; i++)
            {
                newCollection.Add(this.ObservableTestCases[i]);
            }

            for (int i = startIndex + selectedCount + 1; i < this.ObservableTestCases.Count; i++)
            {
                newCollection.Add(this.ObservableTestCases[i]);
            }

            this.ObservableTestCases.Clear();
            newCollection.ForEach(x => this.ObservableTestCases.Add(x));
            //UndoRedoManager.Instance().Push((si, c) => this.CreateNewTestCaseCollectionAfterMoveUp(si, c), startIndex + 1, selectedCount, "Move down selected test steps");
        }

        /// <summary>
        /// Saves the arrangement.
        /// </summary>
        public void SaveArrangement()
        {
            int startIndex = -1;
            foreach (TestCase currentTestCase in this.ObservableTestCases)
            {
                if (currentTestCase.ITestCase != null)
                {
                    int currentIndex = this.GetOldTestCaseIndexInSuiteCollection(currentTestCase);
                    this.CurrentSuite.Entries.Move(currentIndex, ++startIndex);
                }
            }
            ExecutionContext.Preferences.TestPlan.Save();
        }

        /// <summary>
        /// Gets the old test case index information suite collection.
        /// </summary>
        /// <param name="currentTestCase">The current test case.</param>
        /// <returns></returns>
        private int GetOldTestCaseIndexInSuiteCollection(TestCase currentTestCase)
        {
            int oldIndex = -1;
            for (int i = 0; i < this.CurrentSuite.Entries.Count; i++)
            {
                if (this.CurrentSuite.Entries[i].Id.Equals(currentTestCase.Id))
                {
                    oldIndex = i;
                }
            }

            return oldIndex;
        }
    }
}