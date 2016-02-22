// <copyright file="TextBoxExtensions.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace AAngelov.Utilities.UI.ControlExtensions
{
    using System;
    using System.Linq;
    using System.Windows.Controls;

    /// <summary>
    /// Contains helper methods which add custom watermark functionality to WPF textbox
    /// </summary>
    public static class TextBoxExtensions
    {
        /// <summary>
        /// Restores the default text of the text box.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="defaultText">The default text.</param>
        /// <param name="isRealTextSet">if set to <c>true</c> [is real text set].</param>
        public static void RestoreDefaultText(this TextBox textBox, string defaultText, ref bool isRealTextSet)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = defaultText;
                isRealTextSet = false;
            }
        }

        /// <summary>
        /// Clears the default content.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="isRealTextSet">if set to <c>true</c> [is real text set].</param>
        public static void ClearDefaultContent(this TextBox textBox, ref bool isRealTextSet)
        {
            if (!isRealTextSet)
            {
                textBox.Text = string.Empty;
                isRealTextSet = true;
            }
        }
    }
}