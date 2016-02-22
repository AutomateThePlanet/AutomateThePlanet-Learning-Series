// <copyright file="FolderBrowseDialogManager.cs" company="AANGELOV">
// http://aangelov.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace AAngelov.Utilities.UI.Managers
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Helps to pick up folder
    /// </summary>
    public class FolderBrowseDialogManager
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static FolderBrowseDialogManager instance;

        /// <summary>
        /// Gets the intance.
        /// </summary>
        /// <value>
        /// The intance.
        /// </value>
        public static FolderBrowseDialogManager Intance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FolderBrowseDialogManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Gets the folder path.
        /// </summary>
        /// <returns></returns>
        public string GetFolderPath()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;

            // Default to the My Documents folder. 
            dialog.RootFolder = Environment.SpecialFolder.Personal;
            DialogResult result = dialog.ShowDialog();
            string folderPath = String.Empty;
            if (result == DialogResult.OK)
            {
                folderPath = dialog.SelectedPath;
            }

            return folderPath;
        }
    }
}