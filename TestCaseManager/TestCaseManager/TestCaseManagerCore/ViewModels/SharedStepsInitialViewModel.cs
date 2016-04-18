// <copyright file="SharedStepsInitialViewModel.cs" company="Automate The Planet Ltd.">
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
    using AAngelov.Utilities.UI.Core;
    using Fidely.Framework.Compilation.Objects;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Managers;
    using Fidely.Framework;

    /// <summary>
    /// Contains methods and properties related to the TestCasesInitial View
    /// </summary>
    public class SharedStepsInitialViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The test cases count after filtering
        /// </summary>
        private string sharedStepsCount;

        /// <summary>
        /// The test cases count after filtering
        /// </summary>
        private string testCasesCount;

        /// <summary>
        /// The selected shared step
        /// </summary>
        private SharedStep selectedSharedStep;

        /// <summary>
        /// The compiler
        /// </summary>
        private readonly SearchQueryCompiler<SharedStep> compiler;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesInitialViewModel"/> class.
        /// </summary>
        public SharedStepsInitialViewModel()
        {
            this.InitialViewFiltersSharedSteps = new InitialViewFilters();
            this.InitialViewFiltersTestCases = new InitialViewFilters();
            this.ObservableSharedSteps = new ObservableCollection<SharedStep>();
            this.InitialSharedStepsCollection = new ObservableCollection<SharedStep>();
            this.ObservableTestCases = new ObservableCollection<TestCase>();
            this.RefreshSharedSteps();
            this.InitializeInitialTestCaseCollection(this.ObservableSharedSteps);
            this.SharedStepsCount = this.ObservableSharedSteps.Count.ToString();
            this.TestCasesCount = this.ObservableTestCases.Count.ToString();
            this.IsAfterInitialize = true;
            var setting = SearchQueryCompilerBuilder.Instance.BuildUpDefaultObjectCompilerSetting<SharedStep>();
            compiler = SearchQueryCompilerBuilder.Instance.BuildUpCompiler<SharedStep>(setting);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesInitialViewModel"/> class.
        /// </summary>
        /// <param name="viewModel">The old view model.</param>
        public SharedStepsInitialViewModel(SharedStepsInitialViewModel viewModel) : this()
        {
            this.InitialViewFiltersSharedSteps = viewModel.InitialViewFiltersSharedSteps;
            this.InitialViewFiltersTestCases = viewModel.InitialViewFiltersTestCases;
            this.ObservableTestCases = viewModel.ObservableTestCases;
            this.SelectedSharedStep = viewModel.SelectedSharedStep;
            this.TestCasesCount = this.ObservableTestCases.Count.ToString();
        }

        /// <summary>
        /// Gets or sets a value indicating whether [is after initialize]. It will be true only right after the contructor is called. Is used to determine if the Initial filter Reset should be called.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is after initialize]; otherwise, <c>false</c>.
        /// </value>
        public bool IsAfterInitialize { get; set; }

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
        public ObservableCollection<SharedStep> InitialSharedStepsCollection { get; set; }

        /// <summary>
        /// Gets or sets the initial view filters.
        /// </summary>
        /// <value>
        /// The initial view filters.
        /// </value>
        public InitialViewFilters InitialViewFiltersSharedSteps { get; set; }

        /// <summary>
        /// Gets or sets the initial view filters test cases.
        /// </summary>
        /// <value>
        /// The initial view filters test cases.
        /// </value>
        public InitialViewFilters InitialViewFiltersTestCases { get; set; }

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
        /// Gets or sets the test cases count after filtering.
        /// </summary>
        /// <value>
        /// The test cases count.
        /// </value>
        public string TestCasesCount
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
        /// Gets or sets the shared steps count.
        /// </summary>
        /// <value>
        /// The shared steps count.
        /// </value>
        public string SharedStepsCount
        {
            get
            {
                return this.sharedStepsCount;
            }

            set
            {
                this.sharedStepsCount = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected test case.
        /// </summary>
        /// <value>
        /// The selected test case.
        /// </value>
        public SharedStep SelectedSharedStep
        {
            get
            {
                return this.selectedSharedStep;
            }

            set
            {
                this.selectedSharedStep = value;
                if (this.selectedSharedStep != null)
                {
                    log.InfoFormat("Change SelectedSharedStep: {0}", this.selectedSharedStep.Title);
                }
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Resets the initial filters. If its comming from another page the filter is saved but on next suite selected it will be reset.
        /// </summary>
        public void ResetInitialFilters()
        {
            if (this.IsAfterInitialize)
            {
                this.IsAfterInitialize = false;
            }
            else
            {
                this.InitialViewFiltersSharedSteps.Reset();
            }
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SharedStep> Search()
        {
            Expression<Func<SharedStep, bool>> filter = compiler.Compile(this.InitialViewFiltersSharedSteps.AdvancedSearchFilter);
            IEnumerable<SharedStep> result = this.InitialSharedStepsCollection.AsQueryable().Where(filter);
            return result;
        }

        /// <summary>
        /// Filters the test cases.
        /// </summary>
        public void FilterSharedSteps()
        {
            bool shouldSetIdFilter = this.InitialViewFiltersSharedSteps.IsIdTextSet && !string.IsNullOrEmpty(this.InitialViewFiltersSharedSteps.IdFilter);
            string idFilter = this.InitialViewFiltersSharedSteps.IdFilter;
            bool shouldSetTextFilter = this.InitialViewFiltersSharedSteps.IsTitleTextSet && !string.IsNullOrEmpty(this.InitialViewFiltersSharedSteps.TitleFilter);
            string titleFilter = this.InitialViewFiltersSharedSteps.TitleFilter.ToLower();
            bool shouldSetPriorityFilter = this.InitialViewFiltersSharedSteps.IsPriorityTextSet && !string.IsNullOrEmpty(this.InitialViewFiltersSharedSteps.PriorityFilter);
            string priorityFilter = this.InitialViewFiltersSharedSteps.PriorityFilter.ToLower();
            bool shouldSetAssignedToFilter = this.InitialViewFiltersSharedSteps.IsAssignedToTextSet && !string.IsNullOrEmpty(this.InitialViewFiltersSharedSteps.AssignedToFilter);
            string assignedToFilter = this.InitialViewFiltersSharedSteps.AssignedToFilter.ToLower();

            bool shouldSetAdvancedFilter = this.InitialViewFiltersSharedSteps.IsAdvancedSearchTextSet && !string.IsNullOrEmpty(this.InitialViewFiltersSharedSteps.AdvancedSearchFilter);
            IEnumerable<SharedStep> searchableCollection = this.InitialSharedStepsCollection;
            if (shouldSetAdvancedFilter)
            {
                searchableCollection = this.Search();
            }

            var filteredList = searchableCollection.Where(t =>
                (shouldSetIdFilter ? (t.ISharedStep.Id.ToString().Contains(idFilter)) : true) &&
                (shouldSetTextFilter ? (t.Title.ToLower().Contains(titleFilter)) : true) &&
                (shouldSetPriorityFilter ? t.Priority.ToString().ToLower().Contains(priorityFilter) : true) &&
                (t.TeamFoundationIdentityName != null && shouldSetAssignedToFilter ? t.TeamFoundationIdentityName.DisplayName.ToLower().Contains(assignedToFilter) : true)).ToList();
            this.ObservableSharedSteps.Clear();
            filteredList.ForEach(x => this.ObservableSharedSteps.Add(x));

            this.SharedStepsCount = filteredList.Count.ToString();
        }

        /// <summary>
        /// Filters the test cases.
        /// </summary>
        /// <returns>filtered test cases</returns>
        public List<TestCase> FilterTestCases()
        {
            bool shouldSetTextFilter = this.InitialViewFiltersTestCases.IsTitleTextSet && !string.IsNullOrEmpty(this.InitialViewFiltersTestCases.TitleFilter);
            string titleFilter = this.InitialViewFiltersTestCases.TitleFilter.ToLower();
            bool shouldSetSuiteFilter = this.InitialViewFiltersTestCases.IsSuiteTextSet && !string.IsNullOrEmpty(this.InitialViewFiltersTestCases.SuiteFilter);
            string suiteFilter = this.InitialViewFiltersTestCases.SuiteFilter.ToLower();
            if (this.InitialTestCaseCollection == null)
            {
                return null;
            }
            var filteredList = this.InitialTestCaseCollection.Where(t =>
                                                                        (t.ITestCase != null) &&
                                                                        (shouldSetTextFilter ? (t.ITestCase.Title.ToLower().Contains(titleFilter)) : true) &&
                                                                        (this.FilterTestCasesBySuite(shouldSetSuiteFilter, suiteFilter, t))).ToList();
        
            this.TestCasesCount = filteredList.Count.ToString();

            return filteredList;
        }

        /// <summary>
        /// Refreshes the shared steps
        /// </summary>
        public void RefreshSharedSteps()
        {
            this.ObservableSharedSteps.Clear();
            ExecutionContext.Preferences.TestPlan.Refresh();
            ExecutionContext.Preferences.TestPlan.RootSuite.Refresh();
            List<SharedStep> sharedStepsList = SharedStepManager.GetAllSharedStepsInTestPlan(ExecutionContext.TestManagementTeamProject);
            sharedStepsList.Sort();
            sharedStepsList.ForEach(t => this.ObservableSharedSteps.Add(t));
        }

        /// <summary>
        /// Initializes the initial shared steps collection.
        /// </summary>
        /// <param name="sharedSteps">The shared steps.</param>
        public void InitializeInitialTestCaseCollection(ICollection<SharedStep> sharedSteps)
        {
            this.InitialSharedStepsCollection.Clear();
            foreach (var currentTestCase in sharedSteps)
            {
                this.InitialSharedStepsCollection.Add(currentTestCase);
            }
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