// <copyright file="TestCaseEditView.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using Microsoft.TeamFoundation.TestManagement.Client;
using TestCaseManagerCore;
using TestCaseManagerCore.BusinessLogic.Entities;
using TestCaseManagerCore.BusinessLogic.Enums;
using TestCaseManagerCore.BusinessLogic.Managers;
using TestCaseManagerCore.Helpers;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the test case edit(edit mode) page
    /// </summary>
    public partial class TestCaseEditView : System.Windows.Controls.UserControl, IContent
    {
        /// <summary>
        /// The save command
        /// </summary>
        public static readonly RoutedCommand SaveCommand = new RoutedCommand();

        /// <summary>
        /// The save and close command
        /// </summary>
        public static readonly RoutedCommand SaveAndCloseCommand = new RoutedCommand();

        /// <summary>
        /// The share step command
        /// </summary>
        public static readonly RoutedCommand ShareStepCommand = new RoutedCommand();

        /// <summary>
        /// The change step command
        /// </summary>
        public static readonly RoutedCommand ChangeStepCommand = new RoutedCommand();

        /// <summary>
        /// The insert step command
        /// </summary>
        public static readonly RoutedCommand InsertStepCommand = new RoutedCommand();

        /// <summary>
        /// The edit step command
        /// </summary>
        public static readonly RoutedCommand EditStepCommand = new RoutedCommand();

        /// <summary>
        /// The associate test command
        /// </summary>
        public static readonly RoutedCommand AssociateTestCommand = new RoutedCommand();

        /// <summary>
        /// The add shared step command
        /// </summary>
        public static readonly RoutedCommand AddSharedStepCommand = new RoutedCommand();

        /// <summary>
        /// The delete test step command
        /// </summary>
        public static readonly RoutedCommand DeleteTestStepCommand = new RoutedCommand();

        /// <summary>
        /// The move up test step command
        /// </summary>
        public static readonly RoutedCommand MoveUpTestStepsCommand = new RoutedCommand();

        /// <summary>
        /// The move down test step command
        /// </summary>
        public static readonly RoutedCommand MoveDownTestStepsCommand = new RoutedCommand();

        /// <summary>
        /// The copy test steps command
        /// </summary>
        public static readonly RoutedCommand CopyTestStepsCommand = new RoutedCommand();

        /// <summary>
        /// The cut test steps command
        /// </summary>
        public static readonly RoutedCommand CutTestStepsCommand = new RoutedCommand();

        /// <summary>
        /// The paste test steps command
        /// </summary>
        public static readonly RoutedCommand PasteTestStepsCommand = new RoutedCommand();

        /// <summary>
        /// The undo command
        /// </summary>
        public static readonly RoutedCommand UndoCommand = new RoutedCommand();

        /// <summary>
        /// The redo command
        /// </summary>
        public static readonly RoutedCommand RedoCommand = new RoutedCommand();

        /// <summary>
        /// The edit shared step command
        /// </summary>
        public static readonly RoutedCommand EditSharedStepCommand = new RoutedCommand();

        /// <summary>
        /// The duplicate shared step command
        /// </summary>
        public static readonly RoutedCommand DuplicateSharedStepCommand = new RoutedCommand();

        /// <summary>
        /// The edit test step command
        /// </summary>
        public static readonly RoutedCommand EditTestStepCommand = new RoutedCommand();

        /// <summary>
        /// The preview command
        /// </summary>
        public static readonly RoutedCommand PreviewCommand = new RoutedCommand();

        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The edit view context
        /// </summary>
        private EditViewContext editViewContext;

        /// <summary>
        /// The cancellation token source
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseEditView"/> class.
        /// </summary>
        public TestCaseEditView()
        {
            this.InitializeComponent();
            this.InitializeFastKeys();            
        }  

        /// <summary>
        /// Gets or sets the test case edit view model.
        /// </summary>
        /// <value>
        /// The test case edit view model.
        /// </value>
        public TestCaseEditViewModel TestCaseEditViewModel { get; set; }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            this.InitializeUrlParameters(e);
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
            this.editViewContext = new EditViewContext();
            UndoRedoManager.Instance().Clear();   
            this.editViewContext.IsInitialized = false;
            ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbArea, TestCaseManagerCore.ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
            ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbPriority, TestCaseManagerCore.ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
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
            MessageBoxResult result = this.NavigateBackToPreviousPage(true);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                this.cancellationTokenSource.Cancel();
            }
        }

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.InitializeInternal();      
        }

        /// <summary>
        /// Initializes the internal.
        /// </summary>
        private void InitializeInternal()
        {
            if (this.editViewContext.IsInitialized)
            {
                return;
            }
            System.Windows.Threading.Dispatcher uiDispatcher = this.Dispatcher;
            this.ShowProgressBar();
            Task t = Task.Factory.StartNew(() =>
            {
                TestCaseEditViewModel = new TestCaseEditViewModel(this.editViewContext);
            });
            Task t2 = t.ContinueWith(antecedent =>
            {
                TestCaseManagerCore.ExecutionContext.TestCaseEditViewModel = this.TestCaseEditViewModel;
                this.InitializeUiRelatedViewSettings();
                UndoRedoManager.Instance().RedoStackStatusChanged += new UndoRedoManager.OnStackStatusChanged(RedoStackStatusChanged);
                UndoRedoManager.Instance().UndoStackStatusChanged += new UndoRedoManager.OnStackStatusChanged(UndoStackStatusChanged);
                List<TestStep> selectedTestSteps = this.AddMissedSelectedSharedSteps();
                this.UpdateSelectedTestSteps(selectedTestSteps);
                btnSaveTestStep.IsEnabled = false;
                btnCancelEdit.IsEnabled = false;
                btnEdit.IsEnabled = true;
                btnInsertStep.IsEnabled = true;
                this.HideProgressBar();
                this.editViewContext.IsInitialized = true;
                cancellationTokenSource = new CancellationTokenSource();
                this.TestCaseEditViewModel.SharedStepsRefreshEvent += TestCaseEditViewModel_SharedStepsRefreshEvent;
            }, TaskScheduler.FromCurrentSynchronizationContext());
            Task sharedStepsRefreshTask = t2.ContinueWith(antecedent =>
            {
                if (!this.editViewContext.IsSharedStep)
                {
                    this.TestCaseEditViewModel.RefreshSharedStepCollections(uiDispatcher, cancellationTokenSource.Token);
                }
            });
        }

        /// <summary>
        /// Handles the SharedStepsRefreshEvent event of the TestCaseEditViewModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TestCaseEditViewModel_SharedStepsRefreshEvent(object sender, EventArgs e)
        {
            this.TestCaseEditViewModel.FilterSharedSteps(tbSharedStepFilter.Text);
        }

        /// <summary>
        /// Undoes the stack status changed.
        /// </summary>
        /// <param name="hasItems">if set to <c>true</c> [has items].</param>
        private void UndoStackStatusChanged(bool hasItems)
        {
            try
            {
                btnUndo.IsEnabled = hasItems;
            }
            catch
            {
            }           
        }

        /// <summary>
        /// Redoes the stack status changed.
        /// </summary>
        /// <param name="hasItems">if set to <c>true</c> [has items].</param>
        private void RedoStackStatusChanged(bool hasItems)
        {
            try
            {
                btnRedo.IsEnabled = hasItems;
            }
            catch
            {
            }
        }     

        /// <summary>
        /// Initializes the UI related view settings.
        /// </summary>
        private void InitializeUiRelatedViewSettings()
        {
            this.DataContext = this.TestCaseEditViewModel;

            rtbAction.SetText(TestCaseEditViewModel.ActionDefaultText);
            rtbExpectedResult.SetText(TestCaseEditViewModel.ExpectedResultDefaultText);
            tbSharedStepFilter.Text = TestCaseEditViewModel.SharedStepSearchDefaultText;
            if (!this.editViewContext.Duplicate && this.editViewContext.CreateNew)
            {
                this.SetTestCasePropertiesToDefault();
                btnDuplicate.IsEnabled = false;
            }
            else if (this.editViewContext.Duplicate && !this.editViewContext.CreateNew)
            {
                btnDuplicate.IsEnabled = false;
            }
            else if (!this.editViewContext.Duplicate && !this.editViewContext.CreateNew)
            {
                btnDuplicate.IsEnabled = true;
            }
        }

        /// <summary>
        /// Sets the test case properties to default values.
        /// </summary>
        private void SetTestCasePropertiesToDefault()
        {
            cbArea.SelectedIndex = 0;
            cbPriority.SelectedIndex = 0;
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
        /// Initializes the URL parameters.
        /// </summary>
        /// <param name="e">The <see cref="FragmentNavigationEventArgs"/> instance containing the event data.</param>
        private void InitializeUrlParameters(FragmentNavigationEventArgs e)
        {         
            this.editViewContext.CreateNew = false;
            this.editViewContext.Duplicate = false;
            FragmentManager fm = new FragmentManager(e.Fragment);
            string testCaseId = fm.Get("id");
            if (!string.IsNullOrEmpty(testCaseId))
            {
                this.editViewContext.TestCaseId = int.Parse(testCaseId);
            }
            string suiteId = fm.Get("suiteId");
            if (!string.IsNullOrEmpty(suiteId))
            {
                this.editViewContext.TestSuiteId = int.Parse(suiteId);
            }
            string createNew = fm.Get("createNew");
            if (!string.IsNullOrEmpty(createNew))
            {
                this.editViewContext.CreateNew = bool.Parse(createNew);
            }
            string duplicate = fm.Get("duplicate");
            if (!string.IsNullOrEmpty(duplicate))
            {
                this.editViewContext.Duplicate = bool.Parse(duplicate);
            }
            string isSharedStepStr = fm.Get("isSharedStep");
            if (!string.IsNullOrEmpty(isSharedStepStr))
            {
                this.editViewContext.IsSharedStep = bool.Parse(isSharedStepStr);
            }
            else
            {
                this.editViewContext.IsSharedStep = false;
            }
            string sharedStepIdStr = fm.Get("sharedStepId");
            if (!string.IsNullOrEmpty(sharedStepIdStr))
            {
                this.editViewContext.SharedStepId = int.Parse(sharedStepIdStr);
            }
        }

        /// <summary>
        /// Initializes the fast keys.
        /// </summary>
        private void InitializeFastKeys()
        {
            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl + S"));
            SaveAndCloseCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Shift | ModifierKeys.Control, "Ctrl + Shift + S"));
            AssociateTestCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Alt));
            DeleteTestStepCommand.InputGestures.Add(new KeyGesture(Key.Delete, ModifierKeys.Alt));
            MoveUpTestStepsCommand.InputGestures.Add(new KeyGesture(Key.Up, ModifierKeys.Alt));
            MoveDownTestStepsCommand.InputGestures.Add(new KeyGesture(Key.Down, ModifierKeys.Alt));
            AddSharedStepCommand.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Alt));
            ShareStepCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Alt));
            EditStepCommand.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));
            ChangeStepCommand.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));
            InsertStepCommand.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Alt));
            PreviewCommand.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Alt));
            UndoCommand.InputGestures.Add(new KeyGesture(Key.Z, ModifierKeys.Control, "Ctrl + Z"));
            RedoCommand.InputGestures.Add(new KeyGesture(Key.Y, ModifierKeys.Control, "Ctrl + Y"));
        }

        /// <summary>
        /// Handles the Click event of the btnInsertStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnInsertStep_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dgTestSteps.SelectedIndex;
            int oldSelectedIndex = dgTestSteps.SelectedIndex;
            string stepTitle = TestCaseEditViewModel.GetStepTitle(rtbAction.GetText());
            string expectedResult = TestCaseEditViewModel.GetExpectedResult(rtbExpectedResult.GetText());
            TestStep testStepToInsert = null;
            if (!this.editViewContext.IsSharedStep)
            {
                Log.InfoFormat("Add new test step to test case with Title= \"{0}\", id= \"{1}\", ActionTitle= \"{2}\", ExpectedResult= \"{3}\"", TestCaseEditViewModel.TestCase.ITestCase.Title, TestCaseEditViewModel.TestCase.ITestCase.Id, stepTitle, expectedResult);
                testStepToInsert = TestStepManager.CreateNewTestStep(TestCaseEditViewModel.TestCase.ITestCase, stepTitle, expectedResult, default(Guid));
            }
            else
            {
                Log.InfoFormat("Add new test step to sharedStep with Title= \"{0}\", id= \"{1}\", ActionTitle= \"{2}\", ExpectedResult= \"{3}\"", TestCaseEditViewModel.SharedStep.ISharedStep.Title, TestCaseEditViewModel.SharedStep.ISharedStep.Id, stepTitle, expectedResult);
                testStepToInsert = TestStepManager.CreateNewTestStep(TestCaseEditViewModel.SharedStep.ISharedStep, stepTitle, expectedResult, default(Guid));
            }
            using (new UndoTransaction("Insert new step", false))
            {
                int newSelectedIndex = this.TestCaseEditViewModel.InsertTestStepInTestCase(testStepToInsert, selectedIndex, false);
                this.TestCaseEditViewModel.UpdateTestStepsGrid();
                this.ChangeSelectedIndexTestStepsDataGrid(newSelectedIndex + 1, oldSelectedIndex);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnShare control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnShare_Click(object sender, RoutedEventArgs e)
        {
            if (this.editViewContext.IsSharedStep)
            {
                return;
            }
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
                List<TestStep> selectedTestSteps = new List<TestStep>();
                this.GetTestStepsFromGrid(selectedTestSteps);
                bool isThereSharedStepSelected = TestStepManager.IsThereSharedStepSelected(selectedTestSteps);
                if (isThereSharedStepSelected)
                {
                    ModernDialog.ShowMessage("Shared steps cannon be shared again!", "Warning", MessageBoxButton.OK);
                    return;
                }                
                ISharedStep sharedStepCore = this.TestCaseEditViewModel.TestCase.CreateNewSharedStep(newTitle, selectedTestSteps);
                sharedStepCore.Refresh();
                this.TestCaseEditViewModel.ObservableSharedSteps.Add(new SharedStep(sharedStepCore));
                TestStepManager.UpdateGenericSharedSteps(this.TestCaseEditViewModel.ObservableTestSteps);
            }
        }

        /// <summary>
        /// Gets the test steps from grid.
        /// </summary>
        /// <param name="selectedTestSteps">The selected test steps.</param>
        /// <returns>list of selected test steps</returns>
        private List<TestStep> GetTestStepsFromGrid(List<TestStep> selectedTestSteps = null)
        {
            if (selectedTestSteps == null)
            {
                selectedTestSteps = new List<TestStep>();
            }
            if (dgTestSteps.SelectedItems != null)
            {
                foreach (var item in dgTestSteps.SelectedItems)
                {
                    selectedTestSteps.Add(item as TestStep);
                }
            }       

            return selectedTestSteps;
        }

        /// <summary>
        /// Handles the Click event of the btnDeleteStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnDeleteStep_Click(object sender, RoutedEventArgs e)
        {
            this.DeleteStepInternal();   
        }

        /// <summary>
        /// Handles the Command event of the deleteStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void deleteStep_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.DeleteStepInternal();
        }

        /// <summary>
        /// Deletes the test steps internal.
        /// </summary>
        private void DeleteStepInternal()
        {
            if (dgTestSteps.SelectedItems != null)
            {
                using (new UndoTransaction("Delete all selected test steps", false))
                {
                    int oldSelectedIndex = dgTestSteps.SelectedIndex;
                    List<TestStepFull> testStepsToBeRemoved = TestCaseEditViewModel.MarkInitialStepsToBeRemoved(dgTestSteps.SelectedItems.Cast<TestStep>().ToList());
                    testStepsToBeRemoved.Sort();
                    this.TestCaseEditViewModel.RemoveTestSteps(testStepsToBeRemoved);
                    TestStepManager.UpdateGenericSharedSteps(this.TestCaseEditViewModel.ObservableTestSteps);
                    this.TestCaseEditViewModel.UpdateTestStepsGrid();
                    int newSelectedIndex = oldSelectedIndex - testStepsToBeRemoved.Count;
                    if (newSelectedIndex < 0)
                    {
                        newSelectedIndex = 0;
                    }
                    this.ChangeSelectedIndexTestStepsDataGrid(newSelectedIndex, oldSelectedIndex);
                    List<TestStep> selectedTestSteps = this.AddMissedSelectedSharedSteps();
                    this.UpdateSelectedTestSteps(selectedTestSteps);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnMoveUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
            this.MoveUpInternal();            
        }

        /// <summary>
        /// Handles the Command event of the moveUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void moveUp_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.MoveUpInternal();
            dgTestSteps.Focus();
        }

        /// <summary>
        /// Handles the Command event of the duplicateSharedStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void duplicateSharedStep_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
            this.TestCaseEditViewModel.SaveChangesDialog();
			this.editViewContext.IsAlreadyCreated = false;
            this.editViewContext.IsInitialized = false;
            this.editViewContext.CreateNew = true;
            this.editViewContext.Duplicate = true;
            this.editViewContext.IsSharedStep = true;
            this.editViewContext.ComeFromTestCase = true;
            this.editViewContext.SharedStepId = currentSharedStep.Id;        
            Log.Info("Reinitialize edit mode to duplicate.");
            this.InitializeInternal();    
        }

        /// <summary>
        /// Handles the Command event of the moveDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void moveDown_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.MoveDownInternal();
            dgTestSteps.Focus();
        }

        /// <summary>
        /// Moves up test steps internal.
        /// </summary>
        private void MoveUpInternal()
        {
            // validate the move if it's out of the boudaries return
            if (dgTestSteps.SelectedItems.Count == 0)
            {
                return;
            }
            int startIndex = TestCaseEditViewModel.ObservableTestSteps.IndexOf(dgTestSteps.SelectedItems[0] as TestStep);
            if (startIndex == 0)
            {
                return;
            }
            int count = dgTestSteps.SelectedItems.Count;
            if (dgTestSteps.SelectedItems.Count == 0)
            {
                return;
            }
            using (new UndoTransaction("Move up selected steps", true))
            {
                TestCaseEditViewModel.CreateNewTestStepCollectionAfterMoveUp(startIndex, count);
                this.SelectNextItemsAfterMoveUp(startIndex, count);
            }
            dgTestSteps.UpdateLayout();
            dgTestSteps.ScrollIntoView(dgTestSteps.SelectedItem);
            TestStepManager.UpdateGenericSharedSteps(this.TestCaseEditViewModel.ObservableTestSteps);
        }

        /// <summary>
        /// Handles the Click event of the btnMoveDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
            this.MoveDownInternal();
        }

        /// <summary>
        /// Moves down test steps internal.
        /// </summary>
        private void MoveDownInternal()
        {
            // validate the move if it's out of the boudaries return
            if (dgTestSteps.SelectedItems.Count == 0)
            {
                return;
            }
            int startIndex = TestCaseEditViewModel.ObservableTestSteps.IndexOf(dgTestSteps.SelectedItems[0] as TestStep);
            int count = dgTestSteps.SelectedItems.Count;
            if (startIndex == TestCaseEditViewModel.ObservableTestSteps.Count - 1)
            {
                return;
            }
            if ((startIndex + count) >= TestCaseEditViewModel.ObservableTestSteps.Count)
            {
                return;
            }
            using (new UndoTransaction("Move down selected steps", true))
            {
                TestCaseEditViewModel.CreateNewTestStepCollectionAfterMoveDown(startIndex, count);
                this.SelectNextItemsAfterMoveDown(startIndex, count);
            }
            dgTestSteps.UpdateLayout();
            dgTestSteps.ScrollIntoView(dgTestSteps.SelectedItems[dgTestSteps.SelectedItems.Count - 1]);
            TestStepManager.UpdateGenericSharedSteps(this.TestCaseEditViewModel.ObservableTestSteps);
        }

        /// <summary>
        /// Selects the next test steps after move up.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="selectedTestStepsCount">The selected test steps count.</param>
        private void SelectNextItemsAfterMoveUp(int startIndex, int selectedTestStepsCount)
        {
            dgTestSteps.SelectedItems.Clear();
            for (int i = startIndex - 1; i < startIndex + selectedTestStepsCount - 1; i++)
            {
                dgTestSteps.SelectedItems.Add(dgTestSteps.Items[i]);
            }
            UndoRedoManager.Instance().Push((si, c) => this.SelectNextItemsAfterMoveDown(si, c), startIndex - 1, selectedTestStepsCount, "Select next items after move up");
        }

        /// <summary>
        /// Selects the next test steps after move down.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="selectedTestStepsCount">The selected test steps count.</param>
        private void SelectNextItemsAfterMoveDown(int startIndex, int selectedTestStepsCount)
        {
            dgTestSteps.SelectedItems.Clear();
            for (int i = startIndex + 1; i < startIndex + selectedTestStepsCount + 1; i++)
            {
                dgTestSteps.SelectedItems.Add(dgTestSteps.Items[i]);
            }
            UndoRedoManager.Instance().Push((si, c) => this.SelectNextItemsAfterMoveUp(si, c), startIndex + 1, selectedTestStepsCount, "Select next items after move up");
        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {         
            this.InsertSharedStepInternal();
        }

        /// <summary>
        /// Inserts the shared step internal.
        /// </summary>
        private void InsertSharedStepInternal()
        {
            if (this.editViewContext.IsSharedStep)
            {
                return;
            }
            SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
            if (currentSharedStep == null)
            {
                ModernDialog.ShowMessage("Please select a shared step to add first!", "Warning", MessageBoxButton.OK);
                return;
            }
            using (new UndoTransaction("Insert Shared step's inner test steps to the test case test steps Observable collection"))
            {
                int currentSelectedIndex = dgTestSteps.SelectedIndex;
                int finalInsertedStepIndex = this.TestCaseEditViewModel.InsertSharedStep(currentSharedStep, currentSelectedIndex);
                if (currentSharedStep.ISharedStep.Actions.Count == 0)
                {
                    ModernDialog.ShowMessage("No test steps added because the selected shared step doesn't have any!", "Warning", MessageBoxButton.OK);
                    return;
                }
                this.TestCaseEditViewModel.UpdateTestStepsGrid();
                TestStepManager.UpdateGenericSharedSteps(this.TestCaseEditViewModel.ObservableTestSteps);
                this.ChangeSelectedIndexTestStepsDataGrid(finalInsertedStepIndex + currentSharedStep.ISharedStep.Actions.Count, currentSelectedIndex);
            }          
        }

		/// <summary>
		/// Changes the selected index test steps data grid.
		/// </summary>
		/// <param name="newIndex">The new index.</param>
		/// <param name="oldIndex">The old index.</param>
        private void ChangeSelectedIndexTestStepsDataGrid(int newIndex, int oldIndex)
        {
            UndoRedoManager.Instance().Push((i, j) => this.ChangeSelectedIndexTestStepsDataGrid(i, j), oldIndex, newIndex);
            dgTestSteps.SelectedIndex = newIndex;
            dgTestSteps.UpdateLayout();
            if (dgTestSteps.SelectedItem != null)
            {
                dgTestSteps.ScrollIntoView(dgTestSteps.SelectedItem);
                dgTestSteps.Focus();
            }          
        }

        /// <summary>
        /// Handles the MouseLeftButtonUp event of the dgTestSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgTestSteps_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            List<TestStep> selectedTestSteps = this.AddMissedSelectedSharedSteps();
            this.UpdateSelectedTestSteps(selectedTestSteps);
        }

        /// <summary>
        /// Updates the selected test steps. Add updated selected test steps.
        /// </summary>
        /// <param name="selectedTestSteps">The selected test steps.</param>
        private void UpdateSelectedTestSteps(List<TestStep> selectedTestSteps)
        {
            dgTestSteps.SelectedItems.Clear();
            foreach (TestStep currentTestStep in selectedTestSteps)
            {
                dgTestSteps.SelectedItems.Add(currentTestStep);
            }
        }

        /// <summary>
        /// Adds the missed selected shared steps. Select non-selected shared steps because part of the shared step was already selected.
        /// </summary>
        /// <returns>updated selected test steps list</returns>
        private List<TestStep> AddMissedSelectedSharedSteps()
        {
            List<TestStep> selectedTestSteps = this.GetAllSelectedTestSteps();
            foreach (TestStep currentSelectedTestStep in this.TestCaseEditViewModel.ObservableTestSteps)
            {
                for (int i = 0; i < selectedTestSteps.Count; i++)
                {
                    if (currentSelectedTestStep.TestStepGuid.Equals(selectedTestSteps[i].TestStepGuid) &&
                        !currentSelectedTestStep.TestStepId.Equals(selectedTestSteps[i].TestStepId))
                    {
                        selectedTestSteps.Add(currentSelectedTestStep);
                        break;
                    }
                }
            }
            return selectedTestSteps;
        }

        /// <summary>
        /// Edits the current test step internal.
        /// </summary>
        private void EditCurrentTestStepInternal()
        {
            TestStep currentTestStep = this.GetSelectedTestStep();
            if (currentTestStep == null)
            {
                return;
            }
            if (!currentTestStep.IsShared)
            {
                this.EnableSaveStepButton();
                rtbAction.ClearDefaultContent(ref this.TestCaseEditViewModel.IsActionTextSet);
                rtbExpectedResult.ClearDefaultContent(ref this.TestCaseEditViewModel.IsExpectedResultTextSet);
                this.editViewContext.CurrentEditedStepGuid = currentTestStep.TestStepGuid;
                rtbAction.SetText(currentTestStep.ActionTitle);
                rtbExpectedResult.SetText(currentTestStep.ActionExpectedResult);
                TestStepManager.UpdateGenericSharedSteps(this.TestCaseEditViewModel.ObservableTestSteps);
            }
            else
            {
                this.EditSharedStepInternal(currentTestStep.SharedStepId);
            }           
        }

        /// <summary>
        /// Edits the shared step internal.
        /// </summary>
        /// <param name="sharedStepId">The shared step unique identifier.</param>
        private void EditSharedStepInternal(int sharedStepId)
        {
            MessageBoxResult messageBoxResult = this.TestCaseEditViewModel.SaveChangesDialog();
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.TestCaseEditViewModel.SaveEntityInternal();
                this.editViewContext.IsInitialized = false;
                this.editViewContext.IsSharedStep = true;
                this.editViewContext.ComeFromTestCase = true;
                this.editViewContext.SharedStepId = sharedStepId;                
                this.InitializeInternal();
            }
            else if (messageBoxResult == MessageBoxResult.No || messageBoxResult == MessageBoxResult.None)
            {
                this.editViewContext.IsInitialized = false;
                this.editViewContext.IsSharedStep = true;
                this.editViewContext.ComeFromTestCase = true;
                this.editViewContext.SharedStepId = sharedStepId;
                this.InitializeInternal();
            }
            Log.InfoFormat("Reinitialize edit mode to edit shared step with id= {1}", this.editViewContext.SharedStepId);
        }

        /// <summary>
        /// Shows the saved label.
        /// </summary>
        private void ShowSavedLabel()
        {
            lbSaved.Visibility = System.Windows.Visibility.Visible;

            Task t = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
            });
            Task t2 = t.ContinueWith(antecedent =>
            {
                lbSaved.Visibility = System.Windows.Visibility.Hidden;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Gets the selected test step.
        /// </summary>
        /// <returns>the selected test step</returns>
        private TestStep GetSelectedTestStep()
        {
            TestStep currentTestStep = null;
            if (dgTestSteps.SelectedItem != null)
            {
                currentTestStep = dgTestSteps.SelectedItem as TestStep;
            }
            return currentTestStep;
        }

        /// <summary>
        /// Enables the save step button.
        /// </summary>
        private void EnableSaveStepButton()
        {
            btnSaveTestStep.IsEnabled = true;
            btnCancelEdit.IsEnabled = true;
            btnEdit.IsEnabled = false;
            btnInsertStep.IsEnabled = false;
            btnShare.IsEnabled = false;
            btnDeleteStep.IsEnabled = false;
            btnCancelEdit.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// Handles the Click event of the btnSaveTestStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSaveTestStep_Click(object sender, RoutedEventArgs e)
        {
            this.DisableSaveButton();
            TestStep currentTestStep = this.TestCaseEditViewModel.ObservableTestSteps.Where(x => x.TestStepGuid.Equals(this.editViewContext.CurrentEditedStepGuid)).FirstOrDefault();
            string stepTitle = this.TestCaseEditViewModel.GetStepTitle(rtbAction.GetText());
            string expectedResult = this.TestCaseEditViewModel.GetExpectedResult(rtbExpectedResult.GetText());
            using (new UndoTransaction("Edit current test step"))
            {
                TestStepManager.EditTestStepActionTitle(currentTestStep, stepTitle);
                TestStepManager.EditTestStepActionExpectedResult(currentTestStep, expectedResult);
            }
            this.editViewContext.CurrentEditedStepGuid = default(Guid);
            TestStepManager.UpdateGenericSharedSteps(this.TestCaseEditViewModel.ObservableTestSteps);
        }

        /// <summary>
        /// Handles the Click event of the btnCancelEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            this.DisableSaveButton();
            this.TestCaseEditViewModel.IsActionTextSet = false;
            this.TestCaseEditViewModel.IsExpectedResultTextSet = false;
            rtbAction.ClearDefaultContent(ref this.TestCaseEditViewModel.IsActionTextSet);
            rtbExpectedResult.ClearDefaultContent(ref this.TestCaseEditViewModel.IsExpectedResultTextSet);
            this.editViewContext.CurrentEditedStepGuid = default(Guid);
        }

        /// <summary>
        /// Disables the save button.
        /// </summary>
        private void DisableSaveButton()
        {
            btnSaveTestStep.IsEnabled = false;
            btnCancelEdit.IsEnabled = false;
            btnEdit.IsEnabled = true;
            btnInsertStep.IsEnabled = true;
            btnShare.IsEnabled = true;
            btnDeleteStep.IsEnabled = true;            
        }

        /// <summary>
        /// Handles the Click event of the btnDuplicate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnDuplicate_Click(object sender, RoutedEventArgs e)
        {
            this.TestCaseEditViewModel.SaveChangesDialog();
            this.editViewContext.IsInitialized = false;
            this.editViewContext.CreateNew = true;
            this.editViewContext.Duplicate = true;
            Log.Info("Reinitialize edit mode to duplicate.");
            this.InitializeInternal();       
        }

        /// <summary>
        /// Handles the Click event of the btnSaveAndCloseTestCase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSaveAndCloseTestCase_Click(object sender, RoutedEventArgs e)
        {
            var testBase = this.TestCaseEditViewModel.SaveEntityInternal();
            btnDuplicate.IsEnabled = true;
            if (testBase != null)
            {
                this.NavigateBackToPreviousPage();
            }
        }

		/// <summary>
		/// Navigates the back automatic previous page.
		/// </summary>
		/// <param name="isFromNavigation">if set to <c>true</c> [is from navigation].</param>
		/// <returns>
		/// message box result
		/// </returns>
        private MessageBoxResult NavigateBackToPreviousPage(bool isFromNavigation = false)
        {
            MessageBoxResult result = MessageBoxResult.None;
            if (!this.editViewContext.IsSharedStep)
            {
                result = this.TestCaseEditViewModel.SaveChangesDialog();
                if (result != MessageBoxResult.Cancel && !isFromNavigation)
                {
                    Log.Info("Navigate to all Test Cases View.");
                    UndoRedoManager.Instance().Clear();  
                    this.NavigateToTestCasesInitialView();
                }
                else
                {
                    this.editViewContext.IsInitialized = true;
                }
            }
            else if (!this.editViewContext.ComeFromTestCase && this.editViewContext.IsSharedStep)
            {
                result = this.TestCaseEditViewModel.SaveChangesDialog();
                if (result != MessageBoxResult.Cancel && !isFromNavigation)
                {
                    Log.Info("Navigate to all Shared Steps View.");
                    UndoRedoManager.Instance().Clear();  
                    this.NavigateToSharedStepsInitialView();
                }
                else
                {
                    this.editViewContext.IsInitialized = true;
                }
            }
            else
            {
                result = this.TestCaseEditViewModel.SaveChangesDialog();
                if (result != MessageBoxResult.Cancel || result == MessageBoxResult.None)
                {
                    result = MessageBoxResult.Cancel;
                    Log.Info("Navigate back to test case edit mode.");
                    this.editViewContext.IsSharedStep = false;
                    this.editViewContext.ComeFromTestCase = false;
                    this.editViewContext.IsInitialized = false;
                    UndoRedoManager.Instance().Clear();  
                    this.InitializeInternal();
                }
                else
                {
                    this.editViewContext.IsInitialized = true;                    
                }              
            }

            return result;
        }

        /// <summary>
        /// Handles the Click event of the btnSaveTestCase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSaveTestCase_Click(object sender, RoutedEventArgs e)
        {
            this.TestCaseEditViewModel.SaveEntityInternal();
            this.ShowSavedLabel();
            btnDuplicate.IsEnabled = true;
            Log.Info("Save entity without close.");
        } 

        /// <summary>
        /// Handles the Click event of the btnUndo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            if (UndoRedoManager.Instance().HasUndoOperations)
            {
                Log.Info("Perform Undo.");
                UndoRedoManager.Instance().Undo();
                Log.Info("End Undo.");
                List<TestStep> selectedTestSteps = this.AddMissedSelectedSharedSteps();
                this.UpdateSelectedTestSteps(selectedTestSteps);            
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRedo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            if (UndoRedoManager.Instance().HasRedoOperations)
            {
                Log.Info("Perform Redo.");
                UndoRedoManager.Instance().Redo();
                Log.Info("End Redo.");           
                List<TestStep> selectedTestSteps = this.AddMissedSelectedSharedSteps();
                this.UpdateSelectedTestSteps(selectedTestSteps);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.editViewContext.ComeFromTestCase || !this.editViewContext.IsSharedStep)
            {
                Log.Info("Navigate to all test cases view.");
                this.NavigateToTestCasesInitialView();
            }
            else
            {
                Log.Info("Navigate to all shared steps view.");
                this.NavigateToSharedStepsInitialView();
            }
        }

        /// <summary>
        /// Handles the Command event of the copyTestSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void copyTestSteps_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            List<TestStep> selectedTestSteps = this.GetAllSelectedTestSteps();
            Log.InfoFormat("Copy {0} test steps.", selectedTestSteps.Count);
            TestStepManager.CopyToClipboardTestSteps(true, selectedTestSteps);
        }

        /// <summary>
        /// Handles the Command event of the editSharedStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void editSharedStep_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            SharedStep currentSharedStep = dgSharedSteps.SelectedItem as SharedStep;
            Log.InfoFormat("Edit shared step with id= {0}", currentSharedStep.ISharedStep.Id);
            this.EditSharedStepInternal(currentSharedStep.ISharedStep.Id);
        }

        /// <summary>
        /// Gets all selected test steps.
        /// </summary>
        /// <returns>the list of test steps</returns>
        private List<TestStep> GetAllSelectedTestSteps()
        {
            List<TestStep> selectedTestStepsSorted = new List<TestStep>();
            List<TestStep> selectedTestSteps = dgTestSteps.SelectedItems.Cast<TestStep>().ToList();

            foreach (TestStep currentTestStep in this.TestCaseEditViewModel.ObservableTestSteps)
            {
                for (int i = 0; i < selectedTestSteps.Count; i++)
                {
                    if (currentTestStep.Equals(selectedTestSteps[i]))
                    {
                        selectedTestStepsSorted.Add(selectedTestSteps[i]);
                        selectedTestSteps.RemoveAt(i);
                        break;
                    } 
                }
            }

            return selectedTestStepsSorted;
        }

        /// <summary>
        /// Handles the Command event of the cutTestSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void cutTestSteps_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            List<TestStep> selectedTestSteps = this.GetAllSelectedTestSteps();
            Log.InfoFormat("Cut {0} test steps.", selectedTestSteps.Count);
            TestStepManager.CopyToClipboardTestSteps(false, selectedTestSteps);
            ClipBoardTestStep clipBoardTestStep = TestStepManager.GetFromClipboardTestSteps();
            if (clipBoardTestStep != null && clipBoardTestStep.TestSteps != null)
            {
                using (new UndoTransaction("Delete cut steps"))
                {
                    Log.Info("Start Deleting cut test steps.");
                    this.TestCaseEditViewModel.DeleteCutTestSteps(clipBoardTestStep.TestSteps);
                    Log.Info("End Deleting cut test steps.");
                    this.TestCaseEditViewModel.UpdateTestStepsGrid();
                    TestStepManager.UpdateGenericSharedSteps(this.TestCaseEditViewModel.ObservableTestSteps);                   
                }
            }          
        }

        /// <summary>
        /// Handles the Command event of the pasteTestSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void pasteTestSteps_Command(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            ClipBoardTestStep clipBoardTestStep = TestStepManager.GetFromClipboardTestSteps();
            int selectedIndex = dgTestSteps.SelectedIndex;
            Guid previousOldGuid = default(Guid);
            Guid previousNewGuid = default(Guid);
            if (clipBoardTestStep == null || clipBoardTestStep.TestSteps == null)
            {
                return;
            }
            int oldSelectedIndex = dgTestSteps.SelectedIndex;
            int newSelectedIndex = dgTestSteps.SelectedIndex;
            using (new UndoTransaction("Pastes previously selected test steps"))
            {
                Log.Info("Start Pasting previously pasted test steps.");
                foreach (TestStep copiedTestStep in clipBoardTestStep.TestSteps)
                {
                    TestStep testStepToBeInserted = (TestStep)copiedTestStep.Clone();
                    if (copiedTestStep.TestStepGuid.Equals(previousOldGuid))
                    {
                        testStepToBeInserted.TestStepGuid = previousNewGuid;
                    }
                    int newSelectedId = TestCaseEditViewModel.InsertTestStepInTestCase(testStepToBeInserted, selectedIndex++, false);
                    newSelectedIndex = newSelectedId + 1;
                    previousNewGuid = testStepToBeInserted.TestStepGuid;
                    previousOldGuid = copiedTestStep.TestStepGuid;
                }

                // If fake item was inserted in order the paste to be enabled, we delete it from the test steps and select the next item in the grid
                if (this.editViewContext.IsFakeItemInserted)
                {
                    this.TestCaseEditViewModel.ObservableTestSteps.RemoveAt(0);
                    this.editViewContext.IsFakeItemInserted = false;
                }
                if (clipBoardTestStep.ClipBoardCommand == ClipBoardCommand.Cut)
                {
                    System.Windows.Forms.Clipboard.Clear();
                }
                this.TestCaseEditViewModel.UpdateTestStepsGrid();
                TestStepManager.UpdateGenericSharedSteps(this.TestCaseEditViewModel.ObservableTestSteps);            
                this.ChangeSelectedIndexTestStepsDataGrid(newSelectedIndex, oldSelectedIndex);
                List<TestStep> selectedTestSteps = this.AddMissedSelectedSharedSteps();
                this.UpdateSelectedTestSteps(selectedTestSteps);
                Log.Info("End Pasting previously pasted test steps.");
            }
        }    

        /// <summary>
        /// Handles the Click event of the btnEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.EditCurrentTestStepInternal();
        }

        /// <summary>
        /// Handles the Click event of the btnChange control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            this.DisableSaveButton();
            rtbAction.ClearDefaultContent(ref this.TestCaseEditViewModel.IsActionTextSet);
            rtbExpectedResult.ClearDefaultContent(ref this.TestCaseEditViewModel.IsExpectedResultTextSet);
            TestStep currentTestStep = this.GetSelectedTestStep();
            Log.InfoFormat("Perform change operation to test step with ActionTitle= \"{0}\", ActionExpectedResult= \"{1}\".", currentTestStep.ActionTitle, currentTestStep.ActionExpectedResult);
            rtbAction.SetText(currentTestStep.OriginalActionTitle);
            rtbExpectedResult.SetText(currentTestStep.OriginalActionExpectedResult);
        }   

        /// <summary>
        /// Handles the MouseEnter event of the cbArea control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbArea_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (TestCaseManagerCore.ExecutionContext.SettingsViewModel.HoverBehaviorDropDown)
            {
                cbArea.IsDropDownOpen = true;
                cbArea.Focus();
            }      
        }

        /// <summary>
        /// Handles the MouseEnter event of the cbPriority control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbPriority_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (TestCaseManagerCore.ExecutionContext.SettingsViewModel.HoverBehaviorDropDown)
            {
                cbPriority.IsDropDownOpen = true;
                cbPriority.Focus();
            }           
        }

        /// <summary>
        /// Handles the MouseMove event of the cbSuite control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbSuite_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        /// <summary>
        /// Handles the MouseMove event of the cbArea control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbArea_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        /// <summary>
        /// Handles the MouseMove event of the cbPriority control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbPriority_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        /// <summary>
        /// Handles the GotFocus event of the tbSharedStepFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbSharedStepFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSharedStepFilter.ClearDefaultContent(ref TestCaseEditViewModel.IsSharedStepSearchTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the tbSharedStepFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void tbSharedStepFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            tbSharedStepFilter.RestoreDefaultText(TestCaseEditViewModel.SharedStepSearchDefaultText, ref TestCaseEditViewModel.IsSharedStepSearchTextSet);
        }

        /// <summary>
        /// Handles the KeyUp event of the tbSharedStepFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void tbSharedStepFilter_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.TestCaseEditViewModel.FilterSharedSteps(tbSharedStepFilter.Text);
        }

        /// <summary>
        /// Handles the GotFocus event of the rtbStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void rtbStep_GotFocus(object sender, RoutedEventArgs e)
        {
            rtbAction.ClearDefaultContent(ref TestCaseEditViewModel.IsActionTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the rtbStep control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void rtbStep_LostFocus(object sender, RoutedEventArgs e)
        {
            rtbAction.RestoreDefaultText(TestCaseEditViewModel.ActionDefaultText, ref TestCaseEditViewModel.IsActionTextSet);
        }

        /// <summary>
        /// Handles the GotFocus event of the rtbExpectedResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void rtbExpectedResult_GotFocus(object sender, RoutedEventArgs e)
        {
            rtbExpectedResult.ClearDefaultContent(ref this.TestCaseEditViewModel.IsExpectedResultTextSet);
        }

        /// <summary>
        /// Handles the LostFocus event of the rtbExpectedResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void rtbExpectedResult_LostFocus(object sender, RoutedEventArgs e)
        {
            rtbExpectedResult.RestoreDefaultText(TestCaseEditViewModel.ExpectedResultDefaultText, ref this.TestCaseEditViewModel.IsExpectedResultTextSet); 
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the dgSharedSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgSharedSteps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.InsertSharedStepInternal();
        }

        /// <summary>
        /// Handles the Click event of the btnAssociateToAutomation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAssociateToAutomation_Click(object sender, RoutedEventArgs e)
        {
            string projectDllPath = RegistryManager.GetProjectDllPath();
            if (!File.Exists(projectDllPath))
            {
                ModernDialog.ShowMessage("Provide Existing Project Path Dll.", "Warning", MessageBoxButton.OK);
                this.NavigateToAppearanceSettingsView();
            }
            else
            {
                TestCase currentTestCase = this.TestCaseEditViewModel.SaveTestCaseInternal();
                if (currentTestCase != null)
                {
                    if (currentTestCase.ITestSuiteBase != null)
                    {
                        Log.InfoFormat("Navigate to AssociateAutomation, test case id= {0}, suite id= {1}, CreateNew= {2}, Duplicate= {3}.", currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id, this.editViewContext.CreateNew, this.editViewContext.Duplicate);
                        this.NavigateToAssociateAutomationView(currentTestCase.ITestCase.Id, currentTestCase.ITestSuiteBase.Id, this.editViewContext.CreateNew, this.editViewContext.Duplicate);
                    }
                    else
                    {
                        Log.InfoFormat("Navigate to AssociateAutomation, test case id= {0}, suite id= {1}, CreateNew= {2}, Duplicate= {3}.", currentTestCase.ITestCase.Id, -1, this.editViewContext.CreateNew, this.editViewContext.Duplicate);
                        this.NavigateToAssociateAutomationView(currentTestCase.ITestCase.Id, -1, this.editViewContext.CreateNew, this.editViewContext.Duplicate);
                    }                   
                }
            }
        }

        /// <summary>
        /// Handles the PreviewMouseRightButtonDown event of the dgTestSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgTestSteps_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClipBoardTestStep clipBoardItem = TestStepManager.GetFromClipboardTestSteps();
            bool isPasteEnabled = clipBoardItem == null ? false : true;
            dgTestStepsPasteMenuItem.IsEnabled = isPasteEnabled;

            // If there isnt't items in the grid, we add a fake one in order the paste operation to be enabled
            this.AddFakeInitialTestStepForPaste(isPasteEnabled);
            this.dgTestSteps.Focus();
        }

        /// <summary>
        /// Adds the fake initial test step for paste.
        /// </summary>
        /// <param name="isPasteEnabled">if set to <c>true</c> [is paste enabled].</param>
        private void AddFakeInitialTestStepForPaste(bool isPasteEnabled)
        {
            // If there isnt't items in the grid, we add a fake one in order the paste operation to be enabled
            if (this.TestCaseEditViewModel.ObservableTestSteps.Count == 0 && isPasteEnabled)
            {
				this.TestCaseEditViewModel.ObservableTestSteps.Add(new TestStep(false, string.Empty, default(Guid), 0, string.Empty, string.Empty));
                this.dgTestSteps.SelectedIndex = 0;
                this.editViewContext.IsFakeItemInserted = true;
            }
        }

        /// <summary>
        /// Handles the event of the dgTestSteps_SelectedCellsChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectedCellsChangedEventArgs"/> instance containing the event data.</param>
        private void dgTestSteps_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            this.UpdateButtonMenuAvailabilityInternal();        
        }

        /// <summary>
        /// Updates the button menu availability internal.
        /// </summary>
        private void UpdateButtonMenuAvailabilityInternal()
        {
            btnEdit.IsEnabled = true;
            btnMoveUp.IsEnabled = true;
            btnMoveDown.IsEnabled = true;
            btnDeleteStep.IsEnabled = true;
            btnChange.IsEnabled = true;
            btnShare.IsEnabled = true;

            dgTestStepsCopyMenuItem.IsEnabled = true;
            dgTestStepsEditMenuItem.IsEnabled = true;
            dgTestStepsCutMenuItem.IsEnabled = true;
            dgTestStepsShareMenuItem.IsEnabled = true;
            dgTestStepsDeleteMenuItem.IsEnabled = true;

            if (dgTestSteps.SelectedItems.Count == 0)
            {
                btnEdit.IsEnabled = false;
                btnMoveUp.IsEnabled = false;
                btnMoveDown.IsEnabled = false;
                btnDeleteStep.IsEnabled = false;
                btnChange.IsEnabled = false;
                btnShare.IsEnabled = false;

                dgTestStepsEditMenuItem.IsEnabled = false;
                dgTestStepsCopyMenuItem.IsEnabled = false;
                dgTestStepsCutMenuItem.IsEnabled = false;
                dgTestStepsShareMenuItem.IsEnabled = false;
                dgTestStepsDeleteMenuItem.IsEnabled = false;
            }
            if (this.editViewContext.IsSharedStep)
            {
                dgTestStepsShareMenuItem.IsEnabled = false;
                btnShare.IsEnabled = false;
            }
            List<TestStep> selectedTestSteps = this.GetTestStepsFromGrid();
            if (this.TestCaseEditViewModel.IsSharedStepSelected(selectedTestSteps))
            {
                dgTestStepsShareMenuItem.IsEnabled = false;
                btnShare.IsEnabled = false;
            }
            
            btnAdd.IsEnabled = true;
            if (dgSharedSteps.SelectedItems.Count == 0)
            {
                btnAdd.IsEnabled = false;
            }
        }

        /// <summary>
        /// Handles the SelectedCellsChanged event of the dgSharedSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectedCellsChangedEventArgs"/> instance containing the event data.</param>
        private void dgSharedSteps_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            btnAdd.IsEnabled = true;
            if (dgSharedSteps.SelectedItems.Count == 0)
            {
                btnAdd.IsEnabled = false;
            }
        }

        /// <summary>
        /// Handles the LoadingRow event of the dgTestSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.DataGridRowEventArgs"/> instance containing the event data.</param>
        private void dgTestSteps_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            // Adding 1 to make the row count start at 1 instead of 0
            // as pointed out by daub815
            e.Row.Header = (e.Row.GetIndex() + 1).ToString(); 
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the dgTestSteps control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void dgTestSteps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.EditCurrentTestStepInternal();
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
            if (this.TestCaseEditViewModel.TestCase.ITestSuiteBase != null)
            {
                Log.InfoFormat("Preview test case with id= \"{0}\" and suiteId= \"{1}\"", this.TestCaseEditViewModel.TestCase.ITestCase.Id, this.TestCaseEditViewModel.TestCase.ITestSuiteBase.Id);
                this.NavigateToTestCasesDetailedView(this.TestCaseEditViewModel.TestCase.ITestCase.Id, this.TestCaseEditViewModel.TestCase.ITestSuiteBase.Id);
            }
            else
            {
                Log.InfoFormat("Preview test case with id= \"{0}\" and suiteId= \"{1}\"", this.TestCaseEditViewModel.TestCase.ITestCase.Id, -1);
                this.NavigateToTestCasesDetailedView(this.TestCaseEditViewModel.TestCase.ITestCase.Id, -1);
            }
        }
    }
}