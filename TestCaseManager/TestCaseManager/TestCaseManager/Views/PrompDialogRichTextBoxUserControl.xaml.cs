// <copyright file="PrompDialogRichTextBoxUserControl.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AAngelov.Utilities.UI.ControlExtensions;
using AAngelov.Utilities.UI.Managers;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the shared step name prompt dialog
    /// </summary>
    public partial class PrompDialogRichTextBoxUserControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrompDialogRichTextBoxUserControl"/> class.
        /// </summary>
        public PrompDialogRichTextBoxUserControl()
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
            UIRegistryManager.Instance.WriteIsWindowClosedFromX(false);
            this.PromptDialogViewModel.Content = String.Empty;
            Window window = Window.GetWindow(this);
            window.Close();
        }

        /// <summary>
        /// Handles the KeyUp event of the rtbComment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void rtbComment_KeyUp(object sender, KeyEventArgs e)
        {
            this.PromptDialogViewModel.Content = this.rtbComment.GetText();
            if (!string.IsNullOrEmpty(this.PromptDialogViewModel.Content) && !string.IsNullOrWhiteSpace(this.PromptDialogViewModel.Content) && this.PromptDialogViewModel.Content != Environment.NewLine.ToString())
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