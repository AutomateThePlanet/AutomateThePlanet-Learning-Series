// <copyright file="ExecutionContext.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

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