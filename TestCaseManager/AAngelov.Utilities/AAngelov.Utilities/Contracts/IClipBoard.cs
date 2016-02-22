// <copyright file="IClipBoard.cs" company="AANGELOV">
// http://aangelov.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace AAngelov.Utilities.Contracts
{
    /// <summary>
    /// Contract for the add and get commands from clipboard
    /// </summary>
    /// <typeparam name="T">object to be copied to clip board</typeparam>
    public interface IClipBoard<T> where T : class
    {
        /// <summary>
        /// Copies the suite object to clipboard.
        /// </summary>
        /// <param name="copy">if set to <c>true</c> [copy].</param>
        void CopyToClipboard(bool copy);
    }
}