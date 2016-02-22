// <copyright file="PrompCheckboxListDialogWindow.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace TestCaseManagerApp.Views
{
    using System;
    using System.Linq;
    using AAngelov.Utilities.UI.Managers;
    using FirstFloor.ModernUI.Windows.Controls;

    /// <summary>
    /// Initializes promo dialog window
    /// </summary>
    public partial class PrompCheckboxListDialogWindow : ModernWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrompCheckboxListDialogWindow"/> class.
        /// </summary>
        public PrompCheckboxListDialogWindow()
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