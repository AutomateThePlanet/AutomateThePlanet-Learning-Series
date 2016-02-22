// <copyright file="TestCasesMigrationView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System.Threading.Tasks;
using System.Windows;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using Microsoft.TeamFoundation.Client;
using TestCaseManagerCore;
using TestCaseManagerCore.BusinessLogic.Enums;
using TestCaseManagerCore.BusinessLogic.Managers;
using TestCaseManagerCore.Helpers;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the project selection page
    /// </summary>
    public partial class TestCasesMigrationView : System.Windows.Controls.UserControl, IContent
    {
        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

		/// <summary>
		/// Initializes a new instance of the <see cref="TestCasesMigrationView"/> class.
		/// </summary>
		public TestCasesMigrationView()
        {
            this.InitializeComponent();
            isInitialized = false;
        }

        /// <summary>
        /// Gets or sets the project selection view model.
        /// </summary>
        /// <value>
        /// The project selection view model.
        /// </value>
		public TestCasesMigrationViewModel TestCasesMigrationViewModel { get; set; }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            FragmentManager fm = new FragmentManager(e.Fragment);
        }

        /// <summary>
        /// Called when this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Called when a this instance becomes the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            isInitialized = false;
            ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbTestPlansDestination, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
			ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbTestPlansSource, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
        }

        /// <summary>
        /// Called just before this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        /// <remarks>
        /// The method is also invoked when parent frames are about to navigate.
        /// </remarks>
        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (isInitialized)
            {
                return;
            }
            this.ShowProgressBar();
			this.TestCasesMigrationViewModel = new TestCasesMigrationViewModel();

            Task t = Task.Factory.StartNew(() =>
            {
            });
            t.ContinueWith(antecedent =>
            {
				this.DataContext = this.TestCasesMigrationViewModel;
                isInitialized = true;
				HideProgressBar();
				ModernDialog.ShowMessage("Please read carefully the online documentation about the module. The caused changes cannot be reverted!\n\nThe entities should be migrated in the following order:\n1. Shared steps\n2. Suites\n3. Test cases\n4. Add test cases to suites.\n\nBefore proceeding to the next stage of the migration be sure that no errors are present in the previous one!\n\nIn case of unexpected errors you can retry the logic using the created JSON files saved in the default JSON folder.", "Warning", MessageBoxButton.OK);		
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Hides the progress bar.
        /// </summary>
        private void HideProgressBar()
        {
            progressBar.Visibility = System.Windows.Visibility.Hidden;
            mainGrid.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Shows the progress bar.
        /// </summary>
        private void ShowProgressBar()
        {
            progressBar.Visibility = System.Windows.Visibility.Visible;
            mainGrid.Visibility = System.Windows.Visibility.Hidden;
        }

		/// <summary>
		/// Handles the MouseMove event of the cbTestPlansDestination control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
		private void cbTestPlansDestination_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			ComboBoxDropdownExtensions.cboMouseMove(sender, e);
		}

		/// <summary>
		/// Handles the MouseMove event of the cbTestPlansSource control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
		private void cbTestPlansSource_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			ComboBoxDropdownExtensions.cboMouseMove(sender, e);
		}

		/// <summary>
		/// Handles the Click event of the btnSourceBrowser control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnSourceBrowser_Click(object sender, RoutedEventArgs e)
		{
			var projectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
			this.TestCasesMigrationViewModel.LoadProjectSettingsFromUserDecisionSource(projectPicker);
		}

		/// <summary>
		/// Handles the Click event of the btnDestinationBrowser control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnDestinationBrowser_Click(object sender, RoutedEventArgs e)
		{
			var projectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
			this.TestCasesMigrationViewModel.LoadProjectSettingsFromUserDecisionDestination(projectPicker);
		}

		/// <summary>
		/// Handles the Click event of the btnMigrateSharedSteps control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnMigrateSharedSteps_Click(object sender, RoutedEventArgs e)
		{
			bool canMigrate = this.TestCasesMigrationViewModel.CanStartMigration();
			if (canMigrate)
			{
				this.TestCasesMigrationViewModel.StartUiProgressLogging(lblProgress);
				this.TestCasesMigrationViewModel.StartSharedStepsFromSourceToDestinationMigration(internalProgressBar);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnStopSharedStepsMigration control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnStopSharedStepsMigration_Click(object sender, RoutedEventArgs e)
		{
			this.TestCasesMigrationViewModel.StopUiProgressLogging();
			this.TestCasesMigrationViewModel.StopMigrationExecution();
		}

		/// <summary>
		/// Handles the Click event of the btnMigrateSuites control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnMigrateSuites_Click(object sender, RoutedEventArgs e)
		{
			bool canMigrate = this.TestCasesMigrationViewModel.CanStartMigration();
			if (canMigrate)
			{
				this.TestCasesMigrationViewModel.StartUiProgressLogging(lblProgress);
				this.TestCasesMigrationViewModel.StartSuitesFromSourceToDestinationMigration(internalProgressBar);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnStopSuitesMigration control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnStopSuitesMigration_Click(object sender, RoutedEventArgs e)
		{
			this.TestCasesMigrationViewModel.StopUiProgressLogging();
			this.TestCasesMigrationViewModel.StopMigrationExecution();
		}

		/// <summary>
		/// Handles the Click event of the btnBrowseSuitesJsonPath control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnBrowseSuitesJsonPath_Click(object sender, RoutedEventArgs e)
		{
			this.TestCasesMigrationViewModel.MigrationSuitesRetryJsonPath = FileDialogManager.Intance.GetFileName(FileType.JSON);
		}

		/// <summary>
		/// Handles the Click event of the btnMigrateTestCases control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnMigrateTestCases_Click(object sender, RoutedEventArgs e)
		{
			bool canMigrate = this.TestCasesMigrationViewModel.CanStartMigration();
			if (canMigrate)
			{
				this.TestCasesMigrationViewModel.StartUiProgressLogging(lblProgress);
				this.TestCasesMigrationViewModel.StartTestCasesFromSourceToDestinationMigration(internalProgressBar);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnStopTestCasesMigration control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnStopTestCasesMigration_Click(object sender, RoutedEventArgs e)
		{
			this.TestCasesMigrationViewModel.StopUiProgressLogging();
			this.TestCasesMigrationViewModel.StopMigrationExecution();
		}

		/// <summary>
		/// Handles the Click event of the btnBrowseTestCasesJsonPath control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnBrowseTestCasesJsonPath_Click(object sender, RoutedEventArgs e)
		{
			this.TestCasesMigrationViewModel.MigrationTestCasesRetryJsonPath = FileDialogManager.Intance.GetFileName(FileType.JSON);
		}

		/// <summary>
		/// Handles the Click event of the btnAddTestCasesToSuites control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnAddTestCasesToSuites_Click(object sender, RoutedEventArgs e)
		{
			bool canMigrate = this.TestCasesMigrationViewModel.CanStartMigration();
			if (canMigrate)
			{
				this.TestCasesMigrationViewModel.StartUiProgressLogging(lblProgress);
				this.TestCasesMigrationViewModel.StartTestCasesToSuiteFromSourceToDestinationMigration(internalProgressBar);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnStopTestCasesToSuitesAddition control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnStopTestCasesToSuitesAddition_Click(object sender, RoutedEventArgs e)
		{
			this.TestCasesMigrationViewModel.StopUiProgressLogging();
			this.TestCasesMigrationViewModel.StopMigrationExecution();
		}

		/// <summary>
		/// Handles the Click event of the btnDefaultJsonsPath control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnDefaultJsonsPath_Click(object sender, RoutedEventArgs e)
		{
			this.TestCasesMigrationViewModel.DefaultJsonFolder = FolderBrowseDialogManager.Intance.GetFolderPath();
		}

		/// <summary>
		/// Handles the Click event of the btnBrowserSharedStepsJsonPath control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnBrowserSharedStepsJsonPath_Click(object sender, RoutedEventArgs e)
		{
			this.TestCasesMigrationViewModel.MigrationSharedStepsRetryJsonPath = FileDialogManager.Intance.GetFileName(FileType.JSON);
		}

		/// <summary>
		/// Handles the SelectionChanged event of the cbTestPlansSource control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void cbTestPlansSource_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (cbTestPlansSource.SelectedIndex >= 0 && this.TestCasesMigrationViewModel.ObservableSourceTestPlans.Count > cbTestPlansSource.SelectedIndex)
			{
				this.TestCasesMigrationViewModel.SelectedSourceTestPlan = this.TestCasesMigrationViewModel.ObservableSourceTestPlans[cbTestPlansSource.SelectedIndex];
				this.TestCasesMigrationViewModel.InitializeSelectedSourceTestPlan();
			}
		}

		/// <summary>
		/// Handles the SelectionChanged event of the cbTestPlansDestination control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
		private void cbTestPlansDestination_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (cbTestPlansDestination.SelectedIndex >= 0 && this.TestCasesMigrationViewModel.ObservableDestinationTestPlans.Count > cbTestPlansDestination.SelectedIndex)
			{
				this.TestCasesMigrationViewModel.SelectedDestinationTestPlan = this.TestCasesMigrationViewModel.ObservableDestinationTestPlans[cbTestPlansDestination.SelectedIndex];
				this.TestCasesMigrationViewModel.InitializeSelectedDestinationTestPlan();
			}
		}

		/// <summary>
		/// Handles the Click event of the btnBrowseTestCasesToSuitesJsonPath control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void btnBrowseTestCasesToSuitesJsonPath_Click(object sender, RoutedEventArgs e)
		{
			this.TestCasesMigrationViewModel.MigrationAddTestCasesToSuitesRetryJsonPath = FileDialogManager.Intance.GetFileName(FileType.JSON);
		}
    }
}
