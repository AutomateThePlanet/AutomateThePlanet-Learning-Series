// <copyright file="ReplaceTextManager.cs" company="Automate The Planet Ltd.">
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
namespace AAngelov.Utilities.Managers
{
    using System.Collections.Generic;
    using AAngelov.Utilities.Entities;

    /// <summary>
    /// Contains helper methods for replacing multiple text pairs in specific text
    /// </summary>
    public static class ReplaceTextManager
    {
        /// <summary>
        /// Replaces all specified text pairs.
        /// </summary>
        /// <param name="textToReplace">The text to be replaced.</param>
        /// <param name="textReplacePairs">The text replace pairs.</param>
        /// <returns>replaced text</returns>
        public static string ReplaceAll(this string textToReplace, ICollection<TextReplacePair> textReplacePairs)
        {
            string newText = textToReplace ?? string.Empty;
            foreach (TextReplacePair currentPair in textReplacePairs)
            {
                if (currentPair.OldText != null && currentPair.NewText != null)
                {
                    newText = newText.Replace(currentPair.OldText, currentPair.NewText);
                }
            }

            return newText;
        }
    }
}