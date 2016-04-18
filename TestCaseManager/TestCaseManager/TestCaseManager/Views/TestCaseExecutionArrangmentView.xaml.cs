// <copyright file="TestCaseExecutionArrangmentView.xaml.cs" company="Automate The Planet Ltd.">
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
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using TestCaseManagerCore;
using TestCaseManagerCore.BusinessLogic.Entities;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the test case initial(search mode) page
    /// </summary>
    public partial class TestCaseExecutionArrangmentView : System.Windows.Controls.UserControl, IContent
    {
        /// <summary>
        /// The save command
        /// </summary>
        public static RoutedCommand SaveCommand = new RoutedCommand();

        /// <summary>
        /// The move up test case command
        /// </summary>
        public static RoutedCommand MoveUpTestCasesCommand = new RoutedCommand();

        /// <summary>
        /// The move down test case command
        /// </summary>
        public static RoutedCommand MoveDownTestCasesCommand = new RoutedCommand();

        /// <summary>
        /// Indicates if the view model is already initialized
        /// </summary>
        private static bool isInitialized;

        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The suite unique identifier
        /// </summary>
        private int suiteId;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseExecutionArrangmentView"/> class.
        /// </summary>
        public TestCaseExecutionArrangmentView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the test case execution arrangment view model.
        /// </summary>
        /// <value>
        /// The test case execution arrangment view model.
        /// </value>
        public TestCaseExecutionArrangmentViewModel TestCaseExecutionArrangmentViewModel { get; set; }

        /// <summary>
        /// Called when navigation to a content fragment begins.
        /// </summary>
        /// <param name="e">An object that contains the navigation data.</param>
        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            FragmentManager fm = new FragmentManager(e.Fragment);
            string suiteIdStr = fm.Get("suiteId");
            if (!string.IsNullOrEmpty(suiteIdStr))
            {
                this.suiteId = int.Parse(suiteIdStr);
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
            this.InitializeFastKeys();
            Task t = Task.Factory.StartNew(() =>
            {
                this.TestCaseExecutionArrangmentViewModel = new TestCaseManagerCore.ViewModels.TestCaseExecutionArrangmentViewModel(this.suiteId);
            });
            t.ContinueWith(antecedent =>
            {
                this.DataContext = this.TestCaseExecutionArrangmentViewModel;          
                this.HideProgressBar();
                isInitialized = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Initializes the fast keys.
        /// </summary>
        private void InitializeFastKeys()
        {
            MoveUpTestCasesCommand.InputGestures.Add(new KeyGesture(Key.Up, ModifierKeys.Alt));
            MoveDownTestCasesCommand.InputGestures.Add(new KeyGesture(Key.Down, ModifierKeys.Alt));
            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl + S"));
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
        /// Displays the non selection warning.
        /// </summary>
        private void DisplayNonSelectionWarning()
        {
            ModernDialog.ShowMessage("No selected test case.", "Warning", MessageBoxButton.OK);
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
        /// Handles the Click event of the btnMoveDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
            this.MoveDownInternal();
        }

        /// <summary>
        /// Moves up test steps internal.
        /// </summary>
        private void MoveUpInternal()
        {
            // validate the move if it's out of the boudaries return
            if (this.dgTestCases.SelectedItems.Count == 0)
            {
                this.DisplayNonSelectionWarning();
                return;
            }
            int startIndex = this.TestCaseExecutionArrangmentViewModel.ObservableTestCases.IndexOf(this.dgTestCases.SelectedItems[0] as TestCase);
            if (startIndex == 0)
            {
                return;
            }
            int count = this.dgTestCases.SelectedItems.Count;
            if (this.dgTestCases.SelectedItems.Count == 0)
            {
                return;
            }
            //using (new UndoTransaction("Move up selected steps", true))
            //{
            this.TestCaseExecutionArrangmentViewModel.CreateNewTestCaseCollectionAfterMoveUp(startIndex, count);
            this.SelectNextItemsAfterMoveUp(startIndex, count);
            //}
            this.dgTestCases.UpdateLayout();
            this.dgTestCases.ScrollIntoView(this.dgTestCases.SelectedItem);
        }

        /// <summary>
        /// Moves down test steps internal.
        /// </summary>
        private void MoveDownInternal()
        {
            // validate the move if it's out of the boudaries return
            if (this.dgTestCases.SelectedItems.Count == 0)
            {
                this.DisplayNonSelectionWarning();
                return;
            }
            int startIndex = this.TestCaseExecutionArrangmentViewModel.ObservableTestCases.IndexOf(this.dgTestCases.SelectedItems[0] as TestCase);
            int count = this.dgTestCases.SelectedItems.Count;
            if (startIndex == this.TestCaseExecutionArrangmentViewModel.ObservableTestCases.Count - 1)
            {
                return;
            }
            if ((startIndex + count) >= this.TestCaseExecutionArrangmentViewModel.ObservableTestCases.Count)
            {
                return;
            }
            //using (new UndoTransaction("Move down selected steps", true))
            //{
            this.TestCaseExecutionArrangmentViewModel.CreateNewTestCaseCollectionAfterMoveDown(startIndex, count);
            this.SelectNextItemsAfterMoveDown(startIndex, count);
            //}
            this.dgTestCases.UpdateLayout();
            this.dgTestCases.ScrollIntoView(this.dgTestCases.SelectedItems[this.dgTestCases.SelectedItems.Count - 1]);
        }

        /// <summary>
        /// Selects the next test steps after move up.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="selectedTestStepsCount">The selected test steps count.</param>
        private void SelectNextItemsAfterMoveUp(int startIndex, int selectedTestStepsCount)
        {
            this.dgTestCases.SelectedItems.Clear();
            for (int i = startIndex - 1; i < startIndex + selectedTestStepsCount - 1; i++)
            {
                this.dgTestCases.SelectedItems.Add(this.dgTestCases.Items[i]);
            }
            //UndoRedoManager.Instance().Push((si, c) => this.SelectNextItemsAfterMoveDown(si, c), startIndex - 1, selectedTestStepsCount, "Select next items after move up");
        }

        /// <summary>
        /// Selects the next test steps after move down.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="selectedTestStepsCount">The selected test steps count.</param>
        private void SelectNextItemsAfterMoveDown(int startIndex, int selectedTestStepsCount)
        {
            this.dgTestCases.SelectedItems.Clear();
            for (int i = startIndex + 1; i < startIndex + selectedTestStepsCount + 1; i++)
            {
                this.dgTestCases.SelectedItems.Add(this.dgTestCases.Items[i]);
            }
            //UndoRedoManager.Instance().Push((si, c) => this.SelectNextItemsAfterMoveUp(si, c), startIndex + 1, selectedTestStepsCount, "Select next items after move up");
        }

        /// <summary>
        /// Handles the Command event of the moveDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void moveDown_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.MoveDownInternal();
            this.dgTestCases.Focus();
        }

        /// <summary>
        /// Handles the Command event of the moveUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void moveUp_Command(object sender, ExecutedRoutedEventArgs e)
        {
            this.MoveUpInternal();
            this.dgTestCases.Focus();
        }

        /// <summary>
        /// Handles the Click event of the btnSaveArrangement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSaveArrangement_Click(object sender, RoutedEventArgs e)
        {
            this.TestCaseExecutionArrangmentViewModel.SaveArrangement();
            Navigator.Instance.NavigateToTestCasesInitialView(this);
        }

        /// <summary>
        /// Handles the LoadingRow event of the dgTestCases control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridRowEventArgs"/> instance containing the event data.</param>
        private void dgTestCases_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            // Adding 1 to make the row count start at 1 instead of 0
            // as pointed out by daub815
            e.Row.Header = (e.Row.GetIndex() + 1).ToString(); 
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Navigator.Instance.NavigateToTestCasesInitialView(this);
        }
    }
}