// <copyright file="TestCasesMigrationView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

using System.Threading.Tasks;
using System.Windows;
using AAngelov.Utilities.UI.ControlExtensions;
using AAngelov.Utilities.UI.Enums;
using AAngelov.Utilities.UI.Managers;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using Microsoft.TeamFoundation.Client;
using TestCaseManagerCore;
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
                this.HideProgressBar();
                ModernDialog.ShowMessage("Please read carefully the online documentation about this module because the changes cannot be revirted!\nYou should migrate the entities in the following order: shared steps,suites, test cases, add test cases to suites.\nBefore you proceed to the next stage of the sync be sure that there aren't error in the previous!\nIn case of unexpexted errors you can retry the logic using the created JSON file saved in the default JSON folder.", "Warning", MessageBoxButton.OK);		
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Hides the progress bar.
        /// </summary>
        private void HideProgressBar()
        {
            this.progressBar.Visibility = System.Windows.Visibility.Hidden;
            this.mainGrid.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Shows the progress bar.
        /// </summary>
        private void ShowProgressBar()
        {
            this.progressBar.Visibility = System.Windows.Visibility.Visible;
            this.mainGrid.Visibility = System.Windows.Visibility.Hidden;
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
                this.TestCasesMigrationViewModel.StartUiProgressLogging(this.lblProgress);
                this.TestCasesMigrationViewModel.StartSharedStepsFromSourceToDestinationMigration(this.internalProgressBar);
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
                this.TestCasesMigrationViewModel.StartUiProgressLogging(this.lblProgress);
                this.TestCasesMigrationViewModel.StartSuitesFromSourceToDestinationMigration(this.internalProgressBar);
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
                this.TestCasesMigrationViewModel.StartUiProgressLogging(this.lblProgress);
                this.TestCasesMigrationViewModel.StartTestCasesFromSourceToDestinationMigration(this.internalProgressBar);
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
                this.TestCasesMigrationViewModel.StartUiProgressLogging(this.lblProgress);
                this.TestCasesMigrationViewModel.StartTestCasesToSuiteFromSourceToDestinationMigration(this.internalProgressBar);
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
            if (this.cbTestPlansSource.SelectedIndex >= 0 && this.TestCasesMigrationViewModel.ObservableSourceTestPlans.Count > this.cbTestPlansSource.SelectedIndex)
            {
                this.TestCasesMigrationViewModel.SelectedSourceTestPlan = this.TestCasesMigrationViewModel.ObservableSourceTestPlans[this.cbTestPlansSource.SelectedIndex];
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
            if (this.cbTestPlansDestination.SelectedIndex >= 0 && this.TestCasesMigrationViewModel.ObservableDestinationTestPlans.Count > this.cbTestPlansDestination.SelectedIndex)
            {
                this.TestCasesMigrationViewModel.SelectedDestinationTestPlan = this.TestCasesMigrationViewModel.ObservableDestinationTestPlans[this.cbTestPlansDestination.SelectedIndex];
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

        /// <summary>
        /// Handles the Click event of the btnInfo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://aangelov.com/2014/05/05/migrate-tfs-test-cases-tfs-team-projects-tfs-servers-test-case-manager/");
        }
    }
}