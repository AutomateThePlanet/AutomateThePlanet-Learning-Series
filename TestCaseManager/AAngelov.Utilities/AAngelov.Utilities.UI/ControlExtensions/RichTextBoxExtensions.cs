// <copyright file="RichTextBoxExtensions.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace AAngelov.Utilities.UI.ControlExtensions
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Documents;

    /// <summary>
    /// Contains helper methods which add custom watermark functionality to WPF richtextbox
    /// </summary>
    public static class RichTextBoxExtensions
    {
        /// <summary>
        /// Restores the default search box watermark text.
        /// </summary>
        /// <param name="richTextBox">The rich text box.</param>
        /// <param name="defaultText">The default text.</param>
        /// <param name="isRealTextSet">if set to <c>true</c> real text is set.</param>
        public static void RestoreDefaultText(this RichTextBox richTextBox, string defaultText, ref bool isRealTextSet)
        {
            if (string.IsNullOrEmpty(richTextBox.GetText()))
            {
                richTextBox.AppendText(defaultText);
                isRealTextSet = false;
            }
        }

        /// <summary>
        /// Clears the default content of the search box.
        /// </summary>
        /// <param name="richTextBox">The rich text box.</param>
        /// <param name="isRealTextSet">if set to <c>true</c> [the real text is set].</param>
        public static void ClearDefaultContent(this RichTextBox richTextBox, ref bool isRealTextSet)
        {
            if (!isRealTextSet)
            {
                richTextBox.Document.Blocks.Clear();
                isRealTextSet = true;
            }
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="richTextBox">The rich text box.</param>
        /// <returns>the text from the rich text box</returns>
        public static string GetText(this RichTextBox richTextBox)
        {
            string currentText = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
            return currentText;
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="richTextBox">The rich text box.</param>
        /// <param name="textToAdd">The text automatic add.</param>
        public static void SetText(this RichTextBox richTextBox, string textToAdd)
        {
            richTextBox.Document.Blocks.Clear();
            richTextBox.AppendText(textToAdd);
        }
    }
}