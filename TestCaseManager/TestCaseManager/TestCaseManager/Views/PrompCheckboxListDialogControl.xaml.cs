// <copyright file="PrompCheckboxListDialogControl.xaml.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AAngelov.Utilities.UI.Managers;
using TestCaseManagerCore.ViewModels;

namespace TestCaseManagerApp.Views
{
    /// <summary>
    /// Contains logic related to the shared step name prompt dialog
    /// </summary>
    public partial class PrompCheckboxListDialogControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrompCheckboxListDialogControl"/> class.
        /// </summary>
        public PrompCheckboxListDialogControl()
        {
            this.InitializeComponent();            
        }

        /// <summary>
        /// Gets or sets the prompt dialog view model.
        /// </summary>
        /// <value>
        /// The prompt dialog view model.
        /// </value>
        public PrompCheckboxListDialogViewModel PrompCheckboxListDialogViewModel { get; set; }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UIRegistryManager.Instance.WriteIsWindowClosedFromX(true);
            UIRegistryManager.Instance.WriteIsCheckboxDialogSubmitted(false);
            this.PrompCheckboxListDialogViewModel = new PrompCheckboxListDialogViewModel();
            this.DataContext = this.PrompCheckboxListDialogViewModel;         
        }

        /// <summary>
        /// Handles the Click event of the ButtonOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.PrompCheckboxListDialogViewModel.IsCanceled = false;
            List<String> checkedProperties = this.PrompCheckboxListDialogViewModel.PropertiesToBeExported.ToList();
            string checkedPropertiesStr = string.Empty;
            if (checkedProperties.Count != 0)
            {
                checkedPropertiesStr = checkedProperties.Aggregate((i, j) => i + " " + j);
            }
            UIRegistryManager.Instance.WriteCheckedPropertiesToBeExported(checkedPropertiesStr);
            UIRegistryManager.Instance.WriteIsWindowClosedFromX(false);
            UIRegistryManager.Instance.WriteIsCheckboxDialogSubmitted(true);
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
            this.PrompCheckboxListDialogViewModel.IsCanceled = true;
            UIRegistryManager.Instance.WriteIsWindowClosedFromX(false);
            Window window = Window.GetWindow(this);
            window.Close();
        }
    }
}