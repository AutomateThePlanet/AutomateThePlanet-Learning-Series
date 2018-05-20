// <copyright file="TestCasesInitialView.xaml.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using AAngelov.Utilities.Enums;
using AAngelov.Utilities.UI.ControlExtensions;
using AAngelov.Utilities.UI.Managers;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestCaseManagerCore;
using TestCaseManagerCore.BusinessLogic.Entities;
using TestCaseManagerCore.BusinessLogic.Enums;
using TestCaseManagerCore.BusinessLogic.Managers;
using TestCaseManagerCore.ViewModels;
using FirstFloor.ModernUI.Presentation;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the test case initial(search mode) page
    /// </summary>
    public partial class TestCasesInitialView : System.Windows.Controls.UserControl, IContent
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
        /// The preview command
        /// </summary>
        public static RoutedCommand PreviewCommand = new RoutedCommand();

        /// <summary>
        /// The new command
        /// </summary>
        public static RoutedCommand NewCommand = new RoutedCommand();

        /// <summary>
        /// The refresh command
        /// </summary>
        public static RoutedCommand RemoveCommand = new RoutedCommand();

        /// <summary>
        /// The remove test case from suite command
        /// </summary>
        public static RoutedCommand RemoveTestCaseFromSuiteCommand = new RoutedCommand();

        /// <summary>
        /// The rename suite command
        /// </summary>
        public static RoutedCommand RenameSuiteCommand = new RoutedCommand();

        /// <summary>
        /// The add suite command
        /// </summary>
        public static RoutedCommand AddSuiteCommand = new RoutedCommand();

        /// <summary>
        /// The remove suite command
        /// </summary>
        public static RoutedCommand RemoveSuiteCommand = new RoutedCommand();

        /// <summary>
        /// The copy suite command
        /// </summary>
        public static RoutedCommand CopySuiteCommand = new RoutedCommand();

        /// <summary>
        /// The cut suite command
        /// </summary>
        public static RoutedCommand CutSuiteCommand = new RoutedCommand();

        /// <summary>
        /// The paste suite command
        /// </summary>
        public static RoutedCommand PasteSuiteCommand = new RoutedCommand();

        /// <summary>
        /// The copy test cases command
        /// </summary>
        public static RoutedCommand CopyTestCasesCommand = new RoutedCommand();

        /// <summary>
        /// The cut test cases command
        /// </summary>
        public static RoutedCommand CutTestCasesCommand = new RoutedCommand();

        /// <summary>
        /// The paste test cases command
        /// </summary>
        public static RoutedCommand PasteTestCasesCommand = new RoutedCommand();

        /// <summary>
        /// The export test cases command
        /// </summary>
        public static RoutedCommand ExportTestCasesCommand = new RoutedCommand();

        /// <summary>
        /// The set active test cases command
        /// </summary>
        public static RoutedCommand SetActiveTestCasesCommand = new RoutedCommand();

        /// <summary>
        /// The set passed test cases command
        /// </summary>
        public static RoutedCommand SetPassedTestCasesCommand = new RoutedCommand();

        /// <summary>
        /// The set failed test cases command
        /// </summary>
        public static RoutedCommand SetFailedTestCasesCommand = new RoutedCommand();

        /// <summary>
        /// The set blocked test cases command
        /// </summary>
        public static RoutedCommand SetBlockedTestCasesCommand = new RoutedCommand();

        /// <summary>
        /// The run test cases command
        /// </summary>
        public static RoutedCommand RunTestCasesCommand = new RoutedCommand();

        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// The is show test cases subsuite already unchecked
        /// </summary>
        private bool isShowTestCasesSubsuiteAlreadyUnchecked;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesInitialView"/> class.
        /// </summary>
        public TestCasesInitialView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the test cases initial view model.
        /// </summary>
        /// <value>
        /// The test cases initial view model.
        /// </value>
        public TestCasesInitialViewModel TestCasesInitialViewModel { get; set; }

        /// <summary>
        /// Gets the search command.
        /// </summary>
        /// <value>
        /// The search command.
        /// </value>
        public ICommand SearchCommand { get; private set; }

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
        /// Handles the Loaded event of the TestCaseInitialView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void TestCaseInitialView_Loaded(object sender, RoutedEventArgs e)
        {
            if (isInitialized)
            {
                return;
            }
            this.ShowProgressBar();
            this.InitializeFastKeys();
            Task t = Task.Factory.StartNew(() =>
            {
                if (this.TestCasesInitialViewModel != null)
                {
                    this.TestCasesInitialViewModel = new TestCaseManagerCore.ViewModels.TestCasesInitialViewModel(this.TestCasesInitialViewModel);
                }
                else
                {
                    this.TestCasesInitialViewModel = new TestCaseManagerCore.ViewModels.TestCasesInitialViewModel();
                }
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = this.TestCasesInitialViewModel;
                this.UpdateButtonsStatus();
                this.HideProgressBar();
                this.tbTitleFilter.Focus();
                isInitialized = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Initializes the fast keys.
        /// </summary>
        private void InitializeFastKeys()
        {
            EditCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));
            DuplicateCommand.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Alt));
            PreviewCommand.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Alt));
            NewCommand.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Alt));
            RemoveCommand.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Alt));
            ExportTestCasesCommand.InputGestures.Add(new KeyGesture(Key.X, ModifierKeys.Alt));
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
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            this.PreviewSelectedTestCase();
        }

        /// <summary>
        /// Previews the selected test case.
        /// </summary>
        private void PreviewSelectedTestCase()
        {
            this.ValidateTestCaseSelection(() =>
            {
                TestCase currentTestCase = this.dgTestCases.SelectedItem as TestCase;
                if (currentTestCase.ITestSuiteBase != null)
                {
                    log.InfoFormat("Preview test case with id= \"{0}\" and suiteId= \"{1}\"", currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                    Navigator.Instance.NavigateToTestCasesDetailedView(this, currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                }
                else
                {
                    log.InfoFormat("Preview test case with id= \"{0}\" and suiteId= \"{1}\"", currentTestCase.ITestCase.Id, -1);
                    Navigator.Instance.NavigateToTestCasesDetailedView(this, currentTestCase.ITestCase.Id, -1);
                }
            });
        }

        /// <summary>
        /// Validates the test case selection.
        /// </summary>
        /// <param name="action">The action.</param>
        private void ValidateTestCaseSelection(Action action)
        {
            if (this.dgTestCases.SelectedIndex != -1)
            {
                action.Invoke();
            }
            else
            {
                this.DisplayNonSelectionWarning();
            }
        }

        /// <summary>
        /// Displays the non selection warning.
        /// </summary>
        private void DisplayNonSelectionWarning()
        {
            ModernDialog.ShowMessage("No selected test case.", "Warning", MessageBoxButton.OK);
        }

        /// <summary>
        /// Handles the Click event of the EditButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.ValidateTestCaseSelection(() =>
            {
                TestCase currentTestCase = this.dgTestCases.SelectedItem as TestCase;
                if (currentTestCase.ITestSuiteBase != null)
                {
                    log.InfoFormat("Preview test case with id= \"{0}\" and suiteId= \"{1}\"", currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                    Navigator.Instance.NavigateToTestCasesEditView(this, currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                }
                else
                {
                    log.InfoFormat("Preview test case with id= \"{0}\" and suiteId= \"{1}\"", currentTestCase.ITestCase.Id, -1);
                    Navigator.Instance.NavigateToTestCasesEditView(this, currentTestCase.ITestCase.Id, -1);
                }
            });
        }

        /// <summary>
        /// Handles the Click event of the DuplicateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DuplicateButton_Click(object sender, RoutedEventArgs e)
        {
            this.ValidateTestCaseSelection(() =>
            {
                TestCase currentTestCase = this.dgTestCases.SelectedItem as TestCase;
                if (currentTestCase.ITestSuiteBase != null)
                {
                    log.InfoFormat("Preview test case with id= \"{0}\" and suiteId= \"{1}\"", currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                    Navigator.Instance.NavigateToTestCasesEditView(this, currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id, true, true);
                }
                else
                {
                    log.InfoFormat("Preview test case with id= \"{0}\" and suiteId= \"{1}\"", currentTestCase.ITestCase.Id, -1);
                    Navigator.Instance.NavigateToTestCasesEditView(this, currentTestCase.ITestCase.Id, -1, true, true);
                }
            });
        }

        /// <summary>
        /// Handles the Click event of the btnNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            int selectedSuiteId = RegistryManager.Instance.GetSelectedSuiteId();
            log.InfoFormat("Navigate to Create New Test Case, steiId= \"{0}\"", selectedSuiteId);
            Navigator.Instance.NavigateToTestCasesEditView(this, selectedSuiteId, true, false);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbIdFilter.ClearDefaultContent(ref this.TestCasesInitialViewModel.InitialViewFilters.IsIdTextSet);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbTitleFilter.ClearDefaultContent(ref this.TestCasesInitialViewModel.InitialViewFilters.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbTextSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTextSuiteFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            //tbSuiteFilter.ClearDefaultContent(ref TestCasesInitialViewModel.InitialViewFilters.IsSuiteTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbIdFilter.RestoreDefaultText(this.TestCasesInitialViewModel.InitialViewFilters.DetaultId, ref this.TestCasesInitialViewModel.InitialViewFilters.IsIdTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbTitleFilter.RestoreDefaultText(this.TestCasesInitialViewModel.InitialViewFilters.DetaultTitle, ref this.TestCasesInitialViewModel.InitialViewFilters.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbSuiteFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            //tbSuiteFilter.RestoreDefaultText(this.TestCasesInitialViewModel.InitialViewFilters.DetaultSuite, ref this.TestCasesInitialViewModel.InitialViewFilters.IsSuiteTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the KeyUp event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the KeyUp event of the tbSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbSuiteFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the GotFocus event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbAssignedToFilter.ClearDefaultContent(ref this.TestCasesInitialViewModel.InitialViewFilters.IsAssignedToTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbAssignedToFilter.RestoreDefaultText(this.TestCasesInitialViewModel.InitialViewFilters.DetaultAssignedTo, ref this.TestCasesInitialViewModel.InitialViewFilters.IsAssignedToTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the GotFocus event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbPriorityFilter.ClearDefaultContent(ref this.TestCasesInitialViewModel.InitialViewFilters.IsPriorityTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbPriorityFilter.RestoreDefaultText(this.TestCasesInitialViewModel.InitialViewFilters.DetaultPriority, ref this.TestCasesInitialViewModel.InitialViewFilters.IsPriorityTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.dgTestCases.SelectedItem == null)
            {
                return;
            }
            if (System.Windows.Forms.Control.ModifierKeys == Keys.Alt)
            {
                TestCase currentTestCase = this.dgTestCases.SelectedItem as TestCase;
                if (currentTestCase.ITestSuiteBase != null)
                {
                    log.InfoFormat("Edit test case with id: {0} and suite id {1}", currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                    Navigator.Instance.NavigateToTestCasesEditView(this, currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                }
                else
                {
                    log.InfoFormat("Edit test case with id: {0} and suite id {1}", currentTestCase.ITestCase.Id, -1);
                    Navigator.Instance.NavigateToTestCasesEditView(this, currentTestCase.ITestCase.Id, -1);
                }
            }
            else
            {
                this.PreviewSelectedTestCase();
            }
        }

        /// <summary>
        /// Handles the Selected event of the treeViewItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void treeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            this.isShowTestCasesSubsuiteAlreadyUnchecked = false;
            e.Handled = true;
            int selectedSuiteId = (int)this.tvSuites.SelectedValue;         

            this.btnArrange.IsEnabled = true;
            //this.btnArrange1.Visibility = System.Windows.Visibility.Visible;
            if (selectedSuiteId == -1 || !TestSuiteManager.IsStaticSuite(ExecutionContext.TestManagementTeamProject, selectedSuiteId))
            {
                this.btnArrange.IsEnabled = false;
                //this.btnArrange1.Visibility = System.Windows.Visibility.Hidden;
            }
            if (selectedSuiteId.Equals(-1) && this.TestCasesInitialViewModel.IsThereSubnodeSelected(this.TestCasesInitialViewModel.Suites))
            {
                return;
            }

            // Remove the initial view filters because we are currently filtering by suite and the old filters are not valid any more
            this.TestCasesInitialViewModel.ResetInitialFilters();
            RegistryManager.Instance.WriteSelectedSuiteId(selectedSuiteId);
            this.TestCasesInitialViewModel.TestCasesCount = "...";
            this.ShowTestCasesProgressbar();
            
            this.ShowAllExecutionStatusContextMenuItemsStatuses();
            this.spExecutionStatuses.Visibility = System.Windows.Visibility.Visible;
            this.InitializeTestCasesBySelectedSuiteIdInternal(selectedSuiteId);
            this.isShowTestCasesSubsuiteAlreadyUnchecked = true;
        }

        /// <summary>
        /// Initializes the test cases by selected suite unique identifier internal.
        /// </summary>
        /// <param name="selectedSuiteId">The selected suite unique identifier.</param>
        private void InitializeTestCasesBySelectedSuiteIdInternal(int selectedSuiteId)
        {
            List<TestCase> suiteTestCaseCollection = new List<TestCase>();
            bool shouldHideMenuItems = false;
            Task t = Task.Factory.StartNew(() =>
            {
                if (selectedSuiteId != -1)
                {
                    suiteTestCaseCollection = TestCaseManager.GetAllTestCaseFromSuite(ExecutionContext.Preferences.TestPlan, selectedSuiteId);
                    this.TestCasesInitialViewModel.AddTestCasesSubsuites(suiteTestCaseCollection);
                }
                else if (isInitialized)
                {
                    suiteTestCaseCollection = TestCaseManager.GetAllTestCasesInTestPlan(ExecutionContext.TestManagementTeamProject, ExecutionContext.Preferences.TestPlan, false);
                    shouldHideMenuItems = true;
                    log.InfoFormat("Load all test cases in the test plan.");
                }
            });
            t.ContinueWith(antecedent =>
            {
                this.DetermineShowTestCasesWithoutSuiteVisiblityStatus(selectedSuiteId);
                if (shouldHideMenuItems)
                {
                    this.HideAllExecutionStatusContextMenuItemsStatuses();
                    this.TestCasesInitialViewModel.CurrentExecutionStatusOption = TestCaseExecutionType.All;
                    this.spExecutionStatuses.Visibility = System.Windows.Visibility.Hidden;
                }
                this.TestCasesInitialViewModel.InitializeInitialTestCaseCollection(suiteTestCaseCollection);
                this.TestCasesInitialViewModel.FilterTestCases();
                this.HideTestCasesProgressbar();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Determines the show test cases without suite visiblity status.
        /// </summary>
        /// <param name="selectedSuiteId">The selected suite id.</param>
        private void DetermineShowTestCasesWithoutSuiteVisiblityStatus(int selectedSuiteId)
        {
            if (selectedSuiteId == -1)
            {
                this.btnShowTestCaseWithoutSuite.Visibility = System.Windows.Visibility.Visible;
                //this.btnShowTestCaseWithoutSuite1.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.btnShowTestCaseWithoutSuite.Visibility = System.Windows.Visibility.Hidden;
                //this.btnShowTestCaseWithoutSuite1.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        /// <summary>
        /// Handles the Command event of the removeTestCaseFromSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void removeTestCaseFromSuite_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.RemoveTestCaseFromSuiteInternal();
        }

        /// <summary>
        /// Handles the Command event of the renameSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void renameSuite_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.RenameSuiteInternal();
            e.Handled = true;
        }

        /// <summary>
        /// Handles the Command event of the copySuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void copySuite_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            int selectedSuiteId = RegistryManager.Instance.GetSelectedSuiteId();
            Suite suite = this.TestCasesInitialViewModel.GetSuiteById(this.TestCasesInitialViewModel.Suites, selectedSuiteId);
            suite.CopyToClipboard(true);
        }

        /// <summary>
        /// Handles the Command event of the copyTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void copyTestCases_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            List<TestCase> testCases = this.GetSelectedTestCasesInternal();
            TestCaseManager.CopyToClipboardTestCases(true, testCases);
        }

        /// <summary>
        /// Handles the Command event of the cutTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void cutTestCases_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            List<TestCase> testCases = this.GetSelectedTestCasesInternal();
            TestCaseManager.CopyToClipboardTestCases(false, testCases);
        }

        /// <summary>
        /// Handles the Command event of the pasteTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void pasteTestCases_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            int selectedSuiteId = RegistryManager.Instance.GetSelectedSuiteId();
            Suite parentSuite = this.TestCasesInitialViewModel.GetSuiteById(this.TestCasesInitialViewModel.Suites, selectedSuiteId);
            ClipBoardTestCase clipBoardTestCase = TestCaseManager.GetFromClipboardTestCases();
            if (clipBoardTestCase != null)
            {
                this.PasteTestCasesToSuiteInternal(parentSuite, clipBoardTestCase);
            }
        }

        /// <summary>
        /// Gets the selected test cases internal.
        /// </summary>
        /// <returns>selected test cases list collection</returns>
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
        /// Handles the Command event of the cutSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void cutSuite_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            int selectedSuiteId = RegistryManager.Instance.GetSelectedSuiteId();
            Suite suite = this.TestCasesInitialViewModel.GetSuiteById(this.TestCasesInitialViewModel.Suites, selectedSuiteId);
            suite.CopyToClipboard(false);
        }

        /// <summary>
        /// Handles the Command event of the pasteSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void pasteSuite_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            int selectedSuiteId = RegistryManager.Instance.GetSelectedSuiteId();
            Suite suiteToPasteIn = this.TestCasesInitialViewModel.GetSuiteById(this.TestCasesInitialViewModel.Suites, selectedSuiteId);
            Suite clipboardSuite = Suite.GetFromClipboard();
            ClipBoardTestCase clipBoardTestCase = TestCaseManager.GetFromClipboardTestCases();

            if (clipboardSuite != null && suiteToPasteIn.Id.Equals(clipboardSuite.Id))
            {
                ModernDialog.ShowMessage("Cannot paste suite under itself!", "Warrning!", MessageBoxButton.OK);
                return;
            }

            if (clipboardSuite != null)
            {
                this.PasteSuiteToSelectedSuiteInternal(ExecutionContext.TestManagementTeamProject, ExecutionContext.Preferences.TestPlan, suiteToPasteIn, clipboardSuite);
            }
            else if (clipBoardTestCase != null)
            {
                this.PasteTestCasesToSuiteInternal(suiteToPasteIn, clipBoardTestCase);
            }
        }

        /// <summary>
        /// Pastes the suite automatic selected suite internal.
        /// </summary>
        /// <param name="testManagementTeamProject">The test management team project.</param>
        /// <param name="testPlan">The test plan.</param>
        /// <param name="suiteToPasteIn">The suite to paste in.</param>
        /// <param name="clipboardSuite">The clipboard suite.</param>
        private void PasteSuiteToSelectedSuiteInternal(ITestManagementTeamProject testManagementTeamProject, ITestPlan testPlan, Suite suiteToPasteIn, Suite clipboardSuite)
        {
            if (clipboardSuite.ClipBoardCommand.Equals(ClipBoardCommand.Copy))
            {
                this.TestCasesInitialViewModel.CopyPasteSuiteToParentSuite(testManagementTeamProject, testPlan, suiteToPasteIn, clipboardSuite);
            }
            else
            {
                this.TestCasesInitialViewModel.CutPasteSuiteToParentSuite(testManagementTeamProject, testPlan, suiteToPasteIn, clipboardSuite);
            }
        }

        /// <summary>
        /// Pastes the test cases automatic suite internal.
        /// </summary>
        /// <param name="suiteToPasteIn">The suite to paste in.</param>
        /// <param name="clipBoardTestCase">The clip board test case.</param>
        private void PasteTestCasesToSuiteInternal(Suite suiteToPasteIn, ClipBoardTestCase clipBoardTestCase)
        {
            this.ShowTestCasesProgressbar();
            Task t = Task.Factory.StartNew(() =>
            {
                TestSuiteManager.PasteTestCasesToSuite(ExecutionContext.TestManagementTeamProject, ExecutionContext.Preferences.TestPlan, suiteToPasteIn.Id, clipBoardTestCase);
            });
            t.ContinueWith(antecedent =>
            {
                if (clipBoardTestCase.ClipBoardCommand.Equals(ClipBoardCommand.Cut))
                {
                    System.Windows.Forms.Clipboard.Clear();
                }
                this.TestCasesInitialViewModel.AddTestCasesToObservableCollection(suiteToPasteIn, clipBoardTestCase.TestCases[0].TestSuiteId);
                this.HideTestCasesProgressbar();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Shows the test cases progressbar.
        /// </summary>
        private void ShowTestCasesProgressbar()
        {
            this.progressBarTestCases.Visibility = System.Windows.Visibility.Visible;
            this.dgTestCases.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Hides the test cases progressbar.
        /// </summary>
        private void HideTestCasesProgressbar()
        {
            this.progressBarTestCases.Visibility = System.Windows.Visibility.Hidden;
            this.dgTestCases.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Handles the Command event of the removeSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void removeSuite_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.RemoveSuiteInternal(ExecutionContext.TestManagementTeamProject, ExecutionContext.Preferences.TestPlan);
            e.Handled = true;
        }

        /// <summary>
        /// Removes the suite internal.
        /// </summary>
        /// <param name="testManagementTeamProject">The test management team project.</param>
        /// <param name="testPlan">The test plan.</param>
        private void RemoveSuiteInternal(ITestManagementTeamProject testManagementTeamProject, ITestPlan testPlan)
        {
            int selectedSuiteId = RegistryManager.Instance.GetSelectedSuiteId();
            try
            {
                if (ModernDialog.ShowMessage("If you delete this test suite, you will also delete all test suites that are children of this test suite!", "Delete this test suite?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    TestSuiteManager.DeleteSuite(testManagementTeamProject, testPlan, selectedSuiteId);
                    this.TestCasesInitialViewModel.DeleteSuiteObservableCollection(this.TestCasesInitialViewModel.Suites, selectedSuiteId);
                }
            }
            catch (ArgumentException)
            {
                ModernDialog.ShowMessage("The root suite cannot be deleted!", "Warrning!", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Handles the Command event of the addSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void addSuite_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.AddSuiteInternal();
            e.Handled = true;
        }

        /// <summary>
        /// Renames the suite internal.
        /// </summary>
        private void RenameSuiteInternal()
        {
            Suite selectedSuite = this.tvSuites.SelectedItem as Suite;
            UIRegistryManager.Instance.WriteTitleTitlePromtDialog(selectedSuite.Title);

            var dialog = new PrompDialogWindow();
            dialog.ShowDialog();
            bool isCanceled;
            string newTitle;
            Task t = Task.Factory.StartNew(() =>
            {
                isCanceled = UIRegistryManager.Instance.GetIsCanceledPromtDialog();
                newTitle = UIRegistryManager.Instance.GetContentPromtDialog();
                while (string.IsNullOrEmpty(newTitle) && !isCanceled)
                {
                }
            });
            t.Wait();
            isCanceled = UIRegistryManager.Instance.GetIsCanceledPromtDialog();
            newTitle = UIRegistryManager.Instance.GetContentPromtDialog();
            if (!isCanceled)
            {
                int selectedSuiteId = RegistryManager.Instance.GetSelectedSuiteId();
                if (selectedSuiteId == -1)
                {
                    ModernDialog.ShowMessage("Cannot rename root suite!", "Warrning!", MessageBoxButton.OK);
                    return;
                }
                TestSuiteManager.RenameSuite(ExecutionContext.TestManagementTeamProject, ExecutionContext.Preferences.TestPlan, selectedSuiteId, newTitle);
                this.TestCasesInitialViewModel.RenameSuiteInObservableCollection(this.TestCasesInitialViewModel.Suites, selectedSuiteId, newTitle);
            }
        }

        /// <summary>
        /// Adds the suite internal.
        /// </summary>
        private void AddSuiteInternal()
        {
            UIRegistryManager.Instance.WriteTitleTitlePromtDialog(string.Empty);

            var dialog = new PrompDialogWindow();
            dialog.ShowDialog();
            bool isCanceled;
            string newTitle;
            Task t = Task.Factory.StartNew(() =>
            {
                isCanceled = UIRegistryManager.Instance.GetIsCanceledPromtDialog();
                newTitle = UIRegistryManager.Instance.GetContentPromtDialog();
                while (string.IsNullOrEmpty(newTitle) && !isCanceled)
                {
                }
            });
            t.Wait();
            isCanceled = UIRegistryManager.Instance.GetIsCanceledPromtDialog();
            newTitle = UIRegistryManager.Instance.GetContentPromtDialog();
            if (!isCanceled)
            {
                int selectedSuiteId = RegistryManager.Instance.GetSelectedSuiteId();
                bool canBeAddedNewSuite = false;
                int? newSuiteId = TestSuiteManager.AddChildSuite(ExecutionContext.TestManagementTeamProject, ExecutionContext.Preferences.TestPlan, selectedSuiteId, newTitle, out canBeAddedNewSuite);
                if (canBeAddedNewSuite)
                {
                    this.TestCasesInitialViewModel.AddChildSuiteObservableCollection(this.TestCasesInitialViewModel.Suites, selectedSuiteId, (int)newSuiteId);
                }
                else
                {
                    ModernDialog.ShowMessage("Cannot add new suite to Requirments Suite!", "Warrning!", MessageBoxButton.OK);
                }
            }
        }

        /// <summary>
        /// Handles the PreviewMouseRightButtonDown event of the TreeViewItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TreeViewItem treeViewItem = this.VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }

            this.UpdateSuiteContextMenuItemsStatus();
        }

        /// <summary>
        /// Updates the suite context menu items status.
        /// </summary>
        private void UpdateSuiteContextMenuItemsStatus()
        {
            int selectedSuiteId = RegistryManager.Instance.GetSelectedSuiteId();
            Suite suite = this.TestCasesInitialViewModel.GetSuiteById(this.TestCasesInitialViewModel.Suites, selectedSuiteId);

            // Quit if its requirment based suite because no child suites are allowed
            if (suite.IsPasteAllowed)
            {
                suite.IsPasteEnabled = true;
                return;
            }

            Suite clipBoardItem = Suite.GetFromClipboard();
            if (clipBoardItem == null)
            {
                suite.IsPasteEnabled = false;
            }
        }

        /// <summary>
        /// Handles the PreviewMouseRightButtonDown event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.UpdateTestCasesContextMenuItemsStatus();
        }

        /// <summary>
        /// Updates the test cases context menu items status.
        /// </summary>
        private void UpdateTestCasesContextMenuItemsStatus()
        {
            int selectedSuiteId = RegistryManager.Instance.GetSelectedSuiteId();
            Suite suite = this.TestCasesInitialViewModel.GetSuiteById(this.TestCasesInitialViewModel.Suites, selectedSuiteId);

            // If the selected suite is requirement based the paste command is not enabled or if the clipboard object is not list of testcases
            this.dgTestCaseContextItemPaste.IsEnabled = true;
            ClipBoardTestCase clipBoardTestCase = TestCaseManager.GetFromClipboardTestCases();
            if (clipBoardTestCase == null || !suite.IsPasteAllowed)
            {
                this.dgTestCaseContextItemPaste.IsEnabled = false;
                suite.IsPasteEnabled = false;
            }

            if (dgTestCases.SelectedItems.Count == 1)
            {
                TestCase currentTestCase = (TestCase)dgTestCases.SelectedItem;
                if (ExecutionContext.TestCaseRuns.ContainsKey(currentTestCase.Id))
                {
                    TestCaseRun currenteTestCaseRun = ExecutionContext.TestCaseRuns[currentTestCase.Id];
                    if (currenteTestCaseRun.IsPaused)
                    {
                        this.TestCasesInitialViewModel.ExecutionButtonTitle = "Continue";
                    }
                    else
                    {
                        this.TestCasesInitialViewModel.ExecutionButtonTitle = "Pause";
                    }
                }
                else
                {
                    this.TestCasesInitialViewModel.ExecutionButtonTitle = "Run";
                }
            }
            else
            {
                this.TestCasesInitialViewModel.ExecutionButtonTitle = "Run";
            }
        }

        /// <summary>
        /// Visuals the upward search.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>parent tree view item</returns>
        private TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
            {
                source = VisualTreeHelper.GetParent(source);
            }

            return source as TreeViewItem;
        }

        /// <summary>
        /// Handles the Unchecked event of the cbHideAutomated control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void cbHideAutomated_Unchecked(object sender, RoutedEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the Checked event of the cbHideAutomated control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void cbHideAutomated_Checked(object sender, RoutedEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the KeyDown event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                this.PreviewSelectedTestCase();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRemoveTestCase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnRemoveTestCase_Click(object sender, RoutedEventArgs e)
        { 
            this.RemoveTestCaseFromSuiteInternal();
        }

        /// <summary>
        /// Removes the test case from suite internal.
        /// </summary>
        private void RemoveTestCaseFromSuiteInternal()
        {
            if (this.dgTestCases.SelectedItems.Count == 0)
            {
                this.DisplayNonSelectionWarning();
                return;
            }
            int selectedIndex = this.dgTestCases.SelectedIndex;
            do
            {
                TestCase currentTestCase = this.dgTestCases.SelectedItems[0] as TestCase;
                this.TestCasesInitialViewModel.RemoveTestCaseFromSuite(currentTestCase);
            }
            while (this.dgTestCases.SelectedItems.Count != 0);
            if (selectedIndex == this.dgTestCases.Items.Count)
            {
                this.dgTestCases.SelectedIndex = selectedIndex - 1;
            }
            else
            {
                this.dgTestCases.SelectedIndex = selectedIndex;
            }
            this.dgTestCases.Focus();
        }

        /// <summary>
        /// Handles the SelectedCellsChanged event of the dgTestCases control. Disable Preview and Duplicate buttons if more than one row is selected.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectedCellsChangedEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            this.UpdateButtonsStatus();
            this.TestCasesInitialViewModel.SelectedTestCasesCount = this.dgTestCases.SelectedItems.Count.ToString();
        }

        /// <summary>
        /// Updates the buttons status.
        /// </summary>
        private void UpdateButtonsStatus()
        {
            //this.btnPreview.IsEnabled = true;
            //this.btnDuplicate.IsEnabled = true;
            //this.btnEdit.IsEnabled = true;
            //this.btnPreview1.IsEnabled = true;
            //this.btnDuplicate1.IsEnabled = true;
            //this.btnChangeTestCases.IsEnabled = true;
            this.btnChangeTestCases1.IsEnabled = true;
            //this.btnEdit1.IsEnabled = true;
            this.btnExport.IsEnabled = true;
            //this.btnExport1.IsEnabled = true;
            //this.btnRemoveTestCase.IsEnabled = true;
            this.btnRemoveTestCase1.IsEnabled = true;
            this.dgTestCaseContextItemEdit.IsEnabled = true;
            this.dgTestCaseContextItemPreview.IsEnabled = true;
            this.dgTestCaseContextItemDuplicate.IsEnabled = true;
            this.dgTestCaseContextItemRun.IsEnabled = true;
            this.dgTestCaseContextItemCopy.IsEnabled = true;
            this.dgTestCaseContextItemCut.IsEnabled = true;
            this.dgTestCaseContextItemRemove.IsEnabled = true;
            if (this.dgTestCases.SelectedItems.Count < 1)
            {
                //this.btnPreview.IsEnabled = false;
                //this.btnDuplicate.IsEnabled = false;
                //this.btnEdit.IsEnabled = false;
                //this.btnPreview1.IsEnabled = false;
                //this.btnDuplicate1.IsEnabled = false;
                //this.btnEdit1.IsEnabled = false;
                //this.btnRemoveTestCase.IsEnabled = false;
                this.btnRemoveTestCase1.IsEnabled = false;             
                this.dgTestCaseContextItemEdit.IsEnabled = false;
                this.dgTestCaseContextItemPreview.IsEnabled = false;
                this.dgTestCaseContextItemDuplicate.IsEnabled = false;
                this.dgTestCaseContextItemRun.IsEnabled = false;
                //this.btnChangeTestCases.IsEnabled = false;
                this.btnChangeTestCases1.IsEnabled = false;

                this.dgTestCaseContextItemCopy.IsEnabled = false;
                this.dgTestCaseContextItemCut.IsEnabled = false;
                this.dgTestCaseContextItemRemove.IsEnabled = false;
                this.btnExport.IsEnabled = false;
                //this.btnExport1.IsEnabled = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgTestCases.SelectedItems.Count == 0)
            {
                this.DisplayNonSelectionWarning();
            }
            var dialog = new PrompCheckboxListDialogWindow();
            dialog.ShowDialog();
            bool isCanceled;
            bool isSubmitted;
            Task t = Task.Factory.StartNew(() =>
            {
                isCanceled = UIRegistryManager.Instance.GetIsCanceledPromtDialog();
                isSubmitted = UIRegistryManager.Instance.ReadIsCheckboxDialogSubmitted();
                while (!isSubmitted && !isCanceled)
                {
                }
            });
            t.Wait();
            isCanceled = UIRegistryManager.Instance.GetIsCanceledPromtDialog();
            if (!isCanceled)
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

                // Set filter for file extension and default file extension 
                dlg.DefaultExt = ".html";
                dlg.Filter = "Html Files (*.html)|*.html";

                // Display OpenFileDialog by calling ShowDialog method 
                bool? result = dlg.ShowDialog();

                // Get the selected file name and display in a TextBox 
                if (result == true)
                {
                    // Open document 
                    string filename = dlg.FileName;
                    this.ShowTestCasesProgressbar();
                    List<TestCase> selectedTestCases = this.GetSelectedTestCasesInternal();
                    Task t1 = Task.Factory.StartNew(() =>
                    {
                        this.TestCasesInitialViewModel.ExportTestCases(filename, selectedTestCases);
                    });
                    t1.ContinueWith(antecedent =>
                    {
                        this.HideTestCasesProgressbar();
                        ModernDialog.ShowMessage("Test Cases Exported!", "Success!", MessageBoxButton.OK);
                        System.Diagnostics.Process.Start(filename);
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
        }

        /// <summary>
        /// Handles the Checked event of the RadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
            this.UpdateExecutionStatusContectMenuItemsStatuses();
        }

        /// <summary>
        /// Updates the execution status contect menu items statuses.
        /// </summary>
        private void UpdateExecutionStatusContectMenuItemsStatuses()
        {
            this.ShowAllExecutionStatusContextMenuItemsStatuses();
            switch (this.TestCasesInitialViewModel.CurrentExecutionStatusOption)
            {
                case TestCaseExecutionType.Active:
                    this.dgTestCaseContextItemActive.IsEnabled = false;
                    this.dgTestCaseContextItemCopy.IsEnabled = false;
                    this.dgTestCaseContextItemCut.IsEnabled = false;
                    this.dgTestCaseContextItemPaste.IsEnabled = false;
                    this.dgTestCaseContextItemRemove.IsEnabled = false;
                    break;
                case TestCaseExecutionType.Passed:
                    this.dgTestCaseContextItemPass.IsEnabled = false;
                    this.dgTestCaseContextItemCopy.IsEnabled = false;
                    this.dgTestCaseContextItemCut.IsEnabled = false;
                    this.dgTestCaseContextItemPaste.IsEnabled = false;
                    this.dgTestCaseContextItemRemove.IsEnabled = false;
                    break;
                case TestCaseExecutionType.Failed:
                    this.dgTestCaseContextItemFail.IsEnabled = false;
                    this.dgTestCaseContextItemCopy.IsEnabled = false;
                    this.dgTestCaseContextItemCut.IsEnabled = false;
                    this.dgTestCaseContextItemPaste.IsEnabled = false;
                    this.dgTestCaseContextItemRemove.IsEnabled = false;
                    break;
                case TestCaseExecutionType.Blocked:
                    this.dgTestCaseContextItemBlock.IsEnabled = false;
                    this.dgTestCaseContextItemCopy.IsEnabled = false;
                    this.dgTestCaseContextItemCut.IsEnabled = false;
                    this.dgTestCaseContextItemPaste.IsEnabled = false;
                    this.dgTestCaseContextItemRemove.IsEnabled = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Shows all execution status context menu items statuses.
        /// </summary>
        private void ShowAllExecutionStatusContextMenuItemsStatuses()
        {
            this.dgTestCaseContextItemPass.IsEnabled = true;
            this.dgTestCaseContextItemBlock.IsEnabled = true;
            this.dgTestCaseContextItemFail.IsEnabled = true;
            this.dgTestCaseContextItemActive.IsEnabled = true;
            this.dgTestCaseContextItemCopy.IsEnabled = true;
            this.dgTestCaseContextItemCut.IsEnabled = true;
            this.dgTestCaseContextItemPaste.IsEnabled = true;
            this.dgTestCaseContextItemRemove.IsEnabled = true;
        }

        /// <summary>
        /// Hides all execution status context menu items statuses.
        /// </summary>
        private void HideAllExecutionStatusContextMenuItemsStatuses()
        {
            this.dgTestCaseContextItemPass.IsEnabled = false;
            this.dgTestCaseContextItemBlock.IsEnabled = false;
            this.dgTestCaseContextItemFail.IsEnabled = false;
            this.dgTestCaseContextItemActive.IsEnabled = false;
            this.dgTestCaseContextItemCopy.IsEnabled = false;
            this.dgTestCaseContextItemCut.IsEnabled = false;
            this.dgTestCaseContextItemPaste.IsEnabled = false;
            this.dgTestCaseContextItemRemove.IsEnabled = false;
        }

        /// <summary>
        /// Handles the Command event of the setPassed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void setPassed_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            this.SetNewExecutionOutcomeInternal(TestCaseExecutionType.Passed);
        }

        /// <summary>
        /// Handles the Command event of the runTestCase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void runTestCase_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            List<TestCase> selectedTestCases = this.GetSelectedTestCasesInternal();
            this.TestCasesInitialViewModel.RunSelectedTestCases(selectedTestCases);
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
                this.ShowTestCasesProgressbar();
                List<TestCase> selectedTestCases = this.GetSelectedTestCasesInternal();
                Task t1 = Task.Factory.StartNew(() =>
                {
                    foreach (var currentTestCase in selectedTestCases)
                    {
                        currentTestCase.SetNewExecutionOutcome(ExecutionContext.Preferences.TestPlan, testCaseExecutionType, comment, ExecutionContext.TestCaseRuns);
                        currentTestCase.LastExecutionOutcome = testCaseExecutionType;
                    }
                });
                t1.ContinueWith(antecedent =>
                {
                    this.HideTestCasesProgressbar();
                    this.TestCasesInitialViewModel.FilterTestCases();
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        /// <summary>
        /// Handles the Command event of the setActive control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void setActive_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            this.SetNewExecutionOutcomeInternal(TestCaseExecutionType.Active);
        }

        /// <summary>
        /// Handles the Command event of the setFailed control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void setFailed_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            this.SetNewExecutionOutcomeInternal(TestCaseExecutionType.Failed);
        }

        /// <summary>
        /// Handles the Command event of the setBlocked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void setBlocked_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            this.SetNewExecutionOutcomeInternal(TestCaseExecutionType.Blocked);
        }

        /// <summary>
        /// Handles the KeyDown event of the tvSuites control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tvSuites_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            char keyPressedChar = (char)e.Key;
            char f2Char = (char)91;
            if ((System.Windows.Forms.Control.ModifierKeys == Keys.None) && ((keyPressedChar & f2Char) != 0))
            {
                this.RenameSuiteInternal();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnArrange control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnArrange_Click(object sender, RoutedEventArgs e)
        {
            int selectedSuiteId = (int)this.tvSuites.SelectedValue;
            if (selectedSuiteId != -1)
            {
                Navigator.Instance.NavigateToTestCasesExecutionArrangement(this, selectedSuiteId);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnChangeTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnChangeTestCases_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgTestCases.SelectedItems.Count == 0)
            {
                ModernDialog.ShowMessage("No test cases selected.", "Warning", MessageBoxButton.OK);
            }
            else
            {
                ExecutionContext.SelectedTestCasesForChange = new List<TestCase>();
                foreach (TestCase currentTestCase in this.dgTestCases.SelectedItems)
                {
                    ExecutionContext.SelectedTestCasesForChange.Add(currentTestCase);
                }
                log.Info("Navigate to TestCaseBatchDuplicateView initialized with selected test cases.");
                Navigator.Instance.NavigateToTestCaseBatchDuplicateView(this, true, true);
            }
        }

        /// <summary>
        /// Handles the Unchecked event of the cbShowSubsuiteTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void cbShowSubsuiteTestCases_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.isShowTestCasesSubsuiteAlreadyUnchecked)
            {
                this.ShowTestCasesProgressbar();
                int selectedSuiteId = RegistryManager.Instance.GetSelectedSuiteId();
                this.InitializeTestCasesBySelectedSuiteIdInternal(selectedSuiteId);
                RegistryManager.Instance.WriteShowSubsuiteTestCases(false);
            }
        }

        /// <summary>
        /// Handles the Checked event of the cbShowSubsuiteTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void cbShowSubsuiteTestCases_Checked(object sender, RoutedEventArgs e)
        {
            int selectedSuiteId = RegistryManager.Instance.GetSelectedSuiteId();
            if (selectedSuiteId == -1)
            {
                return;
            }
            RegistryManager.Instance.WriteShowSubsuiteTestCases(true);
            this.ShowTestCasesProgressbar();
            List<TestCase> testCasesList = new List<TestCase>();
            Task t = Task.Factory.StartNew(() =>
            {
                ITestSuiteBase currentSuite = TestSuiteManager.GetTestSuiteById(ExecutionContext.TestManagementTeamProject, ExecutionContext.Preferences.TestPlan, selectedSuiteId);
                if (currentSuite is IStaticTestSuite)
                {
                    testCasesList = TestCaseManager.GetAllTestCasesFromSuiteCollection(ExecutionContext.Preferences.TestPlan, (currentSuite as IStaticTestSuite).SubSuites);
                }
            });
            t.ContinueWith(antecedent =>
            {
                testCasesList.ForEach(x => this.TestCasesInitialViewModel.ObservableTestCases.Add(x));
                testCasesList.ForEach(x => this.TestCasesInitialViewModel.InitialTestCaseCollection.Add(x));
                this.TestCasesInitialViewModel.FilterTestCases();
                this.HideTestCasesProgressbar();
            }, TaskScheduler.FromCurrentSynchronizationContext());                    
        }

        /// <summary>
        /// Handles the Click event of the btnShowTestCaseWithoutSuite1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnShowTestCaseWithoutSuite1_Click(object sender, RoutedEventArgs e)
        {
            this.ShowProgressBar();
            Task t = Task.Factory.StartNew(() =>
            {
                this.TestCasesInitialViewModel.FilterSuitesWithoutSuite();
            });
            t.ContinueWith(antecedent =>
            {
                this.TestCasesInitialViewModel.FilterTestCases();	
                this.HideProgressBar();
                ModernDialog.ShowMessage("In order to prevent performance issues, you can cut the test cases without suite in new suite- \"TestCaseWithoutSuite\" and then start working with them!", "Warning", MessageBoxButton.OK);
            }, TaskScheduler.FromCurrentSynchronizationContext());			
        }

        /// <summary>
        /// Handles the Click event of the brnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void bthAdvancedSearchSearch_Click(object sender, RoutedEventArgs e)
        {
            this.TestCasesInitialViewModel.FilterTestCases();
        }

        /// <summary>
        /// Handles the GotFocus event of the tbAdvancedSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAdvancedSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbAdvancedSearch.ClearDefaultContent(ref this.TestCasesInitialViewModel.InitialViewFilters.IsAdvancedSearchTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbAdvancedSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAdvancedSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbAdvancedSearch.RestoreDefaultText(this.TestCasesInitialViewModel.InitialViewFilters.DetaultAdvancedSearch, ref this.TestCasesInitialViewModel.InitialViewFilters.IsAdvancedSearchTextSet);
        }

        /// <summary>
        /// Handles the Click event of the btnInfo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://testcasemanager.codeplex.com/wikipage?title=All%20Test%20Cases%20View&version=4");
        }

        /// <summary>
        /// Handles the Click event of the btnRunStatistics control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnRunStatistics_Click(object sender, RoutedEventArgs e)
        {
            ExecutionContext.SelectedTestCasesForChange = new List<TestCase>();
            foreach (TestCase currentTestCase in this.dgTestCases.SelectedItems)
            {
                ExecutionContext.SelectedTestCasesForChange.Add(currentTestCase);
            }
            log.Info("Navigate to TestCaseRunStatisticsView.");
            Navigator.Instance.NavigateToTestCaseRunStatisticsView(this);
        }

        private void btnShowSubsuites_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCollapseAll_Click(object sender, RoutedEventArgs e)
        {
            this.TestCasesInitialViewModel.CollapseSuites(this.TestCasesInitialViewModel.Suites);
        }
    }
}