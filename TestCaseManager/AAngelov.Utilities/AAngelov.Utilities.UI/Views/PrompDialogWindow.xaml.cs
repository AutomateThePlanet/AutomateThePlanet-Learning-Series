// <copyright file="PrompDialogWindow.xaml.cs" company="Automate The Planet Ltd.">
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
namespace AAngelov.Utilities.UI.Views
{
    using AAngelov.Utilities.UI.Managers;
    using FirstFloor.ModernUI.Windows.Controls;

    /// <summary>
    /// Initializes promo dialog window
    /// </summary>
    public partial class PrompDialogWindow : ModernWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrompDialogWindow"/> class.
        /// </summary>
        public PrompDialogWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the Closing event of the ModernWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ModernWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (UIRegistryManager.Instance.ReadIsWindowClosedFromX())
            {
                UIRegistryManager.Instance.WriteIsCanceledTitlePromtDialog(true);
            }
        }
    }
}