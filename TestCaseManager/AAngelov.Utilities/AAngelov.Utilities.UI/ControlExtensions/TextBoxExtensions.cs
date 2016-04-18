// <copyright file="TextBoxExtensions.cs" company="Automate The Planet Ltd.">
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