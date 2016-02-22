// <copyright file="ReplaceTextManager.cs" company="AANGELOV">
// http://aangelov.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

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