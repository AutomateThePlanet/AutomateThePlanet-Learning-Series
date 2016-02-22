// <copyright file="TestCaseExecutionArrangmentView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

using AAngelov.Utilities.UI.Managers;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestCaseManagerCore.BusinessLogic.Entities;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the test case initial(search mode) page
    /// </summary>
    public partial class TestCaseRunStatisticsView : System.Windows.Controls.UserControl, IContent
    {
        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseExecutionArrangmentView"/> class.
        /// </summary>
        public TestCaseRunStatisticsView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the test case run statistics view model.
        /// </summary>
        /// <value>
        /// The test case run statistics view model.
        /// </value>
        public TestCaseRunStatisticsViewModel TestCaseRunStatisticsViewModel { get; set; }

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
            //this.InitializeFastKeys();
            Task t = Task.Factory.StartNew(() =>
            {
                this.TestCaseRunStatisticsViewModel = new TestCaseManagerCore.ViewModels.TestCaseRunStatisticsViewModel();
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = this.TestCaseRunStatisticsViewModel;          
                this.HideProgressBar();
                isInitialized = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Initializes the fast keys.
        /// </summary>
        private void InitializeFastKeys()
        {
            //MoveUpTestCasesCommand.InputGestures.Add(new KeyGesture(Key.Up, ModifierKeys.Alt));
            //MoveDownTestCasesCommand.InputGestures.Add(new KeyGesture(Key.Down, ModifierKeys.Alt));
            //SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl + S"));
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
        /// Handles the SelectedCellsChanged event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectedCellsChangedEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            List<TestCase> selectedTestCases = this.GetSelectedTestCasesInternal();
            this.TestCaseRunStatisticsViewModel.CalculateTotalExecutionTimeSelectedTestCase(selectedTestCases);
        }

        /// <summary>
        /// Gets the selected test cases internal.
        /// </summary>
        /// <returns></returns>
        private List<TestCase> GetSelectedTestCasesInternal()
        {
            List<TestCase> testCases = new List<TestCase>();
            foreach (TestCase currentTestCase in this.dgTestCases.SelectedItems)
            {
                testCases.Add(currentTestCase);
            }
            return testCases;
        }

        /// <summary>
        /// Handles the Click event of the btnInfo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://aangelov.com/2014/05/19/estimate-better-qa-effort-test-case-manager-run-time-execution-statistics/");
        }
    }
}