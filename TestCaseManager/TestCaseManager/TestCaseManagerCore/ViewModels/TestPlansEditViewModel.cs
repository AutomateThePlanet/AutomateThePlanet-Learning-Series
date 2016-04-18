// <copyright file="TestPlansEditViewModel.cs" company="Automate The Planet Ltd.">
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
    using System.Collections.ObjectModel;
    using System.Linq;
    using AAngelov.Utilities.UI.Core;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.BusinessLogic.Managers;

    /// <summary>
    /// Contains methods and properties related to the EditTestPlans View
    /// </summary>
    public class TestPlansEditViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The log
        /// </summary>
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseExecutionArrangmentViewModel"/> class.
        /// </summary>
        public TestPlansEditViewModel()
        {
            this.ObservableTestPlans = new ObservableCollection<TestPlan>();
            ITestPlanCollection testPlanCores = TestPlanManager.GetAllTestPlans(ExecutionContext.TestManagementTeamProject);
            testPlanCores.ToList().ForEach(t => this.ObservableTestPlans.Add(new TestPlan(t)));
        }

        /// <summary>
        /// Gets or sets the observable test plans.
        /// </summary>
        /// <value>
        /// The observable test plans.
        /// </value>
        public ObservableCollection<TestPlan> ObservableTestPlans { get; set; }

        /// <summary>
        /// Deletes the test plan.
        /// </summary>
        /// <param name="testPlanToBeDeleted">The test plan automatic be deleted.</param>
        public void DeleteTestPlan(TestPlan testPlanToBeDeleted)
        {
            TestPlanManager.RemoveTestPlan(ExecutionContext.TestManagementTeamProject, testPlanToBeDeleted.Id);
            this.ObservableTestPlans.Remove(testPlanToBeDeleted);
        }

        /// <summary>
        /// Adds the test plan.
        /// </summary>
        /// <param name="name">The name.</param>
        public void AddTestPlan(string name)
        {
            TestPlan testPlanToBeAdded = TestPlanManager.CreateTestPlan(ExecutionContext.TfsTeamProjectCollection, ExecutionContext.TestManagementTeamProject, name);
            this.ObservableTestPlans.Add(testPlanToBeAdded);
        }
    }
}