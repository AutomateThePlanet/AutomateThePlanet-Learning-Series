// <copyright file="PrompDialogRichTextBoxUserControl.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using TestCaseManagerCore;
using TestCaseManagerCore.BusinessLogic.Managers;
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

		/// <summary>
		/// Handles the Loaded event of the UserControl control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RegistryManager.WriteIsWindowClosedFromX(true);
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
            PromptDialogViewModel.IsCanceled = false;
            RegistryManager.WriteIsWindowClosedFromX(false);
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
            PromptDialogViewModel.IsCanceled = true;
            RegistryManager.WriteIsWindowClosedFromX(false);
            PromptDialogViewModel.Content = string.Empty;
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
            this.PromptDialogViewModel.Content = rtbComment.GetText();
            if (!string.IsNullOrEmpty(this.PromptDialogViewModel.Content) && !string.IsNullOrWhiteSpace(this.PromptDialogViewModel.Content) && this.PromptDialogViewModel.Content != Environment.NewLine.ToString())
            {
                btnOk.IsEnabled = true;
            }
            else
            {
                btnOk.IsEnabled = false;
            }
        }   
    }
}