// <copyright file="ExecutionContext.cs" company="Automate The Planet Ltd.">
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
namespace TestCaseManagerCore
{
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using System;
    using System.Collections.Generic;
    using TestCaseManagerCore.BusinessLogic.Entities;
    using TestCaseManagerCore.ViewModels;

    /// <summary>
    /// Contains App Execution Context Properties
    /// </summary>
    public class ExecutionContext
    {
        /// <summary>
        /// The preferences of the current execution
        /// </summary>
        private static Preferences preferences;

        /// <summary>
        /// The TFS team project collection
        /// </summary>
        private static TfsTeamProjectCollection tfsTeamProjectCollection;

        /// <summary>
        /// The team project
        /// </summary>
        private static ITestManagementTeamProject teamProject; 

        /// <summary>
        /// Gets or sets the preferences.
        /// </summary>
        /// <value>
        /// The preferences.
        /// </value>
        public static Preferences Preferences
        {
            get
            {
                return preferences;
            }

            set
            {
                preferences = value;
            }
        }

        /// <summary>
        /// Gets or sets the TFS team project collection.
        /// </summary>
        /// <value>
        /// The TFS team project collection.
        /// </value>
        public static TfsTeamProjectCollection TfsTeamProjectCollection
        {
            get
            {
                return tfsTeamProjectCollection;
            }

            set
            {
                tfsTeamProjectCollection = value; 
            }
        }

        /// <summary>
        /// Gets or sets the test management team project.
        /// </summary>
        /// <value>
        /// The test management team project.
        /// </value>
        public static ITestManagementTeamProject TestManagementTeamProject
        {
            get
            {
                return teamProject;
            }

            set
            {
                teamProject = value;
            }
        }

        /// <summary>
        /// Gets or sets the settings view model.
        /// </summary>
        /// <value>
        /// The settings view model.
        /// </value>
        public static SettingsViewModel SettingsViewModel { get; set; }

        /// <summary>
        /// Gets or sets the test case edit view model.
        /// </summary>
        /// <value>
        /// The test case edit view model.
        /// </value>
        public static TestCaseEditViewModel TestCaseEditViewModel { get; set; }

        /// <summary>
        /// Gets or sets the selected test cases for change.
        /// </summary>
        /// <value>
        /// The selected test cases for change.
        /// </value>
        public static List<TestCase> SelectedTestCasesForChange { get; set; }

        /// <summary>
        /// Gets or sets the test case runs.
        /// </summary>
        /// <value>
        /// The test case runs.
        /// </value>
        public static Dictionary<int, TestCaseRun> TestCaseRuns { get; set; }
    }
}