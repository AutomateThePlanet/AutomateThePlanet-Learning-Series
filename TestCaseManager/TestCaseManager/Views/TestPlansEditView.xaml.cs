// <copyright file="TestPlansEditView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using TestCaseManagerCore;
using TestCaseManagerCore.BusinessLogic.Entities;
using TestCaseManagerCore.BusinessLogic.Managers;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to creation and deletion of test plans
    /// </summary>
    public partial class TestPlansEditView : System.Windows.Controls.UserControl, IContent
    {
        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The suite unique identifier
        /// </summary>
        //private int suiteId;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestPlansEditView"/> class.
        /// </summary>
        public TestPlansEditView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the test case execution arrangment view model.
        /// </summary>
        /// <value>
        /// The test case execution arrangment view model.
        /// </value>
        public TestPlansEditViewModel TestPlansEditViewModel { get; set; }

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
        /// Handles the Loaded event of the TestCaseExecutionArrangmentView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TestCaseExecutionArrangmentView_Loaded(object sender, RoutedEventArgs e)
        {
            if (isInitialized)
            {
                return;
            }
            this.ShowProgressBar();
            Task t = Task.Factory.StartNew(() =>
            {
                this.TestPlansEditViewModel = new TestCaseManagerCore.ViewModels.TestPlansEditViewModel();
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = this.TestPlansEditViewModel;          
                this.HideProgressBar();
                isInitialized = true;
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
        /// Displays the non selection warning.
        /// </summary>
        private void DisplayNonSelectionWarning()
        {
            ModernDialog.ShowMessage("No selected test plan.", "Warning", MessageBoxButton.OK);
        }

        /// <summary>
        /// Handles the Click event of the btnAddTestPlan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAddTestPlan_Click(object sender, RoutedEventArgs e)
        {
                 RegistryManager.WriteTitleTitlePromtDialog(string.Empty);
            var dialog = new PrompDialogWindow();
            dialog.ShowDialog();

            bool isCanceled;
            string newTitle;
            Task t = Task.Factory.StartNew(() =>
            {
                isCanceled = RegistryManager.GetIsCanceledPromtDialog();
                newTitle = RegistryManager.GetContentPromtDialog();
                while (string.IsNullOrEmpty(newTitle) && !isCanceled)
                {
                }
            });
            t.Wait();
            isCanceled = RegistryManager.GetIsCanceledPromtDialog();
            newTitle = RegistryManager.GetContentPromtDialog();

            if (!isCanceled)
            {
                Log.InfoFormat("Add New Test Plan with Name=\"{0}\"", newTitle);
                this.TestPlansEditViewModel.AddTestPlan(newTitle);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnDeleteTestPlan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnDeleteTestPlan_Click(object sender, RoutedEventArgs e)
        {
            if (dgTestPlans.SelectedItems.Count == 0)
            {
                this.DisplayNonSelectionWarning();
                return;
            }
            List<TestPlan> testPlansToBeDeleted = new List<TestPlan>();
            foreach (TestPlan currentTestPlan in dgTestPlans.SelectedItems)
            {
                testPlansToBeDeleted.Add(currentTestPlan);               
            }
            foreach (TestPlan currentTestPlan in testPlansToBeDeleted)
            {
                Log.InfoFormat("Delete Test Plan with Name=\"{0}\" Id = \"{1}\"", currentTestPlan.Name, currentTestPlan.Id);
                this.TestPlansEditViewModel.DeleteTestPlan(currentTestPlan);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnFinish control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            Log.Info("Navigate to ProjectSelectionView");
            this.NavigateToProjectSelection();
        }   
    }
}