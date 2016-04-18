// <copyright file="PrompDialogUserControl.xaml.cs" company="Automate The Planet Ltd.">
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AAngelov.Utilities.UI.Managers;
using AAngelov.Utilities.UI.ViewModels;
using FirstFloor.ModernUI.Windows.Controls;

namespace AAngelov.Utilities.UI.Views
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