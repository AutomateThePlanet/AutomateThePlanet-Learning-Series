using System;
// <copyright file="TestCaseDetailedView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using TestCaseManagerCore;
using TestCaseManagerCore.BusinessLogic.Enums;
using TestCaseManagerCore.BusinessLogic.Managers;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the test case detailed(read mode) page
    /// </summary>
    public partial class TestCaseDetailedView : UserControl, IContent
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The edit command
        /// </summary>
        public static RoutedCommand EditCommand = new RoutedCommand();

        /// <summary>
        /// The duplicate command
        /// </summary>
        public static RoutedCommand DuplicateCommand = new RoutedCommand();

        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseDetailedView"/> class.
        /// </summary>
        public TestCaseDetailedView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the test case unique identifier.
        /// </summary>
        /// <value>
        /// The test case unique identifier.
        /// </value>
        public int TestCaseId { get; set; }

        /// <summary>
        /// Gets or sets the test suite unique identifier.
        /// </summary>
        /// <value>
        /// The test suite unique identifier.
        /// </value>
        public int TestSuiteId { get; set; }

        /// <summary>
        /// Gets or sets the test case detailed view model.
        /// </summary>
        /// <value>
        /// The test case detailed view model.
        /// </value>
        public TestCaseDetailedViewModel TestCaseDetailedViewModel { get; set; }

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
            Task t = Task.Factory.StartNew(() =>
            {
                Log.InfoFormat("Preview test case with id: {0} and suite id {1}", this.TestCaseId, this.TestSuiteId);
                TestCaseDetailedViewModel = new TestCaseDetailedViewModel(this.TestCaseId, this.TestSuiteId);
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = TestCaseDetailedViewModel;
                EditCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));
                DuplicateCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Alt));
                this.HideProgressBar();
                isInitialized = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());   
        }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            isInitialized = false;
            FragmentManager fm = new FragmentManager(e.Fragment);
            this.TestCaseId = int.Parse(fm.Fragments["id"]);
            this.TestSuiteId = int.Parse(fm.Fragments["suiteId"]);
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
        /// Handles the Click event of the EditButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (TestCaseDetailedViewModel.TestCase.ITestSuiteBase != null)
            {
                Log.InfoFormat("Edit test case with id: {0} and suite id {1}", TestCaseDetailedViewModel.TestCase.ITestCase.Id, TestCaseDetailedViewModel.TestCase.ITestSuiteBase.Id);
                this.NavigateToTestCasesEditView(TestCaseDetailedViewModel.TestCase.ITestCase.Id, TestCaseDetailedViewModel.TestCase.ITestSuiteBase.Id);
            }
            else
            {
                Log.InfoFormat("Edit test case with id: {0}", TestCaseDetailedViewModel.TestCase.ITestCase.Id);
                this.NavigateToTestCasesEditView(TestCaseDetailedViewModel.TestCase.ITestCase.Id, -1);
            }            
        }

        /// <summary>
        /// Handles the Click event of the DuplicateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DuplicateButton_Click(object sender, RoutedEventArgs e)
        {
            if (TestCaseDetailedViewModel.TestCase.ITestSuiteBase != null)
            {
                Log.InfoFormat("Duplicate test case with id: {0} and suite id {1}", TestCaseDetailedViewModel.TestCase.ITestCase.Id, TestCaseDetailedViewModel.TestCase.ITestSuiteBase.Id);
                this.NavigateToTestCasesEditView(TestCaseDetailedViewModel.TestCase.ITestCase.Id, TestCaseDetailedViewModel.TestCase.ITestSuiteBase.Id, true, true);
            }
            else
            {
                Log.InfoFormat("Duplicate test case with id: {0}", TestCaseDetailedViewModel.TestCase.ITestCase.Id);
                this.NavigateToTestCasesEditView(TestCaseDetailedViewModel.TestCase.ITestCase.Id, -1, true, true);
            }            
        }

        /// <summary>
        /// Handles the LoadingRow event of the dgTestSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridRowEventArgs"/> instance containing the event data.</param>
        private void dgTestSteps_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            // Adding 1 to make the row count start at 1 instead of 0
            // as pointed out by daub815
            e.Row.Header = (e.Row.GetIndex() + 1).ToString(); 
        }

        /// <summary>
        /// Sets the new execution outcome internal.
        /// </summary>
        private void SetNewExecutionOutcomeInternal(TestCaseExecutionType testCaseExecutionType)
        {
            bool shouldCommentWindowShow = RegistryManager.ReadShouldCommentWindowShow();
            string comment = String.Empty;
            bool isCanceled = false;
            if (shouldCommentWindowShow && testCaseExecutionType != TestCaseExecutionType.Active)
            {
                RegistryManager.WriteTitleTitlePromtDialog(string.Empty);
                var dialog = new PrompDialogRichTextBoxWindow();
                dialog.ShowDialog();
                Task t = Task.Factory.StartNew(() =>
                {
                    isCanceled = RegistryManager.GetIsCanceledPromtDialog();
                    comment = RegistryManager.GetContentPromtDialog();
                    while (string.IsNullOrEmpty(comment) && !isCanceled)
                    {
                    }
                });
                t.Wait();
                isCanceled = RegistryManager.GetIsCanceledPromtDialog();
                comment = RegistryManager.GetContentPromtDialog();
            }

            if (!isCanceled || !shouldCommentWindowShow)
            {                
                Task t1 = Task.Factory.StartNew(() =>
                {
					this.TestCaseDetailedViewModel.TestCase.SetNewExecutionOutcome(ExecutionContext.Preferences.TestPlan, testCaseExecutionType, comment);
                    this.TestCaseDetailedViewModel.TestCase.LastExecutionOutcome = testCaseExecutionType;
                });
                t1.ContinueWith(antecedent =>
                {
                    this.NavigateToTestCasesInitialView();
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        /// <summary>
        /// Handles the Click event of the btnPass control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnPass_Click(object sender, RoutedEventArgs e)
        {
            this.SetNewExecutionOutcomeInternal(TestCaseExecutionType.Passed);
        }

        /// <summary>
        /// Handles the Click event of the btnFail control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnFail_Click(object sender, RoutedEventArgs e)
        {
            this.SetNewExecutionOutcomeInternal(TestCaseExecutionType.Failed);
        }

        /// <summary>
        /// Handles the Click event of the btnBlock control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnBlock_Click(object sender, RoutedEventArgs e)
        {
            this.SetNewExecutionOutcomeInternal(TestCaseExecutionType.Blocked);
        }
    }
}
