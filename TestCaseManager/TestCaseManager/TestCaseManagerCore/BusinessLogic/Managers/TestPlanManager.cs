// <copyright file="TestPlanManager.cs" company="Automate The Planet Ltd.">
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
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Entities;

    /// <summary>
    /// Contains helper methods for working with ITestPlan objects
    /// </summary>
    public static class TestPlanManager
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets TestPlan by name.
        /// </summary>
        /// <param name="testManagementTeamProject">TFS team project</param>
        /// <param name="testPlanName">Name of the test plan.</param>
        /// <returns>the found test plan</returns>
        public static ITestPlan GetTestPlanByName(ITestManagementTeamProject testManagementTeamProject, string testPlanName)
        {
            ITestPlanCollection testPlans = GetAllTestPlans(testManagementTeamProject);
            ITestPlan testPlan = null;
            if (testPlans != null)
            {
                foreach (ITestPlan currentTestPlan in testPlans)
                {
                    if (currentTestPlan.Name.Equals(testPlanName))
                    {
                        testPlan = currentTestPlan;
                        break;
                    }
                }
            }

            return testPlan;
        }

        /// <summary>
        /// Gets all test plans in specified TFS team project.
        /// </summary>
        /// <param name="testManagementTeamProject">The _testproject.</param>
        /// <returns>collection of all test plans</returns>
        public static ITestPlanCollection GetAllTestPlans(ITestManagementTeamProject testManagementTeamProject)
        {
            ITestPlanCollection testPlanCollection = null;
            int retryCount = 0;
            try
            {
                testPlanCollection = testManagementTeamProject.TestPlans.Query("SELECT * FROM TestPlan");
            }
            catch (Exception ex)
            {
                log.Error("Getting all plans error.", ex);
                retryCount++;
                throw ex;
            }

            return testPlanCollection;
        }

        /// <summary>
        /// Creates the test plan.
        /// </summary>
        /// <param name="tfsTeamProjectCollection">The TFS team project collection.</param>
        /// <param name="testManagementTeamProject">The test management team project.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static TestPlan CreateTestPlan(TfsTeamProjectCollection tfsTeamProjectCollection, ITestManagementTeamProject testManagementTeamProject, string name)
        {
            ITestPlan newTestPlan = testManagementTeamProject.TestPlans.Create();
            newTestPlan.Name = name;
            newTestPlan.Owner = tfsTeamProjectCollection.AuthorizedIdentity;
            newTestPlan.Save();

            return new TestPlan(newTestPlan);
        }

        /// <summary>
        /// Removes the test plan.
        /// </summary>
        /// <param name="testManagementTeamProject">The test management team project.</param>
        /// <param name="testPlanId">The test plan unique identifier.</param>
        public static void RemoveTestPlan(ITestManagementTeamProject testManagementTeamProject, int testPlanId)
        {
            ITestPlan newTestPlan = testManagementTeamProject.TestPlans.Find(testPlanId);
            newTestPlan.Delete();
        }
    }
}