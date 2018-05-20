// <copyright file="TestCaseDetailedView.xaml.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AAngelov.Utilities.UI.Managers;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using TestCaseManagerCore;
using TestCaseManagerCore.BusinessLogic.Enums;
using TestCaseManagerCore.BusinessLogic.Managers;
using TestCaseManagerCore.ViewModels;

// <copyright file="TestCaseDetailedView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                log.InfoFormat("Preview test case with id: {0} and suite id {1}", this.TestCaseId, this.TestSuiteId);
                this.TestCaseDetailedViewModel = new TestCaseDetailedViewModel(this.TestCaseId, this.TestSuiteId);
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = this.TestCaseDetailedViewModel;
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
        /// Handles the Click event of the EditButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.TestCaseDetailedViewModel.TestCase.ITestSuiteBase != null)
            {
                log.InfoFormat("Edit test case with id: {0} and suite id {1}", this.TestCaseDetailedViewModel.TestCase.ITestCase.Id, this.TestCaseDetailedViewModel.TestCase.ITestSuiteBase.Id);
                Navigator.Instance.NavigateToTestCasesEditView(this, this.TestCaseDetailedViewModel.TestCase.ITestCase.Id, this.TestCaseDetailedViewModel.TestCase.ITestSuiteBase.Id);
            }
            else
            {
                log.InfoFormat("Edit test case with id: {0}", this.TestCaseDetailedViewModel.TestCase.ITestCase.Id);
                Navigator.Instance.NavigateToTestCasesEditView(this, this.TestCaseDetailedViewModel.TestCase.ITestCase.Id, -1);
            }
        }

        /// <summary>
        /// Handles the Click event of the DuplicateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DuplicateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.TestCaseDetailedViewModel.TestCase.ITestSuiteBase != null)
            {
                log.InfoFormat("Duplicate test case with id: {0} and suite id {1}", this.TestCaseDetailedViewModel.TestCase.ITestCase.Id, this.TestCaseDetailedViewModel.TestCase.ITestSuiteBase.Id);
                Navigator.Instance.NavigateToTestCasesEditView(this, this.TestCaseDetailedViewModel.TestCase.ITestCase.Id, this.TestCaseDetailedViewModel.TestCase.ITestSuiteBase.Id, true, true);
            }
            else
            {
                log.InfoFormat("Duplicate test case with id: {0}", this.TestCaseDetailedViewModel.TestCase.ITestCase.Id);
                Navigator.Instance.NavigateToTestCasesEditView(this, this.TestCaseDetailedViewModel.TestCase.ITestCase.Id, -1, true, true);
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
            bool shouldCommentWindowShow = RegistryManager.Instance.ReadShouldCommentWindowShow();
            string comment = string.Empty;
            bool isCanceled = false;
            if (shouldCommentWindowShow && testCaseExecutionType != TestCaseExecutionType.Active)
            {
                UIRegistryManager.Instance.WriteTitleTitlePromtDialog(string.Empty);
                var dialog = new PrompDialogRichTextBoxWindow();
                dialog.ShowDialog();
                Task t = Task.Factory.StartNew(() =>
                {
                    isCanceled = UIRegistryManager.Instance.GetIsCanceledPromtDialog();
                    comment = UIRegistryManager.Instance.GetContentPromtDialog();
                    while (string.IsNullOrEmpty(comment) && !isCanceled)
                    {
                    }
                });
                t.Wait();
                isCanceled = UIRegistryManager.Instance.GetIsCanceledPromtDialog();
                comment = UIRegistryManager.Instance.GetContentPromtDialog();
            }

            if (!isCanceled || !shouldCommentWindowShow)
            { 
                Task t1 = Task.Factory.StartNew(() =>
                {
                    this.TestCaseDetailedViewModel.TestCase.SetNewExecutionOutcome(ExecutionContext.Preferences.TestPlan, testCaseExecutionType, comment, ExecutionContext.TestCaseRuns);
                    this.TestCaseDetailedViewModel.TestCase.LastExecutionOutcome = testCaseExecutionType;
                });
                t1.ContinueWith(antecedent =>
                {
                    Navigator.Instance.NavigateToTestCasesInitialView(this);
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