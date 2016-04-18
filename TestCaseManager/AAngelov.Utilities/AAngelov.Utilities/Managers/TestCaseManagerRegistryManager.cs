// <copyright file="TestCaseManagerRegistryManager.cs" company="Automate The Planet Ltd.">
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
using System;

namespace AAngelov.Utilities.Managers
{
    /// <summary>
    /// Contains Test Case Manager APP Registry related methods
    /// </summary>
    public class TestCaseManagerRegistryManager : BaseRegistryManager
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static TestCaseManagerRegistryManager instance;

        /// <summary>
        /// The should show comment window registry sub key name
        /// </summary>
        private readonly string shouldShowCommentWindowRegistrySubKeyName = "shouldShowCommentWindow";

        /// <summary>
        /// The suite filter registry sub key name
        /// </summary>
        private readonly string suiteFilterRegistrySubKeyName = "suiteFilter";

        /// <summary>
        /// The suite filter registry sub key name
        /// </summary>
        private readonly string selectedSuiteIdFilterRegistrySubKeyName = "selectedSuiteId";

        /// <summary>
        /// The show subsuite test cases registry sub key name
        /// </summary>
        private readonly string showSubsuiteTestCasesRegistrySubKeyName = "showSubsuiteTestCases";

        /// <summary>
        /// The project DLL path registry sub key name
        /// </summary>
        private readonly string projectDllPathRegistrySubKeyName = "projectPathDll";

        /// <summary>
        /// The team project URI registry sub key name
        /// </summary>
        private readonly string teamProjectUriRegistrySubKeyName = "teamProjectUri";

        /// <summary>
        /// The team project name registry sub key name
        /// </summary>
        private readonly string teamProjectNameRegistrySubKeyName = "teamProjectName";

        /// <summary>
        /// The test plan registry sub key name
        /// </summary>
        private readonly string testPlanRegistrySubKeyName = "testPlan";

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseManagerRegistryManager"/> class.
        /// </summary>
        public TestCaseManagerRegistryManager()
        {
            this.MainRegistrySubKey = "TestCaseManager/settings";
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static TestCaseManagerRegistryManager Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new TestCaseManagerRegistryManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Writes the show subsuite test cases.
        /// </summary>
        /// <param name="showSubsuiteTestCases">if set to <c>true</c> [show subsuite test cases].</param>
        public void WriteShowSubsuiteTestCases(bool showSubsuiteTestCases)
        {
            this.Write(this.GenerateMergedKey(this.showSubsuiteTestCasesRegistrySubKeyName), showSubsuiteTestCases); 
        }

        /// <summary>
        /// Writes the should comment window show.
        /// </summary>
        /// <param name="shouldCommentWindowShow">if set to <c>true</c> [should comment window show].</param>
        public void WriteShouldCommentWindowShow(bool shouldCommentWindowShow)
        {
            this.Write(this.GenerateMergedKey(this.shouldShowCommentWindowRegistrySubKeyName), shouldCommentWindowShow);
        }

        /// <summary>
        /// Writes the suite filter.
        /// </summary>
        /// <param name="suiteFilter">The suite filter.</param>
        public void WriteSuiteFilter(string suiteFilter)
        {
            this.Write(this.GenerateMergedKey(this.suiteFilterRegistrySubKeyName), suiteFilter);
        }

        /// <summary>
        /// Writes the selected suite unique identifier.
        /// </summary>
        /// <param name="suiteId">The suite unique identifier.</param>
        public void WriteSelectedSuiteId(int suiteId)
        {
            this.Write(this.GenerateMergedKey(this.selectedSuiteIdFilterRegistrySubKeyName), suiteId);
        }

        /// <summary>
        /// Writes the current team project URI to registry.
        /// </summary>
        /// <param name="teamProjectUri">The team project URI.</param>
        public void WriteCurrentTeamProjectUri(string teamProjectUri)
        {
            this.Write(this.GenerateMergedKey(this.teamProjectUriRegistrySubKeyName), teamProjectUri);
        }

        /// <summary>
        /// Writes the name of the current team project to registry.
        /// </summary>
        /// <param name="teamProjectName">Name of the team project.</param>
        public void WriteCurrentTeamProjectName(string teamProjectName)
        {
            this.Write(this.GenerateMergedKey(this.teamProjectNameRegistrySubKeyName), teamProjectName);
        }

        /// <summary>
        /// Writes the current test plan to registry.
        /// </summary>
        /// <param name="testPlan">The test plan.</param>
        public void WriteCurrentTestPlan(string testPlan)
        {
            this.Write(this.GenerateMergedKey(this.testPlanRegistrySubKeyName), testPlan);
        }

        /// <summary>
        /// Writes the current project DLL path to registry.
        /// </summary>
        /// <param name="projectDllPath">The project DLL path.</param>
        public void WriteCurrentProjectDllPath(string projectDllPath)
        {
            this.Write(this.GenerateMergedKey(this.projectDllPathRegistrySubKeyName), projectDllPath);
        }

        /// <summary>
        /// Reads the show subsuite test cases.
        /// </summary>
        /// <returns>should show subsuite test cases</returns>
        public bool ReadShowSubsuiteTestCases()
        {
            return this.ReadBool(this.GenerateMergedKey(this.showSubsuiteTestCasesRegistrySubKeyName));
        }

        /// <summary>
        /// Reads the should comment window show.
        /// </summary>
        /// <returns>should Comment Window Show</returns>
        public bool ReadShouldCommentWindowShow()
        {
            return this.ReadBool(this.GenerateMergedKey(this.shouldShowCommentWindowRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the team project URI from registry.
        /// </summary>
        /// <returns>team project URI</returns>
        public string GetTeamProjectUri()
        {
            return this.ReadStr(this.GenerateMergedKey(this.teamProjectUriRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the name of the team project from registry.
        /// </summary>
        /// <returns>name of the team project</returns>
        public string GetTeamProjectName()
        {
            return this.ReadStr(this.GenerateMergedKey(this.teamProjectNameRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the project DLL path from registry.
        /// </summary>
        /// <returns>the project DLL path</returns>
        public string GetProjectDllPath()
        {
            return this.ReadStr(this.GenerateMergedKey(this.projectDllPathRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the test plan from registry.
        /// </summary>
        /// <returns>the test plan</returns>
        public string GetTestPlan()
        {
            return this.ReadStr(this.GenerateMergedKey(this.testPlanRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the suite filter.
        /// </summary>
        /// <returns>the suite filter</returns>
        public string GetSuiteFilter()
        {
            return this.ReadStr(this.GenerateMergedKey(this.suiteFilterRegistrySubKeyName));
        }

        /// <summary>
        /// Gets the selected suite unique identifier from registry;
        /// </summary>
        /// <returns>selected suite id</returns>
        public int GetSelectedSuiteId()
        {
            int result = -1;
            try
            {
                result = this.ReadInt(this.GenerateMergedKey(this.selectedSuiteIdFilterRegistrySubKeyName));
            }
            catch (NullReferenceException)
            {
                result = -1;
            }

            return result;
        }
    }
}