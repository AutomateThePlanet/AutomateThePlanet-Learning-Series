// <copyright file="FileDialogManager.cs" company="Automate The Planet Ltd.">
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
            string resultFileName = string.Empty;
            if (result == true)
            {
                resultFileName = dialog.FileName;				
            }

            return resultFileName;
        }
    }
}