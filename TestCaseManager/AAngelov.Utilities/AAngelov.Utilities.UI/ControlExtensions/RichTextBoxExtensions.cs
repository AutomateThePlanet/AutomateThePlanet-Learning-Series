// <copyright file="RichTextBoxExtensions.cs" company="Automate The Planet Ltd.">
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