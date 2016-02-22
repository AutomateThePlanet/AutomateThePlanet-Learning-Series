// <copyright file="SharedStepsInitialView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
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
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                    SharedStepsInitialViewModel = new TestCaseManagerCore.ViewModels.SharedStepsInitialViewModel();
                }
            });
            t.ContinueWith(antecedent =>
            {
                this.SharedStepsInitialViewModel.FilterSharedSteps();
                this.DataContext = SharedStepsInitialViewModel;
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
        /// Validates the shared steps selection.
        /// </summary>
        /// <param name="action">The action.</param>
        private void ValidateSharedStepsSelection(Action action)
        {
            if (dgSharedSteps.SelectedIndex != -1)
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
                SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
                Log.InfoFormat("Edit Shared Step with id: {0} ", currentSharedStep.ISharedStep.Id);
                this.NavigateToTestCasesEditView(true, currentSharedStep.ISharedStep.Id);
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
                SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
                Log.InfoFormat("Duplicate Shared Step with id: {0} ", currentSharedStep.ISharedStep.Id);
                this.NavigateToTestCasesEditView(true, currentSharedStep.ISharedStep.Id, true, true);
            });
        }

        /// <summary>
        /// Handles the Click event of the btnNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            Log.Info("Navigate to Create New Shared Step");
            this.NavigateToTestCasesEditView(true, true, false);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbIdFilter.ClearDefaultContent(ref SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsIdTextSet);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.ClearDefaultContent(ref SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbIdFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.DetaultId, ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsIdTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbTitleFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.DetaultTitle, ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsTitleTextSet);
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
            tbAssignedToFilter.ClearDefaultContent(ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsAssignedToTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbAssignedToFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.DetaultAssignedTo, ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsAssignedToTextSet);
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
            tbPriorityFilter.ClearDefaultContent(ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsPriorityTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbPriorityFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.DetaultPriority, ref this.SharedStepsInitialViewModel.InitialViewFiltersSharedSteps.IsPriorityTextSet);

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
            tbTestCaseTitleFilter.ClearDefaultContent(ref SharedStepsInitialViewModel.InitialViewFiltersTestCases.IsTitleTextSet);           
        }

        /// <summary>
        /// Handles the LostFocus event of the tbTestCaseTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTestCaseTitleFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbTestCaseTitleFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFiltersTestCases.DetaultTitle, ref this.SharedStepsInitialViewModel.InitialViewFiltersTestCases.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbTestCaseTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbTestCaseTitleFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            List<TestCase> filteredList= this.SharedStepsInitialViewModel.FilterTestCases();
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
            tbTestCaseSuiteFilter.ClearDefaultContent(ref this.SharedStepsInitialViewModel.InitialViewFiltersTestCases.IsSuiteTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbTestCaseSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTestCaseSuiteFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbTestCaseSuiteFilter.RestoreDefaultText(this.SharedStepsInitialViewModel.InitialViewFiltersTestCases.DetaultSuite, ref this.SharedStepsInitialViewModel.InitialViewFiltersTestCases.IsSuiteTextSet);
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
            if (dgSharedSteps.SelectedItem != null)
            {
                SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
                Log.InfoFormat("Edit Shared Step with id: {0} ", currentSharedStep.ISharedStep.Id);
                this.NavigateToTestCasesEditView(true, currentSharedStep.ISharedStep.Id);
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
                SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
                this.NavigateToTestCasesEditView(true, currentSharedStep.ISharedStep.Id);
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
            btnDuplicate.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnDuplicate1.IsEnabled = true;
            btnEdit1.IsEnabled = true;
            btnFindReferences.IsEnabled = true;
            btnFindReferences1.IsEnabled = true;
            dgSharedStepsContextItemEdit.IsEnabled = true;
            dgSharedStepsContextItemPreview.IsEnabled = true;
            dgSharedStepsContextItemDuplicate.IsEnabled = true;
            if (dgSharedSteps.SelectedItems.Count < 1)
            {
                btnDuplicate.IsEnabled = false;
                btnEdit.IsEnabled = false;
                btnDuplicate1.IsEnabled = false;
                btnEdit1.IsEnabled = false;
                dgSharedStepsContextItemEdit.IsEnabled = false;
                dgSharedStepsContextItemPreview.IsEnabled = false;
                dgSharedStepsContextItemDuplicate.IsEnabled = false;
                btnFindReferences.IsEnabled = false;
                btnFindReferences1.IsEnabled = false;
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
            progressBar.Visibility = System.Windows.Visibility.Visible;
            mainGrid.Visibility = System.Windows.Visibility.Hidden;
            this.SharedStepsInitialViewModel.ObservableTestCases.Clear();
            List<TestCase> filteredTestCases = new List<TestCase>();
            Task t = Task.Factory.StartNew(() =>
            {
                Log.InfoFormat("Find all reference Test Cases for Shared Step with id: {0} ", this.SharedStepsInitialViewModel.SelectedSharedStep.Id);
				filteredTestCases = TestCaseManager.FindAllReferenceTestCasesForShareStep(ExecutionContext.TestManagementTeamProject, ExecutionContext.Preferences.TestPlan, this.SharedStepsInitialViewModel.SelectedSharedStep.Id);
                this.SharedStepsInitialViewModel.InitialTestCaseCollection = filteredTestCases;
            });
            t.ContinueWith(antecedent =>
            {
                filteredTestCases = this.SharedStepsInitialViewModel.FilterTestCases();
                filteredTestCases.ForEach(tc => this.SharedStepsInitialViewModel.ObservableTestCases.Add(tc));
                this.SharedStepsInitialViewModel.TestCasesCount = filteredTestCases.Count.ToString();

                progressBar.Visibility = System.Windows.Visibility.Hidden;
                mainGrid.Visibility = System.Windows.Visibility.Visible;
            }, TaskScheduler.FromCurrentSynchronizationContext());         
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgTestCases.SelectedItem == null)
            {
                return;
            }
            TestCase currentTestCase = dgTestCases.SelectedItem as TestCase;
            if (currentTestCase.ITestSuiteBase != null)
            {
                Log.InfoFormat("Edit test case with id: {0} and suite id {1}", currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);               
                this.NavigateToTestCasesEditView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id);
            }
            else
            {
                 Log.InfoFormat("Edit test case with id: {0}", currentTestCase.ITestCase.Id);
                this.NavigateToTestCasesEditView(currentTestCase.ITestCase.Id, -1);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnChangeTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnChangeTestCases_Click(object sender, RoutedEventArgs e)
        {
            if (dgTestCases.SelectedItems.Count == 0)
            {
                ModernDialog.ShowMessage("No test cases selected.", "Warning", MessageBoxButton.OK);
            }
            else
            {
                ExecutionContext.SelectedTestCasesForChange = new List<TestCase>();
                foreach (TestCase currentTestCase in dgTestCases.SelectedItems)
                {
                    ExecutionContext.SelectedTestCasesForChange.Add(currentTestCase);
                }
                Log.Info("Navigate to TestCaseBatchDuplicateView initialized with selected test cases.");
                this.NavigateToTestCaseBatchDuplicateView(true, true);
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
    }
}