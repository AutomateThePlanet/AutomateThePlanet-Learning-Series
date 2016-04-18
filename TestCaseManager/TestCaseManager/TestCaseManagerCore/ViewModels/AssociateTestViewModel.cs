// <copyright file="AssociateTestViewModel.cs" company="Automate The Planet Ltd.">
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
    using AAngelov.Utilities.Entities;
    using AAngelov.Utilities.Managers;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Managers;

    /// <summary>
    /// Contains properties and logic related to the association of tests to test cases
    /// </summary>
    public class AssociateTestViewModel
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="AssociateTestViewModel"/> class.
        /// </summary>
        /// <param name="testCaseId">The test case unique identifier.</param>
        public AssociateTestViewModel(int testCaseId)
        {
            ITestCase testCaseCore = ExecutionContext.TestManagementTeamProject.TestCases.Find(testCaseId);
            this.TestCase = new TestCase(testCaseCore, null, ExecutionContext.Preferences.TestPlan);
            this.TestCaseId = testCaseId;
            string projectDllPath = RegistryManager.Instance.GetProjectDllPath();
            List<Test> testsList = ProjectManager.GetProjectTestMethods(projectDllPath);
            this.ObservableTests = new ObservableCollection<Test>();
            this.AssociateTestViewFilters = new AssociateTestViewFilters();
            testsList.ForEach(t => this.ObservableTests.Add(t));
            this.InitializeInitialTestsCollection();
            this.TestTypes = new List<string>()
            {
                "UI Test", "Small Integration Test", "Unit Test", "Large Integration Test"
            };
            this.TestTypes.Sort();
        }

        /// <summary>
        /// Gets or sets the association domain.
        /// </summary>
        /// <value>
        /// The association domain.
        /// </value>
        public AppDomain AssociationDomain { get; set; }

        /// <summary>
        /// Gets or sets the test case unique identifier.
        /// </summary>
        /// <value>
        /// The test case unique identifier.
        /// </value>
        public int TestCaseId { get; set; }

        /// <summary>
        /// Gets or sets the test suite unique identifier.
        /// </summary>
        /// <value>
        /// The test suite unique identifier.
        /// </value>
        public int TestSuiteId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [create new].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [create new]; otherwise, <c>false</c>.
        /// </value>
        public bool CreateNew { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [duplicate].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [duplicate]; otherwise, <c>false</c>.
        /// </value>
        public bool Duplicate { get; set; }

        /// <summary>
        /// Gets or sets the observable tests. This collection is bind to the UI Grids.
        /// </summary>
        /// <value>
        /// The observable tests.
        /// </value>
        public ObservableCollection<Test> ObservableTests { get; set; }

        /// <summary>
        /// Gets or sets the initial tests collection. This collection is used to restore the default search collection after deleting search criterias.
        /// </summary>
        /// <value>
        /// The initial tests collection.
        /// </value>
        public ObservableCollection<Test> InitialTestsCollection { get; set; }

        /// <summary>
        /// Gets or sets the test types. Unit Test, Integration Test in the large/small.
        /// </summary>
        /// <value>
        /// The test types.
        /// </value>
        public List<string> TestTypes { get; set; }

        /// <summary>
        /// Gets or sets the test case.
        /// </summary>
        /// <value>
        /// The test case.
        /// </value>
        public TestCase TestCase { get; set; }

        /// <summary>
        /// Gets or sets the associated automation.
        /// </summary>
        /// <value>
        /// The associated automation.
        /// </value>
        public AssociatedAutomation AssociatedAutomation { get; set; }

        /// <summary>
        /// Gets or sets the associate test view filters.
        /// </summary>
        /// <value>
        /// The associate test view filters.
        /// </value>
        public AssociateTestViewFilters AssociateTestViewFilters { get; set; }

        /// <summary>
        /// Associates the test case automatic test.
        /// </summary>
        /// <param name="test">The test.</param>
        /// <param name="testType">Type of the test.</param>
        public void AssociateTestCaseToTest(Test test, string testType)
        {
            this.TestCase.ITestCase.SetAssociatedAutomation(ExecutionContext.TestManagementTeamProject, test, testType);
            this.TestCase.ITestCase.Save();
        }

        /// <summary>
        /// Removes the association.
        /// </summary>
        public void RemoveAssociation()
        {
            this.TestCase.ITestCase.Implementation = null;
            this.TestCase.ITestCase.Flush();
            this.TestCase.ITestCase.Refresh();
            this.TestCase.ITestCase.Save();
        }

        /// <summary>
        /// Unloads the association domain.
        /// </summary>
        public void UnloadAssociationDomain()
        {
            AppDomain.Unload(this.AssociationDomain);
        }

        /// <summary>
        /// Reinitializes the tests.
        /// </summary>
        public void ReinitializeTests()
        {
            this.ObservableTests.Clear();
            foreach (var currentTest in this.InitialTestsCollection)
            {
                this.ObservableTests.Add(currentTest);
            }
        }

        /// <summary>
        /// Filters the tests.
        /// </summary>
        /// <param name="fullNameFilter">The full name filter.</param>
        /// <param name="classNameFilter">The class name filter.</param>
        public void FilterTests(string fullNameFilter, string classNameFilter)
        {
            this.ReinitializeTests();

            var filteredList = this.ObservableTests.Where(
                                                        t => ((this.AssociateTestViewFilters.IsFullNameFilterSet && !string.IsNullOrEmpty(fullNameFilter)) ? t.FullName.ToLower().Contains(fullNameFilter.ToLower()) : true) &&
                                                             ((this.AssociateTestViewFilters.IsClassNameFilterSet && !string.IsNullOrEmpty(classNameFilter)) ? t.ClassName.ToLower().Contains(classNameFilter.ToLower()) : true)).ToList();
            this.ObservableTests.Clear();
            filteredList.ForEach(x => this.ObservableTests.Add(x));
        }

        /// <summary>
        /// Initializes the initial tests collection.
        /// </summary>
        private void InitializeInitialTestsCollection()
        {
            this.InitialTestsCollection = new ObservableCollection<Test>();
            foreach (var currentTest in this.ObservableTests)
            {
                this.InitialTestsCollection.Add(currentTest);
            }
        }
    }
}