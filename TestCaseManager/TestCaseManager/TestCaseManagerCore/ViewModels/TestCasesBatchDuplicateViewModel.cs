// <copyright file="TestCasesBatchDuplicateViewModel.cs" company="Automate The Planet Ltd.">
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
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;
    using AAngelov.Utilities.Managers;
    using AAngelov.Utilities.UI.Core;
    using Fidely.Framework.Compilation.Objects;
    using Microsoft.TeamFoundation.Framework.Client;
    using Microsoft.TeamFoundation.Framework.Common;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Managers;
    using Fidely.Framework;

    /// <summary>
    /// Contains methods and properties related to the TestCasesBatchDuplicate View
    /// </summary>
    public class TestCasesBatchDuplicateViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The test cases count after filtering
        /// </summary>
        private string testCasesCount;

        /// <summary>
        /// The selected test case count
        /// </summary>
        private string selectedEntitiesCount;

        /// <summary>
        /// The load test cases
        /// </summary>
        private bool loadTestCases;

        /// <summary>
        /// The compiler
        /// </summary>
        private readonly SearchQueryCompiler<TestCase> testCasesCompiler;

        /// <summary>
        /// The compiler
        /// </summary>
        private readonly SearchQueryCompiler<SharedStep> sharedStepsCompiler;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesBatchDuplicateViewModel"/> class.
        /// </summary>
        public TestCasesBatchDuplicateViewModel(bool loadTestCases, bool loadSpecificTestCases)
        {
            this.InitializeInnerCollections();
            this.loadTestCases = loadTestCases;
            this.ShowTestCaseSpecificFields = loadTestCases;
            this.InitializeTeamFoundationIdentityNames();
            this.ReplaceContext = new ReplaceContext();

            if (loadTestCases)
            {
                if (!loadSpecificTestCases)
                {
                    this.InitializeTestCases();
                }
                else
                {
                    this.InitializeTestCasesFromSpecificSelectedTestCases();
                }
                this.InitializeInitialTestCaseCollection();
                this.EntitiesCount = this.ObservableTestCases.Count.ToString();
                this.InitializeTestSuiteList();
                this.ReplaceContext.SelectedSuite = this.ObservableTestSuites[0];
            }
            else
            {
                this.ObservableSharedSteps = new ObservableCollection<SharedStep>();
                List<SharedStep> allSharedSteps = SharedStepManager.GetAllSharedStepsInTestPlan(ExecutionContext.TestManagementTeamProject);
                allSharedSteps.ForEach(s => this.ObservableSharedSteps.Add(s));
                this.InitialSharedStepsCollection = new ObservableCollection<SharedStep>();
                allSharedSteps.ForEach(s => this.InitialSharedStepsCollection.Add(s));
                this.EntitiesCount = this.ObservableSharedSteps.Count.ToString();
            }
      
            if (this.ObservableTeamFoundationIdentityNames.Count > 0)
            {
                this.ReplaceContext.SelectedTeamFoundationIdentityName = this.ObservableTeamFoundationIdentityNames[0];
            }
            this.SelectedEntitiesCount = "0";
            var setting = SearchQueryCompilerBuilder.Instance.BuildUpDefaultObjectCompilerSetting<TestCase>();
            testCasesCompiler = SearchQueryCompilerBuilder.Instance.BuildUpCompiler<TestCase>(setting);
            var sharedSteposSetting = SearchQueryCompilerBuilder.Instance.BuildUpDefaultObjectCompilerSetting<SharedStep>();
            sharedStepsCompiler = SearchQueryCompilerBuilder.Instance.BuildUpCompiler<SharedStep>(sharedSteposSetting);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesBatchDuplicateViewModel"/> class.
        /// </summary>
        /// <param name="viewModel">The old view model.</param>
        public TestCasesBatchDuplicateViewModel(TestCasesBatchDuplicateViewModel viewModel, bool loadTestCases, bool loadSpecificTestCases) : this(loadTestCases, loadSpecificTestCases)
        {
            this.InitialViewFilters = viewModel.InitialViewFilters;
            this.ReplaceContext = viewModel.ReplaceContext;
            if (this.ObservableTestSuites.Count > 0)
            {
                this.ReplaceContext.SelectedSuite = this.ObservableTestSuites[0];
            }
            if (this.ObservableTeamFoundationIdentityNames.Count > 0)
            {
                this.ReplaceContext.SelectedTeamFoundationIdentityName = this.ObservableTeamFoundationIdentityNames[0];
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show test case specific fields].
        /// </summary>
        /// <value>
        /// <c>true</c> if [show test case specific fields]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowTestCaseSpecificFields { get; set; }

        /// <summary>
        /// Gets or sets the replace context.
        /// </summary>
        /// <value>
        /// The replace context.
        /// </value>
        public ReplaceContext ReplaceContext { get; set; }

        /// <summary>
        /// Gets or sets the observable test cases.
        /// </summary>
        /// <value>
        /// The observable test cases.
        /// </value>
        public ObservableCollection<TestCase> ObservableTestCases { get; set; }

        /// <summary>
        /// Gets or sets the observable shared steps.
        /// </summary>
        /// <value>
        /// The observable shared steps.
        /// </value>
        public ObservableCollection<SharedStep> ObservableSharedSteps { get; set; }

        /// <summary>
        /// Gets or sets the initial test case collection.
        /// </summary>
        /// <value>
        /// The initial test case collection.
        /// </value>
        public ObservableCollection<TestCase> InitialTestCaseCollection { get; set; }

        /// <summary>
        /// Gets or sets the initial shared steps collection.
        /// </summary>
        /// <value>
        /// The initial shared steps collection.
        /// </value>
        public ObservableCollection<SharedStep> InitialSharedStepsCollection { get; set; }

        /// <summary>
        /// Gets or sets the observable test suites used in the suite drop down.
        /// </summary>
        /// <value>
        /// The observable test suites.
        /// </value>
        public ObservableCollection<ITestSuiteBase> ObservableTestSuites { get; set; }

        /// <summary>
        /// Gets or sets the initial view filters.
        /// </summary>
        /// <value>
        /// The initial view filters.
        /// </value>
        public InitialViewFilters InitialViewFilters { get; set; }

        /// <summary>
        /// Gets or sets the team foundation identity names.
        /// </summary>
        /// <value>
        /// The team foundation identity names.
        /// </value>
        public ObservableCollection<TeamFoundationIdentityName> ObservableTeamFoundationIdentityNames { get; set; }

        /// <summary>
        /// Gets or sets the test cases count after filtering is applied.
        /// </summary>
        /// <value>
        /// The test cases count.
        /// </value>
        public string EntitiesCount
        {
            get
            {
                return this.testCasesCount;
            }

            set
            {
                this.testCasesCount = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected test cases/sharedsteps count.
        /// </summary>
        /// <value>
        /// The selected test cases count.
        /// </value>
        public string SelectedEntitiesCount
        {
            get
            {
                return this.selectedEntitiesCount;
            }

            set
            {
                this.selectedEntitiesCount = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes the test cases.
        /// </summary>
        public void InitializeTestCases()
        {
            ExecutionContext.Preferences.TestPlan.Refresh();
            ExecutionContext.Preferences.TestPlan.RootSuite.Refresh();
            List<TestCase> testCasesList = TestCaseManager.GetAllTestCasesInTestPlan(ExecutionContext.TestManagementTeamProject, ExecutionContext.Preferences.TestPlan, false);

            //List<TestCase> testCasesList = TestCaseManager.GetAllTestCasesFromSuiteCollection(ExecutionContext.Preferences.TestPlan, ExecutionContext.Preferences.TestPlan.RootSuite.SubSuites);
            //TestCaseManager.AddTestCasesWithoutSuites(ExecutionContext.TestManagementTeamProject, ExecutionContext.Preferences.TestPlan, testCasesList);
            this.ObservableTestCases = new ObservableCollection<TestCase>();
            testCasesList.ForEach(t => this.ObservableTestCases.Add(t));
        }

        /// <summary>
        /// Initializes the test cases from specific selected test cases.
        /// </summary>
        public void InitializeTestCasesFromSpecificSelectedTestCases()
        {
            this.ObservableTestCases = new ObservableCollection<TestCase>();
            ExecutionContext.SelectedTestCasesForChange.ForEach(t => this.ObservableTestCases.Add(t));
        }

        /// <summary>
        /// Filters the entities.
        /// </summary>
        public void FilterEntities()
        {
            if (this.loadTestCases)
            {
                this.FilterTestCases();
            }
            else
            {
                this.FilterSharedSteps();
            }
        }

        /// <summary>
        /// Filters the test cases.
        /// </summary>
        public void FilterTestCases()
        {
            bool shouldSetTextFilter = this.InitialViewFilters.IsTitleTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.TitleFilter);
            string titleFilter = this.InitialViewFilters.TitleFilter.ToLower();
            bool shouldSetSuiteFilter = this.InitialViewFilters.IsSuiteTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.SuiteFilter);
            string suiteFilter = this.InitialViewFilters.SuiteFilter.ToLower();
            bool shouldSetPriorityFilter = this.InitialViewFilters.IsPriorityTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.PriorityFilter);
            string priorityFilter = this.InitialViewFilters.PriorityFilter.ToLower();
            bool shouldSetAssignedToFilter = this.InitialViewFilters.IsAssignedToTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.AssignedToFilter);
            string assignedToFilter = this.InitialViewFilters.AssignedToFilter.ToLower();

            bool shouldSetAdvancedFilter = this.InitialViewFilters.IsAdvancedSearchTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.AdvancedSearchFilter);
            IEnumerable<TestCase> searchableCollection = this.InitialTestCaseCollection;
            if (shouldSetAdvancedFilter)
            {
                searchableCollection = this.SearchTestCases();
            }

            var filteredList = searchableCollection.Where(t =>
                (t.ITestCase != null) &&
                (shouldSetTextFilter ? (t.ITestCase.Title.ToLower().Contains(titleFilter)) : true) &&
                (this.FilterTestCasesBySuite(shouldSetSuiteFilter, suiteFilter, t)) &&
                (shouldSetPriorityFilter ? t.Priority.ToString().ToLower().Contains(priorityFilter) : true) &&
                (shouldSetAssignedToFilter ? t.TeamFoundationIdentityName.DisplayName.ToLower().Contains(assignedToFilter) : true)).ToList();
            this.ObservableTestCases.Clear();
            filteredList.ForEach(x => this.ObservableTestCases.Add(x));
            this.EntitiesCount = filteredList.Count.ToString();
        }

        /// <summary>
        /// Filters the test cases.
        /// </summary>
        public void FilterSharedSteps()
        {
            bool shouldSetTextFilter = this.InitialViewFilters.IsTitleTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.TitleFilter);
            string titleFilter = this.InitialViewFilters.TitleFilter.ToLower();
            bool shouldSetPriorityFilter = this.InitialViewFilters.IsPriorityTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.PriorityFilter);
            string priorityFilter = this.InitialViewFilters.PriorityFilter.ToLower();
            bool shouldSetAssignedToFilter = this.InitialViewFilters.IsAssignedToTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.AssignedToFilter);
            string assignedToFilter = this.InitialViewFilters.AssignedToFilter.ToLower();
            
            bool shouldSetAdvancedFilter = this.InitialViewFilters.IsAdvancedSearchTextSet && !string.IsNullOrEmpty(this.InitialViewFilters.AdvancedSearchFilter);
            IEnumerable<SharedStep> searchableCollection = this.InitialSharedStepsCollection;
            if (shouldSetAdvancedFilter)
            {
                searchableCollection = this.SearchSharedSteps();
            }
            var filteredList = searchableCollection.Where(t =>
                (t.ISharedStep != null) &&
                (shouldSetTextFilter ? (t.ISharedStep.Title.ToLower().Contains(titleFilter)) : true) &&
                (shouldSetPriorityFilter ? t.Priority.ToString().ToLower().Contains(priorityFilter) : true) &&
                (shouldSetAssignedToFilter ? t.TeamFoundationIdentityName.DisplayName.ToLower().Contains(assignedToFilter) : true)).ToList();
            this.ObservableSharedSteps.Clear();
            filteredList.ForEach(x => this.ObservableSharedSteps.Add(x));
            this.EntitiesCount = filteredList.Count.ToString();
        }

        /// <summary>
        /// Searches the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        public IEnumerable<TestCase> SearchTestCases()
        {
            Expression<Func<TestCase, bool>> filter = testCasesCompiler.Compile(this.InitialViewFilters.AdvancedSearchFilter);
            IEnumerable<TestCase> result = this.InitialTestCaseCollection.AsQueryable().Where(filter);
            return result;
        }

        /// <summary>
        /// Searches the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        public IEnumerable<SharedStep> SearchSharedSteps()
        {
            Expression<Func<SharedStep, bool>> filter = sharedStepsCompiler.Compile(this.InitialViewFilters.AdvancedSearchFilter);
            IEnumerable<SharedStep> result = this.InitialSharedStepsCollection.AsQueryable().Where(filter);
            return result;
        }

        /// <summary>
        /// Finds the and replace information test case.
        /// </summary>
        /// <returns></returns>
        public int FindAndReplaceInEntities()
        {
            int replacedCount = 0;
            for (int i = 0; i < this.ReplaceContext.SelectedEntities.Count; i++)
            {
                this.FindAndReplaceInEntityInternal(this.ReplaceContext.SelectedEntities[i]);
                replacedCount++;
            }

            return replacedCount;
        }

        /// <summary>
        /// Duplicates the test case/shared step.
        /// </summary>
        /// <returns></returns>
        public int DuplicateEntity()
        {
            int duplicatedCount = 0;
            foreach (Object currentEntity in this.ReplaceContext.SelectedEntities)
            {
                this.DuplicateEntityInternal(currentEntity);
                duplicatedCount++;
            }

            return duplicatedCount;
        }

        /// <summary>
        /// Duplicates the test case/shared Step.
        /// </summary>
        /// <param name="entityToBeDuplicated">The test case/shared step to be duplicated.</param>
        private void DuplicateEntityInternal(Object entityToBeDuplicated)
        {
            SharedStep currentSharedStep = null;
            if (entityToBeDuplicated is TestCase)
            {
                TestCase testCaseToBeDuplicated = entityToBeDuplicated as TestCase;
                ITestCase testCaseCore = ExecutionContext.TestManagementTeamProject.TestCases.Create();
                TestCase currentTestCase = new TestCase(testCaseCore, testCaseToBeDuplicated.ITestSuiteBase, ExecutionContext.Preferences.TestPlan);
                currentTestCase.ITestCase.Area = testCaseToBeDuplicated.ITestCase.Area;
                currentTestCase.ITestCase.Title = testCaseToBeDuplicated.ITestCase.Title;
                //currentTestCase.ITestCase = ExecutionContext.TestManagementTeamProject.TestCases.Find(currentTestCase.ITestCase.Id);
                log.InfoFormat("Duplicate test case with Title= \"{0}\" id= \"{1}\"", currentTestCase.Title, currentTestCase.Id);
                List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(ExecutionContext.TestManagementTeamProject, testCaseToBeDuplicated.ITestCase.Actions.ToList());
                this.ReplaceTestCaseTitle(currentTestCase);
                this.ChangeTestCasePriority(currentTestCase);
                this.ChangeTestCaseOwner(currentTestCase);
                this.ReplaceStepsInTestCase(currentTestCase, testSteps);

                currentTestCase.ITestCase.Flush();
                currentTestCase.ITestCase.Save();
                this.AddTestCaseToSuite(currentTestCase);
            }
            else
            {
                SharedStep sharedStepToBeDuplicated = entityToBeDuplicated as SharedStep;
                ISharedStep sharedStepCore = ExecutionContext.TestManagementTeamProject.SharedSteps.Create();
                currentSharedStep = new SharedStep(sharedStepCore);
                currentSharedStep.ISharedStep.Area = sharedStepToBeDuplicated.ISharedStep.Area;
                log.InfoFormat("Duplicate shared step with Title= \"{0}\" id= \"{1}\"", currentSharedStep.Title, currentSharedStep.Id);
                List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(ExecutionContext.TestManagementTeamProject, currentSharedStep.ISharedStep.Actions.ToList());
                this.ReplaceSharedStepTitle(currentSharedStep);
                this.ChangeSharedStepPriority(currentSharedStep);
                this.ChangeSharedStepOwner(currentSharedStep);
                this.ReplaceStepsInSharedStep(currentSharedStep, testSteps);

                currentSharedStep.ISharedStep.Flush();
                currentSharedStep.ISharedStep.Save();
            }
        }

        /// <summary>
        /// Adds the test case automatic suite.
        /// </summary>
        /// <param name="currentTestCase">The current test case.</param>
        private void AddTestCaseToSuite(TestCase currentTestCase)
        {
            var newSuite = TestSuiteManager.GetTestSuiteByName(ExecutionContext.TestManagementTeamProject, this.ReplaceContext.SelectedSuite.Title);
            newSuite.AddTestCase(currentTestCase.ITestCase);
        }

        /// <summary>
        /// Finds the and replace information test case/shared step.
        /// </summary>
        /// <param name="entityToReplaceIn">The test case/shared step to replace in.</param>
        private void FindAndReplaceInEntityInternal(Object entityToReplaceIn)
        {
            TestCase currentTestCase = null;
            SharedStep currentSharedStep = null;
            if (entityToReplaceIn is TestCase)
            {
                currentTestCase = entityToReplaceIn as TestCase;
                currentTestCase.ITestCase = ExecutionContext.TestManagementTeamProject.TestCases.Find(currentTestCase.ITestCase.Id);
                log.InfoFormat("Find and Replace in test case with Title= \"{0}\" id= \"{1}\"", currentTestCase.Title, currentTestCase.Id);
                List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(ExecutionContext.TestManagementTeamProject, currentTestCase.ITestCase.Actions.ToList());
                this.ReplaceTestCaseTitle(currentTestCase);
                this.ChangeTestCasePriority(currentTestCase);
                this.ChangeTestCaseOwner(currentTestCase);
                this.ReplaceStepsInTestCase(currentTestCase, testSteps);

                currentTestCase.ITestCase.Actions.Add(currentTestCase.ITestCase.CreateTestStep());
                currentTestCase.ITestCase.Flush();
                currentTestCase.ITestCase.Save();

                currentTestCase.ITestCase.Actions.RemoveAt(currentTestCase.ITestCase.Actions.Count - 1);
                currentTestCase.ITestCase.Save();
            }
            else
            {
                currentSharedStep = entityToReplaceIn as SharedStep;
                currentSharedStep.ISharedStep = ExecutionContext.TestManagementTeamProject.SharedSteps.Find(currentSharedStep.ISharedStep.Id);
                log.InfoFormat("Find and Replace in shared step with Title= \"{0}\" id= \"{1}\"", currentSharedStep.Title, currentSharedStep.Id);
                List<TestStep> testSteps = TestStepManager.GetTestStepsFromTestActions(ExecutionContext.TestManagementTeamProject, currentSharedStep.ISharedStep.Actions.ToList());
                this.ReplaceSharedStepTitle(currentSharedStep);
                this.ChangeSharedStepPriority(currentSharedStep);
                this.ChangeSharedStepOwner(currentSharedStep);
                this.ReplaceStepsInSharedStep(currentSharedStep, testSteps);

                currentSharedStep.ISharedStep.Actions.Add(currentSharedStep.ISharedStep.CreateTestStep());
                currentSharedStep.ISharedStep.Flush();
                currentSharedStep.ISharedStep.Save();
                currentSharedStep.ISharedStep.Actions.RemoveAt(currentSharedStep.ISharedStep.Actions.Count - 1);
                currentSharedStep.ISharedStep.Save();
            }
        }

        /// <summary>
        /// Replaces the test case title.
        /// </summary>
        /// <param name="currentTestCase">The current test case.</param>
        private void ReplaceTestCaseTitle(TestCase currentTestCase)
        {
            if (this.ReplaceContext.ReplaceInTitles)
            { 
                string newTitle = currentTestCase.ITestCase.Title.ReplaceAll(this.ReplaceContext.ObservableTextReplacePairs);
                if (newTitle.Length > 255)
                {
                    return;
                }
                log.InfoFormat("Change Title from \"{0}\" to \"{1}\"", currentTestCase.ITestCase.Title, newTitle);
                currentTestCase.ITestCase.Title = newTitle;
                currentTestCase.Title = newTitle;
            }
        }

        /// <summary>
        /// Replaces the shared step title.
        /// </summary>
        /// <param name="currentSharedStep">The current shared step.</param>
        private void ReplaceSharedStepTitle(SharedStep currentSharedStep)
        {
            if (this.ReplaceContext.ReplaceInTitles)
            { 
                string newTitle = currentSharedStep.ISharedStep.Title.ReplaceAll(this.ReplaceContext.ObservableTextReplacePairs);
                if (newTitle.Length > 255)
                {
                    return;
                }
                log.InfoFormat("Change Title from \"{0}\" to \"{1}\"", currentSharedStep.ISharedStep.Title, newTitle);
                currentSharedStep.ISharedStep.Title = newTitle;
                currentSharedStep.Title = newTitle;
            }
        }

        /// <summary>
        /// Changes the test case owner.
        /// </summary>
        /// <param name="currentTestCase">The current test case.</param>
        private void ChangeTestCaseOwner(TestCase currentTestCase)
        {
            if (this.ReplaceContext.ChangeOwner && this.ReplaceContext.SelectedTeamFoundationIdentityName != null)
            {
                var identity = ExecutionContext.TestManagementTeamProject.TfsIdentityStore.FindByTeamFoundationId(this.ReplaceContext.SelectedTeamFoundationIdentityName.TeamFoundationId);
                log.InfoFormat("Change Owner from \"{0}\" to \"{1}\"", currentTestCase.ITestCase.Owner.DisplayName, identity.DisplayName);
                currentTestCase.ITestCase.Owner = identity;
                currentTestCase.TeamFoundationIdentityName = new TeamFoundationIdentityName(identity.TeamFoundationId, identity.DisplayName);
            }
        }

        /// <summary>
        /// Changes the shared step owner.
        /// </summary>
        /// <param name="currentSharedStep">The current shared step.</param>
        private void ChangeSharedStepOwner(SharedStep currentSharedStep)
        {
            if (this.ReplaceContext.ChangeOwner && this.ReplaceContext.SelectedTeamFoundationIdentityName != null)
            {
                var identity = ExecutionContext.TestManagementTeamProject.TfsIdentityStore.FindByTeamFoundationId(this.ReplaceContext.SelectedTeamFoundationIdentityName.TeamFoundationId);
                log.InfoFormat("Change Owner from \"{0}\" to \"{1}\"", currentSharedStep.ISharedStep.Owner.DisplayName, identity.DisplayName);
                currentSharedStep.ISharedStep.Owner = identity;
                currentSharedStep.TeamFoundationIdentityName = new TeamFoundationIdentityName(identity.TeamFoundationId, identity.DisplayName);
            }
        }

        /// <summary>
        /// Changes the test case priority.
        /// </summary>
        /// <param name="currentTestCase">The current test case.</param>
        private void ChangeTestCasePriority(TestCase currentTestCase)
        {
            if (this.ReplaceContext.ChangePriorities)
            {
                log.InfoFormat("Change Priority from \"{0}\" to \"{1}\"", currentTestCase.ITestCase.Priority, (int)this.ReplaceContext.SelectedPriority);
                currentTestCase.ITestCase.Priority = (int)this.ReplaceContext.SelectedPriority;
                currentTestCase.Priority = this.ReplaceContext.SelectedPriority;
            }
        }

        private void ChangeSharedStepPriority(SharedStep currentSharedStep)
        {
            if (this.ReplaceContext.ChangePriorities)
            {
                log.InfoFormat("Change Priority from \"{0}\" to \"{1}\"", currentSharedStep.ISharedStep.Priority, (int)this.ReplaceContext.SelectedPriority);
                currentSharedStep.ISharedStep.Priority = (int)this.ReplaceContext.SelectedPriority;
                currentSharedStep.Priority = this.ReplaceContext.SelectedPriority;
            }
        }

        /// <summary>
        /// Initializes the test suite list.
        /// </summary>
        private void InitializeTestSuiteList()
        {
            List<ITestSuiteBase> testSuiteList = TestSuiteManager.GetAllTestSuitesInTestPlan(ExecutionContext.Preferences.TestPlan);
            testSuiteList.ForEach(s => this.ObservableTestSuites.Add(s));
        }

        /// <summary>
        /// Initializes the initial test case collection.
        /// </summary>
        private void InitializeInitialTestCaseCollection()
        {
            this.InitialTestCaseCollection = new ObservableCollection<TestCase>();
            foreach (var currentTestCase in this.ObservableTestCases)
            {
                this.InitialTestCaseCollection.Add(currentTestCase);
            }
        }

        /// <summary>
        /// Initializes the inner collections.
        /// </summary>
        private void InitializeInnerCollections()
        {
            this.ObservableTestSuites = new ObservableCollection<ITestSuiteBase>();         
            this.InitialViewFilters = new InitialViewFilters();
            this.ObservableTeamFoundationIdentityNames = new ObservableCollection<TeamFoundationIdentityName>();
        }

        /// <summary>
        /// Initializes the team foundation identity names.
        /// </summary>
        private void InitializeTeamFoundationIdentityNames()
        {
            this.ObservableTeamFoundationIdentityNames = new ObservableCollection<TeamFoundationIdentityName>();
            ExecutionContext.TestManagementTeamProject.TfsIdentityStore.Refresh();
            ITestManagementService testManagementService = (ITestManagementService)ExecutionContext.TfsTeamProjectCollection.GetService(typeof(ITestManagementService));
            foreach (string currentProjectName in this.GetTeamProjectNamesUsingVcs())
            {
                var project = testManagementService.GetTeamProject(currentProjectName);
                foreach (var member in project.TfsIdentityStore.AllUserIdentities)
                {
                    if (member.TeamFoundationId != default(Guid) && !String.IsNullOrEmpty(member.DisplayName) && member.DisplayName.Contains(" ") && this.ObservableTeamFoundationIdentityNames.Where(x => x.TeamFoundationId == member.TeamFoundationId).ToArray().Count() == 0)
                    {
                        this.ObservableTeamFoundationIdentityNames.Add(new TeamFoundationIdentityName(member.TeamFoundationId, member.DisplayName));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the team project names using VCS.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetTeamProjectNamesUsingVcs()
        {
            ReadOnlyCollection<CatalogNode> projectNodes = ExecutionContext.TfsTeamProjectCollection.CatalogNode.QueryChildren(new[] { CatalogResourceTypes.TeamProject }, false, CatalogQueryOptions.None);

            foreach (var tp in projectNodes)
            {
                yield return tp.Resource.DisplayName;
            }
        }

        /// <summary>
        /// Replaces the test steps information in specific test case.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="testSteps">The test steps.</param>
        private void ReplaceStepsInTestCase(TestCase testCase, List<TestStep> testSteps)
        {
            if (this.ReplaceContext.ReplaceSharedSteps || this.ReplaceContext.ReplaceInTestSteps)
            {
                testCase.ITestCase.Actions.Clear();
                List<Guid> addedSharedStepGuids = new List<Guid>();

                foreach (TestStep currentStep in testSteps)
                {
                    if (currentStep.IsShared && !addedSharedStepGuids.Contains(currentStep.TestStepGuid) && this.ReplaceContext.ReplaceSharedSteps)
                    { 
                        if (this.ReplaceContext.ReplaceSharedSteps)
                        {
                            List<int> newSharedStepIds = this.GetNewSharedStepIds(currentStep.SharedStepId, this.ReplaceContext.ObservableSharedStepIdReplacePairs);
                            foreach (int currentId in newSharedStepIds)
                            {
                                if (currentId != 0)
                                {
                                    log.InfoFormat("Replace shared step with title= \"{0}\", id= \"{1}\" to \"{2}\"", currentStep.Title, currentStep.SharedStepId, currentId);
                                    this.AddNewSharedStepInternal(testCase, addedSharedStepGuids, currentStep, currentId);
                                }
                                else
                                {
                                    log.InfoFormat("Remove shared step with title= \"{0}\", id= \"{1}\"", currentStep.Title, currentStep.SharedStepId);
                                }
                            }
                        }
                        else
                        { 
                            this.AddNewSharedStepInternal(testCase, addedSharedStepGuids, currentStep, currentStep.SharedStepId);
                        }
                    }
                    else if (!currentStep.IsShared)
                    {
                        ITestStep testStepCore = testCase.ITestCase.CreateTestStep();
                        if (this.ReplaceContext.ReplaceInTestSteps)
                        {
                            string newActionTitle = currentStep.ActionTitle.ToString().ReplaceAll(this.ReplaceContext.ObservableTextReplacePairs);
                            log.InfoFormat("Change Test step action title from \"{0}\" to \"{1}\"", currentStep.ActionTitle, newActionTitle);
                            testStepCore.Title = newActionTitle;
                            string newActionexpectedResult = currentStep.ActionExpectedResult.ToString().ReplaceAll(this.ReplaceContext.ObservableTextReplacePairs);
                            log.InfoFormat("Change Test step action expected result from \"{0}\" to \"{1}\"", currentStep.ActionExpectedResult, newActionexpectedResult);
                            testStepCore.ExpectedResult = newActionexpectedResult;
                        }
                        else
                        {
                            testStepCore.Title = currentStep.ActionTitle;
                            testStepCore.ExpectedResult = currentStep.ActionExpectedResult;
                        }
                        testCase.ITestCase.Actions.Add(testStepCore);
                    }
                }
            }
        }

        /// <summary>
        /// Replaces the steps information shared step.
        /// </summary>
        /// <param name="sharedStep">The shared step.</param>
        /// <param name="testSteps">The test steps.</param>
        private void ReplaceStepsInSharedStep(SharedStep sharedStep, List<TestStep> testSteps)
        {
            if (this.ReplaceContext.ReplaceInTestSteps)
            {
                sharedStep.ISharedStep.Actions.Clear();
                List<Guid> addedSharedStepGuids = new List<Guid>();

                foreach (TestStep currentStep in testSteps)
                {
                    ITestStep testStepCore = sharedStep.ISharedStep.CreateTestStep();
                    if (this.ReplaceContext.ReplaceInTestSteps)
                    {
                        string newActionTitle = currentStep.ActionTitle.ToString().ReplaceAll(this.ReplaceContext.ObservableTextReplacePairs);
                        log.InfoFormat("Change Test step action title from \"{0}\" to \"{1}\"", currentStep.ActionTitle, newActionTitle);
                        testStepCore.Title = newActionTitle;
                        string newActionexpectedResult = currentStep.ActionExpectedResult.ToString().ReplaceAll(this.ReplaceContext.ObservableTextReplacePairs);
                        log.InfoFormat("Change Test step action expected result from \"{0}\" to \"{1}\"", currentStep.ActionExpectedResult, newActionexpectedResult);
                        testStepCore.ExpectedResult = newActionexpectedResult;
                    }
                    else
                    {
                        testStepCore.Title = currentStep.ActionTitle;
                        testStepCore.ExpectedResult = currentStep.ActionExpectedResult;
                    }
                    sharedStep.ISharedStep.Actions.Add(testStepCore);
                }
            }
        }

        /// <summary>
        /// Adds the new shared step internal.
        /// </summary>
        /// <param name="testCase">The test case.</param>
        /// <param name="addedSharedStepGuids">The added shared step guids.</param>
        /// <param name="currentStep">The current step.</param>
        private void AddNewSharedStepInternal(TestCase testCase, List<Guid> addedSharedStepGuids, TestStep currentStep, int sharedStepId)
        {
            ISharedStep sharedStep = ExecutionContext.TestManagementTeamProject.SharedSteps.Find(sharedStepId);
            ISharedStepReference sharedStepReferenceCore = testCase.ITestCase.CreateSharedStepReference();
            sharedStepReferenceCore.SharedStepId = sharedStep.Id;
            testCase.ITestCase.Actions.Add(sharedStepReferenceCore);
            addedSharedStepGuids.Add(currentStep.TestStepGuid);
        }

        /// <summary>
        /// Ares all shared step ids valid.
        /// </summary>
        /// <returns></returns>
        public bool AreAllSharedStepIdsValid()
        {
            bool result = true;
            string regexPattern = @"^[0-9,]$";
            foreach (SharedStepIdReplacePair currentSharedStepIdReplacePair in this.ReplaceContext.ObservableSharedStepIdReplacePairs)
            {
                try
                {
                    if (currentSharedStepIdReplacePair.OldSharedStepId == 0 || Regex.IsMatch(currentSharedStepIdReplacePair.NewSharedStepIds, regexPattern))
                    {
                        continue;
                    }
                    ExecutionContext.TestManagementTeamProject.SharedSteps.Find(currentSharedStepIdReplacePair.OldSharedStepId);
                    List<int> newSharedStepIds = this.GetNewSharedStepIdsFromString(currentSharedStepIdReplacePair.NewSharedStepIds);
                    foreach (int currentId in newSharedStepIds)
                    {
                        if (currentId > 0)
                        {
                            ExecutionContext.TestManagementTeamProject.SharedSteps.Find(currentId);
                        }
                    }
                }
                catch
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the new shared step unique identifier.
        /// </summary>
        /// <param name="currentSharedStepId">The current shared step unique identifier.</param>
        /// <param name="sharedStepIdReplacePairs">The shared steps replace pairs.</param>
        /// <returns>new shared step id</returns>
        private List<int> GetNewSharedStepIds(int currentSharedStepId, ICollection<SharedStepIdReplacePair> sharedStepIdReplacePairs)
        {
            string newSharedStepIds = String.Empty;
            List<int> sharedStepIds = new List<int>();
            bool isAdded = false;
            foreach (SharedStepIdReplacePair currentPair in sharedStepIdReplacePairs)
            {
                if (currentSharedStepId.Equals(currentPair.OldSharedStepId))
                {
                    newSharedStepIds = currentPair.NewSharedStepIds;
                    sharedStepIds = this.GetNewSharedStepIdsFromString(newSharedStepIds);
                    isAdded = true;
                    break;
                }
            }
            if (!isAdded)
            {
                sharedStepIds.Add(currentSharedStepId);
            }

            return sharedStepIds;
        }

        private List<int> GetNewSharedStepIdsFromString(string newSharedStepIds)
        {
            string[] sharedStepIdsStrs = newSharedStepIds.Split(',');
            List<int> sharedStepIds = new List<int>();
            foreach (var current in sharedStepIdsStrs)
            {
                sharedStepIds.Add(int.Parse(current));
            }

            return sharedStepIds;
        }

        /// <summary>
        /// Filters the test cases by suite.
        /// </summary>
        /// <param name="shouldSetSuiteFilter">if set to <c>true</c> [should set suite filter].</param>
        /// <param name="suiteFilter">The suite filter.</param>
        /// <param name="testCase">The test case.</param>
        /// <returns>should the test case be in</returns>
        private bool FilterTestCasesBySuite(bool shouldSetSuiteFilter, string suiteFilter, TestCase testCase)
        {
            if (!shouldSetSuiteFilter)
            {
                return true;
            }
            else if (testCase.ITestSuiteBase != null)
            {
                return testCase.ITestSuiteBase.Title.ToLower().Contains(suiteFilter);
            }
            else
            {
                return false;
            }
        }
    }
}