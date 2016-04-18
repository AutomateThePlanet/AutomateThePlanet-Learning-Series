// <copyright file="SharedStepManager.cs" company="Automate The Planet Ltd.">
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
namespace TestCaseManagerCore.BusinessLogic.Managers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;

    /// <summary>
    /// Contains helper methods for working with SharedStep entities
    /// </summary>
    public static class SharedStepManager
    {
        /// <summary>
        /// Gets all shared steps information test plan.
        /// </summary>
        /// <returns></returns>
        public static List<SharedStep> GetAllSharedStepsInTestPlan(ITestManagementTeamProject testManagementTeamProject)
        {
            List<SharedStep> sharedSteps = new List<SharedStep>();
            var testPlanSharedStepsCore = testManagementTeamProject.SharedSteps.Query("SELECT * FROM WorkItems WHERE [System.TeamProject] = @project and [System.WorkItemType] = 'Shared Steps'");
            foreach (ISharedStep currentSharedStepCore in testPlanSharedStepsCore)
            {
                SharedStep currentSharedStep = new SharedStep(currentSharedStepCore);
                sharedSteps.Add(currentSharedStep);
            }

            return sharedSteps;
        }

        /// <summary>
        /// Gets the shared step by unique identifier.
        /// </summary>
        /// <param name="sharedStepId">The shared step unique identifier.</param>
        /// <returns></returns>
        public static SharedStep GetSharedStepById(ITestManagementTeamProject testManagementTeamProject, int sharedStepId)
        {
            ISharedStep sharedStepCore = testManagementTeamProject.SharedSteps.Find(sharedStepId);
            SharedStep currentSharedStep = new SharedStep(sharedStepCore);

            return currentSharedStep;
        }

        /// <summary>
        /// Saves the specified shared step.
        /// </summary>
        /// <param name="sharedStep">The shared step.</param>
        /// <param name="createNew">if set to <c>true</c> [create new].</param>
        /// <param name="newSuiteTitle">The new suite title.</param>
        /// <param name="testSteps">The test steps.</param>
        /// <returns></returns>
        public static SharedStep Save(this SharedStep sharedStep, ITestManagementTeamProject testManagementTeamProject, bool createNew, ICollection<TestStep> testSteps, bool shouldSetArea = true)
        {
            SharedStep currentSharedStep = sharedStep;
            if (createNew)
            {
                ISharedStep sharedStepCore = testManagementTeamProject.SharedSteps.Create();
                currentSharedStep = new SharedStep(sharedStepCore);
            }
            if (shouldSetArea)
            {
                currentSharedStep.ISharedStep.Area = sharedStep.Area;
            }
            currentSharedStep.ISharedStep.Title = sharedStep.Title;
            currentSharedStep.ISharedStep.Priority = (int)sharedStep.Priority;
            currentSharedStep.ISharedStep.Actions.Clear();
            currentSharedStep.ISharedStep.Owner = testManagementTeamProject.TfsIdentityStore.FindByTeamFoundationId(sharedStep.TeamFoundationId);
            List<Guid> addedSharedStepGuids = new List<Guid>();
            foreach (TestStep currentStep in testSteps)
            {
                ITestStep testStepCore = currentSharedStep.ISharedStep.CreateTestStep();
                testStepCore.Title = currentStep.ActionTitle;
                testStepCore.ExpectedResult = currentStep.ActionExpectedResult;
                currentSharedStep.ISharedStep.Actions.Add(testStepCore);
            }
            currentSharedStep.ISharedStep.Flush();
            currentSharedStep.ISharedStep.Save();

            return currentSharedStep;
        }
    }
}