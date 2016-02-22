// <copyright file="ProjectSelectionView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

using System;
using System.Threading.Tasks;
using System.Windows;
using AAngelov.Utilities.UI.ControlExtensions;
using AAngelov.Utilities.UI.Managers;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using Microsoft.TeamFoundation.Client;
using TestCaseManagerCore;
using TestCaseManagerCore.BusinessLogic.Managers;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the project selection page
    /// </summary>
    public partial class ProjectSelectionView : System.Windows.Controls.UserControl, IContent
    {
        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// The skip automatic load from registry
        /// </summary>
        private bool skipAutoLoad;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSelectionView"/> class.
        /// </summary>
        public ProjectSelectionView()
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
        public ProjectSelectionViewModel ProjectSelectionViewModel { get; set; }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            FragmentManager fm = new FragmentManager(e.Fragment);
            string skipAutoLoadStr = fm.Get("skipAutoLoad");
            if (!string.IsNullOrEmpty(skipAutoLoadStr))
            {
                this.skipAutoLoad = bool.Parse(skipAutoLoadStr);
            }
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
            ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbTestPlans, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
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
            this.ProjectSelectionViewModel = new ProjectSelectionViewModel();
            bool showTfsServerUnavailableException = false;
            Task t = Task.Factory.StartNew(() =>
            {
                this.ProjectSelectionViewModel.LoadProjectSettingsFromRegistry();
                try
                {
                    this.ProjectSelectionViewModel.InitializeTestPlans(ExecutionContext.TestManagementTeamProject);
                }
                catch (Exception)
                {
                    showTfsServerUnavailableException = true;
                }
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = this.ProjectSelectionViewModel;
                isInitialized = true;
                if (this.ProjectSelectionViewModel.IsInitializedFromRegistry && !this.skipAutoLoad)
                {
                    if (showTfsServerUnavailableException)
                    {
                        ModernDialog.ShowMessage("Team Foundation services are unavailable and no test plans can be populated. Please try again after few seconds.", "Warning", MessageBoxButton.OK);
                        Application.Current.Shutdown();
                    }
                    else
                    {
                        if (this.ProjectSelectionViewModel.SelectedTestPlan != null)
                        {
                            ExecutionContext.Preferences.TestPlan = TestPlanManager.GetTestPlanByName(ExecutionContext.TestManagementTeamProject, this.ProjectSelectionViewModel.SelectedTestPlan);
                        }
                        this.AddNewLinksToWindow();
                    }
                }
                else
                { 
                    this.HideProgressBar();
                }
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
        /// Handles the Click event of the BrowseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var projectPicker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false);
            this.ProjectSelectionViewModel.LoadProjectSettingsFromUserDecision(projectPicker);
            this.ProjectSelectionViewModel.InitializeTestPlans(ExecutionContext.TestManagementTeamProject);
        }

        /// <summary>
        /// Handles the Click event of the DisplayButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {
            this.ProjectSelectionViewModel.SelectedTestPlan = this.cbTestPlans.Text;
            if (string.IsNullOrEmpty(this.ProjectSelectionViewModel.SelectedTestPlan))
            {
                ModernDialog.ShowMessage("No test plan selected.", "Warning", MessageBoxButton.OK);
                return;
            }
            if (ExecutionContext.TestManagementTeamProject == null)
            {
                ModernDialog.ShowMessage("No test project selected.", "Warning", MessageBoxButton.OK);
                return;
            }
            RegistryManager.Instance.WriteCurrentTestPlan(this.ProjectSelectionViewModel.SelectedTestPlan);
            try
            {
                ExecutionContext.Preferences.TestPlan = TestPlanManager.GetTestPlanByName(ExecutionContext.TestManagementTeamProject, this.ProjectSelectionViewModel.SelectedTestPlan);
            }
            catch (Exception)
            {
                ModernDialog.ShowMessage("Team Foundation services are unavailable and no test plans can be populated. Please try again after few seconds.", "Warning", MessageBoxButton.OK);
            }
            this.AddNewLinksToWindow();
        }

        /// <summary>
        /// Adds the new links to the window.
        /// </summary>
        private void AddNewLinksToWindow()
        {
            ModernWindow mw = Window.GetWindow(this) as ModernWindow;
            mw.MenuLinkGroups.Clear();
            LinkGroup lg = new LinkGroup();
            Link l1 = new Link();
            l1.DisplayName = "All Test Cases";
            Uri u1 = new Uri("/Views/TestCasesInitialView.xaml", UriKind.Relative);
            l1.Source = u1;
            mw.ContentSource = u1;
            lg.Links.Add(l1);

            Link l3 = new Link();
            l3.DisplayName = "All Shared Steps";
            Uri u3 = new Uri("/Views/SharedStepsInitialView.xaml", UriKind.Relative);
            l3.Source = u3;
            lg.Links.Add(l3);

            Uri u2 = new Uri("/Views/TestCaseBatchDuplicateView.xaml#loadTestCases=true&loadSpecificTestCases=false", UriKind.Relative);
            Link l2 = new Link();
            l2.DisplayName = "Replace|Duplicate Test Cases";
            l2.Source = u2;
            lg.Links.Add(l2);

            Uri u4 = new Uri("/Views/TestCaseBatchDuplicateView.xaml#loadTestCases=false&loadSpecificTestCases=false", UriKind.Relative);
            Link l4 = new Link();
            l4.DisplayName = "Replace|Duplicate Shared Steps";
            l4.Source = u4;
            lg.Links.Add(l4);

            Uri u5 = new Uri("/Views/TestCasesMigrationView.xaml", UriKind.Relative);
            Link l5 = new Link();
            l5.DisplayName = "Migrate Test Cases";
            l5.Source = u5;
            lg.Links.Add(l5);

            mw.MenuLinkGroups.Add(lg);
        }

        /// <summary>
        /// Handles the MouseMove event of the cbTestPlans control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbTestPlans_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the btnEditTestPlans control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnEditTestPlans_Click(object sender, RoutedEventArgs e)
        {
            Navigator.Instance.NavigateToTestPlansEdit(this);
        }

        /// <summary>
        /// Handles the Click event of the btnInfo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://testcasemanager.codeplex.com/wikipage?title=Change%20Project%20View&version=14");
        }
    }
}