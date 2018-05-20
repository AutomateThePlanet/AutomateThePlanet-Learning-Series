// <copyright file="FolderBrowseDialogManager.cs" company="Automate The Planet Ltd.">
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
            string folderPath = string.Empty;
            if (result == DialogResult.OK)
            {
                folderPath = dialog.SelectedPath;
            }

            return folderPath;
        }
    }
}