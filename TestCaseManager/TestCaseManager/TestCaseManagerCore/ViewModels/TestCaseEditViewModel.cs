// <copyright file="TestCaseEditViewModel.cs" company="Automate The Planet Ltd.">
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
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Xml;
    using AAngelov.Utilities.Entities;
    using AAngelov.Utilities.Managers;
    using AAngelov.Utilities.UI.Core;
    using FirstFloor.ModernUI.Windows.Controls;
    using Microsoft.TeamFoundation.Server;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Managers;

    /// <summary>
    /// Contains methods and properties related to the TestCaseEdit View
    /// </summary>
    public class TestCaseEditViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The shared step search default text
        /// </summary>
        public const string SharedStepSearchDefaultText = "Search for a Shared Step";

        /// <summary>
        /// The action default text
        /// </summary>
        public const string ActionDefaultText = "Action";

        /// <summary>
        /// The expected result default text
        /// </summary>
        public const string ExpectedResultDefaultText = "Expected Result";

        /// <summary>
        /// The is action text set
        /// </summary>
        public bool IsActionTextSet;

        /// <summary>
        /// The is expected result text set
        /// </summary>
        public bool IsExpectedResultTextSet;

        /// <summary>
        /// The is shared step search text set
        /// </summary>
        public bool IsSharedStepSearchTextSet;

        /// <summary>
        /// The shared steps filter
        /// </summary>
        public string sharedStepsFilter;

        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The page title
        /// </summary>
        private string pageTitle;

        /// <summary>
        /// Gets or sets the initial shared step collection.
        /// </summary>
        /// <value>
        /// The initial shared step collection.
        /// </value>
        public List<SharedStep> InitialSharedStepCollection;

        /// <summary>
        /// Occurs when [shared steps refresh event].
        /// </summary>
        public event EventHandler SharedStepsRefreshEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseEditViewModel"/> class.
        /// </summary>
        /// <param name="editViewContext">The edit view context.</param>
        public TestCaseEditViewModel(EditViewContext editViewContext)
        {
            this.EditViewContext = editViewContext;
            this.Areas = this.GetProjectAreas();
            this.ObservableTestSteps = new ObservableCollection<TestStep>();
            this.GenericParameters = new Dictionary<string, Dictionary<string, string>>();
            if (!this.EditViewContext.IsSharedStep)
            {
                this.ShowTestCaseSpecificFields = true;
                ITestSuiteBase testSuiteBaseCore = null;
                if (this.EditViewContext.TestSuiteId != -1)
                {
                    testSuiteBaseCore = TestSuiteManager.GetTestSuiteById(TestCaseManagerCore.ExecutionContext.TestManagementTeamProject, TestCaseManagerCore.ExecutionContext.Preferences.TestPlan, this.EditViewContext.TestSuiteId);
                }
                if (this.EditViewContext.CreateNew && !this.EditViewContext.Duplicate)
                {
                    ITestCase newTestCase = TestCaseManagerCore.ExecutionContext.TestManagementTeamProject.TestCases.Create();
                    this.TestCase = new TestCase(newTestCase, testSuiteBaseCore, TestCaseManagerCore.ExecutionContext.Preferences.TestPlan, false);
                }
                else
                {
                    ITestCase testCaseCore = TestCaseManagerCore.ExecutionContext.TestManagementTeamProject.TestCases.Find(this.EditViewContext.TestCaseId);
                    this.TestCase = new TestCase(testCaseCore, testSuiteBaseCore, TestCaseManagerCore.ExecutionContext.Preferences.TestPlan, false);
                }
                this.ObservableSharedSteps = new ObservableCollection<SharedStep>();
                this.InitializeObservableSharedSteps();
                this.InitializeInitialSharedStepCollection();
                this.InitializeTestCaseTestStepsFromITestCaseActions();   
                this.AssociatedAutomation = this.TestCase.ITestCase.GetAssociatedAutomation();
                this.TestBase = this.TestCase;
            }
            else
            { 
                if (this.EditViewContext.CreateNew && !this.EditViewContext.Duplicate)
                {
                    ISharedStep currentSharedStepCore = TestCaseManagerCore.ExecutionContext.TestManagementTeamProject.SharedSteps.Create();
                    this.SharedStep = new SharedStep(currentSharedStepCore);
                }
                else
                {
                    SharedStep currentSharedStep = SharedStepManager.GetSharedStepById(TestCaseManagerCore.ExecutionContext.TestManagementTeamProject, this.EditViewContext.SharedStepId);
                    this.SharedStep = currentSharedStep;
                }

                List<TestStep> innerTestSteps = TestStepManager.GetAllTestStepsInSharedStep(this.SharedStep.ISharedStep, false);
                this.AddTestStepsToObservableCollection(innerTestSteps);
                this.ShowTestCaseSpecificFields = false;
                this.TestBase = this.SharedStep;
                this.ClearTestStepNames();
            }
            this.InitializeIdLabelFromTestBase(this.EditViewContext.CreateNew, this.EditViewContext.Duplicate);
            this.InitializePageTitle();
            log.InfoFormat("Load Edit View with Context: {0} ", editViewContext);

            TestStepManager.UpdateGenericSharedSteps(this.ObservableTestSteps);
        }

        /// <summary>
        /// Gets or sets the test case.
        /// </summary>
        /// <value>
        /// The test case.
        /// </value>
        public TestCase TestCase { get; set; }

        /// <summary>
        /// Gets or sets the cancellation token.
        /// </summary>
        /// <value>
        /// The cancellation token.
        /// </value>
        public CancellationToken CancellationToken { get; set; }

        /// <summary>
        /// Gets or sets the shared step.
        /// </summary>
        /// <value>
        /// The shared step.
        /// </value>
        public SharedStep SharedStep { get; set; }

        /// <summary>
        /// Gets or sets the edit view context.
        /// </summary>
        /// <value>
        /// The edit view context.
        /// </value>
        public EditViewContext EditViewContext { get; set; }

        /// <summary>
        /// Gets or sets the test base.
        /// </summary>
        /// <value>
        /// The test base.
        /// </value>
        public TestBase TestBase { get; set; }

        /// <summary>
        /// Gets or sets the test case unique identifier label.
        /// </summary>
        /// <value>
        /// The test case unique identifier label.
        /// </value>
        public string TestCaseIdLabel { get; set; }

        /// <summary>
        /// Gets or sets the observable test steps.
        /// </summary>
        /// <value>
        /// The observable test steps.
        /// </value>
        public ObservableCollection<TestStep> ObservableTestSteps { get; set; }

        /// <summary>
        /// Gets or sets the test case test steps. Used to preserve the current state of the test case test steps if the user edit shared step.
        /// </summary>
        /// <value>
        /// The test case test steps.
        /// </value>
        public List<TestStep> TestCaseTestSteps { get; set; }

        /// <summary>
        /// Gets or sets the observable shared steps.
        /// </summary>
        /// <value>
        /// The observable shared steps.
        /// </value>
        public ObservableCollection<SharedStep> ObservableSharedSteps { get; set; }

        /// <summary>
        /// Gets or sets the test suite list.
        /// </summary>
        /// <value>
        /// The test suite list.
        /// </value>
        public List<ITestSuiteBase> TestSuiteList { get; set; }

        /// <summary>
        /// Gets or sets the associated automation.
        /// </summary>
        /// <value>
        /// The associated automation.
        /// </value>
        public AssociatedAutomation AssociatedAutomation { get; set; }

        /// <summary>
        /// Gets or sets the areas.
        /// </summary>
        /// <value>
        /// The areas.
        /// </value>
        public List<string> Areas { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show test case specific fields].
        /// </summary>
        /// <value>
        /// <c>true</c> if [show test case specific fields]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowTestCaseSpecificFields { get; set; }

        /// <summary>
        /// Gets or sets the generic parameters.
        /// </summary>
        /// <value>
        /// The generic parameters.
        /// </value>
        public Dictionary<string, Dictionary<string, string>> GenericParameters { get; set; }

        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        /// <value>
        /// The page title.
        /// </value>
        public string PageTitle
        {
            get
            {
                return this.pageTitle;
            }

            set
            {
                this.pageTitle = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the shared steps filter.
        /// </summary>
        /// <value>
        /// The shared steps filter.
        /// </value>
        public string SharedStepsFilter
        {
            get
            {
                return this.sharedStepsFilter;
            }

            set
            {
                this.sharedStepsFilter = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Copies the current test steps automatic copy.
        /// </summary>
        public void CopyCurrentTestStepsToCopy()
        {
            this.TestCaseTestSteps = new List<TestStep>();
            foreach (TestStep currentTestStep in this.ObservableTestSteps)
            {
                this.TestCaseTestSteps.Add(currentTestStep);
            }
        }

        /// <summary>
        /// Saves the changes dialog.
        /// </summary>
        /// <returns></returns>
        public MessageBoxResult SaveChangesDialog()
        {
            MessageBoxResult messageBoxResult = MessageBoxResult.None;
            if (UndoRedoManager.Instance().HasUndoOperations || UndoRedoManager.Instance().HasRedoOperations)
            {
                messageBoxResult = ModernDialog.ShowMessage("Do you want to save changes to the following item?", "Save Changes!", MessageBoxButton.YesNoCancel);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.SaveEntityInternal();
                }
            }

            return messageBoxResult;
        }

        /// <summary>
        /// Filters the shared steps.
        /// </summary>
        public void FilterSharedSteps(string sharedStepsFilter)
        {
            if (this.InitialSharedStepCollection != null && this.ObservableSharedSteps != null)
            {
                bool shouldSetSharedStepsFilter = this.IsSharedStepSearchTextSet && !string.IsNullOrEmpty(sharedStepsFilter);

                var filteredList = this.InitialSharedStepCollection
                                       .Where(t => (shouldSetSharedStepsFilter ? t.ISharedStep.Title.ToLower().Contains(sharedStepsFilter.ToLower()) : true)).ToList();
                this.ObservableSharedSteps.Clear();
                filteredList.ForEach(x => this.ObservableSharedSteps.Add(x));
            }
        }

        /// <summary>
        /// Saves the entity internal.
        /// </summary>
        /// <returns>test base</returns>
        public TestBase SaveEntityInternal()
        {
            TestBase currentBase;
            if (!this.EditViewContext.IsSharedStep)
            {
                log.Info("Save test case.");
                currentBase = this.SaveTestCaseInternal();
            }
            else
            {
                log.Info("Save shared step.");
                currentBase = this.SaveSharedStepInternal();
            }
            UndoRedoManager.Instance().Clear();            

            return currentBase;
        }

        /// <summary>
        /// Saves the test case internal.
        /// </summary>
        /// <returns>the saved test case</returns>
        public TestCase SaveTestCaseInternal()
        {
            this.TestCase.Title = this.TestBase.Title;
            this.TestCase.Area = this.TestBase.Area;
            this.TestCase.Priority = this.TestBase.Priority;
            this.TestCase.TeamFoundationIdentityName = this.TestBase.TeamFoundationIdentityName;
            this.TestCase.TeamFoundationId = this.TestBase.TeamFoundationId;
            this.TestCase.OwnerDisplayName = this.TestBase.OwnerDisplayName;
            log.InfoFormat("Test Case Saved Info: Title= {0}, Area= {1}, Priority= {2}, OwnerDisplayName= {3}", this.TestCase.Title, this.TestCase.Area, this.TestCase.Priority, this.TestCase.OwnerDisplayName);
            TestCase savedTestCase;
            int? suiteId = this.TestCase.ITestSuiteBase != null ? this.TestCase.TestSuiteId : null;
            if (string.IsNullOrEmpty(this.TestBase.Title))
            {
                ModernDialog.ShowMessage("Test case title cannot be empty!", "Warning", MessageBoxButton.OK);
                return null;
            }
            if ((this.EditViewContext.CreateNew || this.EditViewContext.Duplicate) && !this.EditViewContext.IsAlreadyCreated)
            {
                log.Info("Save test case as new one.");
                savedTestCase = this.TestCase.Save(TestCaseManagerCore.ExecutionContext.TestManagementTeamProject, TestCaseManagerCore.ExecutionContext.Preferences.TestPlan, true, suiteId, this.ObservableTestSteps);
                this.TestCase = savedTestCase;
                this.EditViewContext.IsAlreadyCreated = true;
                this.EditViewContext.CreateNew = false;
                this.EditViewContext.Duplicate = false;
            }
            else
            {
                log.InfoFormat("Save edited test case with id= {0}.", this.TestCase.Id);
                savedTestCase = this.TestCase.Save(TestCaseManagerCore.ExecutionContext.TestManagementTeamProject, TestCaseManagerCore.ExecutionContext.Preferences.TestPlan, false, suiteId, this.ObservableTestSteps);
            }
            this.EditViewContext.TestCaseId = savedTestCase.ITestCase.Id;
            this.TestCaseIdLabel = savedTestCase.ITestCase.Id.ToString();
            //this.TestCaseEditViewModel.TestBase = savedTestCase;

            return savedTestCase;
        }

        /// <summary>
        /// Saves the shared step internal.
        /// </summary>
        /// <returns></returns>
        public SharedStep SaveSharedStepInternal()
        {
            this.SharedStep.Title = this.TestBase.Title;
            this.SharedStep.Area = this.TestBase.Area;
            this.SharedStep.Priority = this.TestBase.Priority;
            this.SharedStep.TeamFoundationIdentityName = this.TestBase.TeamFoundationIdentityName;
            this.SharedStep.TeamFoundationId = this.TestBase.TeamFoundationId;
            this.SharedStep.OwnerDisplayName = this.TestBase.OwnerDisplayName;
            log.InfoFormat("Shared Step Saved Info: Title= {0}, Area= {1}, Priority= {2}, OwnerDisplayName= {3}", this.SharedStep.Title, this.SharedStep.Area, this.SharedStep.Priority, this.SharedStep.OwnerDisplayName);
            SharedStep savedSharedStep;
            if (string.IsNullOrEmpty(this.TestBase.Title))
            {
                ModernDialog.ShowMessage("Shared Step title cannot be empty!", "Warning", MessageBoxButton.OK);
                return null;
            }
            if ((this.EditViewContext.CreateNew || this.EditViewContext.Duplicate) && !this.EditViewContext.IsAlreadyCreated)
            {
                log.Info("Save shared step as new one.");
                savedSharedStep = this.SharedStep.Save(TestCaseManagerCore.ExecutionContext.TestManagementTeamProject, true, this.ObservableTestSteps);
                this.SharedStep = savedSharedStep;
                this.EditViewContext.IsAlreadyCreated = true;
                this.EditViewContext.CreateNew = false;
                this.EditViewContext.Duplicate = false;
            }
            else
            {
                log.InfoFormat("Save edited shared step with id= {0}.", this.SharedStep.Id);
                savedSharedStep = this.SharedStep.Save(TestCaseManagerCore.ExecutionContext.TestManagementTeamProject, false, this.ObservableTestSteps);
            }
            this.EditViewContext.SharedStepId = savedSharedStep.ISharedStep.Id;
            this.TestCaseIdLabel = this.EditViewContext.SharedStepId.ToString();

            return savedSharedStep;
        }

        /// <summary>
        /// Initializes the test steps from copy.
        /// </summary>
        public void InitializeTestStepsFromCopy()
        {
            foreach (TestStep currentTestStep in this.TestCaseTestSteps)
            {
                this.ObservableTestSteps.Add(currentTestStep);
            }
        }

        /// <summary>
        /// Reinitializes the shared step collection.
        /// </summary>
        public void ReinitializeSharedStepCollection()
        {
            this.ObservableSharedSteps.Clear();
            foreach (var item in this.InitialSharedStepCollection)
            {
                this.ObservableSharedSteps.Add(item);
            }
        }

        /// <summary>
        /// Gets the step title.
        /// </summary>
        /// <param name="stepTitle">The step title.</param>
        /// <returns>the test step title</returns>
        public string GetStepTitle(string stepTitle)
        {
            string finalStepTitle = stepTitle.Trim();
            if (finalStepTitle.Equals(ActionDefaultText))
            {
                finalStepTitle = string.Empty;
            }

            return finalStepTitle;
        }

        /// <summary>
        /// Determines whether [is shared step selected] [the specified test steps].
        /// </summary>
        /// <param name="testSteps">The test steps.</param>
        /// <returns>is shared step selected</returns>
        public bool IsSharedStepSelected(List<TestStep> testSteps)
        {
            return testSteps.Where(x => x.IsShared).Count() > 0;
        }

        /// <summary>
        /// Gets the expected result.
        /// </summary>
        /// <param name="expectedResult">The expected result.</param>
        /// <returns>the expected result</returns>
        public string GetExpectedResult(string expectedResult)
        {
            string finalExpectedResult = expectedResult.Trim();
            if (finalExpectedResult.Equals(ExpectedResultDefaultText))
            {
                finalExpectedResult = string.Empty;
            }

            return finalExpectedResult;
        }

        /// <summary>
        /// Deletes the cut test steps.
        /// </summary>
        /// <param name="cutTestSteps">The cut test steps.</param>
        public void DeleteCutTestSteps(List<TestStep> cutTestSteps)
        { 
            foreach (TestStep currentTestStepToBeRemoved in cutTestSteps)
            {
                for (int i = 0; i < this.ObservableTestSteps.Count; i++)
                {
                    int index = i - 1;

                    // If the step to be removed is initial we set -2 for index in order the Insert Operation to be completed successfully. 
                    // If index == -2 the insert function will insert the test step to the beginning of the collection
                    if (index == -1)
                    {
                        index--;
                    }
                    if (this.ObservableTestSteps[i].TestStepGuid.Equals(currentTestStepToBeRemoved.TestStepGuid))
                    {
                        this.RemoveTestStepFromObservableCollection(this.ObservableTestSteps[i], index);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Marks the initial steps to be removed.
        /// </summary>
        /// <param name="selectedTestSteps">The selected test steps.</param>
        /// <returns>the list of test steps to be removed</returns>
        public List<TestStepFull> MarkInitialStepsToBeRemoved(IList<TestStep> selectedTestSteps)
        {
            List<TestStepFull> testStepsToBeRemoved = new List<TestStepFull>();
            foreach (TestStep currentStepToBeRemoved in selectedTestSteps)
            {
                for (int i = 0; i < this.ObservableTestSteps.Count; i++)
                {
                    if (this.ObservableTestSteps[i].TestStepGuid.Equals(currentStepToBeRemoved.TestStepGuid))
                    {
                        int index = i - 1;

                        // If the step to be removed is initial we set -2 for index in order the Insert Operation to be completed successfully. 
                        // If index == -2 the insert function will insert the test step to the beginning of the collection
                        if (index == -1)
                        {
                            index--;
                        }
                        TestStepFull currentTestStep = new TestStepFull(this.ObservableTestSteps[i], index);
                        if (!testStepsToBeRemoved.Contains(currentTestStep))
                        {
                            testStepsToBeRemoved.Add(currentTestStep);
                        }
                    }
                }
            }

            return testStepsToBeRemoved;
        }

        /// <summary>
        /// Inserts the test step information to the test case.
        /// </summary>
        /// <param name="testStepToInsert">The test step to be insert.</param>
        /// <param name="selectedIndex">Index of the selected test step.</param>
        /// <param name="skipShared">if set to <c>true</c> [skip shared].</param>
        /// <returns>
        /// new selected index
        /// </returns>
        public int InsertTestStepInTestCase(TestStep testStepToInsert, int selectedIndex, bool skipShared = true)
        {
            if (!skipShared)
            {
                selectedIndex = this.FindNextNotSharedStepIndex(selectedIndex);
            }
            // If you delete first step and call redo operation, the step should be inserted at the beginning
            if (selectedIndex == -2)
            {
                log.InfoFormat("Insert test step ActionTitle= {0}, ActionExpectedResult= {1}, index= {2}", testStepToInsert.ActionTitle, testStepToInsert.ActionExpectedResult, 0);
                this.ObservableTestSteps.Insert(0, testStepToInsert);
            }
            else if (selectedIndex != -1)
            {
                this.ObservableTestSteps.Insert(selectedIndex + 1, testStepToInsert);
                log.InfoFormat("Insert test step ActionTitle= {0}, ActionExpectedResult= {1}, index= {2}", testStepToInsert.ActionTitle, testStepToInsert.ActionExpectedResult, selectedIndex + 1);
            }
            else
            {
                this.ObservableTestSteps.Add(testStepToInsert);
                selectedIndex = this.ObservableTestSteps.Count - 1;
                log.InfoFormat("Insert test step ActionTitle= {0}, ActionExpectedResult= {1}, end of test case", testStepToInsert.ActionTitle, testStepToInsert.ActionExpectedResult);
            }
            UndoRedoManager.Instance().Push((r, i) => this.RemoveTestStepFromObservableCollection(r, i), testStepToInsert, selectedIndex);
            TestStepManager.UpdateGenericSharedSteps(this.ObservableTestSteps);
            return selectedIndex;
        }

        /// <summary>
        /// Determines whether [is next test step shared] [the specified selected index].
        /// </summary>
        /// <param name="selectedIndex">Index of the selected.</param>
        /// <returns></returns>
        private bool IsNextTestStepShared(int selectedIndex)
        {
            if (selectedIndex == -2)
            {
                selectedIndex += 2;
            }
            else if (selectedIndex == -1)
            {
                selectedIndex++;
            }
            bool isNextStepShared = false;
            if (this.ObservableTestSteps.Count > selectedIndex + 1)
            {
                if (this.ObservableTestSteps[selectedIndex + 1].IsShared && !this.EditViewContext.IsSharedStep && this.ObservableTestSteps[selectedIndex].TestStepGuid.Equals(this.ObservableTestSteps[selectedIndex + 1].TestStepGuid))
                {
                    isNextStepShared = true;
                }
            }

            return isNextStepShared;
        }

        /// <summary>
        /// Finds the index of the next not shared step.
        /// </summary>
        /// <param name="selectedIndex">Index of the selected.</param>
        /// <returns>new selected index</returns>
        private int FindNextNotSharedStepIndex(int selectedIndex)
        {
            int newSelectedIndex = -1;
            for (int i = selectedIndex; i < this.ObservableTestSteps.Count; i++)
            {
                if (this.IsNextTestStepShared(i))
                {
                    continue;
                }
                else
                {
                    newSelectedIndex = i;
                    break;
                }
            }

            return newSelectedIndex;
        }

        /// <summary>
        /// Removes the test step from test steps observable collection.
        /// </summary>
        /// <param name="testStepToBeRemoved">The test step to be removed.</param>
        /// <param name="selectedIndex">Index of the selected.</param>
        public void RemoveTestStepFromObservableCollection(TestStep testStepToBeRemoved, int selectedIndex)
        {
            this.ObservableTestSteps.Remove(testStepToBeRemoved);
            log.InfoFormat("Remove test step ActionTitle = {0}, ExpectedResult= {1}", testStepToBeRemoved.ActionTitle, testStepToBeRemoved.ActionExpectedResult);
            UndoRedoManager.Instance().Push((r, i) => this.InsertTestStepInTestCase(r, i), testStepToBeRemoved, selectedIndex, "remove Test Step");
            TestStepManager.UpdateGenericSharedSteps(this.ObservableTestSteps);
        }

        /// <summary>
        /// Creates the new test step collection after move up.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="selectedCount">The count of the selected steps.</param>
        public void CreateNewTestStepCollectionAfterMoveUp(int startIndex, int selectedCount)
        {
            List<TestStep> newCollection = new List<TestStep>();
            for (int i = 0; i < startIndex - 1; i++)
            {
                newCollection.Add(this.ObservableTestSteps[i]);
            }

            for (int i = startIndex; i < startIndex + selectedCount; i++)
            {
                newCollection.Add(this.ObservableTestSteps[i]);
            }
            for (int i = startIndex - 1; i < startIndex; i++)
            {
                newCollection.Add(this.ObservableTestSteps[i]);
            }

            for (int i = startIndex + selectedCount; i < this.ObservableTestSteps.Count; i++)
            {
                newCollection.Add(this.ObservableTestSteps[i]);
            }

            this.ObservableTestSteps.Clear();
            newCollection.ForEach(x => this.ObservableTestSteps.Add(x));
            UndoRedoManager.Instance().Push((si, c) => this.CreateNewTestStepCollectionAfterMoveDown(si, c), startIndex - 1, selectedCount, "Move up selected test steps");
        }

        /// <summary>
        /// Creates the new test step collection after move down.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="selectedCount">The count of the selected test steps.</param>
        public void CreateNewTestStepCollectionAfterMoveDown(int startIndex, int selectedCount)
        {
            List<TestStep> newCollection = new List<TestStep>();
            for (int i = 0; i < startIndex; i++)
            {
                newCollection.Add(this.ObservableTestSteps[i]);
            }
            newCollection.Add(this.ObservableTestSteps[startIndex + selectedCount]);
            for (int i = startIndex; i < startIndex + selectedCount; i++)
            {
                newCollection.Add(this.ObservableTestSteps[i]);
            }

            for (int i = startIndex + selectedCount + 1; i < this.ObservableTestSteps.Count; i++)
            {
                newCollection.Add(this.ObservableTestSteps[i]);
            }

            this.ObservableTestSteps.Clear();
            newCollection.ForEach(x => this.ObservableTestSteps.Add(x));
            UndoRedoManager.Instance().Push((si, c) => this.CreateNewTestStepCollectionAfterMoveUp(si, c), startIndex + 1, selectedCount, "Move down selected test steps");
        }

        /// <summary>
        /// Updates the test steps grid.
        /// </summary>
        public void UpdateTestStepsGrid()
        {
            List<TestStep> testSteps = new List<TestStep>();
            this.ObservableTestSteps.ToList().ForEach(x => testSteps.Add(x));
            this.ObservableTestSteps.Clear();
            testSteps.ForEach(x => this.ObservableTestSteps.Add(x));
        }

        /// <summary>
        /// Refreshes the shared step collections.
        /// </summary>
        public void RefreshSharedStepCollections(System.Windows.Threading.Dispatcher uiDispatcher, CancellationToken cancellationToken)
        {
            do
            {
                if (cancellationToken != null && cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                if (this.EditViewContext.IsInitialized && !this.EditViewContext.IsSharedStep)
                {
                    List<ISharedStep> sharedStepList = TestStepManager.GetAllSharedSteps();
                    List<SharedStep> sharedSteps = new List<BusinessLogic.Entities.SharedStep>();
                    sharedStepList.ForEach(s =>
                    {
                        sharedSteps.Add(new SharedStep(s));
                    });
                    sharedSteps.Sort();
                    Action action = new Action(() =>
                    {
                        if (this.ObservableSharedSteps != null && this.InitialSharedStepCollection != null)
                        {
                            this.ObservableSharedSteps.Clear();
                            sharedSteps.ForEach(s =>
                            {
                                this.ObservableSharedSteps.Add(s);
                            });
                            this.InitialSharedStepCollection.Clear();
                            sharedSteps.ForEach(s =>
                            {
                                this.InitialSharedStepCollection.Add(s);
                            });
                            this.InitializeInitialSharedStepCollection();
                            this.SharedStepsRefreshEvent(this, EventArgs.Empty);
                        }
                    });
                    try
                    {
                        uiDispatcher.Invoke(action);
                    }
                    catch (AggregateException e)
                    {
                        log.Error(e);
                        foreach (var innerException in e.InnerExceptions)
                        {
                            log.Error(innerException);
                        }
                    }
                }
                Thread.Sleep(30000);                
            }
            while (true);
        }

        /// <summary>
        /// Inserts the new shared step.
        /// </summary>
        /// <param name="currentSharedStep">The current shared step.</param>
        /// <param name="selectedIndex">Index of the selected test step.</param>
        /// <returns>return the index of the last inserted step</returns>
        public int InsertSharedStep(SharedStep currentSharedStep, int selectedIndex)
        {
            List<TestStep> innerTestSteps = TestStepManager.GetAllTestStepsInSharedStep(currentSharedStep.ISharedStep);
            log.InfoFormat("Insert Shared Step Title= {0}, SelectedIndex= {1}", currentSharedStep.Title, selectedIndex);
            int j = 0;
            int finalInsertedStepIndex = 0;
            for (int i = selectedIndex; i < innerTestSteps.Count + selectedIndex; i++)
            {
                finalInsertedStepIndex = this.InsertTestStepInTestCase(innerTestSteps[j], i, false);
                j++;
            }

            return finalInsertedStepIndex;
        }

        /// <summary>
        /// Deletes the test step.
        /// </summary>
        /// <param name="testStepsToBeRemoved">The test steps to be removed.</param>
        public void RemoveTestSteps(List<TestStepFull> testStepsToBeRemoved)
        {
            using (new UndoTransaction("Delete all selected test steps", false))
            {
                for (int i = testStepsToBeRemoved.Count - 1; i >= 0; i--)
                {
                    this.RemoveTestStepFromObservableCollection(testStepsToBeRemoved[i], testStepsToBeRemoved[i].Index);
                }
            }
        }

        /// <summary>
        /// Initializes the page title.
        /// </summary>
        private void InitializePageTitle()
        {
            if (!this.EditViewContext.IsSharedStep && this.EditViewContext.CreateNew && !this.EditViewContext.Duplicate)
            {
                this.PageTitle = "Create New Test Case";
            }
            else if (!this.EditViewContext.IsSharedStep && this.EditViewContext.CreateNew && this.EditViewContext.Duplicate)
            {
                this.PageTitle = "Duplicate Test Case";
            }
            else if (this.EditViewContext.IsSharedStep && this.EditViewContext.CreateNew && !this.EditViewContext.Duplicate)
            {
                this.PageTitle = "Create New Shared Step";
            }
            else if (this.EditViewContext.IsSharedStep && this.EditViewContext.CreateNew && this.EditViewContext.Duplicate)
            {
                this.PageTitle = "Duplicate Shared Step";
            }
            else if (this.EditViewContext.IsSharedStep)
            {
                this.PageTitle = "Edit Shared Step";
            }
            else
            {
                this.PageTitle = "Edit Test Case";
            }
        }

        /// <summary>
        /// Clears the test step names.
        /// </summary>
        private void ClearTestStepNames()
        {
            foreach (TestStep currentTestStep in this.ObservableTestSteps)
            {
                currentTestStep.Title = string.Empty;
            }
        }

        /// <summary>
        /// Initializes the unique identifier label.
        /// </summary>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="duplicate">if set to <c>true</c> [duplicate].</param>
        private void InitializeIdLabelFromTestBase(bool createNew, bool duplicate)
        {
            if (duplicate || createNew)
            {
                this.TestCaseIdLabel = "*";
            }
            else
            {
                this.TestCaseIdLabel = this.TestBase.Id.ToString();
            }
        }

        /// <summary>
        /// Initializes the initial shared step collection.
        /// </summary>
        private void InitializeInitialSharedStepCollection()
        {
            this.InitialSharedStepCollection = new List<SharedStep>();
            foreach (var currentSharedStep in this.ObservableSharedSteps)
            {
                this.InitialSharedStepCollection.Add(currentSharedStep);
            }
        }

        /// <summary>
        /// Gets the project areas.
        /// </summary>
        /// <returns>list of areas names as string list</returns>
        private List<string> GetProjectAreas()
        {
            List<string> areas = new List<string>();
            ICommonStructureService css = (ICommonStructureService)TestCaseManagerCore.ExecutionContext.TfsTeamProjectCollection.GetService(typeof(ICommonStructureService));
            log.InfoFormat("Get All Areas for Project= {0}", TestCaseManagerCore.ExecutionContext.Preferences.TestProjectName);
            ProjectInfo projectInfo = css.GetProjectFromName(TestCaseManagerCore.ExecutionContext.Preferences.TestProjectName);
            NodeInfo[] nodes = css.ListStructures(projectInfo.Uri);
            foreach (NodeInfo currentNode in nodes)
            {
                if (currentNode.Name.Equals("Area"))
                {
                    XmlElement areaTree = css.GetNodesXml(new string[] { currentNode.Uri }, true);
                    areas.Clear();
                    XmlNode areaNodes = areaTree.ChildNodes[0];
                    this.CreateAreasList(areaNodes, areas);
                }
            }

            return areas;
        }

        /// <summary>
        /// Initializes the test case test steps from attribute test case actions.
        /// </summary>
        private void InitializeTestCaseTestStepsFromITestCaseActions()
        {
            List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(TestCaseManagerCore.ExecutionContext.TestManagementTeamProject, this.TestCase.ITestCase.Actions);
            this.AddTestStepsToObservableCollection(testSteps);
        }

        /// <summary>
        /// Adds the test steps automatic observable collection.
        /// </summary>
        /// <param name="testSteps">The test steps.</param>
        private void AddTestStepsToObservableCollection(List<TestStep> testSteps)
        {
            foreach (var currentTestStep in testSteps)
            {
                this.ObservableTestSteps.Add(currentTestStep);
            }
        }

        /// <summary>
        /// Initializes the observable shared steps.
        /// </summary>
        private void InitializeObservableSharedSteps()
        {
            List<ISharedStep> sharedStepList = TestStepManager.GetAllSharedSteps();
            List<SharedStep> sharedSteps = new List<BusinessLogic.Entities.SharedStep>();
            sharedStepList.ForEach(s =>
            {
                sharedSteps.Add(new SharedStep(s));
            });
            sharedSteps.Sort();           
            sharedSteps.ForEach(s =>
            {
                this.ObservableSharedSteps.Add(s);
            });
        }

        /// <summary>
        /// Creates the areas list.
        /// </summary>
        /// <param name="xmlNode">The XML node.</param>
        /// <param name="areas">The areas.</param>
        private void CreateAreasList(XmlNode xmlNode, List<string> areas)
        {
            this.GetAreasSingleNode(xmlNode, areas);
            this.GetAreasNodes(xmlNode.ChildNodes, areas);
        }

        /// <summary>
        /// Gets the areas nodes.
        /// </summary>
        /// <param name="nodeList">The node list.</param>
        /// <param name="areas">The areas.</param>
        private void GetAreasNodes(XmlNodeList nodeList, List<string> areas)
        {
            foreach (XmlNode currentNode in nodeList)
            {
                this.GetAreasSingleNode(currentNode, areas);
            }
        }

        /// <summary>
        /// Gets the areas single node.
        /// </summary>
        /// <param name="currentNode">The current node.</param>
        /// <param name="areas">The areas.</param>
        private void GetAreasSingleNode(XmlNode currentNode, List<string> areas)
        {
            if (currentNode.Attributes.Count > 0)
            {
                string path = currentNode.Attributes["Path"].Value.TrimStart('\\').Replace("\\Area", string.Empty);
                if (!areas.Contains(path))
                {
                    areas.Add(path);
                }
            }
            if (currentNode.ChildNodes.Count != 0)
            {
                this.GetAreasNodes(currentNode.ChildNodes, areas);
            }
        }
    }
}