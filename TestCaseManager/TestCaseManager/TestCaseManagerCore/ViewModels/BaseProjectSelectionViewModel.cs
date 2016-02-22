// <copyright file="BaseProjectSelectionViewModel.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Forms;
    using AAngelov.Utilities.UI.Core;
    using FirstFloor.ModernUI.Windows.Controls;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.TestManagement.Client;
    using TestCaseManagerCore.BusinessLogic.Managers;

    /// <summary>
    /// Provides base methods and properties for team project/collection
    /// </summary>
    public class BaseProjectSelectionViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets or sets a value indicating whether [is initialized from registry].
        /// </summary>
        /// <value>
        /// <c>true</c> if [is initialized from registry]; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitializedFromRegistry { get; set; }

        /// <summary>
        /// Load project settings from TFS team project picker.
        /// </summary>
        /// <param name="projectPicker">The project picker.</param>
        public void LoadProjectSettingsFromUserDecision(TeamProjectPicker projectPicker, ref TfsTeamProjectCollection tfsTeamProjectCollection, ref ITestManagementTeamProject testManagementTeamProject, ref Preferences preferences, ITestManagementService testService, string selectedTestPlan, bool writeToRegistry = true)
        {
            preferences = new Preferences();
            log.Info("Load project info depending on the user choice from project picker!");
            try
            {
                using (projectPicker)
                {
                    var userSelected = projectPicker.ShowDialog();

                    if (userSelected == DialogResult.Cancel)
                    {
                        return;
                    }

                    if (projectPicker.SelectedTeamProjectCollection != null)
                    {
                        preferences.TfsUri = projectPicker.SelectedTeamProjectCollection.Uri;
                        log.InfoFormat("Picker: TFS URI: {0}", preferences.TfsUri);
                        preferences.TestProjectName = projectPicker.SelectedProjects[0].Name;
                        log.InfoFormat("Picker: Test Project Name: {0}", preferences.TestProjectName);
                        tfsTeamProjectCollection = projectPicker.SelectedTeamProjectCollection;
                        log.InfoFormat("Picker: TfsTeamProjectCollection: {0}", tfsTeamProjectCollection);
                        testService = (ITestManagementService)tfsTeamProjectCollection.GetService(typeof(ITestManagementService));
                        testManagementTeamProject = testService.GetTeamProject(preferences.TestProjectName);
                    }
                    log.InfoFormat("Test Project Name: {0}", preferences.TestProjectName);
                    log.InfoFormat("TFS URI: {0}", preferences.TfsUri);
                    if (writeToRegistry)
                    {
                        RegistryManager.Instance.WriteCurrentTeamProjectName(preferences.TestProjectName);
                        RegistryManager.Instance.WriteCurrentTeamProjectUri(preferences.TfsUri.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage("Error selecting team project.", "Warning", MessageBoxButton.OK);
                log.Error("Project info not selected.", ex);
            }
        }

        /// <summary>
        /// Loads project settings from registry.
        /// </summary>
        public void LoadProjectSettingsFromRegistry(ref TfsTeamProjectCollection tfsTeamProjectCollection, ref ITestManagementTeamProject testManagementTeamProject, ref Preferences preferences, ITestManagementService testService, string selectedTestPlan)
        {
            log.Info("Load project info loaded from registry!");
            string teamProjectUri = RegistryManager.Instance.GetTeamProjectUri();
            string teamProjectName = RegistryManager.Instance.GetTeamProjectName();
            string projectDllPath = RegistryManager.Instance.GetProjectDllPath();
            if (!string.IsNullOrEmpty(teamProjectUri) && !string.IsNullOrEmpty(teamProjectName))
            {
                preferences.TfsUri = new Uri(teamProjectUri);
                log.InfoFormat("Registry> TFS URI: {0}", preferences.TfsUri);
                preferences.TestProjectName = teamProjectName;
                log.InfoFormat("Registry> Test Project Name: {0}", preferences.TestProjectName);
                tfsTeamProjectCollection = new TfsTeamProjectCollection(preferences.TfsUri);
                log.InfoFormat("Registry> TfsTeamProjectCollection: {0}", tfsTeamProjectCollection);
                testService = (ITestManagementService)tfsTeamProjectCollection.GetService(typeof(ITestManagementService));
                testManagementTeamProject = testService.GetTeamProject(preferences.TestProjectName);
                selectedTestPlan = RegistryManager.Instance.GetTestPlan();
                log.InfoFormat("Registry> SelectedTestPlan: {0}", selectedTestPlan);
                if (!string.IsNullOrEmpty(selectedTestPlan))
                {
                    preferences.TestPlan = TestPlanManager.GetTestPlanByName(testManagementTeamProject, selectedTestPlan);
                    this.IsInitializedFromRegistry = true;
                }
            }
        }

        /// <summary>
        /// Initializes the test plans.
        /// </summary>
        /// <param name="testManagementTeamProject">The _testproject.</param>
        public void InitializeTestPlans(ITestManagementTeamProject testManagementTeamProject, ICollection<string> testPlans)
        {
            testPlans.Clear();
            ITestPlanCollection testPlansCollection = TestPlanManager.GetAllTestPlans(testManagementTeamProject);
            foreach (ITestPlan tp in testPlansCollection)
            {
                testPlans.Add(tp.Name);
            }
        }

        /// <summary>
        /// Generates the full name of the team project.
        /// </summary>
        /// <returns>The full name of the team project</returns>
        public string GenerateFullTeamProjectName(string tfsUri, string testProjectName)
        {
            string fullTeamProjectName = string.Concat(tfsUri, "/", testProjectName);
            return fullTeamProjectName;
        }
    }
}