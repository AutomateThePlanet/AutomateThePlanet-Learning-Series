// <copyright file="SharedStepsInitialView.xaml.cs" company="Automate The Planet Ltd.">
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
using System.Windows.Input;
using AAngelov.Utilities.UI.ControlExtensions;
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
    /// Contains logic related to the test case initial(search mode) page
    /// </summary>
    public partial class SharedStepsInitialView : System.Windows.Controls.UserControl, IContent
    {
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
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCasesInitialView"/> class.
        /// </summary>
        public SharedStepsInitialView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the shared steps initial view model.
        /// </summary>
        /// <value>
        /// The shared steps initial view model.
        /// </value>
        public SharedStepsInitialViewModel SharedStepsInitialViewModel { get; set; }

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
                if (this.SharedStepsInitialViewModel != null)
                {
                    this.SharedStepsInitialViewModel = new TestCaseManagerCore.ViewModels.SharedStepsInitialViewModel(this.SharedStepsInitialViewModel);
                }
                else
                {
                    this.SharedStepsInitialViewModel = new TestCaseManagerCore.ViewModels.SharedStepsInitialViewModel();
                }
            });
            t.ContinueWith(antecedent =>
            {
                this.SharedStepsInitialViewModel.FilterSharedSteps();
                this.DataContext = this.SharedStepsInitialViewModel;
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
        /// Validates the shared steps selection.
        /// </summary>
        /// <param name="action">The action.</param>
        private void ValidateSharedStepsSelection(Action action)
        {
            if (this.dgSharedSteps.SelectedIndex != -1)
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
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.ValidateSharedStepsSelection(() =>
            {
                SharedStep currentSharedStep = this.dgSharedSteps.SelectedItem as SharedStep;
                log.InfoFormat("Edit Shared Step with id: {0} ", currentSharedStep.ISharedStep.Id);
                Navigator.Instance.NavigateToTestCasesEditView(this, true, currentSharedStep.ISharedStep.Id);
            });
        }

        /// <summary>
        /// Handles the Click event of the DuplicateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnDuplicate_Click(object sender, RoutedEventArgs e)
        {
            this.ValidateSharedStepsSelection(() =>
            {
                SharedStep currentSharedStep = this.dgSharedSteps.SelectedItem as SharedStep;
                log.InfoFormat("Duplicate Shared Step with id: {0} ", currentSharedStep.ISharedStep.Id);
                Navigator.Instance.NavigateToTestCasesEditView(this, true, currentSharedStep.ISharedStep.Id, true, true);
            });
        }

        /// <summary>
        /// Handles the Click event of the btnNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            log.Info("Navigate to Create New Shared Step");
            Navigator.Instance.NavigateToTestCasesEditView(this, true, true, false);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbIdFilter.ClearDefaultContent(ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsIdTextSet);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbTitleFilter.ClearDefaultContent(ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbIdFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.DetaultId, ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsIdTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbTitleFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.DetaultTitle, ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.SharedStepsInitialViewModel.FilterSharedSteps();
        }

        /// <summary>
        /// Handles the KeyUp event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.SharedStepsInitialViewModel.FilterSharedSteps();
        }

        /// <summary>
        /// Handles the GotFocus event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbAssignedToFilter.ClearDefaultContent(ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsAssignedToTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbAssignedToFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.DetaultAssignedTo, ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsAssignedToTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.SharedStepsInitialViewModel.FilterSharedSteps();
        }

        /// <summary>
        /// Handles the GotFocus event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbPriorityFilter.ClearDefaultContent(ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsPriorityTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbPriorityFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.DetaultPriority, ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsPriorityTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.SharedStepsInitialViewModel.FilterSharedSteps();
        }

        /// <summary>
        /// Handles the GotFocus event of the tbTestCaseTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTestCaseTitleFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbTestCaseTitleFilter.ClearDefaultContent(ref this.SharedStepsInitialViewModel.InitialViewFiltersTestCases.IsTitleTextSet);           
        }

        /// <summary>
        /// Handles the LostFocus event of the tbTestCaseTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTestCaseTitleFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbTestCaseTitleFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFiltersTestCases.DetaultTitle, ref this.SharedStepsInitialViewModel.InitialViewFiltersTestCases.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbTestCaseTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbTestCaseTitleFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            List<TestCase> filteredList = this.SharedStepsInitialViewModel.FilterTestCases();
            if (filteredList != null)
            {
                this.SharedStepsInitialViewModel.ObservableTestCases.Clear();
                filteredList.ForEach(x => this.SharedStepsInitialViewModel.ObservableTestCases.Add(x));
            }
        }

        /// <summary>
        /// Handles the GotFocus event of the tbTestCaseSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTestCaseSuiteFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbTestCaseSuiteFilter.ClearDefaultContent(ref this.SharedStepsInitialViewModel.InitialViewFiltersTestCases.IsSuiteTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbTestCaseSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTestCaseSuiteFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbTestCaseSuiteFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFiltersTestCases.DetaultSuite, ref this.SharedStepsInitialViewModel.InitialViewFiltersTestCases.IsSuiteTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbTestCaseSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbTestCaseSuiteFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            List<TestCase> filteredList = this.SharedStepsInitialViewModel.FilterTestCases();
            if (filteredList != null)
            {
                this.SharedStepsInitialViewModel.ObservableTestCases.Clear();
                filteredList.ForEach(x => this.SharedStepsInitialViewModel.ObservableTestCases.Add(x));
            }
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgSharedSteps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.EditTestCaseInternal();      
        }

        /// <summary>
        /// Edits the test case internal.
        /// </summary>
        private void EditTestCaseInternal()
        {
            if (this.dgSharedSteps.SelectedItem != null)
            {
                SharedStep currentSharedStep = this.dgSharedSteps.SelectedItem as SharedStep;
                log.InfoFormat("Edit Shared Step with id: {0} ", currentSharedStep.ISharedStep.Id);
                Navigator.Instance.NavigateToTestCasesEditView(this, true, currentSharedStep.ISharedStep.Id);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void dgSharedSteps_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                SharedStep currentSharedStep = this.dgSharedSteps.SelectedItem as SharedStep;
                Navigator.Instance.NavigateToTestCasesEditView(this, true, currentSharedStep.ISharedStep.Id);
            }
        }

        /// <summary>
        /// Handles the SelectedCellsChanged event of the dgTestCases control. Disable Preview and Duplicate buttons if more than one row is selected.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectedCellsChangedEventArgs"/> instance containing the event data.</param>
        private void dgSharedSteps_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            this.UpdateButtonsStatus();
        }

        /// <summary>
        /// Updates the buttons status.
        /// </summary>
        private void UpdateButtonsStatus()
        {
            //this.btnDuplicate.IsEnabled = true;
            //this.btnEdit.IsEnabled = true;
            this.btnDuplicate1.IsEnabled = true;
            this.btnEdit1.IsEnabled = true;
            //this.btnFindReferences.IsEnabled = true;
            this.btnFindReferences1.IsEnabled = true;
            this.dgSharedStepsContextItemEdit.IsEnabled = true;
            this.dgSharedStepsContextItemPreview.IsEnabled = true;
            this.dgSharedStepsContextItemDuplicate.IsEnabled = true;
            if (this.dgSharedSteps.SelectedItems.Count < 1)
            {
                //this.btnDuplicate.IsEnabled = false;
                //this.btnEdit.IsEnabled = false;
                this.btnDuplicate1.IsEnabled = false;
                this.btnEdit1.IsEnabled = false;
                this.dgSharedStepsContextItemEdit.IsEnabled = false;
                this.dgSharedStepsContextItemPreview.IsEnabled = false;
                this.dgSharedStepsContextItemDuplicate.IsEnabled = false;
                //this.btnFindReferences.IsEnabled = false;
                this.btnFindReferences1.IsEnabled = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnFindReferences control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnFindReferences_Click(object sender, RoutedEventArgs e)
        {
            if (this.SharedStepsInitialViewModel.SelectedSharedStep == null)
            {
                ModernDialog.ShowMessage("No shared step selected.", "Warning", MessageBoxButton.OK);
                return;
            }
            this.progressBar.Visibility = System.Windows.Visibility.Visible;
            this.mainGrid.Visibility = System.Windows.Visibility.Hidden;
            this.SharedStepsInitialViewModel.ObservableTestCases.Clear();
            List<TestCase> filteredTestCases = new List<TestCase>();
            Task t = Task.Factory.StartNew(() =>
            {
                log.InfoFormat("Find all reference Test Cases for Shared Step with id: {0} ", this.SharedStepsInitialViewModel.SelectedSharedStep.Id);
                filteredTestCases = TestCaseManager.FindAllReferenceTestCasesForShareStep(ExecutionContext.TestManagementTeamProject, ExecutionContext.Preferences.TestPlan, this.SharedStepsInitialViewModel.SelectedSharedStep.Id);
                this.SharedStepsInitialViewModel.InitialTestCaseCollection = filteredTestCases;
            });
            t.ContinueWith(antecedent =>
            {
                filteredTestCases = this.SharedStepsInitialViewModel.FilterTestCases();
                filteredTestCases.ForEach(tc => this.SharedStepsInitialViewModel.ObservableTestCases.Add(tc));
                this.SharedStepsInitialViewModel.TestCasesCount = filteredTestCases.Count.ToString();

                this.progressBar.Visibility = System.Windows.Visibility.Hidden;
                this.mainGrid.Visibility = System.Windows.Visibility.Visible;
            }, TaskScheduler.FromCurrentSynchronizationContext());         
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
            TestCase currentTestCase = this.dgTestCases.SelectedItem as TestCase;
            if (currentTestCase.ITestSuiteBase != null)
            {
                log.InfoFormat("Edit test case with id: {0} and suite id {1}", currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
                Navigator.Instance.NavigateToTestCasesEditView(this, currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
            }
            else
            {
                log.InfoFormat("Edit test case with id: {0}", currentTestCase.ITestCase.Id);
                Navigator.Instance.NavigateToTestCasesEditView(this, currentTestCase.ITestCase.Id, -1);
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
        /// Handles the PreviewKeyDown event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                this.EditTestCaseInternal();   
            }
        }

        /// <summary>
        /// Handles the GotFocus event of the tbAdvancedSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAdvancedSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbAdvancedSearch.ClearDefaultContent(ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsAdvancedSearchTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbAdvancedSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAdvancedSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbAdvancedSearch.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.DetaultAdvancedSearch, ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsAdvancedSearchTextSet);

        }

        /// <summary>
        /// Handles the Click event of the btnAdvancedSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAdvancedSearch_Click(object sender, RoutedEventArgs e)
        {
            this.SharedStepsInitialViewModel.FilterSharedSteps();
        }
    }
}