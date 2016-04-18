// <copyright file="BaseFileTypeManager.cs" company="Automate The Planet Ltd.">
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
    /// Common methods for all dialog class managers
    /// </summary>
    public static class BaseFileTypeManager
    {
        /// <summary>
        /// Gets the type of the file filters by file.
        /// </summary>
        /// <param name="dlg">The DLG.</param>
        /// <param name="fileType">Type of the file.</param>
        public static string GetFileFiltersByFileType(FileType fileType)
        {
            string currentFileFilter = String.Empty;
            switch (fileType)
            {
                case FileType.DLL:
                    currentFileFilter = "Assembly Files (*.dll)|*.dll";
                    break;
                case FileType.JSON:
                    currentFileFilter = "JSON Files (*.json)|*.json";
                    break;
            }

            return currentFileFilter;
        }

        /// <summary>
        /// Gets the type of the extension by file.
        /// </summary>
        /// <param name="fileType">Type of the file.</param>
        public static string GetExtensionByFileType(FileType fileType)
        {
            string currentExtension = String.Empty;
            switch (fileType)
            {
                case FileType.DLL:
                    currentExtension = ".dll";
                    break;
                case FileType.JSON:
                    currentExtension = ".json";
                    break;
            }
            return currentExtension;
        }
    }
}