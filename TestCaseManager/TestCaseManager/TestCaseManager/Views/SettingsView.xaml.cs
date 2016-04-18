// <copyright file="SettingsView.xaml.cs" company="Automate The Planet Ltd.">
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
namespace TestCaseManagerApp
{
    using System.Windows;
    using System.Windows.Controls;
    using AAngelov.Utilities.UI.ControlExtensions;
    using FirstFloor.ModernUI.Windows;
    using FirstFloor.ModernUI.Windows.Navigation;
    using TestCaseManagerCore;
    using TestCaseManagerCore.BusinessLogic.Managers;

    /// <summary>
    /// Contains logic related to the settings page
    /// </summary>
    public partial class SettingsView : UserControl, IContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsView"/> class.
        /// </summary>
        public SettingsView()
        {
            this.InitializeComponent();
            this.DataContext = ExecutionContext.SettingsViewModel;
            string projectDllPath = RegistryManager.Instance.GetProjectDllPath();
            this.tbAssociatedProjectDllPath.Text = projectDllPath;
        }

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
            ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbThemes, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
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
        /// Handles the Click event of the btnBrowse control. Opens file dialog with dll filter.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".dll";
            dlg.Filter = "Assembly Files (*.dll)|*.dll";

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                this.tbAssociatedProjectDllPath.Text = filename;
                RegistryManager.Instance.WriteCurrentProjectDllPath(filename);
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the cbThemes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        private void cbThemes_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ComboBoxDropdownExtensions.cboMouseMove(sender, e);
        }

        /// <summary>
        /// Handles the Checked event of the CheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ComboBoxDropdownExtensions.SetOpenDropDownAutomatically(this.cbThemes, ExecutionContext.SettingsViewModel.HoverBehaviorDropDown);
        }
    }
}