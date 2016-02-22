// <copyright file="ProjectSelectionViewModel.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerCore.ViewModels
{
    using System.Collections.ObjectModel;
    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.TestManagement.Client;

    /// <summary>
    /// Provides methods and properties related to the Project Selection View
    /// </summary>
    public class ProjectSelectionViewModel : BaseProjectSelectionViewModel
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The test management team project
        /// </summary>
        private ITestManagementTeamProject testManagementTeamProject;

        /// <summary>
        /// The TFS team project collection
        /// </summary>
        private TfsTeamProjectCollection tfsTeamProjectCollection;

        /// <summary>
        /// Gets or sets the TFS client service.
        /// </summary>
        /// <value>
        /// The TFS client service.
        /// </value>
        private ITestManagementService tfsClientService;

        /// <summary>
        /// The preference
        /// </summary>
        private Preferences preferences;

        /// <summary>
        /// The full team project name
        /// </summary>
        private string fullTeamProjectName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSelectionViewModel"/> class.
        /// </summary>
        public ProjectSelectionViewModel()
        {
            this.ObservableTestPlans = new ObservableCollection<string>();
            this.preferences = new Preferences();
        }

        /// <summary>
        /// Gets or sets the observable test plans.
        /// </summary>
        /// <value>
        /// The observable test plans.
        /// </value>
        public ObservableCollection<string> ObservableTestPlans { get; set; }

        /// <summary>
        /// Gets or sets the full name of the team project.
        /// </summary>
        /// <value>
        /// The full name of the team project.
        /// </value>
        public string FullTeamProjectName
        {
            get
            {
                return this.fullTeamProjectName;
            }

            set
            {
                this.fullTeamProjectName = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected test plan.
        /// </summary>
        /// <value>
        /// The selected test plan.
        /// </value>
        public string SelectedTestPlan { get; set; }

        /// <summary>
        /// Load project settings from TFS team project picker.
        /// </summary>
        /// <param name="projectPicker">The project picker.</param>
        public void LoadProjectSettingsFromUserDecision(TeamProjectPicker projectPicker)
        {
            base.LoadProjectSettingsFromUserDecision(projectPicker, ref this.tfsTeamProjectCollection, ref this.testManagementTeamProject, ref this.preferences, this.tfsClientService, this.SelectedTestPlan);
            ExecutionContext.TfsTeamProjectCollection = this.tfsTeamProjectCollection;
            ExecutionContext.TestManagementTeamProject = this.testManagementTeamProject;
            ExecutionContext.Preferences = this.preferences;
            this.FullTeamProjectName = base.GenerateFullTeamProjectName(ExecutionContext.Preferences.TfsUri.ToString(), ExecutionContext.Preferences.TestProjectName);
        }

        /// <summary>
        /// Loads project settings from registry.
        /// </summary>
        public void LoadProjectSettingsFromRegistry()
        {
            base.LoadProjectSettingsFromRegistry(ref this.tfsTeamProjectCollection, ref this.testManagementTeamProject, ref this.preferences, this.tfsClientService, this.SelectedTestPlan);
            ExecutionContext.TfsTeamProjectCollection = this.tfsTeamProjectCollection;
            ExecutionContext.TestManagementTeamProject = this.testManagementTeamProject;
            ExecutionContext.Preferences = this.preferences;
            this.FullTeamProjectName = base.GenerateFullTeamProjectName(ExecutionContext.Preferences.TfsUri.ToString(), ExecutionContext.Preferences.TestProjectName);
        }
	
        /// <summary>
        /// Initializes the test plans.
        /// </summary>
        /// <param name="testManagementTeamProject">The _testproject.</param>
        public void InitializeTestPlans(ITestManagementTeamProject testManagementTeamProject)
        {
            base.InitializeTestPlans(testManagementTeamProject, this.ObservableTestPlans);
        }
    }
}