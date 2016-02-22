// <copyright file="TestCaseBatchDuplicateView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AAngelov.Utilities.Entities;
using AAngelov.Utilities.UI.ControlExtensions;
using AAngelov.Utilities.UI.Managers;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using TestCaseManagerCore;
using TestCaseManagerCore.BusinessLogic.Entities;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the batch duplicate, find replace page
    /// </summary>
    public partial class TestCaseBatchDuplicateView : UserControl, IContent
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// The load test cases
        /// </summary>
        private bool loadTestCases;

        /// <summary>
        /// The load specific test cases
        /// </summary>
        private bool loadSpecificTestCases;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseBatchDuplicateView"/> class.
        /// </summary>
        public TestCaseBatchDuplicateView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the test cases batch duplicate view model.
        /// </summary>
        /// <value>
        /// The test cases batch duplicate view model.
        /// </value>
        public TestCasesBatchDuplicateViewModel TestCasesBatchDuplicateViewModel { get; set; }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            this.InitializeUrlParameters(e);
            if (isInitialized)
            {
                this.ShowProgressBar();
                this.InitializeInternal();
            }
        }

        /// <summary>
        /// Called when this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Called when a this instance becomes the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            isInitialized = false;
            ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbSuite, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
            ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbPriority, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
            ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbTeamFoundationIdentityNames, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
        }

        /// <summary>
        /// Called just before this instance is no longer the active content in a frame.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        /// <remarks>
        /// The method is also invoked when parent frames are about to navigate.
        /// </remarks>
        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }

        /// <summary>
        /// Initializes the URL parameters.
        /// </summary>
        /// <param name="e">The <see cref="FragmentNavigationEventArgs"/> instance containing the event data.</param>
        private void InitializeUrlParameters(FragmentNavigationEventArgs e)
        {
            FragmentManager fm = new FragmentManager(e.Fragment);
            string loadTestCasesStr = fm.Get("loadTestCases");
            if (!string.IsNullOrEmpty(loadTestCasesStr))
            {
                this.loadTestCases = bool.Parse(loadTestCasesStr);
            }
            string loadSpecificTestCasesStr = fm.Get("loadSpecificTestCases");
            if (!string.IsNullOrEmpty(loadSpecificTestCasesStr))
            {
                this.loadSpecificTestCases = bool.Parse(loadSpecificTestCasesStr);
            }
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
            this.InitializeInternal();
        }

        private void InitializeInternal()
        { 
            Task t = Task.Factory.StartNew(() =>
            {
                if (this.TestCasesBatchDuplicateViewModel != null)
                {
                    this.TestCasesBatchDuplicateViewModel = new TestCaseManagerCore.ViewModels.TestCasesBatchDuplicateViewModel(this.TestCasesBatchDuplicateViewModel, this.loadTestCases, this.loadSpecificTestCases);
                    this.TestCasesBatchDuplicateViewModel.FilterEntities();
                }
                else
                {
                    this.TestCasesBatchDuplicateViewModel = new TestCaseManagerCore.ViewModels.TestCasesBatchDuplicateViewModel(this.loadTestCases, this.loadSpecificTestCases);
                }
            });
            t.ContinueWith(antecedent =>
            {
                if (this.dgTestCases.SelectedItems != null)
                {
                    this.TestCasesBatchDuplicateViewModel.SelectedEntitiesCount = this.dgTestCases.SelectedItems.Count.ToString();
                }
                
                this.DataContext = this.TestCasesBatchDuplicateViewModel;
                if (this.loadTestCases)
                {
                    this.dgTestCases.ItemsSource = this.TestCasesBatchDuplicateViewModel.ObservableTestCases;
                }
                else
                {
                    this.dgTestCases.ItemsSource = this.TestCasesBatchDuplicateViewModel.ObservableSharedSteps;
                }
                this.cbTeamFoundationIdentityNames.SelectedIndex = 0;
                this.cbPriority.SelectedIndex = 0;
                this.HideProgressBar();
                this.tbTitleFilter.Focus();
                isInitialized = true;
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
        /// Handles the GotFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbTitleFilter.ClearDefaultContent(ref this.TestCasesBatchDuplicateViewModel.InitialViewFilters.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbTextSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTextSuiteFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbSuiteFilter.ClearDefaultContent(ref this.TestCasesBatchDuplicateViewModel.InitialViewFilters.IsSuiteTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbTitleFilter.RestoreDefaultText(this.TestCasesBatchDuplicateViewModel.InitialViewFilters.DetaultTitle, ref this.TestCasesBatchDuplicateViewModel.InitialViewFilters.IsTitleTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbSuiteFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbSuiteFilter.RestoreDefaultText(this.TestCasesBatchDuplicateViewModel.InitialViewFilters.DetaultSuite, ref this.TestCasesBatchDuplicateViewModel.InitialViewFilters.IsSuiteTextSet);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbAssignedToFilter.ClearDefaultContent(ref this.TestCasesBatchDuplicateViewModel.InitialViewFilters.IsAssignedToTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbAssignedToFilter.RestoreDefaultText(this.TestCasesBatchDuplicateViewModel.InitialViewFilters.DetaultAssignedTo, ref this.TestCasesBatchDuplicateViewModel.InitialViewFilters.IsAssignedToTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbAssignedToFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbAssignedToFilter_KeyUp(object sender, KeyEventArgs e)
        {
            this.TestCasesBatchDuplicateViewModel.FilterEntities();
        }

        /// <summary>
        /// Handles the GotFocus event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbPriorityFilter.ClearDefaultContent(ref this.TestCasesBatchDuplicateViewModel.InitialViewFilters.IsPriorityTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbPriorityFilter.RestoreDefaultText(this.TestCasesBatchDuplicateViewModel.InitialViewFilters.DetaultPriority, ref this.TestCasesBatchDuplicateViewModel.InitialViewFilters.IsPriorityTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbPriorityFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbPriorityFilter_KeyUp(object sender, KeyEventArgs e)
        {
            this.TestCasesBatchDuplicateViewModel.FilterEntities();
        }

        /// <summary>
        /// Handles the KeyUp event of the tbIdFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbIdFilter_KeyUp(object sender, KeyEventArgs e)
        {
            this.TestCasesBatchDuplicateViewModel.FilterEntities();
        }

        /// <summary>
        /// Handles the KeyUp event of the tbTitleFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbTitleFilter_KeyUp(object sender, KeyEventArgs e)
        {
            this.TestCasesBatchDuplicateViewModel.FilterEntities();
        }

        /// <summary>
        /// Handles the KeyUp event of the tbSuiteFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbSuiteFilter_KeyUp(object sender, KeyEventArgs e)
        {
            this.TestCasesBatchDuplicateViewModel.FilterEntities();
        }

        /// <summary>
        /// Handles the MouseEnter event of the cbSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void cbSuite_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ExecutionContext.SettingsViewModel.HoverBehaviorDropDown)
            {
                this.cbSuite.IsDropDownOpen = true;
                this.cbSuite.Focus();
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the cbPriority control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void cbPriority_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ExecutionContext.SettingsViewModel.HoverBehaviorDropDown)
            {
                this.cbPriority.IsDropDownOpen = true;
                this.cbPriority.Focus();
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the cbTeamFoundationIdentityNames control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void cbTeamFoundationIdentityNames_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ExecutionContext.SettingsViewModel.HoverBehaviorDropDown)
            {
                this.cbTeamFoundationIdentityNames.IsDropDownOpen = true;
                this.cbTeamFoundationIdentityNames.Focus();
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the cbSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void cbSuite_MouseMove(object sender, MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        /// <summary>
        /// Handles the MouseMove event of the cbTeamFoundationIdentityNames control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void cbTeamFoundationIdentityNames_MouseMove(object sender, MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        /// <summary>
        /// Handles the MouseMove event of the cbPriority control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void cbPriority_MouseMove(object sender, MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the btnBatchDuplicate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnBatchDuplicate_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgTestCases.SelectedItems.Count == 0)
            {
                ModernDialog.ShowMessage("No test cases are selected.", "Warrning!", MessageBoxButton.OK);
                return;
            }
            if (!this.TestCasesBatchDuplicateViewModel.AreAllSharedStepIdsValid())
            {
                this.ShowNotCorrectSharedStepIdMessageBox();
                return;
            }
            this.InitializeCurrentSelectedEntities();
            int duplicatedCount = 0;
            this.ShowProgressBar();
            Task t = Task.Factory.StartNew(() =>
            {
                duplicatedCount = this.TestCasesBatchDuplicateViewModel.DuplicateEntity();
            });
            Task t1 = t.ContinueWith(antecedent =>
            {
                this.InitializeInternal();               
            }, TaskScheduler.FromCurrentSynchronizationContext());
            t1.ContinueWith(antecedent =>
            {
                ModernDialog.ShowMessage(string.Format("{0} test cases duplicated.", duplicatedCount), "Success!", MessageBoxButton.OK);
            }, TaskScheduler.FromCurrentSynchronizationContext());            
        }

        /// <summary>
        /// Shows the not correct shared step unique identifier message box.
        /// </summary>
        private void ShowNotCorrectSharedStepIdMessageBox()
        {
            ModernDialog.ShowMessage("Some of the provided shared step ids is incorrect.", "Success!", MessageBoxButton.OK);
        }

        /// <summary>
        /// Handles the Click event of the btnFindAndReplace control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnFindAndReplace_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgTestCases.SelectedItems.Count == 0)
            {
                ModernDialog.ShowMessage("No test cases are selected.", "Warrning!", MessageBoxButton.OK);
                return;
            }
            if (!this.TestCasesBatchDuplicateViewModel.AreAllSharedStepIdsValid())
            {
                this.ShowNotCorrectSharedStepIdMessageBox();
                return;
            }
            this.InitializeCurrentSelectedEntities();
            List<TextReplacePair> textReplacePairsList = this.TestCasesBatchDuplicateViewModel.ReplaceContext.ObservableTextReplacePairs.ToList();
            List<SharedStepIdReplacePair> sharedStepIdReplacePairList = this.TestCasesBatchDuplicateViewModel.ReplaceContext.ObservableSharedStepIdReplacePairs.ToList();
            int replacedCount = 0;
            this.ShowProgressBar();
            Task t = Task.Factory.StartNew(() =>
            {
                replacedCount = this.TestCasesBatchDuplicateViewModel.FindAndReplaceInEntities();              
            });
            Task t1 = t.ContinueWith(antecedent =>
            {
                this.InitializeInternal();
            }, TaskScheduler.FromCurrentSynchronizationContext());
            t1.ContinueWith(antecedent =>
            {
                ModernDialog.ShowMessage(string.Format("{0} test cases replaced.", replacedCount), "Success!", MessageBoxButton.OK);
            }, TaskScheduler.FromCurrentSynchronizationContext());              
        }

        /// <summary>
        /// Initializes the current selected test cases.
        /// </summary>
        private void InitializeCurrentSelectedEntities()
        {
            this.TestCasesBatchDuplicateViewModel.ReplaceContext.SelectedEntities.Clear();
            foreach (Object currentSelectedItem in this.dgTestCases.SelectedItems)
            {
                this.TestCasesBatchDuplicateViewModel.ReplaceContext.SelectedEntities.Add(currentSelectedItem);
            }
        }

        /// <summary>
        /// Handles the SelectedCellsChanged event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectedCellsChangedEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            this.TestCasesBatchDuplicateViewModel.SelectedEntitiesCount = this.dgTestCases.SelectedItems.Count.ToString();
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.EditCurrentEntityInternal();
        }

        /// <summary>
        /// Edits the current entity internal.
        /// </summary>
        private void EditCurrentEntityInternal()
        {
            if (this.dgTestCases.SelectedItem == null)
            {
                return;
            }

            if (this.dgTestCases.SelectedItem is TestCase)
            {
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
            else
            {
                SharedStep currentSharedStep = this.dgTestCases.SelectedItem as SharedStep;
                log.InfoFormat("Edit Shared Step with id: {0} ", currentSharedStep.ISharedStep.Id);
                Navigator.Instance.NavigateToTestCasesEditView(this, true, currentSharedStep.ISharedStep.Id);
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                this.EditCurrentEntityInternal();
            }
        }

        /// <summary>
        /// Handles the GotFocus event of the tbAdvancedSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAdvancedSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            this.tbAdvancedSearch.ClearDefaultContent(ref this.TestCasesBatchDuplicateViewModel.InitialViewFilters.IsAdvancedSearchTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbAdvancedSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbAdvancedSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            this.tbAdvancedSearch.RestoreDefaultText(this.TestCasesBatchDuplicateViewModel.InitialViewFilters.DetaultAdvancedSearch, ref this.TestCasesBatchDuplicateViewModel.InitialViewFilters.IsAdvancedSearchTextSet);
        }

        /// <summary>
        /// Handles the Click event of the btnAdvancedSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAdvancedSearch_Click(object sender, RoutedEventArgs e)
        {
            this.TestCasesBatchDuplicateViewModel.FilterEntities();
        }

        /// <summary>
        /// Handles the Click event of the btnInfo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            if (this.loadTestCases)
            {
                System.Diagnostics.Process.Start("http://aangelov.com/2014/04/21/find-replace-tfs-test-cases-test-case-manager/");
            }
            else
            {
                System.Diagnostics.Process.Start("http://aangelov.com/2014/05/01/find-replace-tfs-shared-steps-test-case-manager/");
            }
        }
    }
}