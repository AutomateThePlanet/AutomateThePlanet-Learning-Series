// <copyright file="FileDialogManager.cs" company="AANGELOV">
// http://aangelov.com All rights reserved.
// </copyright>
// <author>Anton Angelov</author>

namespace AAngelov.Utilities.UI.Managers
{
    using System;
    using AAngelov.Utilities.UI.Enums;

    /// <summary>
    /// Helps to get the path to specific file
    /// </summary>
    public class FileDialogManager
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static FileDialogManager instance;

        /// <summary>
        /// Gets the intance.
        /// </summary>
        /// <value>
        /// The intance.
        /// </value>
        public static FileDialogManager Intance
        { 
            get
            {
                if (instance == null)
                {
                    instance = new FileDialogManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <returns></returns>
        public string GetFileName(FileType fileType)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = BaseFileTypeManager.GetFileFiltersByFileType(fileType);
            dialog.DefaultExt = BaseFileTypeManager.GetExtensionByFileType(fileType);

            bool? result = dialog.ShowDialog();
            string resultFileName = String.Empty;
            if (result == true)
            {
                resultFileName = dialog.FileName;				
            }

            return resultFileName;
        }
    }
}