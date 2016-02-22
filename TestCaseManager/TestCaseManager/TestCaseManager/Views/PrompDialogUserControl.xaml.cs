// <copyright file="PrompDialogUserControl.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AAngelov.Utilities.UI.Managers;
using FirstFloor.ModernUI.Windows.Controls;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the shared step name prompt dialog
    /// </summary>
    public partial class PrompDialogUserControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrompDialogUserControl"/> class.
        /// </summary>
        public PrompDialogUserControl()
        {
            this.InitializeComponent();            
        }

        /// <summary>
        /// Gets or sets the prompt dialog view model.
        /// </summary>
        /// <value>
        /// The prompt dialog view model.
        /// </value>
        public PromptDialogViewModel PromptDialogViewModel { get; set; }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UIRegistryManager.Instance.WriteIsWindowClosedFromX(true);
            this.PromptDialogViewModel = new PromptDialogViewModel();
            this.DataContext = this.PromptDialogViewModel;         
        }

        /// <summary>
        /// Handles the Click event of the ButtonOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(this.PromptDialogViewModel.Content))
            {
                ModernDialog.ShowMessage("Content cannot be empty!", "Warrning!", MessageBoxButton.OK);
            }
            this.PromptDialogViewModel.IsCanceled = false;
            UIRegistryManager.Instance.WriteIsWindowClosedFromX(false);
            Window window = Window.GetWindow(this);
            window.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.PromptDialogViewModel.IsCanceled = true;
            this.PromptDialogViewModel.Content = String.Empty;
            UIRegistryManager.Instance.WriteIsWindowClosedFromX(false);
            Window window = Window.GetWindow(this);
            window.Close();
        }

        /// <summary>
        /// Handles the KeyUp event of the tbSharedStepTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbTitle_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tbTitle.Text))
            {
                this.btnOk.IsEnabled = true;
            }
            else
            {
                this.btnOk.IsEnabled = false;
            }
        }
    }
}